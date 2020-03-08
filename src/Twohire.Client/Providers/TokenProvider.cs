using System;
using Devpro.Twohire.Client.Domain.Providers;
using Devpro.Twohire.Client.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.Providers
{
    public class TokenProvider : ITokenProvider
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly ILogger<TokenProvider> _logger;
        private string _tokenValue;
        private DateTime _expirationTime;

        public TokenProvider(ILogger<TokenProvider> logger, ITokenRepository tokenRepository)
        {
            _logger = logger;
            _tokenRepository = tokenRepository;
        }

        public string Token
        {
            get
            {
                var now = DateTime.Now;
                if (_tokenValue == null || _expirationTime <= now)
                {
                    var tokenDto = _tokenRepository.CreateAsync().GetAwaiter().GetResult();
                    _tokenValue = tokenDto.Value;
                    _expirationTime = tokenDto.ExpiredDate;
                    _logger.LogDebug("New token generated");
                }
                return _tokenValue;
            }
        }
    }
}
