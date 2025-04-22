using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amalgama.Model
{
   public class UserModel
    { 
        public int Id { get; set; }
        public string NameSurename { get; set; }
        public string PhoneNumber { get; set; }
        public string Result {get; set; }
        public string Age { get; set; }
        public UserModel() { }
        public UserModel(string namesurename, string phonenumber, string age, string result)
        {
            NameSurename = namesurename;
            PhoneNumber = phonenumber;
            Result = result;
            Age = age;
        }
    }
}
