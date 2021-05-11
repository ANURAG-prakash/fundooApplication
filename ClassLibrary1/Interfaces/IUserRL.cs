using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using CommonLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        bool UserRegister(RegisterModel user);
      
        public UserModel Authenticate(string email, string password);
        public UserModel ForgetPassWordModel(string email);
    }
}