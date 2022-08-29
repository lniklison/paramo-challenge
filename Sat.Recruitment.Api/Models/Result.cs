using System;
namespace Sat.Recruitment.Api.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }

        public Result(bool success, string errors)
        {
            IsSuccess = success;
            Errors = errors;
        }
    }
}

