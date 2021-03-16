using FinanceApp.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Shared.Models
{
    public class Account
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Number { get; set; }

        public long CategoryId { get; set; }

        public long AccountVendorId { get; set; }

        public long UserId { get; set; }

        public Currency Currency { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Projection { get; set; }
        
        public bool SubAccount { get; set; }

        public long? ParentAccountId { get; set; }

        public AccountStatus Status { get; set; }

        #region Navigation Properties
        #endregion
    }
}