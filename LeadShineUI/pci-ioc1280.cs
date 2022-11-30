using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LeadShineUI
{
    public class pci_ioc1280
    {
        public static int ioc_board_init()
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_board_init();
            else return x86.ioc_board_init();
        }
        public static void ioc_board_close()
        {
            if (Basic.MySystem.IsSystem_x64()) x64.ioc_board_close();
            else x86.ioc_board_close();
        }
        public static int ioc_read_inbit(ushort cardno, ushort bitno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_read_inbit(cardno, bitno);
            else return x86.ioc_read_inbit(cardno, bitno);
        }
        public static int ioc_read_outbit(ushort cardno, ushort bitno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_read_outbit(cardno, bitno);
            else return x86.ioc_read_outbit(cardno, bitno);
        }
        public static uint ioc_write_outbit(ushort cardno, ushort bitno, int on_off)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_write_outbit(cardno, bitno, on_off);
            else return x86.ioc_write_outbit(cardno, bitno, on_off);
        }
        public static uint ioc_reverse_outbit(ushort cardno, ushort bitno, double ms_time)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_reverse_outbit(cardno, bitno, ms_time);
            else return x86.ioc_reverse_outbit(cardno, bitno, ms_time);
        }
        public static int ioc_read_inport(ushort cardno, ushort m_PortNo)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_read_inport(cardno, m_PortNo);
            else return x86.ioc_read_inport(cardno, m_PortNo);
        }
        public static int ioc_read_outport(ushort cardno, ushort m_PortNo)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_read_outport(cardno, m_PortNo);
            else return x86.ioc_read_outport(cardno, m_PortNo);
        }
        public static uint ioc_write_outport(ushort cardno, ushort m_PortNo, uint port_value)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_write_outport(cardno, m_PortNo, port_value);
            else return x86.ioc_write_outport(cardno, m_PortNo, port_value);
        }
        public static uint ioc_config_intbitmode(ushort cardno, ushort bitno, ushort enable, ushort logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_config_intbitmode(cardno, bitno, enable, logic);
            else return x86.ioc_config_intbitmode(cardno, bitno, enable, logic);
        }
        public static uint ioc_config_intbitmode(ushort cardno, ushort bitno, ushort[] enable, ushort[] logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_config_intbitmode(cardno, bitno, enable, logic);
            else return x86.ioc_config_intbitmode(cardno, bitno, enable, logic);
        }
        public static int ioc_read_intbitstatus(ushort cardno, ushort m_PortNo)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_read_intbitstatus(cardno, m_PortNo);
            else return x86.ioc_read_intbitstatus(cardno, m_PortNo);
        }
        public static uint ioc_config_intporten(ushort cardno, ushort m_PortNo, uint port_en)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_config_intporten(cardno, m_PortNo, port_en);
            else return x86.ioc_config_intporten(cardno, m_PortNo, port_en);
        }
        public static uint ioc_config_intportlogic(ushort cardno, ushort m_PortNo, uint port_logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_config_intportlogic(cardno, m_PortNo, port_logic);
            else return x86.ioc_config_intportlogic(cardno, m_PortNo, port_logic);
        }
        public static uint ioc_read_intportmode(ushort cardno, ushort m_PortNo, uint[] enable, uint[] logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_read_intportmode(cardno, m_PortNo, enable, logic);
            else return x86.ioc_read_intportmode(cardno, m_PortNo, enable, logic);
        }
        public static int ioc_read_intportstatus(ushort cardno, ushort m_PortNo)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_read_intportstatus(cardno, m_PortNo);
            else return x86.ioc_read_intportstatus(cardno, m_PortNo);
        }
        public static uint ioc_set_filter(ushort cardno, double filter)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.ioc_set_filter(cardno, filter);
            else return x86.ioc_set_filter(cardno, filter);
        }
        private class x64
        {
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_board_init();
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern void ioc_board_close();

            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_read_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_inbit(ushort cardno, ushort bitno);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_read_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_outbit(ushort cardno, ushort bitno);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_write_outbit(ushort cardno, ushort bitno, int on_off);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_reverse_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_reverse_outbit(ushort cardno, ushort bitno, double ms_time);

            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_read_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_inport(ushort cardno, ushort m_PortNo);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_read_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_outport(ushort cardno, ushort m_PortNo);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_write_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_write_outport(ushort cardno, ushort m_PortNo, uint port_value);

            public delegate uint IOC0640_OPERATE(IntPtr operate_data);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_int_enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_int_enable(ushort cardno, IOC0640_OPERATE funcIntHandler, IntPtr operate_data);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_int_disable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_int_disable(ushort cardno);

            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_config_intbitmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_config_intbitmode(ushort cardno, ushort bitno, ushort enable, ushort logic);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_config_intbitmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_config_intbitmode(ushort cardno, ushort bitno, ushort[] enable, ushort[] logic);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_read_intbitstatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_intbitstatus(ushort cardno, ushort bitno);

            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_config_intporten", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_config_intporten(ushort cardno, ushort m_PortNo, uint port_en);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_config_intportlogic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_config_intportlogic(ushort cardno, ushort m_PortNo, uint port_logic);

            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_read_intportmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_read_intportmode(ushort cardno, ushort m_PortNo, uint[] enable, uint[] logic);
            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_read_intportstatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_intportstatus(ushort cardno, ushort m_PortNo);

            [DllImport(@"IOC1280\x64\IOC0640.dll", EntryPoint = "ioc_set_filter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_set_filter(ushort cardno, double filter);
        }
        private class x86
        {
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_board_init();
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern void ioc_board_close();

            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_read_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_inbit(ushort cardno, ushort bitno);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_read_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_outbit(ushort cardno, ushort bitno);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_write_outbit(ushort cardno, ushort bitno, int on_off);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_reverse_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_reverse_outbit(ushort cardno, ushort bitno, double ms_time);

            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_read_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_inport(ushort cardno, ushort m_PortNo);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_read_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_outport(ushort cardno, ushort m_PortNo);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_write_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_write_outport(ushort cardno, ushort m_PortNo, uint port_value);

            public delegate uint IOC0640_OPERATE(IntPtr operate_data);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_int_enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_int_enable(ushort cardno, IOC0640_OPERATE funcIntHandler, IntPtr operate_data);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_int_disable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_int_disable(ushort cardno);

            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_config_intbitmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_config_intbitmode(ushort cardno, ushort bitno, ushort enable, ushort logic);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_config_intbitmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_config_intbitmode(ushort cardno, ushort bitno, ushort[] enable, ushort[] logic);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_read_intbitstatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_intbitstatus(ushort cardno, ushort bitno);

            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_config_intporten", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_config_intporten(ushort cardno, ushort m_PortNo, uint port_en);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_config_intportlogic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_config_intportlogic(ushort cardno, ushort m_PortNo, uint port_logic);

            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_read_intportmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_read_intportmode(ushort cardno, ushort m_PortNo, uint[] enable, uint[] logic);
            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_read_intportstatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int ioc_read_intportstatus(ushort cardno, ushort m_PortNo);

            [DllImport(@"IOC1280\x86\IOC0640.dll", EntryPoint = "ioc_set_filter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern uint ioc_set_filter(ushort cardno, double filter);
        }
    }
}
