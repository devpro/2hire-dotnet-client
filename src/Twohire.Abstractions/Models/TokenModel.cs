using System;

namespace Devpro.Twohire.Client.Domain.Models
{
    public class TokenModel
    {
        public string Value { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
