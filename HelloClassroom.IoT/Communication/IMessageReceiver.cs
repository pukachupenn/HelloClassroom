using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace HelloClassroom.IoT.Communication
{
    internal interface IMessageReceiver
    {
        Task<Message> ReceiveMessageAsync();

        Task<string> ReceiveStringMessageAsync();
    }
}