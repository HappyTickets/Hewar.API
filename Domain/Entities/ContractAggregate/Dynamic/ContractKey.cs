using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ContractAggregate.Dynamic
{
    public class ContractKey
    {
        public int Id { get; set; }
        public long ContractId { get; set; }
        public int KeyId { get; set; }
        public string Value { get; set; }


        [ForeignKey(nameof(ContractKey))]
        public Contract Contract { get; set; }

        [ForeignKey(nameof(KeyId))]
        public Key Key { get; set; }

    }
}

/*
 
 Keys {
    
    id: 1    
    name: ContractSignDate
    DataType: Date
    }   
 
contract keys 
     
first send fields normal like : {

public DateTime ContractSignDate {get; set;}
        }

initial the contract with fields
 offer id:  1
ContractSignDate = 20/12/2025

create new Contract {offerId = 1;
    
    contractKeys = new List {
        new ContractKey { 
            KeyId = keys [nameof(ContractSignDate)]
            value = ContractSignDate
        }
            
        } 
} 
 */






