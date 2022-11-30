///
///	Description:
///			C# class for PCI-9016
///	Author:
///			yuanxiaowei
///	History:
///			2012-9-12  Create, part of APIs implemented
///			2014-4-11  update, all APIs implemented
///
using System;
using System.Runtime.InteropServices;

namespace Pci9016
{
    public class CPci9016
    {
        //open/close
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_initial(ref int pCardCount, int[] pBoardId);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_close();

        //pulse input/output configuration
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_set_pls_outmode(int axis, int pls_outmode);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_set_pls_iptmode(int axis, int pls_iptmode);

        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_set_t_profile(int axis, double start_vel, double max_vel, double acc, double dec);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_set_s_profile(int axis, double start_vel, double max_vel, double acc, double dec, double jerk_percent);

        //single axis driving functions
        //dist_mode:  0  incremental coordinate; 1 - absolute coordinate
        //vel_mode:   0  low speed mode without acc/dec;
        //			  1 high speed mode without acc/dec;
        //			  2 high speed mode with acc/dec
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_pmove(int axis, int dist, int dist_mode, int vel_mode);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_vmove(int axis, int plus_dir, int vel_mode);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_stop(int axis, int emg_stop);

        //home return
        //mode:		0	- low speed, ORG only;
        //			2   - low speed,  ORG + EZ;
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_home_config(int axis, int mode, int org_level, int ez_level);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_home_move(int axis, int plus_dir);

        //position counter control
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_set_pos(int axis, int cntr_no, int pos);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_get_pos(int axis, int cntr_no, ref int pPos);

        //I/O control
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_set_do(int card_no, uint data);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_set_do_bit(int card_no, uint bit_no, uint data);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_get_do(int card_no, ref uint pData);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_get_di(int card_no, ref uint pData);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_get_di_bit(int card_no, uint bit_no, ref uint pData);

        //axis switch input
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_get_io_status(int axis, ref uint pStatus);

        //axis's motion status
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_get_motion_status(int axis, ref uint pStatus);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_get_current_speed(int axis, ref double pSpeed);

        //version information
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_get_version(int axis, ref uint pApi_ver, ref uint pDriver_ver, ref uint pLogic_ver);
        //[DllImport(@"\Pci9016\Pci9016.dll")] public static extern int p9016_get_revision(int card_no, int *pLogic_revision);

        //set End Limit Input active level
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_set_el_level(int axis, int active_level);

        //set stop mode(decelerate to stop, suddenly stop) on error
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_set_error_stop_mode(int axis, int stop_mode);

        //config alarm input
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_set_alarm(int axis, int enable, int active_level);

        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_outpd(int card_no, uint offset, uint data);
        [DllImport(@"\Pci9016\Pci9016.dll")]
        public static extern int p9016_inpd(int card_no, uint offset, ref uint pData);



        public static uint MAX_DEVICE_COUNT = 16;
        public static uint POS_CMP_INDEX_MAX = 2;   /*position comparator count*/
        public static uint LOGIC_VER = 0x010b;

        public static uint[] posCmpBase = { 0xc0, 0xd0 };

        public static int RC_SUCCESS = 0;
        public static int RC_NO_ERROR = 0;

        //general error code
        public static int RC_INVALID_PARAM = 1;	//invalid parameter
        public static int RC_ALLOCATE_MEMORY_FAIL = 2;	//allocate memory fail
        public static int RC_QUE_FULL = 3;
        public static int RC_QUE_EMPTY = 4;
        public static int RC_DEVICE_DETACH = 5;	//device detached, cannot detect device
        public static int RC_ERROR = 6;	//unkown error
        public static int RC_COMM_FAIL = 7;	//
        public static int RC_MEMORY_TOOL_SMALL = 8;	//
        public static int RC_INVALID_DRIVER = 9;
        public static int RC_INSUFFICIENT_RESOURCE = 10;	//insufficient system resource, create system object fail
        public static int RC_INVALID_HARDWARE = 11;
        public static int RC_DEVICE_ALREADY_OPEN = 12;

        //peripheral error
        public static int RC_PCI_ACCESS_ERROR = (100 + 0);
        public static int RC_PCI_CONFIG_ERROR = (100 + 1);
        public static int RC_PCI_ACCESS_TIMEOUT = (100 + 3);

        //MOTION control error
        public static int RC_MOV_ERR = (200 + 1);		//unknow motion error
        public static int RC_AXIS_BUSY = (200 + 2);	//the axis is busy
        public static int RC_INTERP_BUSY = (200 + 3);		//the axis is busy interplating
        public static int RC_FAIL_SEARCH_HOME = (200 + 4);		//fail to search home
        public static int RC_EL_ACTIVE = (200 + 5);	//limit switch is active while driving
        public static int RC_CI_MODE_DISABLED = (200 + 6);		//continuous interpolation mode is disable
        public static int RC_CI_MODE_ENABLED = (200 + 7);		//continuous interplation mode is enabled
        public static int RC_DIST_LARGE = (200 + 8);		//distance too large for positioning drive or linear interpolation
        public static int RC_ARC_LEN_LARGE = (200 + 9);		//arc length too large for circular interpolation
        public static int RC_DIST_SMALL = (200 + 10);

