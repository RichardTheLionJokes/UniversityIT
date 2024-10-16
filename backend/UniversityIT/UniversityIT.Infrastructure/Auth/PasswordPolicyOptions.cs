namespace UniversityIT.Infrastructure.Auth
{
    public class PasswordPolicyOptions
    {
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public int DefaultLength { get; set; }
    }
}