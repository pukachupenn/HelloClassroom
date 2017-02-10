using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace HelloWorld
{
    internal interface IMessageReceiver
    {
        Task<Message> ReceiveMessageAsync();

        Task<string> ReceiveStringMessageAsync();
    }
}