        public static int cmp_enable(int card_no, int index, int enable, int active_level, int output_sel, int length)
        {
            uint reg = 0;
            int rc = 0;
            uint out_len = 150000 / 30;
            uint nDestValue;

            if ((card_no >= MAX_DEVICE_COUNT))
                return RC_INVALID_PARAM;

            if (index < 0 || index >= POS_CMP_INDEX_MAX)
                return RC_INVALID_PARAM;

            //we should check logic version first
            reg = 0;
            rc = p9016_inpd(card_no, 0xa8, ref reg);
            if ((reg & 0xffff) < LOGIC_VER)
                return RC_INVALID_PARAM;

            rc = p9016_inpd(card_no, posCmpBase[index], ref reg);

            if (enable != 0)
            {
                nDestValue = 0x1;
                reg |= nDestValue;
            }
            else
            {
                nDestValue = 0x1;
                reg &= ~nDestValue;
            }

            //2018/4/23 fix bug
            if (active_level != 0)
            {
                nDestValue = 0x2;
                reg &= ~nDestValue;
            }
            else
            {
                nDestValue = 0x2;
                reg |= nDestValue;
            }

            //always use feedback position
            //if(ref_source)
            //    reg |= 0x4;
            //else
            //    reg &= ~0x4;

            //2019/1/21
            //feedback position source
            nDestValue = (uint)((index) << 2);
            reg |= nDestValue;

            nDestValue = (uint)(0xf << 8);
            reg &= ~(nDestValue);
            reg |= (((uint)output_sel & 0xf) << 8);

            out_len = (uint)length;
            if (out_len > 65535 * 30)
                out_len = 65535 * 30;
            if (out_len < 1000)
                out_len = 1000;

            out_len = out_len / 30;
            reg &= 0xffff;
            reg |= ((out_len & 0xffff) << 16);

            rc += p9016_outpd(card_no, posCmpBase[index], reg);

            return rc;
        }

        public static int cmp_get_fifoStatus(int card_no, int index, ref uint stat)
        {
            uint reg = 0;
            int rc = 0;
            uint fifo_wcnt = 0;

            if ((card_no >= MAX_DEVICE_COUNT))
                return RC_INVALID_PARAM;

            if (index < 0 || index >= POS_CMP_INDEX_MAX)
                return RC_INVALID_PARAM;

            rc = p9016_inpd(card_no, posCmpBase[index], ref reg);
            rc += p9016_inpd(card_no, posCmpBase[index] + 0x8, ref fifo_wcnt);


            stat = (uint)(((reg >> 4) & 0xf) | (fifo_wcnt & ~0xffff));
            return rc;
        }

        public static int cmp_add_ref(int card_no, int index, int PosRef)
        {
            int rc = 0;

            if ((card_no >= MAX_DEVICE_COUNT))
                return RC_INVALID_PARAM;

            if (index < 0 || index >= POS_CMP_INDEX_MAX)
                return RC_INVALID_PARAM;

            rc = p9016_outpd(card_no, posCmpBase[index] + 0x4, (uint)PosRef);

            return rc;
        }

        public static int cmp_get_curRef(int card_no, int index, ref int PosRef)
        {
            uint reg = 0;
            int rc = 0;

            if ((card_no >= MAX_DEVICE_COUNT))
                return RC_INVALID_PARAM;

            if (index < 0 || index >= POS_CMP_INDEX_MAX)
                return RC_INVALID_PARAM;

            rc = p9016_inpd(card_no, posCmpBase[index] + 0x4, ref reg);

            PosRef = (int)reg;

            return rc;
        }

        public static int cmp_clr_fifo(int card_no, int index)
        {
            uint reg = 0;
            int rc = 0, i;

            if ((card_no >= MAX_DEVICE_COUNT))
                return RC_INVALID_PARAM;

            if (index < 0 || index >= POS_CMP_INDEX_MAX)
                return RC_INVALID_PARAM;

            //get count of items in FIFO
            reg = 512 << 16;
            rc = p9016_inpd(card_no, posCmpBase[index] + 0x8, ref reg);
            reg = reg >> 16;

            if (reg > 512) reg = 512;
            for (i = 0; i < 512; i++)
                p9016_outpd(card_no, posCmpBase[index] + 0x8, 0x2);//read fifo

            return rc;
        }

        public static int cmp_get_matchCount(int card_no, int index, ref uint Count)
        {
            uint reg = 0;
            int rc = 0;

            if ((card_no >= MAX_DEVICE_COUNT))
                return RC_INVALID_PARAM;

            if (index < 0 || index >= POS_CMP_INDEX_MAX)
                return RC_INVALID_PARAM;

            rc = p9016_inpd(card_no, posCmpBase[index] + 0x8, ref reg);

            Count = reg & 0xffff;

            return rc;
        }
        public static int cmp_clr_matchCount(int card_no, int index)
        {
            int rc = 0;

            if ((card_no >= MAX_DEVICE_COUNT))
                return RC_INVALID_PARAM;

            if (index < 0 || index >= POS_CMP_INDEX_MAX)
                return RC_INVALID_PARAM;

            rc = p9016_outpd(card_no, posCmpBase[index] + 0x8, 0x1);//clear match counter

            return rc;
        }

    }
}
