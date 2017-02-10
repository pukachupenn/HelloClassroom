namespace HelloClassroom.Communication
{
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.Devices;

    public class CloudToDeviceMessageSender : IMessageSender
    {
        private ServiceClient ServiceClient { get; }

        public CloudToDeviceMessageSender(string connectionString)
        {
            ServiceClient = ServiceClient.CreateFromConnectionString(connectionString);
        }

        public async Task SendMessageAsync(string deviceId, Message message)
        {
            await ServiceClient.SendAsync(deviceId, message);
        }

        public Task SendMessageAsync(string deviceId, byte[] bytes)
        {
            return SendMessageAsync(deviceId, bytes, DeliveryAcknowledgement.None);
        }

        public Task SendMessageAsync(string deviceId, string stringMessage)
        {
            return SendMessageAsync(deviceId, stringMessage, DeliveryAcknowledgement.None);
        }

        public async Task SendMessageAsync(string deviceId, byte[] bytes, DeliveryAcknowledgement ack)
        {
            var message = new Message(bytes) { Ack = ack };
            await SendMessageAsync(deviceId, message);
        }

        public async Task SendMessageAsync(string deviceId, string stringMessage, DeliveryAcknowledgement ack)
        {
            var bytes = Encoding.ASCII.GetBytes(stringMessage);
            await SendMessageAsync(deviceId, bytes, ack);
        }

        public async Task<FeedbackBatch> ReceiveFeedbackAsync()
        {
            var feedbackReceiver = ServiceClient.GetFeedbackReceiver();
            return await feedbackReceiver.ReceiveAsync();
        }

        public async Task CompleteFeedbackAsync(FeedbackBatch feedbackBatch)
        {
            var feedbackReceiver = ServiceClient.GetFeedbackReceiver();
            await feedbackReceiver.CompleteAsync(feedbackBatch);
        }
    }
}