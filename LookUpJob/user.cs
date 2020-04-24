using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;

namespace LookUpJob
{
    class User
    {
        private string firstName;
        private string lastName;
        private string email;
        private bool user_type;
        private string username;
        private string password;

        public User() { }
 

        //Insert user into the DB
        public void createNewUser(string firstName, string lastName, string email, bool is_Employee_or_JobSeeker, string username, string password)
        {
            using(UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                User_details ud = new User_details();
                ud.first_name = firstName;
                ud.last_name = lastName;
                ud.email = email;
                ud.is_Employee_or_JobSeeker = is_Employee_or_JobSeeker;
                ud.user_name = username;
                ud.password = password;

                udt.Users.InsertOnSubmit(ud);
                udt.SubmitChanges();

            }
        }

        //Load user profile
        public IList<User_details> loadUserProfile(string username)
        {
            IList<User_details> list = null;
            using(UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                IQueryable<User_details> query = from users in udt.Users where users.user_name == username select users;
                list = query.ToList();
            }
            return list;
        }


        //Create IsolatedStorageSetting storage function
        public void saveUsername(string usernameKey, string username)
        {
            IsolatedStorageSettings.ApplicationSettings[username] = usernameKey;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        //Return username from IsolatedStorageSetting
        public static string loadUsername(string usernameKey)
        {
            if(IsolatedStorageSettings.ApplicationSettings.Contains(usernameKey))
            {
                return (string)IsolatedStorageSettings.ApplicationSettings[usernameKey];
            }
            else
            {
                //Login in
                return null;
            }
        }

        public static int loadUserId(string userIdKey)
        {
            if(IsolatedStorageSettings.ApplicationSettings.Contains(userIdKey))
            {
                return (int)IsolatedStorageSettings.ApplicationSettings[userIdKey];
            }
            else
            {
                return 0;
            }
        }

        
        
    }
}
