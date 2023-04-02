namespace MessageQuery
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
