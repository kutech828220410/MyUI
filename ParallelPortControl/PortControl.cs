using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Basic;
namespace PortControl
{
    public class ProtMethod
    {
        [DllImport("inpout32.dll")]
        public static extern UInt32 IsInpOutDriverOpen();
        [DllImport("inpout32.dll")]
        public static extern void Out32(short PortAddress, short Data);
        [DllImport("inpout32.dll")]
        public static extern char Inp32(int PortAddress);

        [DllImport("inpout32.dll")]
        public static extern void DlPortWritePortUshort(short PortAddress, ushort Data);
        [DllImport("inpout32.dll")]
        public static extern ushort DlPortReadPortUshort(short PortAddress);

        [DllImport("inpout32.dll")]
        public static extern void DlPortWritePortUlong(int PortAddress, uint Data);
        [DllImport("inpout32.dll")]
        public static extern uint DlPortReadPortUlong(int PortAddress);

        [DllImport("inpout32.DLL", EntryPoint = "Out32")]
        public static extern void Output(int adress, int value);

        [DllImport("inpout32.DLL", EntryPoint = "Inp32")]
        public static extern int Input(int adress);


        [DllImport("inpoutx64.dll", EntryPoint = "IsInpOutDriverOpen")]
        public static extern UInt32 IsInpOutDriverOpen_x64();
        [DllImport("inpoutx64.dll", EntryPoint = "Out32")]
        public static extern void Out32_x64(short PortAddress, short Data);
        [DllImport("inpoutx64.dll", EntryPoint = "Inp32")]
        public static extern char Inp32_x64(int PortAddress);

        [DllImport("inpoutx64.dll", EntryPoint = "DlPortWritePortUshort")]
        public static extern void DlPortWritePortUshort_x64(short PortAddress, ushort Data);
        [DllImport("inpoutx64.dll", EntryPoint = "DlPortReadPortUshort")]
        public static extern ushort DlPortReadPortUshort_x64(short PortAddress);

        [DllImport("inpoutx64.dll", EntryPoint = "DlPortWritePortUlong")]
        public static extern void DlPortWritePortUlong_x64(int PortAddress, uint Data);
        [DllImport("inpoutx64.dll", EntryPoint = "DlPortReadPortUlong")]
        public static extern uint DlPortReadPortUlong_x64(int PortAddress);

        [DllImport("inpoutx64.dll", EntryPoint = "GetPhysLong")]
        public static extern bool GetPhysLong_x64(ref int PortAddress, ref uint Data);
        [DllImport("inpoutx64.dll", EntryPoint = "SetPhysLong")]
        public static extern bool SetPhysLong_x64(ref int PortAddress, ref uint Data);
    }
    public class ParallelPortControl
    {
        static MyConvert _MyConvert = new MyConvert();     
        private int PortAcess = 0;

        public enum OUT
        {
            PIN02 = 0,
            PIN03 = 1,
            PIN04 = 2,
            PIN05 = 3,
            PIN06 = 4,
            PIN07 = 5,
            PIN08 = 6,
            PIN09 = 7,
        };
        public enum IN
        {
            PIN10 = 6,
            PIN11 = 7,
            PIN12 = 5,
            PIN13 = 4,
            PIN15 = 3,
        };

        public bool OPIN02 = false;
        public bool OPIN03 = false;
        public bool OPIN04 = false;
        public bool OPIN05 = false;
        public bool OPIN06 = false;
        public bool OPIN07 = false;
        public bool OPIN08 = false;
        public bool OPIN09 = false;


        public bool IPIN10 = false;
        public bool IPIN11 = false;
        public bool IPIN12 = false;
        public bool IPIN13 = false;
        public bool IPIN15 = false;

