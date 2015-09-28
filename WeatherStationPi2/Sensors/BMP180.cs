using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherStationPi2.Sensors
{
    // Draft version based on the code from http://www.guruumeditation.net/en/io-ic-with-windows-10-iot-raspberry-pi-2-and-the-temperature-sensor-bmp180/
    public class BMP180 : I2CDevice
    {
        public double Temperature
        {
            get
            {
                var mre = new ManualResetEventSlim(false);
                Write(_bmp085RegisterControl, _bmp085RegisterReadtempcmd);
                mre.Wait(TimeSpan.FromMilliseconds(5));
                var t = (int)ReadUInt16(_bmp085RegisterTempdata);

                var b5 = ComputeB5(t);
                t = (b5 + 8) >> 4;
                var temp = t / 10.0;
                return temp;
            }
        }

        private ushort _ac5;
        private ushort _ac6;
        private short _mc;
        private short _md;
        private int ComputeB5(int value)
        {
            var x1 = (value - _ac6) * _ac5 >> 15;
            var x2 = (_mc << 11) / (x1 + _md);
            return x1 + x2;
        }

        public override async Task Init(int n = 0)
        {
            await base.Init(n);
            _ac5 = ReadUInt16(_bmp085RegisterCalAc5);
            _ac6 = ReadUInt16(_bmp085RegisterCalAc6);
            _mc = ReadInt16(_bmp085RegisterCalMc);
            _md = ReadInt16(_bmp085RegisterCalMd);
        }

        private byte _bmp085RegisterCalAc5 = 0xB2; // R   Calibration data (16 bits)
        private byte _bmp085RegisterCalAc6 = 0xB4; // R   Calibration data (16 bits)
        private byte _bmp085RegisterCalMc = 0xBC; // R   Calibration data (16 bits)
        private byte _bmp085RegisterCalMd = 0xBE; // R   Calibration data (16 bits) 

        private byte _bmp085RegisterControl = 0xF4;
        private byte _bmp085RegisterTempdata = 0xF6;

        private byte _bmp085RegisterReadtempcmd = 0x2E;



    }
}
