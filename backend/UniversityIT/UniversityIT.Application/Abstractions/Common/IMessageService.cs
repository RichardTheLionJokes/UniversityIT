namespace UniversityIT.Application.Abstractions.Common
{
    public interface IMessageService
    {
        public Task SendMessage(string receiver, string subject, string message);
    }
}