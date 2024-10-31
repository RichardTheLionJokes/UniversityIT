using UniversityIT.Application.ValueObjects;

namespace UniversityIT.Application.Abstractions.Common
{
    public interface IMessageService
    {
        public Task<bool> SendMessage(MessageReceiver receiver, string subject, string message);
    }
}