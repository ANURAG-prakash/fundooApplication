using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly UserContext _userContext;
        public UserRL(UserContext context)
        {
            _userContext = context;
        }



        public bool UserRegister(RegisterModel entity)
        {

            try
            {

                UserModel _user = new UserModel()
                {

                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    Password = Encryptedpassword(entity.Password),


                };
                _userContext.Users.Add(_user);
                int result = _userContext.SaveChanges();
                if (result <= 0)
                {
                    return false;
                }
                else
                {
                    return true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public UserModel Authenticate(string email, string password)
        {
            try
            {
                var user = _userContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
                //return null if user is not found 
                if (user == null)
                    return null;
                //if user found 



                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Encryptedpassword(string password)
        {
            return password;
        }




       

        public UserModel ForgetPassWordModel(string email)
        {
            try
            {
                var user = _userContext.Users.FirstOrDefault(x => x.Email == email );
                //return null if user is not found 
                if (user == null)
                    return null;
                //if user found 



                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
