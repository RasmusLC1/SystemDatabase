namespace DeviceNameSpace
{
    public class DeviceHandler
    {
        private static DeviceHandler? _instance;
        private List<Device> devices;

        private DeviceHandler()
        {
            devices = new List<Device>();
        }

        public static DeviceHandler GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DeviceHandler();
            }
            return _instance;
        }

        public void Add_Device(Device device)
        {
            devices.Add(device);
        }
    }
}
