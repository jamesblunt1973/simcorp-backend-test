namespace Backend_Test.Configuration
{
    public sealed class ApiInfoOptions
    {
        public const string SectionName = "ApiInfo";

        public string ApiVersion { get; set; } = string.Empty;
        public string UiVersion { get; set; } = string.Empty;
    }
}
