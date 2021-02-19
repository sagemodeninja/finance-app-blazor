using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Shared.Models
{
    public class User
    {
        public long Id { get; set; }
        public Guid AccountId { get; set; }
        [MaxLength(254)]
        public string Email { get; set; }
        public string TOTPSecret { get; set; }
    }
}