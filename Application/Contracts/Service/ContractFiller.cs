using Application.Contracts.DTOs;
using Newtonsoft.Json;

namespace Application.Contracts.Service
{
    public static class ContractFiller
    {
        public static string PopulateTemplateManually(string template, ContractFields1 contractFields)
        {

            var placeholders = new Dictionary<string, string>
                {
                    {"{{PartyOne.Name.Ar}}", contractFields.PartyOne.Name.Ar} ,
                    {"{{PartyOne.Name.En}}", contractFields.PartyOne.Name.En} ,
                    {"{{PartyOne.MainOfficeCity.Ar}}", contractFields.PartyOne.MainOfficeCity.Ar},
                    { "{{PartyOne.MainOfficeCity.En}}", contractFields.PartyOne.MainOfficeCity.En},
                    { "{{PartyOne.CommercialRegistration}}", contractFields.PartyOne.CommercialRegistration},
                    { "{{PartyOne.PublicSecurityLicense}}", contractFields.PartyOne.PublicSecurityLicense},
                    { "{{PartyOne.Telephone}}", contractFields.PartyOne.Telephone},
                    { "{{PartyOne.Mobile}}", contractFields.PartyOne.Mobile},
                    { "{{PartyOne.NationalAddress.City.Ar}}", contractFields.PartyOne.NationalAddress.City.Ar},
                    { "{{PartyOne.NationalAddress.City.En}}", contractFields.PartyOne.NationalAddress.City.En},
                    { "{{PartyOne.NationalAddress.PostalCode}}", contractFields.PartyOne.NationalAddress.PostalCode},
                    { "{{PartyOne.NationalAddress.UnitNumber}}", contractFields.PartyOne.NationalAddress.UnitNumber},
                    { "{{PartyOne.NationalAddress.BuildingNumber}}", contractFields.PartyOne.NationalAddress.BuildingNumber},
                    { "{{PartyOne.RegistrationInSabl}}", contractFields.PartyOne.RegistrationInSabl},
                    { "{{PartyOne.Email}}", contractFields.PartyOne.Email},
                    { "{{PartyOne.RepresentativeName.Ar}}", contractFields.PartyOne.RepresentativeName.Ar},
                    { "{{PartyOne.RepresentativeName.En}}", contractFields.PartyOne.RepresentativeName.En},
                    { "{{PartyOne.RepresentativeTitle.Ar}}", contractFields.PartyOne.RepresentativeTitle.Ar},
                    { "{{PartyOne.RepresentativeTitle.En}}", contractFields.PartyOne.RepresentativeTitle.En},
                    { "{{PartyOne.GuardsCount}}", contractFields.PartyOne.GuardsCount.ToString()},

                    { "{{OfferNumber}}", contractFields.OfferNumber.ToString()},
                    { "{{OfferDate}}", contractFields.OfferDate.ToString("dd/MM/yyyy")},

                    // PartyTwo ////////////////////////////////////////////////

                    { "{{PartyTwo.Name.Ar}}", contractFields.PartyTwo.Name.Ar},
                    { "{{PartyTwo.Name.En}}", contractFields.PartyTwo.Name.En},
                    { "{{PartyTwo.MainOfficeCity.Ar}}", contractFields.PartyTwo.MainOfficeCity.Ar},
                    { "{{PartyTwo.MainOfficeCity.En}}", contractFields.PartyTwo.MainOfficeCity.En},
                    { "{{PartyTwo.CommercialRegistrationCity.Ar}}", contractFields.PartyTwo.CommercialRegistrationCity.Ar},
                    { "{{PartyTwo.CommercialRegistrationCity.En}}", contractFields.PartyTwo.CommercialRegistrationCity.En},
                    { "{{PartyTwo.CommercialRegistration}}", contractFields.PartyOne.CommercialRegistration},
                    { "{{PartyTwo.PublicSecurityLicense}}", contractFields.PartyTwo.PublicSecurityLicense},
                    { "{{PartyTwo.Telephone}}", contractFields.PartyTwo.Telephone},
                    { "{{PartyTwo.Mobile}}", contractFields.PartyTwo.Mobile},
                    { "{{PartyTwo.Email}}", contractFields.PartyTwo.Email},
                    { "{{PartyTwo.NationalAddress.City.Ar}}", contractFields.PartyTwo.NationalAddress.City.Ar},
                    { "{{PartyTwo.NationalAddress.City.En}}", contractFields.PartyTwo.NationalAddress.City.En},
                    { "{{PartyTwo.NationalAddress.PostalCode}}", contractFields.PartyTwo.NationalAddress.PostalCode},
                    { "{{PartyTwo.NationalAddress.UnitNumber}}", contractFields.PartyTwo.NationalAddress.UnitNumber},
                    { "{{PartyTwo.NationalAddress.BuildingNumber}}", contractFields.PartyTwo.NationalAddress.BuildingNumber},
                    { "{{PartyTwo.LocationToBeSecured.Ar}}", contractFields.PartyTwo.LocationToBeSecured.Ar},
                    { "{{PartyTwo.LocationToBeSecured.En}}", contractFields.PartyTwo.LocationToBeSecured.En},

                    {"{{ContractSignDate}}", contractFields.ContractSignDate.ToString("dd/MM/yyyy")},
                    {"{{ContractStartDate}}", contractFields.ContractStartDate.ToString("dd/MM/yyyy")},

                    { "{{PartyTwo.RepresentativeName.Ar}}", contractFields.PartyTwo.RepresentativeName.Ar},
                    { "{{PartyTwo.RepresentativeTitle.Ar}}", contractFields.PartyTwo.RepresentativeTitle.Ar } ,

                   { "{{PartyTwo.RepresentativeName.En}}", contractFields.PartyTwo.RepresentativeName.En},
                   { "{{PartyTwo.RepresentativeTitle.En}}", contractFields.PartyTwo.RepresentativeTitle.En }

            };

            foreach (var placeholder in placeholders)
            {
                template = template.Replace(placeholder.Key, placeholder.Value);
            }

            return template;
        }
        public static string PopulateTemplate(string template, ContractFields1 contractFields)
        {
            var placeholders = GetPlaceholders(contractFields, "");

            foreach (var placeholder in placeholders)
            {
                template = template.Replace(placeholder.Key, placeholder.Value);
            }

            return template;
        }
        public static Dictionary<string, string> GetPlaceholders(object obj, string prefix)
        {
            var placeholders = new Dictionary<string, string>();

            if (obj is null) return placeholders;

            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.GetIndexParameters().Length > 0)
                {
                    continue;
                }

                var value = property.GetValue(obj);
                var propertyName = $"{prefix}{property.Name}";

                if (value != null && property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    var nestedPlaceholders = GetPlaceholders(value, propertyName + ".");
                    foreach (var nestedPlaceholder in nestedPlaceholders)
                    {
                        placeholders.Add(nestedPlaceholder.Key, nestedPlaceholder.Value);
                    }
                }
                else
                {
                    placeholders.Add($"{{{{{propertyName}}}}}", value?.ToString() ?? string.Empty);
                }
            }

            return placeholders;
        }


        public static ContractFields1? DeserializeContractFields(string contractJson)
        {
            return JsonConvert.DeserializeObject<ContractFields1>(contractJson);
        }
        public static ContractDto? DeserializeContract(string contractJson)
        {
            return JsonConvert.DeserializeObject<ContractDto>(contractJson);
        }

        public static string SerializeContract(ContractFields1 contract)
        {
            return JsonConvert.SerializeObject(contract);
        }

    }
}
