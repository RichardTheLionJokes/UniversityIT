namespace UniversityIT.Infrastructure.Common.Email
{
    public class EMailOptions
    {
        public string YandexHost { get; set; } = string.Empty;
        public int YandexPort { get; set; }
        public bool YandexSsl { get; set; }
        public string YandexLogin { get; set; } = string.Empty;
        public string YandexPass { get; set; } = string.Empty;
    }
}