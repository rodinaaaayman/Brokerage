using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Application.DTOs.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
}
