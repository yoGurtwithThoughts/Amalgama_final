using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Amalgama.Model
{
    class DataHelper
    {
        private void RegisterUser(string namesurename, string phonenumber, string age, string result)
        {
            using (var db = new ContextBD())
            {
                var user = new UserModel(namesurename,phonenumber, age,  result);
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}
//private void RegisterButton_Click(object sender, RoutedEventArgs e)
//{
//    string login = LoginTextBox.Text;
//    string email = EmailTextBox.Text;
//    string password = PasswordBox.Password;

//    RegisterUser(login, email, password);
//    MessageBox.Show("Пользователь зарегистрирован!");
//}
