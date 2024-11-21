namespace DevOpsApi.core.api.ConfigurationModel
{
    public class ServiceCoreSettings
    {
        public string? MQHostName { get; set; }

        public int MQPort { get; set; }

        public string? MQUsername { get; set; }

        public string? MQPassword { get; set; }

        public string? Version { get; set; }

        public string SignatureFolder { get; set; }

        public string TempusUri { get; set; }

        public int TokenValidityMins { get; set; }
    }
}
