using System;
using System.Collections.Generic;
using System.Text;
using BusinessManager.Interfaces;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;


namespace BusinessManager.Services
{
    public class UserBL : IUserBL
    {
        readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public bool UserRegister(RegisterModel user)
        {
            try
            {
                return this.userRL.UserRegister(user);
            }
            catch (Exception a)
            {
                throw a;
            }
        }

      
        

        public UserModel Authenticate(string email, string password)
        {
            try
            {
                return userRL.Authenticate(email, password);
            }
            catch(Exception a)
            {
                throw a;
            }
        }

        public UserModel ForgetPassWordModel(string email)
        {
            try
            {
                return userRL.ForgetPassWordModel(email);
            }
            catch (Exception a)
            {
                throw a;
            }
        }
    }
}
