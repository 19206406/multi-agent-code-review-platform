namespace WebhookGateway.Common.Options
{
    public class WebhookOptions
    {
        public string SectionName { get; set; } = "Github";
        public string WebhookSecret { get; set; } = string.Empty; 
    }
}
