using triaxis.BluetoothLE;

namespace DeviceBrowser;

public partial class MainPage : ContentPage
{
    public MainPage(BluetoothScanner scanner)
    {
        InitializeComponent();

        BindingContext = scanner;
        this.scanner = scanner;
    }

    BluetoothScanner scanner;

    BluetoothDevice? GetDevice()
    {

        var deviceFound = scanner.Devices.FirstOrDefault(x => x.DeviceName.Contains("P13") || x.DeviceName.Contains("K10"));

        if (deviceFound == null)
        {
            return null;
        }

        return deviceFound;


    }

    BluetoothDevice? _selectedDevice;
    IPeripheralConnection? _deviceConnection;
    IList<IService> _deviceServices;

    async void cmdConnect(object sender, EventArgs args)
    {
        if (_selectedDevice == null)
        {
            _selectedDevice = GetDevice();
        }

        _deviceConnection = await _selectedDevice.Peripheral.ConnectAsync(5000);
        _deviceConnection.Closed += deviceConnection_Closed;
        _deviceServices = await _deviceConnection.GetServicesAsync();
        var temp = await _deviceServices[0].GetCharacteristicsAsync();


    }

    async private void deviceConnection_Closed(object? sender, Exception e)
    {
        // Debug.WriteLine("Device Closed"); 

    }

    async void cmdDisconnect(object sender, EventArgs args)
    {
        await _deviceConnection.DisconnectAsync();
        _deviceConnection = null;
        _selectedDevice = null;
        this.scanner.Devices.Clear();
    }

}

