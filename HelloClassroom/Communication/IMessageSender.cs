namespace HelloClassroom.Communication
{
    using System.Threading.Tasks;
    using Microsoft.Azure.Devices;

    public interface IMessageSender
    {
        Task SendMessageAsync(string deviceId, Message message);

        Task SendMessageAsync(string deviceId, byte[] bytes);

        Task SendMessageAsync(string deviceId, string stringMessage);
    }
}