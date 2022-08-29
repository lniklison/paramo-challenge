using System;
using Sat.Recruitment.Api.Models;
using System.Net;
using System.Security.Policy;
using System.Xml.Linq;
using System.Collections.Generic;
using Sat.Recruitment.Api.Helpers;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Sat.Recruitment.Api.Services.Abstract;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>();
        public UserService(){}
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            _users = await loadUsersFromFile();

            if (isUserDataDuplicated(name, email, address, phone))
            {
                return new Result(false, Constants.USER_DUPLICATED);
            }

            var newUser = new User(name, email, address, phone, userType, decimal.Parse(money));
            newUser.Money = calculateUserMoney(decimal.Parse(money), userType, newUser);

            newUser.Email = NormalizeEmail(newUser.Email);

            return new Result(true, Constants.USER_CREATED);
        }

        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });

        }

        private async Task<List<User>> loadUsersFromFile()
        {
            var users = new List<User>();
            var reader = ReadUsersFromFile();
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var user = new User
                (
                    line.Split(',')[0].ToString(),
                    line.Split(',')[1].ToString(),
                    line.Split(',')[2].ToString(),
                    line.Split(',')[3].ToString(),
                    line.Split(',')[4].ToString(),
                    decimal.Parse(line.Split(',')[5].ToString())
                );
                users.Add(user);
            }
            reader.Close();
            return users;
        }

        private bool isUserDataDuplicated(string name, string email, string address, string phone)
        {
            var duplicatedUser = _users.Where(x => x.Email == email ||
                                    x.Phone == phone ||
                                    (x.Name == name && x.Address == address));

            return duplicatedUser.Any();
        }

        private decimal calculateUserMoney(decimal money, string userType, User newUser)
        {
            var rate = Convert.ToDecimal(0);

            if (money > 100)
                rate = Convert.ToDecimal(Enum.Parse(typeof(Rates), userType));

            if (newUser.UserType == Constants.NORMAL_USER && money < 100)
                rate = money > 10 ? Convert.ToDecimal(0.8) : Convert.ToDecimal(0);

            return newUser.Money + (money * rate);
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}

