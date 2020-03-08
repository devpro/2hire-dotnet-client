using System;
using Newtonsoft.Json;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.Dto
{
    public class TokenDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public long Expire { get; set; }
        public int ClientType { get; set; }
        public int UserId { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public UserDto User { get; set; }
        public bool Unlimited { get; set; }
    }
}
