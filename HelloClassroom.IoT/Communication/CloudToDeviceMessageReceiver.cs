namespace HelloClassroom.IoT.Communication
{
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.Devices.Client;

    class CloudToDeviceMessageReceiver : IMessageReceiver
    {
        private DeviceClient DeviceClient { get; }

        public CloudToDeviceMessageReceiver(string iotHubUri, string deviceId, string deviceKey, TransportType transportType = TransportType.Mqtt)
        {
            var credential = new DeviceAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey);
            DeviceClient = DeviceClient.Create(iotHubUri, credential, transportType);
        }

        public async Task<Message> ReceiveMessageAsync()
        {
            var receivedMessage = await DeviceClient.ReceiveAsync();
            if (receivedMessage != null)
            {
                await DeviceClient.CompleteAsync(receivedMessage);
            }

            return receivedMessage;
        }

        public async Task<string> ReceiveStringMessageAsync()
        {
            var receivedMessage = await ReceiveMessageAsync();
            return receivedMessage == null? null : Encoding.ASCII.GetString(receivedMessage.GetBytes());
        }
    }
}
