namespace HelloClassroom.DeviceCredentialManager
{
    using Microsoft.Azure.Devices.Common.Exceptions;
    using System;

    class Program
    {
        private static DeviceRegistryManager DeviceRegistryManager { get; set; }

        public static void Main(string[] args)
        {
            SetupDeviceRegistryManager();

            RunLoop();
        }

        private static void RunLoop()
        {
            while (true)
            {
                Console.Write("Please input your command (register/query/quit):");
                var command = Console.ReadLine();
                if (string.Equals(command, "register", StringComparison.OrdinalIgnoreCase))
                {
                    var deviceId = ReadDeviceId();
                    try
                    {
                        var result = DeviceRegistryManager.RegisterDeviceAsync(deviceId).Result;
                        Console.WriteLine($"Registration succeed, device key is {result}");
                    }
                    catch (DeviceAlreadyExistsException)
                    {
                        Console.WriteLine($"Device with ID {deviceId} has existed, use \"query\" to get its device key.");
                    }
                }
                else if (string.Equals(command, "query", StringComparison.OrdinalIgnoreCase))
                {
                    var deviceId = ReadDeviceId();
                    try
                    {
                        var result = DeviceRegistryManager.GetPrimaryKeyOfExistingDevice(deviceId).Result;
                        Console.WriteLine($"Device Key is {result}");
                    }
                    catch (AggregateException)
                    {
                        Console.WriteLine($"Failed to query device {deviceId}");
                    }
                }
                else if (string.Equals(command, "quit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Unknown command: {command}");
                }
            }
        }

        private static void SetupDeviceRegistryManager()
        {
            Console.WriteLine("Please input the connection string:");

            while (true)
            {
                try
                {
                    var connectionString = Console.ReadLine();
                    DeviceRegistryManager = new DeviceRegistryManager(connectionString);
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid connection string.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Malformed connection string.");
                }
            }
        }

        private static string ReadDeviceId()
        {
            Console.Write("Device ID:");
            return Console.ReadLine();
        }
    }
}
