using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace WeatherStationPi2.Sensors
{
    // First draft version of a class representing I2C Device
    // Based on the code from http://www.guruumeditation.net/en/io-ic-with-windows-10-iot-raspberry-pi-2-and-the-temperature-sensor-bmp180/
    public class I2CDevice
    {

        public I2CDevice()
        {

        }

        public virtual async Task Init(int n=0)
        {
            var i2CSettings = new I2cConnectionSettings(0x77)
            {
                BusSpeed = I2cBusSpeed.FastMode,
                SharingMode = I2cSharingMode.Shared
            };
            var i2c1 = I2cDevice.GetDeviceSelector("I2C1");
            var devices = await DeviceInformation.FindAllAsync(i2c1);
            sensor = await I2cDevice.FromIdAsync(devices[n].Id, i2CSettings);
        }

        protected I2cDevice sensor;

        public short ReadInt16(byte register)
        {
            var value = new byte[2];

            sensor.WriteRead(new[] { register }, value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(value);

            return BitConverter.ToInt16(value, 0);
        }

        public ushort ReadUInt16(byte register)
        {
            var value = ReadInt16(register);

            return (ushort)value;
        }

        public void Write(byte register, byte command)
        {
            sensor.Write(new[] { register, command });
        }

    }
}
