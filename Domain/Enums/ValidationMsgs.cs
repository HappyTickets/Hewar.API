using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ValidationMsgs
    {
        RequiredField = 0,
        UserName_NoSpaces = 1,
        Email_Required_Validation = 2,
        Email_Format_Validation = 3,
        Password_Validation = 4,
        Password_Format_Validation = 5,
        InvalidValue = 6,
        RepeatedPassword = 7,
        Passwords_NotMatching = 8,
        InvalidName = 9,
        PhoneNumber_Unique_Validation = 10,



    }
}
