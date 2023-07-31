﻿using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ERole Role { get; set; }        
    }
}
