using FinanceApp.Server.Enums;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FinanceApp.Server.Models
{
    public class Category
    {
        public long Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(6)]
        public string Color { get; set; }
        public int Order { get; set; }
        public GenericStatus Status { get; set; }

        public ICollection<Account> Accounts { get; set; }

        // TODO: Add relationship to User.
    }
}