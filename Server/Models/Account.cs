using FinanceApp.Server.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Server.Models {
    public class Account {
        public long Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public AccountStatus Status { get; set; }

        public Category Category { get; set; }
    }
}