using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Web_Development.Models;
using static BCrypt.Net.BCrypt;

namespace Web_Development.Utils
{
    public class Auth
    {
        private readonly Database _database;
        private readonly HttpResponse _response;

        public Auth(HttpResponse response)
        {
            _response = response;
            _database = new Database();
        }

        public void Login(User user)
        {
            var dictionary = new Dictionary<string, string>
            {
                {"id", user.Id.ToString()},
                {"data", EnhancedHashPassword($"{user.Id}{user.Email}{user.Password}")}
            };
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dictionary)));
            _response.Cookies.Append("Auth", data);
        }

        public User User()
        {
            var data = DecodeSessionData();
            if (data == null) return null;

            var user = new Database().Users.FirstOrDefault(b => b.Id == Convert.ToInt64(data["id"]));
            if (user == null) return null;

            return EnhancedVerify($"{user.Id}{user.Email}{user.Password}", data["data"]) ? user : null;
        }

        private Dictionary<string, string> DecodeSessionData()
        {
            var session = _response.HttpContext.Request.Cookies["Auth"];
            if (session == null) return null;

            return JsonSerializer.Deserialize<Dictionary<string, string>>(
                Encoding.UTF8.GetString(Convert.FromBase64String(session)));
        }

        public void Logout()
        {
            _response.Cookies.Delete("Auth");
        }
    }
}