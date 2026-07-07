namespace WebhookGateway.Common.Options
{
    public class WebhookOptions
    {
        public const string SectionName = "Github";
        public string WebhookSecret { get; set; } = string.Empty; 
    }
}
