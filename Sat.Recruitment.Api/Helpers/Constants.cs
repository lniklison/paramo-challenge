using System;
using Microsoft.AspNetCore.Http;

namespace Sat.Recruitment.Api.Helpers
{
    public static class Constants
    {
        public const string NORMAL_USER = "Normal";
        public const string SUPERL_USER = "SuperUser";
        public const string PREMIUM_USER = "Premium";

        public const string USER_CREATED = "User Created";
        public const string USER_DUPLICATED = "The user is duplicated";

        public const string NAME_REQUIRED = "The name is required";
        public const string EMAIL_REQUIRED = " The email is required";
        public const string ADDRESS_REQUIRED = " The address is required";
        public const string PHONE_REQUIRED = " The phone is required";
    }
}

