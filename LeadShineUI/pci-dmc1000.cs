using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LeadShineUI
{
    public class Dmc1000
    {
        public static int d1000_board_init()
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_board_init();
            else return x86.d1000_board_init();
        }
        public static int d1000_board_close()
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_board_close();
            else return x86.d1000_board_close();
        }
        public static int d1000_set_pls_outmode(int axis, int pls_outmode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_set_pls_outmode(axis, pls_outmode);
            else return x86.d1000_set_pls_outmode(axis, pls_outmode);
        }
        public static int d1000_get_speed(int axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_get_speed(axis);
            else return x86.d1000_get_speed(axis);
        }
        public static int d1000_change_speed(int axis, int NewVel)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_change_speed(axis, NewVel);
            else return x86.d1000_change_speed(axis, NewVel);
        }
        public static int d1000_decel_stop(int axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_decel_stop(axis);
            else return x86.d1000_decel_stop(axis);
        }
        public static int d1000_immediate_stop(int axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_immediate_stop(axis);
            else return x86.d1000_immediate_stop(axis);
        }
        public static int d1000_start_t_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_start_t_move(axis, Dist, StrVel, MaxVel, Tacc);
            else return x86.d1000_start_t_move(axis, Dist, StrVel, MaxVel, Tacc);
        }
        public static int d1000_start_ta_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_start_ta_move(axis, Dist, StrVel, MaxVel, Tacc);
            else return x86.d1000_start_ta_move(axis, Dist, StrVel, MaxVel, Tacc);
        }
        public static int d1000_start_s_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_start_s_move(axis, Dist, StrVel, MaxVel, Tacc);
            else return x86.d1000_start_s_move(axis, Dist, StrVel, MaxVel, Tacc);
        }
        public static int d1000_start_sa_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_start_sa_move(axis, Dist, StrVel, MaxVel, Tacc);
            else return x86.d1000_start_sa_move(axis, Dist, StrVel, MaxVel, Tacc);
        }
        public static int d1000_start_tv_move(int axis, int StrVel, int MaxVel, double Tacc)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_start_tv_move(axis, StrVel, MaxVel, Tacc);
            else return x86.d1000_start_tv_move(axis, StrVel, MaxVel, Tacc);
        }
        public static int d1000_start_sv_move(int axis, int StrVel, int MaxVel, double Tacc)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_start_sv_move(axis, StrVel, MaxVel, Tacc);
            else return x86.d1000_start_sv_move(axis, StrVel, MaxVel, Tacc);
        }
        public static int d1000_set_s_profile(int axis, double s_para)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_set_s_profile(axis, s_para);
            else return x86.d1000_set_s_profile(axis, s_para);
        }
        public static int d1000_get_s_profile(int axis, ref double s_para)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_get_s_profile(axis, ref s_para);
            else return x86.d1000_get_s_profile(axis, ref s_para);
        }
        public static int d1000_start_t_line(int TotalAxis, UInt16[] AxisArray, int[] DistArray, int StrVel, int MaxVel, double Tacc)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_start_t_line(TotalAxis, AxisArray, DistArray, StrVel, MaxVel, Tacc);
            else return x86.d1000_start_t_line(TotalAxis, AxisArray, DistArray, StrVel, MaxVel, Tacc);
        }
        public static int d1000_start_ta_line(int TotalAxis, UInt16[] AxisArray, int[] DistArray, int StrVel, int MaxVel, double Tacc)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_start_ta_line(TotalAxis, AxisArray, DistArray, StrVel, MaxVel, Tacc);
            else return x86.d1000_start_ta_line(TotalAxis, AxisArray, DistArray, StrVel, MaxVel, Tacc);
        }
        public static int d1000_check_done(int axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_check_done(axis);
            else return x86.d1000_check_done(axis);
        }
        public static int d1000_get_command_pos(int axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_get_command_pos(axis);
            else return x86.d1000_get_command_pos(axis);
        }
        public static int d1000_set_command_pos(int axis, double Pos)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_set_command_pos(axis, Pos);
            else return x86.d1000_set_command_pos(axis, Pos);
        }
        public static int d1000_out_bit(int BitNo, int BitData)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_out_bit(BitNo, BitData);
            else return x86.d1000_out_bit(BitNo, BitData);
        }
        public static int d1000_in_bit(int BitNo)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_in_bit(BitNo);
            else return x86.d1000_in_bit(BitNo);
        }
        public static int d1000_get_outbit(int BitNo)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_get_outbit(BitNo);
            else return x86.d1000_get_outbit(BitNo);
        }
        public static int d1000_in_enable(int CardNo, int InputEn)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_in_enable(CardNo, InputEn);
            else return x86.d1000_in_enable(CardNo, InputEn);
        }
        public static int d1000_set_sd(int axis, int SdMode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_set_sd(axis, SdMode);
            else return x86.d1000_set_sd(axis, SdMode);
        }
        public static int d1000_get_axis_status(int axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_get_axis_status(axis);
            else return x86.d1000_get_axis_status(axis);
        }
        public static int d1000_WriteDWord(int addr, int data)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_WriteDWord(addr, data);
            else return x86.d1000_WriteDWord(addr, data);
        }
        public static int d1000_ReadDWord(int addr)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d1000_ReadDWord(addr);
            else return x86.d1000_ReadDWord(addr);
        }

        private class x64
        {
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_board_init();
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_board_close();

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_set_pls_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_set_pls_outmode(int axis, int pls_outmode);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_get_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_get_speed(int axis);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_change_speed(int axis, int NewVel);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_decel_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_decel_stop(int axis);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_immediate_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_immediate_stop(int axis);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_start_t_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_t_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_start_ta_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_ta_move(int axis, int Pos, double StrVel, double MaxVel, double Tacc);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_start_s_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_s_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_start_sa_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_sa_move(int axis, int Pos, int StrVel, int MaxVel, double Tacc);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_start_tv_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_tv_move(int axis, int StrVel, int MaxVel, double Tacc);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_start_sv_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_sv_move(int axis, int StrVel, int MaxVel, double Tacc);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_set_s_profile(int axis, double s_para);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_get_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_get_s_profile(int axis, ref double s_para);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_start_t_line", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_t_line(int TotalAxis, UInt16[] AxisArray, int[] DistArray, int StrVel, int MaxVel, double Tacc);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_start_ta_line", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_ta_line(int TotalAxis, UInt16[] AxisArray, int[] DistArray, int StrVel, int MaxVel, double Tacc);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_home_move(int axis, int StrVel, int MaxVel, double Tacc);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_check_done(int axis);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_get_command_pos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_get_command_pos(int axis);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_set_command_pos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_set_command_pos(int axis, double Pos);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_out_bit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_out_bit(int BitNo, int BitData);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_in_bit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_in_bit(int BitNo);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_get_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_get_outbit(int BitNo);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_in_enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_in_enable(int CardNo, int InputEn);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_set_sd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_set_sd(int axis, int SdMode);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_get_axis_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_get_axis_status(int axis);

            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_WriteDWord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_WriteDWord(int addr, int data);
            [DllImport(@"DMC1000B\x64\Dmc1000.dll", EntryPoint = "d1000_ReadDWord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_ReadDWord(int addr);
        }
        private class x86
        {
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_board_init();
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_board_close();

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_set_pls_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_set_pls_outmode(int axis, int pls_outmode);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_get_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_get_speed(int axis);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_change_speed(int axis, int NewVel);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_decel_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_decel_stop(int axis);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_immediate_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_immediate_stop(int axis);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_start_t_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_t_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_start_ta_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_ta_move(int axis, int Pos, double StrVel, double MaxVel, double Tacc);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_start_s_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_s_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_start_sa_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_sa_move(int axis, int Pos, int StrVel, int MaxVel, double Tacc);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_start_tv_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_tv_move(int axis, int StrVel, int MaxVel, double Tacc);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_start_sv_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_sv_move(int axis, int StrVel, int MaxVel, double Tacc);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_set_s_profile(int axis, double s_para);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_get_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_get_s_profile(int axis, ref double s_para);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_start_t_line", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_t_line(int TotalAxis, UInt16[] AxisArray, int[] DistArray, int StrVel, int MaxVel, double Tacc);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_start_ta_line", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_start_ta_line(int TotalAxis, UInt16[] AxisArray, int[] DistArray, int StrVel, int MaxVel, double Tacc);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_home_move(int axis, int StrVel, int MaxVel, double Tacc);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_check_done(int axis);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_get_command_pos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_get_command_pos(int axis);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_set_command_pos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_set_command_pos(int axis, double Pos);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_out_bit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_out_bit(int BitNo, int BitData);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_in_bit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_in_bit(int BitNo);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_get_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_get_outbit(int BitNo);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_in_enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_in_enable(int CardNo, int InputEn);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_set_sd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_set_sd(int axis, int SdMode);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_get_axis_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_get_axis_status(int axis);

            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_WriteDWord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_WriteDWord(int addr, int data);
            [DllImport(@"DMC1000B\x86\Dmc1000.dll", EntryPoint = "d1000_ReadDWord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d1000_ReadDWord(int addr);
        }
    }
}
