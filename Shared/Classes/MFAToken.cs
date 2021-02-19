using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text.Json;

namespace FinanceApp.Shared.Classes
{
    public class MFAToken
    {
        public string Identity { get; set; }

        public string SessionId { get; set; }

        public DateTime DateIssued { get; set; }

        public string Token { get; set; }

        public bool IsValid { get; set; }

        public static async Task<MFAToken> GenerateAsync(string identity, string hashKey)
        {
            string sessionId = Guid.NewGuid().ToString();
            DateTime dateIssued = DateTime.Now;
            string token = await GenerateTokenAsync(sessionId, dateIssued, hashKey);

            return new MFAToken
            {
                Identity = identity,
                SessionId = sessionId,
                DateIssued = dateIssued,
                Token = token
            };
        }

        public async Task ValidateAsync(string hashKey)
        {
            string testToken = await GenerateTokenAsync(SessionId, DateIssued, hashKey);
            
            // Default to 30s.
            DateTime expiry = DateIssued.AddMinutes(30);
            this.IsValid = (Token == testToken && DateTime.Now < expiry);
        }

        private static async Task<string> GenerateTokenAsync(string sessionId, DateTime dateIssued, string hashKey)
        {
            // refer: https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes($"{sessionId}.{hashKey}.{dateIssued:ddMMyyyy}");
            
            using (SHA256 sha256 = SHA256.Create())
            {
                Stream stream = new MemoryStream(plainTextBytes);
                byte[] hashValue = await sha256.ComputeHashAsync(stream);

                return Convert.ToBase64String(hashValue);
            }
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}