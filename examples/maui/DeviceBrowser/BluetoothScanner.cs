namespace DeviceBrowser;

using triaxis.BluetoothLE;

public class BluetoothScanner
{
    private readonly IBluetoothLE _bluetooth;
    private readonly BluetoothDeviceCollection _devices = new();
    private IDisposable? _scanner;
    private IAdapter _adapter;

    public BluetoothScanner(IBluetoothLE bluetooth)
    {
        Console.WriteLine("Instantiating BluetoothScanner");
        _bluetooth = bluetooth;
        _bluetooth.WhenAdapterChanges().Subscribe(OnAdapterChange);
    }

    private void OnAdapterChange(IAdapter adapter)
    {
        // stop previous scanner, if any
        _scanner?.Dispose();

        _adapter = adapter;

        if (adapter.State == AdapterState.On)
        {
            // start scanning whenever Bluetooth is available
            _scanner = adapter.Scan().Subscribe(_devices.ProcessAdvertisement);
        }
        else
        {
            _scanner = null;
        }
    }

    public void Reset()
    {
        _devices.Clear();
        OnAdapterChange(_adapter);
    }

    public BluetoothDeviceCollection Devices => _devices;
}
