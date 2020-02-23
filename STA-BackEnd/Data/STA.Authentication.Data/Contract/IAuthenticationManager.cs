using STA.EventBus.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Authentication.Data.Contract
{
    public interface IAuthenticationManager
    {
        TokenVM Register(UserRegistrationModel model);
        TokenVM Signin(AuthenticateModel model);
    }
}
