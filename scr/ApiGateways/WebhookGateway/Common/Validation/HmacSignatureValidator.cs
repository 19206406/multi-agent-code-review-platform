using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using WebhookGateway.Common.Options;

namespace WebhookGateway.Common.Validation
{
    public class HmacSignatureValidator : IHmacSignatureValidator
    {
        private readonly string _secret;  
        public HmacSignatureValidator(IOptions<WebhookOptions> options)
        {
            _secret = options.Value.WebhookSecret; 
        }

        public bool IsValid(string rawPayload, string signature)
        {
            var key = Encoding.UTF8.GetBytes(_secret);
            var data = Encoding.UTF8.GetBytes(rawPayload);

            var hash = HMACSHA256.HashData(key, data);
            var expected = "sha256=" + Convert.ToHexString(hash).ToLowerInvariant();

            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(expected), Encoding.UTF8.GetBytes(signature)); 
        }
    }
}
