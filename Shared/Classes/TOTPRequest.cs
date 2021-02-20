using FinanceApp.Shared.Models;

namespace FinanceApp.Shared.Classes
{
    public class TOTPRequest
    {
        public long RequestorId { get; set; }
        public string Code { get; set; }
    }
}