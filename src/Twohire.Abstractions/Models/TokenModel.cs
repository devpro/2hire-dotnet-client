using System;

namespace Devpro.Twohire.Abstractions.Models
{
    public class TokenModel
    {
        public string Value { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
