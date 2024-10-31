using CSharpFunctionalExtensions;

namespace UniversityIT.Application.ValueObjects
{
    public class MessageReceiver : ValueObject
    {
        public string StringAddress { get; }

        public long LongAddress { get; }

        private MessageReceiver(string stringAddress)
        {
            StringAddress = stringAddress;
            LongAddress = 0;
        }

        private MessageReceiver(long longAddress)
        {
            StringAddress = "";
            LongAddress = longAddress;
        }

        public static Result<MessageReceiver> Create(string stringAddress)
        {
            if (String.IsNullOrEmpty(stringAddress))
            {
                return Result.Failure<MessageReceiver>($"'{nameof(stringAddress)}' can't be empty at the same time");
            }

            return new MessageReceiver(stringAddress);
        }

        public static Result<MessageReceiver> Create(long longAddress)
        {
            return new MessageReceiver(longAddress);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return StringAddress;
            yield return LongAddress;
        }
    }
}
