using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace FinanceApp.Shared.Models
{
    public class GraphUser
    {
        [JsonPropertyName("id")]
        public Guid AccountId { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("givenName")]
        public string GivenName { get; set; }

        [JsonPropertyName("mail")]
        public string Mail { get; set; }

        public string NameInitials => GivenName.FirstOrDefault().ToString();

        public bool IsMailAvailable => !string.IsNullOrEmpty(Mail);
    }
}