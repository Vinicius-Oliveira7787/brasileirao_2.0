﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Domain.People;

namespace Domain.Users
{
    public class User : Person
    {
        public Profile Profile { get; set; }
        public string Password { get; set; }
        // Transformar em VO
        public string Email { get; set; }

        public User(string name, string password, string email, Profile profile) : base(name)
        {
            Password = password;
            Email = email;
            Profile = profile;
        }

        private bool ValidateEmail()
        {
            return Regex.IsMatch(
                Email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase
            );
        }

        public (IList<string> errors, bool isValid) Validate()
        {
            var errors = new List<string>();
            if (!ValidateName())
            {
                errors.Add("Nome inválido.");
            }

            if (!ValidateEmail())
            {
                errors.Add("Email inválido.");
            }

            return (errors, errors.Count == 0);
        }
    }
}