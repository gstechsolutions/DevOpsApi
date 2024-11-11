namespace DevOpsApi.core.api.Models
{
    public class BaseModel
    {
        public long CreatedById { get; set; }

        public long UpdatedById { get; set; }

        public long RequestId { get; set; }

        public string? RequestMessage { get; set; }

        public bool? IsValid { get; set; }

        public string? ModifiedById { get; set; }

        public string? ModifiedByName { get; set; }

        public void SetError(string errorMessage)
        {
            this.RequestId = (int)RequestStatus.Error;
            this.RequestMessage = errorMessage;
        }

        public void SetError(bool isValid, string invalidReason)
        {
            this.IsValid = isValid;
            this.RequestId = (int)RequestStatus.Error;
            this.RequestMessage = invalidReason;
        }

        public void SetSuccess(string message)
        {
            this.RequestId = (int)RequestStatus.Success;
            this.RequestMessage = message;
        }
    }
}
