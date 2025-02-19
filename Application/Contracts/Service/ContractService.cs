using Application.Clauses.DTOs;
using Application.Contracts.DTOs;
using Application.Contracts.DTOs.Dynamic;
using Application.Contracts.DTOs.Static;
using Application.PriceOffers.Dtos;
using Application.ScheduleEntries.DTOs;
using AutoMapper;
using Domain.Entities.ContractAggregate.Dynamic;
using Domain.Entities.ContractAggregate.Static;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Contracts.Service
{
    public class ContractService(IUnitOfWorkService ufw, ICurrentUserService currentUser, IMapper mapper, IMemoryCache cache) : IContractService
    {
        private const string STATIC_CONTRACT_CACHE_KEY = "static_contract";
        private const string STATIC_CLAUSES_CACHE_KEY = "static_clauses";
        private const string KEYS_CACHE_KEY = "contract_keys";
        private TimeSpan CACHE_DURATION = new TimeSpan(1, 0, 0);

        public async Task<Result<long>> CreateContractForOfferAsync(long offerId, ContractFieldsDto contractFields)
        {
            var offer = await ufw.GetRepository<PriceOffer>().GetByIdAsync(offerId);
            if (offer is null)
                return new NotFoundError();

            var contract = new Contract
            {
                OfferId = offerId,
                ContractKeys = await MapContractFieldsToKeys(contractFields)
            };

            await ufw.GetRepository<Contract>().CreateAsync(contract);
            await ufw.SaveChangesAsync();

            return Result<long>.Success(contract.Id, SuccessCodes.ContractCreated);
        }

        public async Task<Result<long>> CreateContractForCompanyAsync(long companyId, ContractFieldsDto contractFields, long? facilityId = null)
        {
            var contract = new Contract
            {
                CompanyId = companyId,
                FacilityId = facilityId,
                ContractKeys = await MapContractFieldsToKeys(contractFields)
            };

            await ufw.GetRepository<Contract>().CreateAsync(contract);
            await ufw.SaveChangesAsync();

            return Result<long>.Success(contract.Id, SuccessCodes.ContractCreated);
        }

        public async Task<Result<Empty>> UpdateContractByFieldsAsync(long contractId, ContractFieldsDto contractFields)
        {
            var contract = await ufw.GetRepository<Contract>()
                .FirstOrDefaultAsync(c => c.Id == contractId, [nameof(Contract.ContractKeys)]);

            if (contract is null)
                return new NotFoundError();

            contract.ContractKeys.Clear();
            contract.ContractKeys = await MapContractFieldsToKeys(contractFields);

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractUpdated);
        }

        public async Task<Result<Empty>> UpdateContractByKeysAsync(long contractId, List<UpdateContractKeyDto> contractKeys)
        {
            var keyIds = contractKeys.Select(k => k.ContractKeyId).ToList();

            var existingKeys = await ufw.GetRepository<ContractKey>()
                .FilterAsync(ck => ck.ContractId == contractId && keyIds.Contains(ck.Id));

            if (!existingKeys.Any())
                return new NotFoundError();

            var updateDict = contractKeys.ToDictionary(k => k.ContractKeyId, k => k.NewValue);

            foreach (var key in existingKeys)
            {
                if (updateDict.TryGetValue(key.Id, out var newValue))
                {
                    key.Value = newValue;
                }
            }

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractUpdated);
        }
        public async Task<Result<RichContractDto>> GetContractByIdAsync(long contractId)
        {
            var contract = await ufw.GetRepository<Contract>()
                .FirstOrDefaultAsync(c => c.Id == contractId,
                [nameof(Contract.ContractKeys),
                $"{nameof(Contract.ContractKeys)}.{nameof(ContractKey.Key)}",
                 nameof(Contract.ScheduleEntries),
                 nameof(Contract.CustomClauses)
                 ]);

            if (contract is null)
                return new NotFoundError();

            var richContract = await BuildRichContractDto(contract);
            return Result<RichContractDto>.Success(richContract);
        }

        public async Task<Result<RichContractDto>> GetContractByOfferIdAsync(long offerId)
        {
            var contract = await ufw.GetRepository<Contract>()
                .FirstOrDefaultAsync(c => c.OfferId == offerId,
                [nameof(Contract.ContractKeys),
                $"{nameof(Contract.ContractKeys)}.{nameof(ContractKey.Key)}",
                 nameof(Contract.ScheduleEntries),
                 nameof(Contract.CustomClauses)]);

            if (contract is null)
                return new NotFoundError();

            var richContract = await BuildRichContractDto(contract);
            return Result<RichContractDto>.Success(richContract);
        }

        public async Task<Result<Empty>> SignContractAsync(long contractId, string signature)
        {
            var contract = await ufw.GetRepository<Contract>().GetByIdAsync(contractId);
            if (contract is null)
                return new NotFoundError();

            if (currentUser.EntityType == EntityTypes.Company)
                contract.CompanySignature = signature;
            else if (currentUser.EntityType == EntityTypes.Facility)
                contract.FacilitySignature = signature;

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractSigned);
        }

        private async Task<RichContractDto> BuildRichContractDto(Contract contract)
        {
            var staticContract = await GetStaticContractAsync();
            var staticClauses = await GetStaticClausesAsync();

            var dto = new RichContractDto
            {
                ContractId = contract.Id,
                StaticContractTemplate = mapper.Map<StaticContractDto>(staticContract),
                StaticClauses = mapper.Map<List<StaticClauseDto>>(staticClauses),
                CustomClauses = mapper.Map<List<CustomClauseDto>>(contract.CustomClauses),
                ScheduleEntries = mapper.Map<List<ScheduleEntryDto>>(contract.ScheduleEntries),
                ContractKeys = mapper.Map<List<ContractKeyDto>>(contract.ContractKeys),
                FacilitySignature = contract.FacilitySignature,
                CompanySignature = contract.CompanySignature
            };

            await SetOfferInfo(dto, contract.OfferId);
            return dto;
        }
        private async Task<StaticContract> GetStaticContractAsync()
        {
            if (cache.TryGetValue(STATIC_CONTRACT_CACHE_KEY, out StaticContract? cachedContract))
                return cachedContract!;

            var contract = await ufw.GetRepository<StaticContract>().FirstOrDefaultAsync(c => c.Id == 1);

            if (contract != null)
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(CACHE_DURATION)
                    .SetPriority(CacheItemPriority.Normal);

                cache.Set(STATIC_CONTRACT_CACHE_KEY, contract, cacheOptions);
            }

            return contract!;
        }

        private async Task<List<StaticClause>> GetStaticClausesAsync()
        {
            if (cache.TryGetValue(STATIC_CLAUSES_CACHE_KEY, out List<StaticClause>? cachedClauses))
                return cachedClauses!;

            var clauses = await ufw.GetRepository<StaticClause>().GetAllAsync();
            if (clauses.Any())
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(CACHE_DURATION)
                    .SetPriority(CacheItemPriority.Normal);

                cache.Set(STATIC_CLAUSES_CACHE_KEY, clauses, cacheOptions);
            }

            return clauses.ToList();
        }

        private async Task<Dictionary<string, Key>> GetKeysAsync()
        {
            if (cache.TryGetValue(KEYS_CACHE_KEY, out Dictionary<string, Key>? cachedKeys))
                return cachedKeys!;

            var keys = (await ufw.GetRepository<Key>().GetAllAsync()).ToDictionary(k => k.Name);
            if (keys.Any())
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(CACHE_DURATION)
                    .SetPriority(CacheItemPriority.Normal);

                cache.Set(KEYS_CACHE_KEY, keys, cacheOptions);
            }

            return keys;
        }

        private async Task<List<ContractKey>> MapContractFieldsToKeys(ContractFieldsDto fields)
        {
            var keys = await GetKeysAsync();
            return await ContractHelper.MapContractFields(fields, keys);
        }

        private async Task<ContractFieldsDto> MapContractKeysToFields(ICollection<ContractKey> contractKeys)
        {
            var keys = await GetKeysAsync();
            return await ContractHelper.MapContractFieldsToDto(contractKeys, keys);
        }
        private async Task SetOfferInfo(RichContractDto dto, long? offerId)
        {
            if (offerId is null)
                return;

            var offer = await ufw.GetRepository<PriceOffer>().FirstOrDefaultAsync<GetPriceOfferDto>
                (o => o.Id == offerId);
            if (offer != null)
            {
                dto.OfferNumber = offer.Id;
                dto.OfferDate = offer.CreatedOn;
                dto.Services = offer.Services;
                dto.OtherServices = offer.OtherServices;
            }
        }


        public async Task<Result<GetContractKeysDto?>> GetContractKeysByContractIdAsync(long contractId)
        {

            var contractKeys = await ufw.GetRepository<ContractKey>()
                .FilterAsync<ContractKeyDto>(c => c.ContractId == contractId);

            return Result<GetContractKeysDto?>.Success
                (new GetContractKeysDto { ContractId = contractId, ContractKeys = contractKeys.ToList() });
        }

        public async Task<Result<GetContractKeysDto?>> GetContractKeysByOfferIdAsync(long offer)
        {
            var contract = await ufw.GetRepository<Contract>()
                .FirstOrDefaultAsync(c => c.OfferId == offer,
                [nameof(Contract.ContractKeys), $"{nameof(Contract.ContractKeys)}.{nameof(ContractKey.Key)}"]);

            if (contract is null || contract.ContractKeys is null)
                return new NotFoundError();

            var contractKeys = mapper.Map<List<ContractKeyDto>>(contract.ContractKeys);

            return Result<GetContractKeysDto?>.Success
              (new GetContractKeysDto { ContractId = contract.Id, ContractKeys = contractKeys });
        }

        public async Task<Result<GetContractFieldsDto>> GetContractFieldsByContractIdAsync(long contractId)
        {
            var contract = await ufw.GetRepository<Contract>()
                .FirstOrDefaultAsync(c => c.Id == contractId,
                [nameof(Contract.ContractKeys), $"{nameof(Contract.ContractKeys)}.{nameof(ContractKey.Key)}"]);

            if (contract is null || contract.ContractKeys is null)
                return new NotFoundError();

            var contractFields = await MapContractKeysToFields(contract.ContractKeys);

            var resultDto = new GetContractFieldsDto
            {
                ContractId = contractId,
                OfferId = contract.OfferId,
                ContractFields = contractFields
            };

            return Result<GetContractFieldsDto>.Success(resultDto);
        }

        public async Task<Result<GetContractFieldsDto>> GetContractFieldsByOfferIdAsync(long offerId)
        {
            var contract = await ufw.GetRepository<Contract>()
                .FirstOrDefaultAsync(c => c.OfferId == offerId,
                [nameof(Contract.ContractKeys), $"{nameof(Contract.ContractKeys)}.{nameof(ContractKey.Key)}"]);

            if (contract is null || contract.ContractKeys is null)
                return new NotFoundError();

            var contractFields = await MapContractKeysToFields(contract.ContractKeys);

            var resultDto = new GetContractFieldsDto
            {
                ContractId = contract.Id,
                OfferId = offerId,
                ContractFields = contractFields
            };

            return Result<GetContractFieldsDto>.Success(resultDto);
        }



    }
}

