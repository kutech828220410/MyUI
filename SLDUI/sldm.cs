///
///	Description:
///			C# class for e400
///	Author:
///			zenglicheng
///	History:
///			2012-9-12  Create, part of APIs implemented
///			2014-4-11  update, all APIs implemented
///
//      [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace ESLDM
{
    static public class SLD_Basic
    {
        public static short SLDM_ERR_OK = 0;				/*无错误*/
        public static short SLDM_ERR_PMVAL = -1;				/*参数值错误*/
        public static short SLDM_ERR_PULSEOVERFLOW = -2;				/*脉冲发生器溢出*/
        public static short SLDM_ERR_PMID = -3;				/*参数ID不存在，无此参数*/
        public static short SLDM_ERR_MCMAX = -4;				/*控制器不存在，超出系统支持的最大控制器索引号*/
        public static short SLDM_ERR_CHMAX = -5;				/*通道号不存在，超出系统最大通道数*/
        public static short SLDM_ERR_AXISMAX = -6;			/*轴号不存在，超出系统最大轴数*/
        public static short SLDM_ERR_NOAUTH = -7;				/*控制器无授权*/
        public static short SLDM_ERR_PLCADDROVERFLOW = -8;			/*PLC地址溢出*/
        public static short SLDM_ERR_NOFLAG = -9;			/*没有此状态或标志*/
        public static short SLDM_ERR_NONSTOPPED = -10;				/*运动没有停止*/
        public static short SLDM_ERR_MCNOFILE = -11;				/*文件号打开失败。文件号不存在，或SD卡不存在。*/
        public static short SLDM_ERR_HOSTNOFILE = -12;				/*HOST打开文件失败*/

        public static short SLDM_ERR_COM_ADDR = -101;			/*与HOST通讯的设备地址错误*/
        public static short SLDM_ERR_COM_CHECKSUM = -102;			/*与HOST通讯的校验和错误*/
        public static short SLDM_ERR_COM_INVCMD = -103;			/*与HOST通讯的命令ID错误*/
        public static short SLDM_ERR_SOCKET = -104;			/*HOST库中，SOCKET初始化失败*/
        public static short SLDM_ERR_SHM = -105;			/*HOST库中，SHM初始化失败*/
        public static short SLDM_ERR_PIPE = -106;			/*HOST库中，PIPE初始化失败*/
        public static short SLDM_ERR_NOLIBINITD = -107;			/*HOST库没有初始化*/
        public static short SLDM_ERR_NOMCOPEN = -108;			/*HOST库中，控制器未打开*/
        public static short SLDM_ERR_MCOPEND = -109;			/*HOST库中，控制器已打开*/
        public static short SLDM_ERR_NOHOSTCONN = -110;			/*HOST与控制器通讯超时*/
        public static short SLDM_ERR_THREAD = -111;			/*HOST库中，线程初始化失败*/
        public static short SLDM_ERR_BUFFNOOPEN = -112;			/*命令缓冲区未打开*/
        public static short SLDM_ERR_BUFFFULL = -113;			/*命令缓冲区满*/


        public static short SLDM_ERR_FB_TIMEOUT = -121;			/*伺服现场总线超时*/
        public static short SLDM_ERR_FB_NCYCWNG = -122;			/*伺服总线中非周期命令执行报警*/
        public static short SLDM_ERR_FB_CCYCERR = -123;			/*伺服总线中非周期命令执行错误*/

        public static short SLDM_ERR_UPDATE = -255;			/*内部使用，更新命令*/

        static public bool ErrorCode(short code , bool ShowMessageBox)
         {
             bool flag = true;
             string error_name = "";
             if (code == SLD_Basic.SLDM_ERR_OK) error_name = "";
             else if (code == SLD_Basic.SLDM_ERR_PMVAL) error_name = "參數值錯誤";
             else if (code == SLD_Basic.SLDM_ERR_PULSEOVERFLOW) error_name = "脈衝發生器溢出";
             else if (code == SLD_Basic.SLDM_ERR_PMID) error_name = "參數ID不存在，無此參數";
             else if (code == SLD_Basic.SLDM_ERR_MCMAX) error_name = "控制器不存在，超出系統支持的最大控制器索引號";
             else if (code == SLD_Basic.SLDM_ERR_CHMAX) error_name = "通道號不存在，超出系統最大通道數";
             else if (code == SLD_Basic.SLDM_ERR_AXISMAX) error_name = "軸號不存在超出系統最大軸數";
             else if (code == SLD_Basic.SLDM_ERR_NOAUTH) error_name = "控制器無授權";
             else if (code == SLD_Basic.SLDM_ERR_PLCADDROVERFLOW) error_name = "PLC地址溢出";
             else if (code == SLD_Basic.SLDM_ERR_NOFLAG) error_name = "沒有此狀態或標誌";
             else if (code == SLD_Basic.SLDM_ERR_NONSTOPPED) error_name = "運動沒有停止";
             else if (code == SLD_Basic.SLDM_ERR_MCNOFILE) error_name = "文件號打開失敗‧文件號不存在，或SD卡不存在";
             else if (code == SLD_Basic.SLDM_ERR_HOSTNOFILE) error_name = "HOST文件打開失敗";
             else if (code == SLD_Basic.SLDM_ERR_COM_ADDR) error_name = "與HOST通訊的設備地址設定錯誤";
             else if (code == SLD_Basic.SLDM_ERR_COM_CHECKSUM) error_name = "與HOST通訊的設備校驗位錯誤";
             else if (code == SLD_Basic.SLDM_ERR_COM_INVCMD) error_name = "與HOST通訊的設備命令ID錯誤";
             else if (code == SLD_Basic.SLDM_ERR_SOCKET) error_name = "HOST庫中，SOCKET初始化失敗";
             else if (code == SLD_Basic.SLDM_ERR_SHM) error_name = "HOST庫中，SHM初始化失敗";
             else if (code == SLD_Basic.SLDM_ERR_PIPE) error_name = "HOST庫中，PIPE初始化失敗";
             else if (code == SLD_Basic.SLDM_ERR_NOLIBINITD) error_name = "HOST庫沒有初始化";
             else if (code == SLD_Basic.SLDM_ERR_NOMCOPEN) error_name = "HOST庫中，控制器未打開";
             else if (code == SLD_Basic.SLDM_ERR_MCOPEND) error_name = "HOST庫中，控制器已打開";
             else if (code == SLD_Basic.SLDM_ERR_NOHOSTCONN) error_name = "HOST與控制器通訊超時";
             else if (code == SLD_Basic.SLDM_ERR_THREAD) error_name = "HOST库中，线程初始化失败";
             else if (code == SLD_Basic.SLDM_ERR_BUFFNOOPEN) error_name = "命令緩衝區未打開";
             else if (code == SLD_Basic.SLDM_ERR_BUFFFULL) error_name = "命令緩衝區滿位";
             else if (code == SLD_Basic.SLDM_ERR_FB_TIMEOUT) error_name = "伺服現場總線超時";
             else if (code == SLD_Basic.SLDM_ERR_FB_NCYCWNG) error_name = "伺服總線中，非週期命令執行警報";
             else if (code == SLD_Basic.SLDM_ERR_FB_CCYCERR) error_name = "伺服總線中，非週期命令執行錯誤";
             else if (code == SLD_Basic.SLDM_ERR_UPDATE) error_name = "";
             if (error_name != "" )
             {
                 if (ShowMessageBox)
                 MessageBox.Show(error_name, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                 flag = false;
             }
             return flag;
         }
    }
    public class SLDMclass
    {
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_Test(short mc);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_Open(short mc, short axisnum, short timeout/*ms*/);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_IsOpen(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_Close(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetConnStatus(short mc);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_Debug(short mc, short on);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetFBConn(short mc, ushort fb_type);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetFBStat(short mc, uint axes, short[] cmd_err, short[] timeout, short[] macid, short[] runstat);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_Update(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_Reset(short mc);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_JogA(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_JogI(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_JogP(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_JogM(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_Home(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_Stop(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_EStop(short mc, uint axes);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_RstAlm(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ServoOn(short mc, uint axes, short on);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChStart(short mc, ushort ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChStop(short mc, ushort ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChHold(short mc, ushort ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChEStop(short mc, ushort ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChReset(short mc, ushort ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetBuffer(short mc, ushort ch, short buff);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetIntpMode(short mc, ushort ch, short[] intp);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetBuffer(short mc, ushort ch, short[] buff);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetStat(short mc, ushort ch, short[] running, short[] stopped);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetAcc(short mc, ushort ch, float a);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetSmth(short mc, ushort ch, ushort t);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetVelMax(short mc, ushort ch, float v);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetAccMax(short mc, ushort ch, float a);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetVelS(short mc, ushort ch, float v);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetVelE(short mc, ushort ch, float v);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetBlendTm(short mc, ushort ch, ushort t);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetCrd(short mc, short ch, short num, short[] axis);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetOvrd(short mc, ushort ch, short ovrd);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetMR(short mc, ushort ch, short mr);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChSetInit(short mc, ushort ch);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetBlkID(short mc, ushort ch, uint[] id);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChActFeed(short mc, ushort ch, float[] f);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetFeed(short mc, ushort ch, float[] f);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetAcc(short mc, ushort ch, float[] a);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetSmth(short mc, ushort ch, ushort[] t);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetVelMax(short mc, ushort ch, float[] v);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetAccMax(short mc, ushort ch, float[] a);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetVelS(short mc, ushort ch, float[] v);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetVelE(short mc, ushort ch, float[] v);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetBlendTm(short mc, ushort ch, ushort[] t);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetCrd(short mc, short ch, short[] num, short[] axis);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetCrdOft(short mc, short ch, short[] oftidx);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetOvrd(short mc, ushort ch, short[] ovrd);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetMFn(short mc, ushort ch, short[] m);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetSpindle(short mc, ushort ch, int[] cs, int[] as1);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ChGetTool(short mc, ushort ch, short[] t);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ParStart(short mc, ushort ch, short filno_0, short bufno_0, short filno_1, short bufno_1);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ParStop(short mc, ushort ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ParReset(short mc, ushort ch);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_LatchBegin(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_LatchEnd(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetLatchSrc(short mc, uint axes, short src);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetLatchTrigger(short mc, uint axes, short trigger);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetLatchCmprAxis(short mc, uint axes, short cmpraxis);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetLatchSrc(short mc, ref uint src);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetLatchTrigger(short mc, uint axes, short[] trigger);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetLatchCmprAxis(short mc, uint axes, short[] cmpraxis);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetLatchPos(short mc, short axis, int[] pos, short[] numofcapt);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_CmprBegin(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_CmprEnd(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetCmprSrc(short mc, uint axes, short src);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetCmprPos(short mc, uint axes, int[] pos, short numtocmpr);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetCmprQPol(short mc, uint axes, short pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetCmprQWidth(short mc, uint axes, uint width);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetCmprQEnable(short mc, uint axes, short en);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetCmprSrc(short mc, ref uint src);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetCmprPos(short mc, short axis, int[] pos, short[] numtocmpr);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetCmprNum(short mc, short axis, short[] numofcmpr);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetCmprQPol(short mc, ref uint pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetCmprQWidth(short mc, uint axes, uint[] width);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetCmprQEnable(short mc, ref uint en);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetPosT(short mc, uint axes, int pos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetVelT(short mc, uint axes, float vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetAccT(short mc, uint axes, float acc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetDecT(short mc, uint axes, float dec);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetSmthT(short mc, uint axes, ushort tm);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetVelS(short mc, uint axes, float vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetVelM(short mc, uint axes, float vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetAccM(short mc, uint axes, float acc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetDecM(short mc, uint axes, float dec);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetPosC(short mc, uint axes, int pos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetPosA(short mc, uint axes, int pos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetOvrd(short mc, uint axes, short ovrd);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPosT(short mc, uint axes, int[] pos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetVelT(short mc, uint axes, float[] vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetAccT(short mc, uint axes, float[] acc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetDecT(short mc, uint axes, float[] dec);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSmthT(short mc, uint axes, ushort[] tm);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetVelM(short mc, uint axes, float[] vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetAccM(short mc, uint axes, float[] acc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetDecM(short mc, uint axes, float[] dec);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPosA(short mc, uint axes, int[] pos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPosC(short mc, uint axes, int[] pos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetVelC(short mc, uint axes, float[] vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetVelA(short mc, uint axes, float[] vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetAccC(short mc, uint axes, float[] acc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetAccA(short mc, uint axes, float[] acc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetOvrd(short mc, uint axes, short[] ovrd);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetPosAux(short mc, ushort encs, int pos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPosAux(short mc, ushort encs, int[] pos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetPulseRatioAux(short mc, ushort encs, uint pulse, uint disp);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPulseRatioAux(short mc, ushort encs, uint[] pulse, uint[] disp);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetLaserPWM(short mc, ushort ch, uint freq, ushort duty, short on);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetLaserPWM(short mc, ushort ch, uint[] freq, ushort[] duty, short[] on);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetComPm(short mc, short com, short baud, short databits, short stopbits, short parity);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetComPm(short mc, short com, ref short baud, ref short databits, ref short stopbits, ref short parity);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetComData(short mc, short com, short format, short size, short[] data, short[] stat);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetComData(short mc, short com, short format, short size, short[] data, short timeout, short[] stat);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetHmDir(short mc, uint axes, short dir);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetHmMode(short mc, uint axes, short mode);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetHmHiVel(short mc, uint axes, float vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetHmLoVel(short mc, uint axes, float vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetHmAccTm(short mc, uint axes, ushort tm);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetHmOft(short mc, uint axes, int oft);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetHmRef(short mc, uint axes, int refpos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetPSL(short mc, uint axes, int pos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetNSL(short mc, uint axes, int pos);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHmDir(short mc, ref uint dir);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHmMode(short mc, uint axes, short[] mode);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHmHiVel(short mc, uint axes, float[] vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHmLoVel(short mc, uint axes, float[] vel);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHmAccTm(short mc, uint axes, ushort[] tm);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHmOft(short mc, uint axes, int[] oft);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHmRef(short mc, uint axes, int[] refpos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPSL(short mc, uint axes, int[] pos);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetNSL(short mc, uint axes, int[] pos);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetHmPol(short mc, uint axes, short pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetPOTOn(short mc, uint axes, short on);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetNOTOn(short mc, uint axes, short on);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetPOTPol(short mc, uint axes, short pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetNOTPol(short mc, uint axes, short pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetEncOn(short mc, uint axes, short on);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetEncPol(short mc, uint axes, short pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetIdxPol(short mc, uint axes, short pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetAlmOn(short mc, uint axes, short on);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetAlmPol(short mc, uint axes, short pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetPulseMode(short mc, uint axes, short mode);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetModular(short mc, uint axes, short mod);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHmPol(short mc, ref uint pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPOTOn(short mc, ref uint on);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetNOTOn(short mc, ref uint on);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPOTPol(short mc, ref uint pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetNOTPol(short mc, ref uint pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetEncOn(short mc, ref uint on);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetEncPol(short mc, ref uint pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetIdxPol(short mc, ref uint pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetAlmOn(short mc, ref uint on);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetAlmPol(short mc, ref uint pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPulseMode(short mc, ref uint mode);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetModular(short mc, ref uint mod);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetMotRes(short mc, uint axes, uint res);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetEncRes(short mc, uint axes, uint res);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetPulseRatio(short mc, uint axes, uint pulse, uint disp);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetGearRatio(short mc, uint axes, uint master, uint slave);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetMotRes(short mc, uint axes, uint[] res);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetEncRes(short mc, uint axes, uint[] res);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPulseRatio(short mc, uint axes, uint[] pulse, uint[] disp);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetGearRatio(short mc, uint axes, uint[] master, uint[] slave);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetEGear(short mc, short slave, short master, short dir, short possrc, short mode, int trans);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_StartEGear(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_StopEGear(short mc, uint axes);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetEGear(short mc, short slave, ref short master, ref short dir, ref short possrc, ref short mode, ref int trans);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetEGearStat(short mc, ref uint stat);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SaveIOCfg(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_ResetIOCfg(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_StartIOCfg(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetIOCfg(short mc, short node, short type, short valid, short bytesofin, short bytesofout);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetIOCfg(short mc, short node, ref short type, ref short stat, ref short bytesofin, ref short bytesofout);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetOutBit(short mc, short node, short index, ushort data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetOutByte(short mc, short node, short index, ushort data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetOutWord(short mc, short node, short index, ushort data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetOutDWord(short mc, short node, short index, uint data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetOutBit(short mc, short node, short index, ref ushort data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetOutByte(short mc, short node, short index, ref ushort data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetOutWord(short mc, short node, short index, ref ushort data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetOutDWord(short mc, short node, short index, ref uint data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetInBit(short mc, short node, short index, ref ushort data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetInByte(short mc, short node, short index, ref ushort data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetInWord(short mc, short node, short index, ref ushort data);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetInDWord(short mc, short node, short index, ref uint data);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_SetEMGCfg(short mc, short en, short pol);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetEMGCfg(short mc, ref short en, ref short pol);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPOT(short mc, ref uint POT);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetNOT(short mc, ref uint NOT);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHome(short mc, ref uint home);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetEstop(short mc, ref uint estop);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetStopped(short mc, ref uint stopped);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetRunning(short mc, ref uint running);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetReached(short mc, ref uint reached);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetStopping(short mc, ref uint stopping);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHoming(short mc, ref uint homing);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetHomed(short mc, ref uint homed);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetAlarm(short mc, ref uint alm);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetAlmCode(short mc, uint axes, short[] maincode, short[] subcode);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetPSLFlag(short mc, ref uint psl);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetNSLFlag(short mc, ref uint nsl);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetMotAct(short mc, ref uint act);////检查电机是否激活（使能）
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetMonData(short mc, uint axes, int[] data);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_FILStatus(short mc, short fileno, ref ushort status);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_FILOpen(short mc, short fileno, ref char filename, short dir);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_FILtoMC(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_FILfromMC(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_FILClose(short mc, short fileno);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffSelect(short mc, short buff);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffReset(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffSet(short mc, short size, short mode);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffClear(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffSend(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffIdleSpace(short mc, ref short size, ref short mode);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_HDBusy(short mc);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_HDError(short mc);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffRapid(short mc, ref int p, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffLinear(short mc, ref int p, short est, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffPlane(short mc, int nx, int ny, int nz, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffCCWArc(short mc, int x, int y, int z, int cx, int cy, int cz, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffCWArc(short mc, int x, int y, int z, int cx, int cy, int cz, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffDwell(short mc, ushort tm, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffDWait(short mc, short node, short bit, short val, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffDOut(short mc, short node, short bit, short val, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffCrdMode(short mc, ushort mode, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffCrdOft(short mc, short idx, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffFeed(short mc, float f, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffSpindle(short mc, int s, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffTool(short mc, short t, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffMFn(short mc, short m, uint bid);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_BuffEnd(short mc, uint bid);

        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysTicks(short mc, ref uint ticks);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysPeriod(short mc, ref uint tm);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysAxisMax(short mc, ref short num);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysChMax(short mc, ref short num);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysDIMax(short mc, ref short ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysDOMax(short mc, ref short ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysAIMax(short mc, ref short ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysAOMax(short mc, ref short ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysAIRsol(short mc, ref short bits);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysAORsol(short mc, ref short bits);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysPIMax(short mc, ref short ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysPOMax(short mc, ref short ch);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysModel(short mc, ref short model);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysFnType(short mc, ref short fn);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysVersion(short mc, ref ushort ver);
        [DllImport("sldm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short SLDM_GetSysIP(short mc, ref short ip4);
    }
}
