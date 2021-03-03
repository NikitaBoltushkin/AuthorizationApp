using Data;
using Models;

namespace Services
{
    public class AuthUtil
    {
        public static bool Authorization(string login, string password)
        {
            var userDataAccess = new UserDataAccess();
            var userList = userDataAccess.Select();
            foreach (var element in userList)
            {
                if (element.Login == login)
                {
                   if(element.Password == EncryiptionService.GetHashString(password))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Registration(string login, string password, Profile profile)
        {
            var userDataAccess = new UserDataAccess();
            var userList = userDataAccess.Select();
            var profileDataAccess = new ProfileDataAccess();
            foreach (var element in userList)
            {
                if (element.Login == login)
                {
                    return false;
                }
            }
            //Проверяю на валидность Email
            var isValid = EmailValidationService.IsValidEmail(profile.Email);
            if (isValid)
            {
                var user = new User()
                {
                    Login = login,
                    Password = EncryiptionService.GetHashString(password)
                };
                userDataAccess.Insert(user);
                profileDataAccess.Insert(profile);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
