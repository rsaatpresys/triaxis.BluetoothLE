using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CoreBluetooth;
using Foundation;
using UIKit;

namespace triaxis.Xamarin.BluetoothLE.iOS
{
    static class PrivateExtensions
    {
        public static Uuid ToUuid(this CBUUID uuid)
        {
            var bytes = uuid.Data.ToArray();
            return Uuid.FromBE(bytes);
        }

        public static Uuid ToUuid(this NSUuid uuid)
        {
            var bytes = uuid.GetBytes();
            return Uuid.FromBE(bytes);
        }
    }
}
