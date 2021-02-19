namespace FinanceApp.Server.Classes
{
    public class FinanceAppOptions
    {
        public const string Section = "FinanceAppOptions";
        public string TOTPHashKey { get; set; }
        public string MFATokenHashKey { get; set; }
    }
}