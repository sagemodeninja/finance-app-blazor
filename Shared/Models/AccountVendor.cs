using FinanceApp.Shared.Enums;

namespace FinanceApp.Shared.Models
{
    public class AccountVendor
    {
        public long Id { get; set; }

        public string Name { get; set; }

        // TODO: Do RND for Azure Storage then store blob reference here...
        public string Logo { get; set; }

        public GenericStatus Status { get; set; }

        #region Navigation Properties
        #endregion Navigation Properties
    }
}