using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using CommonLayer.Models;

namespace BusinessManager.Interfaces
{
    public interface IUserBL
    {
        bool UserRegister(RegisterModel user);
       
        public UserModel Authenticate(string email, string password);
        public UserModel ForgetPassWordModel(string email);
    }
}
