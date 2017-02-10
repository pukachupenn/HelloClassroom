namespace DeviceCredentialManager
{
    using System.Threading.Tasks;
    using Microsoft.Azure.Devices;

    public class DeviceRegistryManager
    {
        private RegistryManager RegistryManager { get; }

        public DeviceRegistryManager(string connectionString)
        {
            RegistryManager = RegistryManager.CreateFromConnectionString(connectionString);
        }

        public async Task<string> RegisterDeviceAsync(string deviceId)
        {
            var device = await RegistryManager.AddDeviceAsync(new Device(deviceId));
            return device.Authentication.SymmetricKey.PrimaryKey;
        }

        public async Task<string> GetPrimaryKeyOfExistingDevice(string deviceId)
        {
            var device = await RegistryManager.GetDeviceAsync(deviceId);
            return device.Authentication.SymmetricKey.PrimaryKey;
        }
    }
}
