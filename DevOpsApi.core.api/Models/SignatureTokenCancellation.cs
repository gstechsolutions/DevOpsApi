namespace DevOpsApi.core.api.Models
{
    public class SignatureTokenCancellation
    {
        public string SubscriberKey { get; set; }

        public CancellationTokenSource CancellationTokenSource { get; set; }
    }
}
