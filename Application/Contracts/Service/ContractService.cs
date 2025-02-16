using Application.Contracts.DTOs;
using Application.Contracts.DTOs.Dynamic;
using Application.PriceOffers.Dtos;
using AutoMapper;
using Domain.Entities.ContractJson;

namespace Application.Contracts.Service
{
    public class ContractService(IUnitOfWorkService ufw, ICurrentUserService currentUser, IMapper mapper) : IContractService
    {
        public async Task<Result<long>> FillContractFieldsAsync(FillContractFieldsDto dto)
        {
            var offer = await ufw.GetRepository<PriceOffer>().GetByIdAsync(dto.OfferId);
            if (offer is null)
                return new NotFoundError();

            var contract = new ContractTemplate
            {
                ContractJson = ContractFiller.SerializeContract(dto.Contract),
                OfferId = dto.OfferId
            };

            await ufw.GetRepository<ContractTemplate>().CreateAsync(contract);
            await ufw.SaveChangesAsync();

            return Result<long>.Success(contract.Id, SuccessCodes.ContractCreated);
        }

        public async Task<Result<Empty>> UpdateContractFieldsAsync(UpdateContractFieldsDto dto)
        {
            var contract = await ufw.GetRepository<ContractTemplate>().GetByIdAsync(dto.Id);

            if (contract is null)
                return new NotFoundError();

            contract.ContractJson = ContractFiller.SerializeContract(dto.Contract);

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.Updated);
        }

        public async Task<Result<GetContractFieldsDto>> GetContractFieldsByOfferIdAsync(long offerId)
        {
            var contract = await ufw.GetRepository<ContractTemplate>().FirstOrDefaultAsync(c => c.OfferId == offerId);

            if (contract is null)
                return new NotFoundError();

            var contractJson = ContractFiller.DeserializeContractFields(contract.ContractJson);

            if (contractJson is null)
                return new ConflictError(ErrorCodes.Conflict);

            var resultDto = mapper.Map<GetContractFieldsDto>(contractJson);
            resultDto.ContractId = contract.Id;

            return Result<GetContractFieldsDto>.Success(resultDto, SuccessCodes.OperationSuccessful);
        }

        public async Task<Result<GetContractFieldsDto>> GetContractFieldsByIdAsync(long contractId)
        {
            var contract = await ufw.GetRepository<ContractTemplate>().GetByIdAsync(contractId);

            if (contract is null)
                return new NotFoundError();

            var contractJson = ContractFiller.DeserializeContractFields(contract.ContractJson);

            if (contractJson is null)
                return new ConflictError(ErrorCodes.Conflict);

            var resultDto = mapper.Map<GetContractFieldsDto>(contractJson);
            resultDto.ContractId = contract.Id;

            return Result<GetContractFieldsDto>.Success(resultDto, SuccessCodes.OperationSuccessful);
        }


        public async Task<Result<Empty>> SignContractAsync(long contractId, string signature)
        {
            var contract = await ufw.GetRepository<ContractTemplate>().GetByIdAsync(contractId);
            if (contract is null)
                return new NotFoundError(ErrorCodes.ContractNotExists);

            var entityType = currentUser.EntityType;

            if (entityType == EntityTypes.Company)
                contract.PartyOneSignature = signature;

            else if (entityType == EntityTypes.Facility)
                contract.PartyTwoSignature = signature;

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractSigned);

        }




        #region Helper Methods

        private async Task<StaticContractTemplate?> GetStaticContractTemplateAsync(long templateId = 1)
        {
            return await ufw.GetRepository<StaticContractTemplate>().GetByIdAsync(templateId);
        }

        private void SetOfferFields(ContractFields1 contractFields, GetPriceOfferDto offer)
        {
            contractFields.OfferNumber = offer.Id;
            contractFields.OfferDate = offer.CreatedOn.Date; // Test
        }

        private ContractDto? PopulateAndDeserializeTemplate(string templateJson, ContractFields1 contractFields)
        {
            var template = ContractFiller.PopulateTemplate(templateJson, contractFields);
            return ContractFiller.DeserializeContract(template);
        }

        private void MapAdditionalFields(
            ContractDto filledContract,
            ContractFields1 contractFields,
            GetPriceOfferDto offer,
            ContractTemplate contract)
        {
            filledContract.ServicesOffer = offer.Services.ToList();
            filledContract.OtherServicesOffer = offer.OtherServices?.ToList() ?? new();
            filledContract.ScheduleEntries = contractFields.ScheduleEntries;
            filledContract.CustomClauses = contractFields.CustomClauses;
            filledContract.Signatures.PartyOneSignature = contract.PartyOneSignature;
            filledContract.Signatures.PartyTwoSignature = contract.PartyTwoSignature;
            filledContract.ContractId = contract.Id;
        }

        private async Task<ContractTemplate?> getContractTemplateAsync(long contractId)
        {
            return await ufw.GetRepository<ContractTemplate>().GetByIdAsync(contractId);
        }

        private async Task<ContractTemplate?> getContractTemplateByOfferIdAsync(long offerId)
        {
            return await ufw.GetRepository<ContractTemplate>()
                            .FirstOrDefaultAsync(c => c.OfferId == offerId);
        }

        private async Task<Result<ContractDto?>> ProcessContractTemplateAsync(ContractTemplate contract)
        {
            var contractFields = ContractFiller.DeserializeContractFields(contract.ContractJson);
            if (contractFields is null)
                return new ConflictError(ErrorCodes.DeserializeOperationFailed);

            var offer = await ufw.GetRepository<PriceOffer>()
                            .FirstOrDefaultAsync<GetPriceOfferDto>
                            (o => o.Id == contract.OfferId);

            if (offer is null)
                return new NotFoundError(ErrorCodes.PriceOfferNotExists);

            SetOfferFields(contractFields, offer);

            var staticContract = await GetStaticContractTemplateAsync();
            if (staticContract is null)
                return new NotFoundError(ErrorCodes.ContractTemplatesNotExist);

            var filledContract = PopulateAndDeserializeTemplate(staticContract.JsonData, contractFields);
            if (filledContract is null)
                return new ConflictError(ErrorCodes.DeserializeOperationFailed);

            MapAdditionalFields(filledContract, contractFields, offer, contract);

            return filledContract;
        }
        #endregion

        public async Task<Result<ContractDto?>> GetContractTemplateByIdAsync(long contractId)
        {
            var contract = await getContractTemplateAsync(contractId);
            if (contract is null)
                return new NotFoundError(ErrorCodes.ContractNotExists);

            return await ProcessContractTemplateAsync(contract);
        }

        public async Task<Result<ContractDto?>> GetContractTemplateByOfferIdAsync(long offerId)
        {
            var contract = await getContractTemplateByOfferIdAsync(offerId);
            if (contract is null)
                return new NotFoundError(ErrorCodes.ContractNotExists);

            return await ProcessContractTemplateAsync(contract);
        }

    }

}

