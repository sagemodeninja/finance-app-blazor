using FinanceApp.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Shared.Models
{
    public class AccountVendor
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(180, ErrorMessage = "Name can't exceed the maximum length of 180 characters.")]

        public string Name { get; set; }

#nullable enable
        // TODO: Do RND for Azure Storage then store blob reference here...
        public string? Logo { get; set; }
#nullable disable

        public GenericStatus Status { get; set; }

        #region Navigation Properties
        #endregion Navigation Properties
    }
}