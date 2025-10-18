using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Sunny.UI.Win32;
using Sunny.UI;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.ComTypes;

namespace HsWin
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PAINTSTRUCT
    {
        public IntPtr hdc;
        public int fErase;
        public RECT rcPaint;
        public int fRestore;
        public int fIncUpdate;
        public int Reserved1;
        public int Reserved2;
        public int Reserved3;
        public int Reserved4;
        public int Reserved5;
        public int Reserved6;
        public int Reserved7;
        public int Reserved8;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public Point reserved;
        public Size maxSize;
        public Point maxPosition;
        public Size minTrackSize;
        public Size maxTrackSize;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(Rectangle rect)
        {
            Left = rect.Left;
            Top = rect.Top;
            Right = rect.Right;
            Bottom = rect.Bottom;
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(
                    Left,
                    Top,
                    Right - Left,
                    Bottom - Top);
            }
        }

        public Size Size
        {
            get
            {
                return new Size(Right - Left, Bottom - Top);
            }
        }

        public static RECT FromXYWH(
            int x, int y, int width, int height)
        {
            return new RECT(x,
                            y,
                            x + width,
                            y + height);
        }

        public static RECT FromRectangle(Rectangle rect)
        {
            return new RECT(rect.Left,
                             rect.Top,
                             rect.Right,
                             rect.Bottom);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        public UInt32 cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public UInt32 dwStyle;
        public UInt32 dwExStyle;
        public UInt32 dwWindowStatus;
        public UInt32 cxWindowBorders;
        public UInt32 cyWindowBorders;
        public IntPtr atomWindowType;
        public UInt16 wCreatorVersion;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct TRACKMOUSEEVENT
    {
        public uint cbSize;
        public uint dwFlags;
        public IntPtr hwndTrack;
        public uint dwHoverTime;
    }
    [ComImport,
   InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
   Guid("00020D00-0000-0000-c000-000000000046")]
    public interface IRichEditOle
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetClientSite(out IOleClientSite site);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetObjectCount();
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetLinkCount();
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetObject(int iob, [In, Out] REOBJECT lpreobject, [MarshalAs(UnmanagedType.U4)] GETOBJECTOPTIONS flags);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int InsertObject(REOBJECT lpreobject);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ConvertObject(int iob, Guid rclsidNew, string lpstrUserTypeNew);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ActivateAs(Guid rclsid, Guid rclsidAs);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetHostNames(string lpstrContainerApp, string lpstrContainerObj);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetLinkAvailable(int iob, bool fAvailable);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetDvaspect(int iob, uint dvaspect);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int HandsOffStorage(int iob);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SaveCompleted(int iob, IStorage lpstg);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int InPlaceDeactivate();
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ContextSensitiveHelp(bool fEnterMode);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetClipboardData([In, Out] ref CHARRANGE lpchrg, [MarshalAs(UnmanagedType.U4)] GETCLIPBOARDDATAFLAGS reco, out IDataObject lplpdataobj);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ImportDataObject(IDataObject lpdataobj, int cf, IntPtr hMetaPict);
    }
    public enum GETCLIPBOARDDATAFLAGS
    {
        RECO_PASTE = 0,
        RECO_DROP = 1,
        RECO_COPY = 2,
        RECO_CUT = 3,
        RECO_DRAG = 4
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct CHARRANGE
    {
        public int cpMin;
        public int cpMax;
    }
    public enum GETOBJECTOPTIONS
    {
        REO_GETOBJ_NO_INTERFACES = 0x00000000,
        REO_GETOBJ_POLEOBJ = 0x00000001,
        REO_GETOBJ_PSTG = 0x00000002,
        REO_GETOBJ_POLESITE = 0x00000004,
        REO_GETOBJ_ALL_INTERFACES = 0x00000007,
    }
    [StructLayout(LayoutKind.Sequential)]
    public class REOBJECT
    {
        public int cbStruct = Marshal.SizeOf(typeof(REOBJECT));	// Size of structure
        public int cp;											// Character position of object
        public Guid clsid;										// Class ID of object
        public IntPtr poleobj;								// OLE object interface
        public IStorage pstg;									// Associated storage interface
        public IOleClientSite polesite;							// Associated client site interface
        public Size sizel;										// Size of object (may be 0,0)
        public uint dvAspect;									// Display aspect to use
        public uint dwFlags;									// Object status flags
        public uint dwUser;										// Dword for user's use
    }
    [ComVisible(true),
  Guid("00000118-0000-0000-C000-000000000046"),
  InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleClientSite
    {

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SaveObject();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetMoniker(
            [In, MarshalAs(UnmanagedType.U4)] int dwAssign,
            [In, MarshalAs(UnmanagedType.U4)] int dwWhichMoniker,
            [Out, MarshalAs(UnmanagedType.Interface)] out object ppmk);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetContainer([MarshalAs(UnmanagedType.Interface)] out IOleContainer container);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ShowObject();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int OnShowWindow(
            [In, MarshalAs(UnmanagedType.I4)] int fShow);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int RequestNewObjectLayout();
    }
    [ComVisible(true),
   Guid("0000011B-0000-0000-C000-000000000046"),
   InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleContainer
    {
        void ParseDisplayName(
            [In, MarshalAs(UnmanagedType.Interface)] object pbc,
            [In, MarshalAs(UnmanagedType.BStr)] string pszDisplayName,
            [Out, MarshalAs(UnmanagedType.LPArray)] int[] pchEaten,
            [Out, MarshalAs(UnmanagedType.LPArray)] object[] ppmkOut);

        void EnumObjects(
            [In, MarshalAs(UnmanagedType.U4)] int grfFlags,
            [Out, MarshalAs(UnmanagedType.LPArray)] object[] ppenum);

        void LockContainer(
            [In, MarshalAs(UnmanagedType.I4)] int fLock);
    }
    [ComImport]
    [Guid("0000000b-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStorage
    {
        int CreateStream(
            /* [string][in] */ string pwcsName,
            /* [in] */ uint grfMode,
            /* [in] */ uint reserved1,
            /* [in] */ uint reserved2,
            /* [out] */ out IStream ppstm);

        int OpenStream(
            /* [string][in] */ string pwcsName,
            /* [unique][in] */ IntPtr reserved1,
            /* [in] */ uint grfMode,
            /* [in] */ uint reserved2,
            /* [out] */ out IStream ppstm);

        int CreateStorage(
            /* [string][in] */ string pwcsName,
            /* [in] */ uint grfMode,
            /* [in] */ uint reserved1,
            /* [in] */ uint reserved2,
            /* [out] */ out IStorage ppstg);

        int OpenStorage(
            /* [string][unique][in] */ string pwcsName,
            /* [unique][in] */ IStorage pstgPriority,
            /* [in] */ uint grfMode,
            /* [unique][in] */ IntPtr snbExclude,
            /* [in] */ uint reserved,
            /* [out] */ out IStorage ppstg);

        int CopyTo(
            /* [in] */ uint ciidExclude,
            /* [size_is][unique][in] */ Guid rgiidExclude,
            /* [unique][in] */ IntPtr snbExclude,
            /* [unique][in] */ IStorage pstgDest);

        int MoveElementTo(
            /* [string][in] */ string pwcsName,
            /* [unique][in] */ IStorage pstgDest,
            /* [string][in] */ string pwcsNewName,
            /* [in] */ uint grfFlags);

        int Commit(
            /* [in] */ uint grfCommitFlags);

        int Revert();

        int EnumElements(
            /* [in] */ uint reserved1,
            /* [size_is][unique][in] */ IntPtr reserved2,
            /* [in] */ uint reserved3,
            /* [out] */ out IEnumSTATSTG ppenum);

        int DestroyElement(
            /* [string][in] */ string pwcsName);

        int RenameElement(
            /* [string][in] */ string pwcsOldName,
            /* [string][in] */ string pwcsNewName);

        int SetElementTimes(
            /* [string][unique][in] */ string pwcsName,
            /* [unique][in] */ System.Runtime.InteropServices.ComTypes.FILETIME pctime,
            /* [unique][in] */  System.Runtime.InteropServices.ComTypes.FILETIME patime,
            /* [unique][in] */  System.Runtime.InteropServices.ComTypes.FILETIME pmtime);

        int SetClass(
            /* [in] */ Guid clsid);

        int SetStateBits(
            /* [in] */ uint grfStateBits,
            /* [in] */ uint grfMask);

        int Stat(
            /* [out] */ out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg,
            /* [in] */ uint grfStatFlag);

    }
    [ComImport]
    [Guid("0000000d-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumSTATSTG
    {
        // The user needs to allocate an STATSTG array whose size is celt.
        [PreserveSig]
        uint
            Next(
            uint celt,
            [MarshalAs(UnmanagedType.LPArray), Out]
            System.Runtime.InteropServices.ComTypes.STATSTG[] rgelt,
            out uint pceltFetched
            );

        void Skip(uint celt);

        void Reset();

        [return: MarshalAs(UnmanagedType.Interface)]
        IEnumSTATSTG Clone();
    }
    public struct FILETIME
    {
        public int dwLowDateTime;

        public int dwHighDateTime;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct STATSTG
    {
        public string pwcsName;

        public int type;

        public long cbSize;

        public FILETIME mtime;

        public FILETIME ctime;

        public FILETIME atime;

        public int grfMode;

        public int grfLocksSupported;

        public Guid clsid;

        public int grfStateBits;

        public int reserved;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct TOOLINFO
    {
        public TOOLINFO(int flags)
        {
            this.cbSize = Marshal.SizeOf(typeof(TOOLINFO));
            this.uFlags = flags;
            this.hwnd = IntPtr.Zero;
            this.uId = IntPtr.Zero;
            this.rect = new RECT(0, 0, 0, 0);
            this.hinst = IntPtr.Zero;
            this.lpszText = IntPtr.Zero;
            this.lParam = IntPtr.Zero;
        }

        public int cbSize;
        public int uFlags;
        public IntPtr hwnd;
        public IntPtr uId;
        public RECT rect;
        public IntPtr hinst;
        public IntPtr lpszText;
        public IntPtr lParam;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct NMHDR
    {
        public NMHDR(int flag)
        {
            this.hwndFrom = IntPtr.Zero;
            this.idFrom = 0;
            this.code = 0;
        }

        public IntPtr hwndFrom;
        public int idFrom;
        public int code;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct SCROLLBARINFO
    {
        public int cbSize;
        public RECT rcScrollBar;
        public int dxyLineButton;
        public int xyThumbTop;
        public int xyThumbBottom;
        public int reserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] rgstate;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct INITCOMMONCONTROLSEX
    {
        public INITCOMMONCONTROLSEX(int flags)
        {
            this.dwSize = Marshal.SizeOf(typeof(INITCOMMONCONTROLSEX));
            this.dwICC = flags;
        }

        public int dwSize;
        public int dwICC;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct NMTTDISPINFO
    {
        public NMTTDISPINFO(int flags)
        {
            this.hdr = new NMHDR(0);
            this.lpszText = IntPtr.Zero;
            this.szText = IntPtr.Zero;
            this.hinst = IntPtr.Zero;
            this.uFlags = 0;
            this.lParam = IntPtr.Zero;
        }

        public NMHDR hdr;
        public IntPtr lpszText;
        public IntPtr szText;
        public IntPtr hinst;
        public int uFlags;
        public IntPtr lParam;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct NMTTCUSTOMDRAW
    {
        public NMCUSTOMDRAW nmcd;
        public uint uDrawFlags;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct NMCUSTOMDRAW
    {
        public NMHDR hdr;
        public uint dwDrawStage;
        public IntPtr hdc;
        public RECT rc;
        public IntPtr dwItemSpec;
        public uint uItemState;
        public IntPtr lItemlParam;
    }
    [ComImport]
    [Guid("0000000a-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ILockBytes
    {
        int ReadAt(
            /* [in] */ ulong ulOffset,
            /* [unique][out] */ IntPtr pv,
            /* [in] */ uint cb,
            /* [out] */ out IntPtr pcbRead);

        int WriteAt(
            /* [in] */ ulong ulOffset,
            /* [size_is][in] */ IntPtr pv,
            /* [in] */ uint cb,
            /* [out] */ out IntPtr pcbWritten);

        int Flush();

        int SetSize(
            /* [in] */ ulong cb);

        int LockRegion(
            /* [in] */ ulong libOffset,
            /* [in] */ ulong cb,
            /* [in] */ uint dwLockType);

        int UnlockRegion(
            /* [in] */ ulong libOffset,
            /* [in] */ ulong cb,
            /* [in] */ uint dwLockType);

        int Stat(
            /* [out] */ out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg,
            /* [in] */ uint grfStatFlag);

    }
}
