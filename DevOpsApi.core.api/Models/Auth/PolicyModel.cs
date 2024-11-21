namespace DevOpsApi.core.api.Models.Auth
{
    public class PolicyModel : BaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
