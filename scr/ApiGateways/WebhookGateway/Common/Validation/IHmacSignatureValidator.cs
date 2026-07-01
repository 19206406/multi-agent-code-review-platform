namespace WebhookGateway.Common.Validation
{
    public interface IHmacSignatureValidator
    {
        public bool IsValid (string rawPayload, string signature); 
    }
}
