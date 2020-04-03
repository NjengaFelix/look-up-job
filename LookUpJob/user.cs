using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUpJob
{
    class User
    {
        private string firstName;
        private string lastName;
        private string email;
        private string username;
        private string password;
        private bool is_staff;
        private string dateJoined;

        User(string firstName, string lastName, string email, string username, string password, bool is_staff, string dateJoined)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.username = username;
            this.password = password;
            this.is_staff = is_staff;
            this.dateJoined = dateJoined;
        }
    }
}
