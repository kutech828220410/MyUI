///
///	Description:
///			C# class for PCI-1230
///	Author:
///			PengYi
///	History:
///			2014-7-22  Create

///
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;


namespace Pcie1230
{

    public class CPcie1230
    {
        public static int PCI1230Success = 1024;
        public static int PCI1230ApiFailed = 1025;
        public static int PCI1230InvalidParam = 1026;
        public static int PCI1230DevNotFind = 1027;
        //open/close
        [DllImport(@"\Pci1230\pci1230.dll")]
        public static extern int Pci1230Open(int pBoardId);
        [DllImport(@"\Pci1230\pci1230.dll")]
        public static extern int Pci1230Close(int pBoardId);


        [DllImport(@"\Pci1230\pci1230.dll")]
        public static extern int Pci1230Read(int pBoardid, ref uint Data);
        [DllImport(@"\Pci1230\pci1230.dll")]
        public static extern int Pci1230Write(int pBoardid, uint writedata);

        [DllImport(@"\Pci1230\pci1230.dll")]
        public static extern int Pci1230ReadDiBit(int pBoardid, int bit, ref uint Data);
        [DllImport(@"\Pci1230\pci1230.dll")]
        public static extern int Pci1230WriteDoBit(int pBoardid, int bit, uint writedata);

        [DllImport(@"\Pci1230\pci1230.dll")]
        public static extern int Pci1230ReadDoBit(int pBoardid, int bit, ref uint Data);

    }
}