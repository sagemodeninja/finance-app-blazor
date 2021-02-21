using FinanceApp.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Shared.Models
{
    public class Category
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(6)]
        public string Color { get; set; }

        public int Order { get; set; }

        public long UserId { get; set; }

        public GenericStatus Status { get; set; }

        #region Navigation Properties
        #endregion Navigation Properties
    }
}