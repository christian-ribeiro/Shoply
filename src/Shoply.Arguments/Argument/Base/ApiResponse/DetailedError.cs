namespace Shoply.Arguments.Argument.Base
{
    public class DetailedError
    {
        public int? Index { get; private set; }
        public long? Id { get; private set; }
        public string? Identifier { get; private set; }
        public string? Message { get; private set; }
        public List<string>? ListMessage { get; private set; }

        public DetailedError(int? index, string? identifier, List<string>? listMessage)
        {
            Index = index;
            Identifier = identifier;
            ListMessage = listMessage;
        }

        public DetailedError(string? identifier, string message)
        {
            Identifier = identifier;
            Message = message;
        }
    }
}