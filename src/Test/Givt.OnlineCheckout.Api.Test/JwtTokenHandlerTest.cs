using Givt.OnlineCheckout.API.Utils;
using Givt.OnlineCheckout.Infrastructure.Loggers;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using Serilog.Sinks.Http.Logger;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Givt.OnlineCheckout.Api.Test
{
    public class JwtTokenHandlerTest
    {
        private JwtOptions _options;
        private ILog _logger;
        private SecurityKey _key;

        [SetUp]
        public void Setup()
        {
            // Create options object
            _options = new JwtOptions
            {
                Issuer = "https://api.givtapp.net",
                IssuerSigningKey = "21ED1753C9BE4BB5A13A710A336EE010C70DBBF3AFF84830B3841842BFEEB59878421F599B154A07865E338FFB5D2D40", 
                Audience = "https://api.givtapp.net",
                Authority = "https://api.givtapp.net",
                ExpireHours = 24
            };
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.IssuerSigningKey));
            _logger = new LogitHttpLogger("GOC Debug - Test", "73b6d8f0-132f-45ff-a8cf-6654ffee1922");
        }

        [Test]
        public void PreserveTransactionReference()
        {
            const string transactionReference = "pi_3KqHOOLgFatYzb8p34aLCNhh";
            var handler = new JwtTokenHandler(_logger, _options);
            var tokenString = handler.GetBearerToken(transactionReference);

            var securityHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.Audience, // ???
                IssuerSigningKey = _key,
                ClockSkew = TimeSpan.FromMinutes(1),
            };
            var claimsPrincipal = securityHandler.ValidateToken(tokenString, validationParameters, out SecurityToken validatedToken);

            var returnedTransactionReference = handler.GetTransactionReference(claimsPrincipal);

            Assert.AreEqual(transactionReference, returnedTransactionReference);
        }
    }
}