        private byte InValue = 0;
        private byte OutValue = 0;
        public ParallelPortControl(int PortAcess)
        {
            this.PortAcess = PortAcess;
            ProtMethod.Output(PortAcess, 0);
            ProtMethod.Output(PortAcess + 1, 0);
            ProtMethod.Output(PortAcess + 2, 12);
        }
        public bool PortTest()
        {
            int temp = 0;
            if (Basic.MySystem.IsSystem_x64())
            {
                try
                {
                    if (ProtMethod.IsInpOutDriverOpen_x64() == 0)
                    {
                        temp = -1;
                    }
                }
                catch
                {
                    temp = -1;
                }
            }
            else
            {
                try
                {
                    if (ProtMethod.IsInpOutDriverOpen() == 0)
                    {
                        temp = -1;
                    }
                }
                catch
                {
                    temp = -1;
                }
            }

            return !(temp == -1);
        }
        public void WriteByte(byte Data)
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                ProtMethod.Out32_x64((short)this.PortAcess, Data);    
            }
            else
            {
                ProtMethod.Output(this.PortAcess, Data);
            }

        }
        public void WritePIN(OUT PIN ,bool value)
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                OutValue = ReadOUT();
                OutValue = _MyConvert.ByteSetBit(value, OutValue, (int)PIN);
                ProtMethod.Out32_x64((short)this.PortAcess, OutValue); 
            }
            else
            {
                OutValue = ReadOUT();
                OutValue = _MyConvert.ByteSetBit(value, OutValue, (int)PIN);
                ProtMethod.Output(this.PortAcess, OutValue);
            }
        }
        public bool ReadPIN(IN PIN, bool value)
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                InValue = ReadIN();
                return _MyConvert.ByteGetBit((byte)InValue, (int)PIN);
            }
            else
            {
                InValue = ReadIN();
                return _MyConvert.ByteGetBit((byte)InValue, (int)PIN);
            }
    
        }
        public byte GetINValue()
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                return (byte)ProtMethod.Inp32_x64(PortAcess + 1);
            }
            else
            {
                return (byte)ProtMethod.Input(PortAcess + 1);
            }

        }
        public byte GetControlValue()
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                return (byte)ProtMethod.Inp32_x64(PortAcess + 2);
            }
            else
            {
                return (byte)ProtMethod.Input(PortAcess + 2);
            }

        }
        public byte GetOUTValue()
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                return (byte)ProtMethod.Inp32_x64(PortAcess);
            }
            else
            {
                return (byte)ProtMethod.Input(PortAcess);
            }
   
        }
        public byte ReadIN()
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                InValue = (((byte)ProtMethod.Inp32_x64(PortAcess + 1)));
                IPIN10 = !_MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN10);
                IPIN11 = _MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN11);
                IPIN12 = !_MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN12);
                IPIN13 = !_MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN13);
                IPIN15 = !_MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN15);
                return InValue;
            }
            else
            {
                InValue = (((byte)ProtMethod.Input(PortAcess + 1)));
                IPIN10 = !_MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN10);
                IPIN11 = _MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN11);
                IPIN12 = !_MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN12);
                IPIN13 = !_MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN13);
                IPIN15 = !_MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN15);
                return InValue;
            }
         
        }
        public byte ReadOUT()
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                OutValue = (byte)ProtMethod.Inp32_x64(PortAcess);
                OPIN02 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN02);
                OPIN03 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN03);
                OPIN04 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN04);
                OPIN05 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN05);
                OPIN06 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN06);
                OPIN07 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN07);
                OPIN08 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN08);
                OPIN09 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN09);
                return OutValue;
            }
            else
            {
                OutValue = (byte)ProtMethod.Input(PortAcess);
                OPIN02 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN02);
                OPIN03 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN03);
                OPIN04 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN04);
                OPIN05 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN05);
                OPIN06 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN06);
                OPIN07 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN07);
                OPIN08 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN08);
                OPIN09 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN09);
                return OutValue;
            }
         
       } 

    }
    public class SerialPortControl
    {
        static MyConvert _MyConvert = new MyConvert();
        public enum OUT
        {
            PIN04 = 0,
            PIN07 = 1,
        };
        public enum IN
        {
            PIN01 = 7,
            PIN06 = 5,
            PIN08 = 4,
            PIN09 = 6,
        };
        private int PortAcess = 0;

        public bool OPIN04 = false;
        public bool OPIN07 = false;

        public bool IPIN01 = false;
        public bool IPIN06 = false;
        public bool IPIN08 = false;
        public bool IPIN09 = false;

        private byte InValue = 0;
        private byte OutValue = 0;
        public bool PortTest()
        {
            int temp = 0;
            if (Basic.MySystem.IsSystem_x64())
            {
                try
                {
                    if (ProtMethod.IsInpOutDriverOpen_x64() == 0)
                    {
                        temp = -1;
                    }
                }
                catch
                {
                    temp = -1;
                }

            }
            else
            {      
                try
                {
                    if (ProtMethod.IsInpOutDriverOpen() == 0)
                    {
                        temp = -1;
                    }
                }
                catch
                {
                    temp = -1;
                }

            }        
            return !(temp == -1);
        }
        public SerialPortControl(int PortAcess)
        {
            this.PortAcess = PortAcess;
        }
        public void WriteByte(byte Data)
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                ProtMethod.Out32_x64((short)(this.PortAcess + 4), Data);  

            }
            else
            {
                ProtMethod.Output(this.PortAcess + 4, Data);
            } 
           
        }
        public void WritePIN(OUT PIN, bool value)
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                OutValue = ReadOUT();
                OutValue = _MyConvert.ByteSetBit(value, OutValue, (int)PIN);
                ProtMethod.Out32_x64((short)(this.PortAcess + 4), OutValue);  
            }
            else
            {
                OutValue = ReadOUT();
                OutValue = _MyConvert.ByteSetBit(value, OutValue, (int)PIN);
                ProtMethod.Output(this.PortAcess + 4, OutValue);
            } 
     
        }
        public bool ReadPIN(IN PIN, bool value)
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                InValue = ReadIN();
                return _MyConvert.ByteGetBit((byte)InValue, (int)PIN);
            }
            else
            {
                InValue = ReadIN();
                return _MyConvert.ByteGetBit((byte)InValue, (int)PIN);
            } 
  
        }
        public byte GetINValue()
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                return (byte)ProtMethod.Inp32_x64(PortAcess + 6);
            }
            else
            {
                return (byte)ProtMethod.Input(PortAcess + 6);
            } 
            
        }
        public byte GetOUTValue()
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                return (byte)ProtMethod.Inp32_x64(PortAcess + 4);
            }
            else
            {
                return (byte)ProtMethod.Input(PortAcess + 4);
            } 
           
        }
        public byte ReadIN()
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                InValue = (((byte)ProtMethod.Inp32_x64(PortAcess + 6)));
                IPIN01 = _MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN01);
                IPIN06 = _MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN06);
                IPIN08 = _MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN08);
                IPIN09 = _MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN09);
                return InValue;
            }
            else
            {
                InValue = (((byte)ProtMethod.Input(PortAcess + 6)));
                IPIN01 = _MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN01);
                IPIN06 = _MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN06);
                IPIN08 = _MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN08);
                IPIN09 = _MyConvert.ByteGetBit((byte)InValue, (int)IN.PIN09);
                return InValue;
            } 
        }
        public byte ReadOUT()
        {
            if (Basic.MySystem.IsSystem_x64())
            {
                OutValue = (byte)ProtMethod.Inp32_x64(PortAcess + 4);
                OPIN04 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN04);
                OPIN07 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN07);
                return OutValue;
            }
            else
            {
                OutValue = (byte)ProtMethod.Input(PortAcess + 4);
                OPIN04 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN04);
                OPIN07 = _MyConvert.ByteGetBit((byte)OutValue, (int)OUT.PIN07);
                return OutValue;
            } 
        }

    }
}
