using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Shared.Models
{
    [Index(nameof(AccountId), IsUnique = true)]
    public class User
    {
        public long Id { get; set; }

        public Guid AccountId { get; set; }

        [MaxLength(254)]
        public string Email { get; set; }

        public string TOTPSecret { get; set; }

        public bool HasRegisteredMFA { get; set; }
        
        public bool EnableMFA { get; set; }

        #region Navigation Properties
        #endregion Navigation Properties
    }
}