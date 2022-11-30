using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LeadShineUI
{
    public class Dmc2C80
    {
        public static int d2c80_board_init()
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_board_init();
            else return x86.d2c80_board_init();
        }
        public static void d2c80_board_close()
        {
            if (Basic.MySystem.IsSystem_x64()) x64.d2c80_board_close();
            else x86.d2c80_board_close();
        }
        public static UInt32 d2c80_get_card_version(ushort card_num)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_card_version(card_num);
            else return x86.d2c80_get_card_version(card_num);
        }
        public static UInt32 d2c80_get_card_soft_version(UInt16 card, ref UInt16 firm_id, ref UInt32 sub_firm_id)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_card_soft_version(card, ref firm_id, ref sub_firm_id);
            else return x86.d2c80_get_card_soft_version(card, ref firm_id, ref sub_firm_id);
        }
        public static UInt32 d2c80_get_client_ID(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_client_ID(card);
            else return x86.d2c80_get_client_ID(card);
        }
        public static UInt32 d2c80_get_lib_version()
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_lib_version();
            else return x86.d2c80_get_lib_version();
        }
        public static UInt32 d2c80_get_card_ID(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_card_ID(card);
            else return x86.d2c80_get_card_ID(card);
        }
        public static UInt32 d2c80_get_total_axes(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_total_axes(card);
            else return x86.d2c80_get_total_axes(card);
        }
        public static UInt32 d2c80_test_software(UInt16 card, UInt16 testid, UInt16 para1, UInt16 para2, UInt16 para3)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_test_software(card, testid, para1, para2, para3);
            else return x86.d2c80_test_software(card, testid, para1, para2, para3);
        }
        public static UInt32 d2c80_test_hardware(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_test_hardware(card);
            else return x86.d2c80_test_hardware(card);
        }
        public static UInt32 d2c80_board_rest(ushort card_num)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_board_reset(card_num);
            else return x86.d2c80_board_reset(card_num);
        }
        public static UInt32 d2c80_set_pulse_outmode(UInt16 axis, UInt16 outmode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_set_pulse_outmode(axis, outmode);
            else return x86.d2c80_set_pulse_outmode(axis, outmode);
        }
        public static UInt32 d2c80_download_firmware(UInt16 card, ref char pfilename)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_download_firmware(card, ref pfilename);
            else return x86.d2c80_download_firmware(card, ref pfilename);
        }
        public static UInt32 d2c80_counter_config(UInt16 axis, UInt16 mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_counter_config(axis, mode);
            else return x86.d2c80_counter_config(axis, mode);
        }
        public static UInt32 d2c80_get_pulse_outmode(UInt16 axis, ref UInt16 outmode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_pulse_outmode(axis, ref outmode);
            else return x86.d2c80_get_pulse_outmode(axis, ref outmode);
        }
        public static UInt32 d2c80_get_counter_config(UInt16 axis, ref UInt16 mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_counter_config(axis, ref mode);
            else return x86.d2c80_get_counter_config(axis, ref mode);
        }
        public static UInt32 d2c80_get_config_INP_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 inp_logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_INP_PIN(axis, ref enable, ref inp_logic);
            else return x86.d2c80_get_config_INP_PIN(axis, ref enable, ref inp_logic);
        }
        public static UInt32 d2c80_get_config_ERC_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 erc_logic, ref UInt16 erc_width, ref UInt16 erc_off_time)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_ERC_PIN(axis, ref enable, ref erc_logic, ref erc_width, ref erc_off_time);
            else return x86.d2c80_get_config_ERC_PIN(axis, ref enable, ref erc_logic, ref erc_width, ref erc_off_time);
        }
        public static UInt32 d2c80_get_config_ALM_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 alm_logic, ref UInt16 alm_action)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_ALM_PIN(axis, ref enable, ref alm_logic, ref alm_action);
            else return x86.d2c80_get_config_ALM_PIN(axis, ref enable, ref alm_logic, ref alm_action);
        }
        public static UInt32 d2c80_get_config_EL_PIN(UInt16 axis, ref UInt16 el_logic, ref UInt16 el_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_EL_PIN(axis, ref el_logic, ref el_mode);
            else return x86.d2c80_get_config_EL_PIN(axis, ref el_logic, ref el_mode);
        }
        public static UInt32 d2c80_get_config_HOME_PIN_logic(UInt16 axis, ref UInt16 org_logic, ref UInt16 filter)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_HOME_PIN_logic(axis, ref org_logic, ref filter);
            else return x86.d2c80_get_config_HOME_PIN_logic(axis, ref org_logic, ref filter);
        }
        public static UInt32 d2c80_get_config_home_mode(UInt16 axis, ref UInt16 home_dir, ref double vel, ref UInt16 home_mode, ref UInt16 EZ_count)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_home_mode(axis, ref home_dir, ref vel, ref home_mode, ref EZ_count);
            else return x86.d2c80_get_config_home_mode(axis, ref home_dir, ref vel, ref home_mode, ref EZ_count);
        }
        public static UInt32 d2c80_get_handwheel_inmode(UInt16 axis, ref UInt16 inmode, ref double multi)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_handwheel_inmode(axis, ref inmode, ref multi);
            else return x86.d2c80_get_handwheel_inmode(axis, ref inmode, ref multi);
        }
        public static UInt32 d2c80_get_config_LTC_PIN(UInt16 axis, ref UInt16 ltc_logic, ref UInt16 ltc_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_LTC_PIN(axis, ref ltc_logic, ref ltc_mode);
            else return x86.d2c80_get_config_LTC_PIN(axis, ref ltc_logic, ref ltc_mode);
        }
        public static UInt32 d2c80_config_INP_PIN(UInt16 axis, UInt16 enable, UInt16 inp_logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_INP_PIN(axis, enable, inp_logic);
            else return x86.d2c80_config_INP_PIN(axis, enable, inp_logic);
        }
        public static UInt32 d2c80_config_ERC_PIN(UInt16 axis, UInt16 enable, UInt16 erc_logic, UInt16 erc_width, UInt16 erc_off_time)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_ERC_PIN(axis, enable, erc_logic, erc_width, erc_off_time);
            else return x86.d2c80_config_ERC_PIN(axis, enable, erc_logic, erc_width, erc_off_time);
        }
        public static UInt32 d2c80_config_EMG_PIN(UInt16 cardno, UInt16 option, UInt16 emg_logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_EMG_PIN(cardno, option, emg_logic);
            else return x86.d2c80_config_EMG_PIN(cardno, option, emg_logic);
        }
        public static UInt32 d2c80_get_config_EMG_PIN(UInt16 cardno, ref UInt16 enbale, ref UInt16 emg_logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_EMG_PIN(cardno, ref enbale, ref emg_logic);
            else return x86.d2c80_get_config_EMG_PIN(cardno, ref enbale, ref emg_logic);
        }
        public static UInt32 d2c80_config_ALM_PIN(UInt16 axis, UInt16 enable, UInt16 alm_logic, UInt16 alm_action)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_ALM_PIN(axis, enable, alm_logic, alm_action);
            else return x86.d2c80_config_ALM_PIN(axis, enable, alm_logic, alm_action);
        }
        public static UInt32 d2c80_config_EL_PIN(UInt16 axis, UInt16 el_logic, UInt16 el_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_EL_PIN(axis, el_logic, el_mode);
            else return x86.d2c80_config_EL_PIN(axis, el_logic, el_mode);
        }
        public static UInt32 d2c80_config_HOME_PIN_logic(UInt16 axis, UInt16 org_logic, UInt16 filter)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_HOME_PIN_logic(axis, org_logic, filter);
            else return x86.d2c80_config_HOME_PIN_logic(axis, org_logic, filter);
        }
        public static UInt32 d2c80_write_ERC_PIN(UInt16 axis, UInt16 sel)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_write_ERC_PIN(axis, sel);
            else return x86.d2c80_write_ERC_PIN(axis, sel);
        }
        public static UInt32 d2c80_set_backlash(UInt16 axis, Int32 backlash)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_set_backlash(axis, backlash);
            else return x86.d2c80_set_backlash(axis, backlash);
        }
        public static UInt32 d2c80_get_backlash(UInt16 axis, ref Int32 backlash)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_backlash(axis, ref backlash);
            else return x86.d2c80_get_backlash(axis, ref backlash);
        }
        public static Int32 d2c80_read_inbit(UInt16 cardno, UInt16 bitno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_read_inbit(cardno, bitno);
            else return x86.d2c80_read_inbit(cardno, bitno);
        }
        public static UInt32 d2c80_write_outbit(UInt16 cardno, UInt16 bitno, UInt16 on_off)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_write_outbit(cardno, bitno, on_off);
            else return x86.d2c80_write_outbit(cardno, bitno, on_off);
        }
        public static Int32 d2c80_read_outbit(UInt16 cardno, UInt16 bitno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_read_outbit(cardno, bitno);
            else return x86.d2c80_read_outbit(cardno, bitno);
        }
        public static Int32 d2c80_read_inport(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_read_inport(cardno);
            else return x86.d2c80_read_inport(cardno);
        }
        public static Int32 d2c80_read_outport(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_read_outport(cardno);
            else return x86.d2c80_read_outport(cardno);
        }
        public static UInt32 d2c80_write_outport(UInt16 cardno, UInt32 port_value)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_write_outport(cardno, port_value);
            else return x86.d2c80_write_outport(cardno, port_value);
        }
        public static UInt32 d2c80_decel_stop(UInt16 axis, double dec)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_decel_stop(axis, dec);
            else return x86.d2c80_decel_stop(axis, dec);
        }
        public static UInt32 d2c80_imd_stop(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_imd_stop(axis);
            else return x86.d2c80_imd_stop(axis);
        }
        public static UInt32 d2c80_imd_stop()
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_emg_stop();
            else return x86.d2c80_emg_stop();
        }
        public static UInt32 d2c80_simultaneous_stop(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_simultaneous_stop(axis);
            else return x86.d2c80_simultaneous_stop(axis);
        }
        public static Int32 d2c80_get_position(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_position(axis);
            else return x86.d2c80_get_position(axis);
        }
        public static UInt32 d2c80_set_position(UInt16 axis, Int32 current_position)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_set_position(axis, current_position);
            else return x86.d2c80_set_position(axis, current_position);
        }
        public static Int32 d2c80_check_done(UInt16 axis)   
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_check_done(axis);
            else return x86.d2c80_check_done(axis);
        }
        public static UInt32 d2c80_axis_io_status(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_axis_io_status(axis);
            else return x86.d2c80_axis_io_status(axis);
        }
        public static double d2c80_read_current_speed(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_read_current_speed(axis);
            else return x86.d2c80_read_current_speed(axis);
        }
        public static double d2c80_read_vector_speed(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_read_vector_speed(card);
            else return x86.d2c80_read_vector_speed(card);
        }
        public static UInt32 d2c80_change_speed(UInt16 axis, double Curr_Vel)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_change_speed(axis, Curr_Vel);
            else return x86.d2c80_change_speed(axis, Curr_Vel);
        }
        public static UInt32 d2c80_set_vector_profile(UInt16 cardno, double s_para, double Max_Vel, double acc, double dec)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_set_vector_profile(cardno, s_para, Max_Vel, acc, dec);
            else return x86.d2c80_set_vector_profile(cardno, s_para, Max_Vel, acc, dec);
        }
        public static UInt32 d2c80_get_vector_profile(UInt16 cardno, ref double s_para, ref double Max_Vel, ref double acc, ref double dec)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_vector_profile(cardno, ref s_para, ref Max_Vel, ref acc, ref dec);
            else return x86.d2c80_get_vector_profile(cardno, ref s_para, ref Max_Vel, ref acc, ref dec);
        }
        public static UInt32 d2c80_set_profile(UInt16 axis, double option, double Max_Vel, double acc, double dec)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_set_profile(axis, option, Max_Vel, acc, dec);
            else return x86.d2c80_set_profile(axis, option, Max_Vel, acc, dec);
        }
        public static UInt32 d2c80_set_s_profile(UInt16 axis, double s_para)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_set_s_profile(axis, s_para);
            else return x86.d2c80_set_s_profile(axis, s_para);
        }
        public static UInt32 d2c80_get_profile(UInt16 axis, ref double option, ref double Max_Vel, ref double acc, ref double dec)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_profile(axis, ref option, ref Max_Vel, ref acc, ref dec);
            else return x86.d2c80_get_profile(axis, ref option, ref Max_Vel, ref acc, ref dec);
        }
        public static UInt32 d2c80_get_s_profile(UInt16 axis, ref double s_para)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_s_profile(axis, ref s_para);
            else return x86.d2c80_get_s_profile(axis, ref s_para);
        }
        public static UInt32 d2c80_reset_target_position(UInt16 axis, Int32 dist)  
        {
            if (Basic.MySystem.IsSystem_x64()) return x64. d2c80_reset_target_position(axis, dist);
            else return x86. d2c80_reset_target_position(axis, dist);
        }
        public static UInt32 d2c80_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_pmove(axis, Dist, posi_mode);
            else return x86.d2c80_pmove(axis, Dist, posi_mode);
        }
        public static UInt32 d2c80_vmove(UInt16 axis, UInt16 dir, double vel)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_vmove(axis, dir, vel);
            else return x86.d2c80_vmove(axis, dir, vel);
        }
        public static UInt32 d2c80_line2(UInt16 axis1, Int32 Dist1, UInt16 axis2, Int32 Dist2, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_line2(axis1, Dist1, axis2, Dist2, posi_mode);
            else return x86.d2c80_line2(axis1, Dist1, axis2, Dist2, posi_mode);
        }
        public static UInt32 d2c80_line3(ref UInt16 axis, Int32 Dist1, Int32 Dist2, Int32 Dist3, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_line3(ref axis, Dist1, Dist2, Dist3, posi_mode);
            else return x86.d2c80_line3(ref axis, Dist1, Dist2, Dist3, posi_mode);
        }
        public static UInt32 d2c80_line4(UInt16 cardno, Int32 Dist1, Int32 Dist2, Int32 Dist3, Int32 Dist4, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_line4(cardno, Dist1, Dist2, Dist3, Dist4, posi_mode);
            else return x86.d2c80_line4(cardno, Dist1, Dist2, Dist3, Dist4, posi_mode);
        }
        public static UInt32 d2c80_lineN(UInt16 axisNum, ref UInt16 piaxisList, ref Int32 pPosList, UInt16 posi_mode)  
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_lineN(axisNum, ref piaxisList, ref pPosList, posi_mode);
            else return x86.d2c80_lineN(axisNum, ref piaxisList, ref pPosList, posi_mode);
        }
        public static UInt32 d2c80_set_handwheel_inmode(UInt16 axis, UInt16 inmode, double multi)     
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_set_handwheel_inmode(axis, inmode, multi);
            else return x86.d2c80_set_handwheel_inmode(axis, inmode, multi);
        }
        public static UInt32 d2c80_handwheel_move(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_handwheel_move(axis);
            else return x86.d2c80_handwheel_move(axis);
        }
        public static UInt32 d2c80_config_home_mode(UInt16 axis, UInt16 home_dir, double vel, UInt16 mode, UInt16 EZ_count)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_home_mode(axis, home_dir, vel, mode, EZ_count);
            else return x86.d2c80_config_home_mode(axis, home_dir, vel, mode, EZ_count);
        }
        public static UInt32 d2c80_home_move(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_home_move(axis);
            else return x86.d2c80_home_move(axis);
        }
        public static UInt32 d2c80_arc_move(ref UInt16 axis, ref Int32 target_pos, ref Int32 cen_pos, UInt16 arc_dir)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_arc_move(ref axis, ref target_pos, ref cen_pos, arc_dir);
            else return x86.d2c80_arc_move(ref axis, ref target_pos, ref cen_pos, arc_dir);
        }
        public static UInt32 d2c80_rel_arc_move(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_rel_arc_move(ref axis, ref rel_pos, ref rel_cen, arc_dir);
            else return x86.d2c80_rel_arc_move(ref axis, ref rel_pos, ref rel_cen, arc_dir);
        }
        public static UInt32 d2c80_compare_config(UInt16 card, UInt16 enable, UInt16 axis, UInt16 cmp_source)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_compare_config(card, enable, axis, cmp_source);
            else return x86.d2c80_compare_config(card, enable, axis, cmp_source);
        }
        public static UInt32 d2c80_compare_get_config(UInt16 card, ref UInt16 enable, ref UInt16 axis, ref UInt16 cmp_source)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_compare_get_config(card, ref enable, ref axis, ref cmp_source);
            else return x86.d2c80_compare_get_config(card, ref enable, ref axis, ref cmp_source);
        }
        public static UInt32 d2c80_compare_clear_points(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_compare_clear_points(card);
            else return x86.d2c80_compare_clear_points(card);
        }
        public static UInt32 d2c80_compare_add_point(UInt16 card, Int32 pos, UInt16 dir, UInt16 action, Int32 actpara)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_compare_add_point(card, pos, dir, action, actpara);
            else return x86.d2c80_compare_add_point(card, pos, dir, action, actpara);
        }
        public static Int32 d2c80_compare_get_current_point(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_compare_get_current_point(card);
            else return x86.d2c80_compare_get_current_point(card);
        }
        public static Int32 d2c80_compare_get_points_runned(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_compare_get_points_runned(card);
            else return x86.d2c80_compare_get_points_runned(card);
        }
        public static Int32 d2c80_compare_get_points_remained(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_compare_get_points_remained(card);
            else return x86.d2c80_compare_get_points_remained(card);
        }
        public static Int32 d2c80_get_encoder(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_encoder(card);
            else return x86.d2c80_get_encoder(card);
        }
        public static UInt32 d2c80_set_encoder(UInt16 axis, Int32 encoder_value)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_set_encoder(axis, encoder_value);
            else return x86.d2c80_set_encoder(axis, encoder_value);
        }
        public static UInt32 d2c80_config_EZ_PIN(UInt16 axis, UInt16 ez_logic, UInt16 ez_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_EZ_PIN(axis, ez_logic, ez_mode);
            else return x86.d2c80_config_EZ_PIN(axis, ez_logic, ez_mode);
        }
        public static UInt32 d2c80_get_config_EZ_PIN(UInt16 axis, ref UInt16 ez_logic, ref UInt16 ez_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_EZ_PIN(axis, ref ez_logic, ref ez_mode);
            else return x86.d2c80_get_config_EZ_PIN(axis, ref ez_logic, ref ez_mode);
        }
        public static UInt32 d2c80_config_LTC_PIN(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_LTC_PIN(axis, ltc_logic, ltc_mode);
            else return x86.d2c80_config_LTC_PIN(axis, ltc_logic, ltc_mode);
        }
        public static UInt32 d2c80_config_latch_mode(UInt16 cardno, UInt16 all_enable)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_latch_mode(cardno, all_enable);
            else return x86.d2c80_config_latch_mode(cardno, all_enable);
        }
        public static Int32 d2c80_get_latch_value(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_latch_value(axis);
            else return x86.d2c80_get_latch_value(axis);
        }
        public static Int32 d2c80_get_latch_flag(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_latch_flag(cardno);
            else return x86.d2c80_get_latch_flag(cardno);
        }
        public static UInt32 d2c80_reset_latch_flag(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_reset_latch_flag(cardno);
            else return x86.d2c80_reset_latch_flag(cardno);
        }
        public static Int32 d2c80_get_counter_flag(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_counter_flag(cardno);
            else return x86.d2c80_get_counter_flag(cardno);
        }
        public static UInt32 d2c80_reset_counter_flag(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_reset_counter_flag(cardno);
            else return x86.d2c80_reset_counter_flag(cardno);
        }
        public static UInt32 d2c80_reset_clear_flag(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_reset_clear_flag(cardno);
            else return x86.d2c80_reset_clear_flag(cardno);
        }
        public static UInt32 d2c80_triger_chunnel(UInt16 cardno, UInt16 num)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_triger_chunnel(cardno, num);
            else return x86.d2c80_triger_chunnel(cardno, num);
        }
        public static UInt32 d2c80_set_speaker_logic(UInt16 cardno, UInt16 logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_set_speaker_logic(cardno, logic);
            else return x86.d2c80_set_speaker_logic(cardno, logic);
        }
        public static UInt32 d2c80_get_speaker_logic(UInt16 cardno, ref UInt16 logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_speaker_logic(cardno,ref logic);
            else return x86.d2c80_get_speaker_logic(cardno,ref logic);
        }
        public static UInt32 d2c80_get_config_latch_mode(UInt16 cardno, ref UInt16 all_enable)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_latch_mode(cardno, ref all_enable);
            else return x86.d2c80_get_config_latch_mode(cardno, ref all_enable);
        }
        public static UInt32 d2c80_config_softlimit(UInt16 axis, UInt16 ON_OFF, UInt16 source_sel, UInt16 SL_action, Int32 N_limit, Int32 P_limit)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_config_softlimit(axis, ON_OFF, source_sel, SL_action, N_limit, P_limit);
            else return x86.d2c80_config_softlimit(axis, ON_OFF, source_sel, SL_action, N_limit, P_limit);
        }
        public static UInt32 d2c80_get_config_softlimit(UInt16 axis, ref UInt16 ON_OFF, ref UInt16 source_sel, ref UInt16 SL_action, ref Int32 N_limit, ref Int32 P_limit)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_config_softlimit(axis, ref ON_OFF, ref source_sel, ref SL_action, ref N_limit, ref P_limit);
            else return x86.d2c80_get_config_softlimit(axis, ref ON_OFF, ref source_sel, ref SL_action, ref N_limit, ref P_limit);
        }
        public static UInt32 d2c80_conti_lines(UInt16 axisNum, ref UInt16 piaxisList, ref Int32 pPosList, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_lines(axisNum, ref piaxisList,ref pPosList, posi_mode);
            else return x86.d2c80_conti_lines(axisNum, ref piaxisList, ref pPosList, posi_mode);
        }
        public static UInt32 d2c80_conti_arc(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_arc(ref axis, ref rel_pos, ref rel_cen, arc_dir, posi_mode);
            else return x86.d2c80_conti_arc(ref axis, ref rel_pos, ref rel_cen, arc_dir, posi_mode);
        }
        public static UInt32 d2c80_conti_restrain_speed(UInt16 card, double v)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_restrain_speed(card, v);
            else return x86.d2c80_conti_restrain_speed(card, v);
        }
        public static UInt32 d2c80_conti_change_speed_ratio(UInt16 card, double percent)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_change_speed_ratio(card, percent);
            else return x86.d2c80_conti_change_speed_ratio(card, percent);
        }
        public static double d2c80_conti_get_current_speed_ratio(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_get_current_speed_ratio(card);
            else return x86.d2c80_conti_get_current_speed_ratio(card);
        }
        public static UInt32 d2c80_conti_set_mode(UInt16 card, UInt16 conti_mode, double conti_vl, double conti_para, double filter)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_set_mode(card, conti_mode, conti_vl, conti_para, filter);
            else return x86.d2c80_conti_set_mode(card, conti_mode, conti_vl, conti_para, filter);
        }
        public static UInt32 d2c80_conti_get_mode(UInt16 card, ref UInt16 conti_mode, ref double conti_vl, ref double conti_para, ref double filter)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_get_mode(card, ref conti_mode, ref conti_vl, ref conti_para, ref filter);
            else return x86.d2c80_conti_get_mode(card, ref conti_mode, ref conti_vl, ref conti_para, ref filter);
        }
        public static UInt32 d2c80_conti_open_list(UInt16 axisNum, ref UInt16 piaxisList)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_open_list(axisNum, ref piaxisList);
            else return x86.d2c80_conti_open_list(axisNum, ref piaxisList);
        }
        public static UInt32 d2c80_conti_close_list(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_close_list(card);
            else return x86.d2c80_conti_close_list(card);
        }
        public static UInt32 d2c80_conti_check_remain_space(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_check_remain_space(card);
            else return x86.d2c80_conti_check_remain_space(card);
        }
        public static UInt32 d2c80_conti_decel_stop_list(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_decel_stop_list(card);
            else return x86.d2c80_conti_decel_stop_list(card);
        }
        public static UInt32 d2c80_conti_sudden_stop_list(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_sudden_stop_list(card);
            else return x86.d2c80_conti_sudden_stop_list(card);
        }
        public static UInt32 d2c80_conti_pause_list(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_pause_list(card);
            else return x86.d2c80_conti_pause_list(card);
        }
        public static UInt32 d2c80_conti_start_list(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_start_list(card);
            else return x86.d2c80_conti_start_list(card);
        }
        public static Int32 d2c80_conti_read_current_mark(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_read_current_mark(card);
            else return x86.d2c80_conti_read_current_mark(card);
        }
        public static UInt32 d2c80_conti_extern_lines(UInt16 axisNum, ref UInt16 piaxisListw,ref Int32 pPosList, UInt16 posi_mode, Int32 imark)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_extern_lines(axisNum, ref piaxisListw,ref pPosList, posi_mode, imark);
            else return x86.d2c80_conti_extern_lines(axisNum, ref piaxisListw, ref pPosList, posi_mode, imark);
        }
        public static UInt32 d2c80_conti_extern_arc(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir, UInt16 posi_mode, Int32 imark)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_conti_extern_arc(ref axis, ref rel_pos, ref rel_cen, arc_dir, posi_mode, imark);
            else return x86.d2c80_conti_extern_arc(ref axis, ref rel_pos, ref rel_cen, arc_dir, posi_mode, imark);
        }
        public static UInt32 d2c80_Enable_EL_PIN(UInt16 axis, UInt16 enable)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_Enable_EL_PIN(axis, enable);
            else return x86.d2c80_Enable_EL_PIN(axis, enable);
        }
        public static UInt32 d2c80_get_Enable_EL_PIN(UInt16 axis, ref UInt16 enable)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2c80_get_Enable_EL_PIN(axis, ref enable);
            else return x86.d2c80_get_Enable_EL_PIN(axis, ref enable);
        }


        private class x64
        {
            //---------------------   板卡初始和配置函数  ----------------------
            /********************************************************************************
            ** 函数名称: d2c80_board_init
            ** 功能描述: 控制板初始化，设置初始化和速度等设置
            ** 输　  入: 无
            ** 返 回 值: 0：无卡； 1-8：成功(实际卡数) 
            **     
            *********************************************************************************/
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_board_init();


            /********************************************************************************
            ** 函数名称: d2c80_board_close
            ** 功能描述: 关闭所有卡
            ** 输　  入: 无
            ** 返 回 值: 无
            ** 日    期: 2007.02.1
            *********************************************************************************/
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern void d2c80_board_close();

            /********************************************************************************
            ** 函数名称: 控制卡复位
            ** 功能描述: 复位所有卡，只能在初始化完成之后调用．
            ** 输　  入: 卡号
            ** 返 回 值: 错误码
            ** 日    期: 
            *********************************************************************************/
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_board_reset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_board_reset(UInt16 card);
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_card_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_card_version(UInt16 card);
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_card_soft_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_card_soft_version(UInt16 card, ref UInt16 firm_id, ref UInt32 sub_firm_id);
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_client_ID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_client_ID(UInt16 card);
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_lib_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_lib_version();
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_card_ID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_card_ID(UInt16 card);
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_total_axes", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_total_axes(UInt16 card);
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_test_software", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_test_software(UInt16 card, UInt16 testid, UInt16 para1, UInt16 para2, UInt16 para3);
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_test_hardware(UInt16 card);
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_download_firmware", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_download_firmware(UInt16 card, ref char pfilename);

            //脉冲输入输出配置
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_set_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_pulse_outmode(UInt16 axis, UInt16 outmode);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_counter_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_counter_config(UInt16 axis, UInt16 mode);


            //添加配置读
            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_pulse_outmode(UInt16 axis, ref UInt16 outmode);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_counter_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_counter_config(UInt16 axis, ref UInt16 mode);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_INP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_INP_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 inp_logic);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_ERC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_ERC_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 erc_logic,
                            ref UInt16 erc_width, ref UInt16 erc_off_time);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_ALM_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_ALM_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 alm_logic, ref UInt16 alm_action);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_EL_PIN(UInt16 axis, ref UInt16 el_logic, ref UInt16 el_mode);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_HOME_PIN_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_HOME_PIN_logic(UInt16 axis, ref UInt16 org_logic, ref UInt16 filter);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_home_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_home_mode(UInt16 axis, ref UInt16 home_dir, ref double vel, ref UInt16 home_mode, ref UInt16 EZ_count);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_handwheel_inmode(UInt16 axis, ref UInt16 inmode, ref double multi);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_LTC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_LTC_PIN(UInt16 axis, ref UInt16 ltc_logic, ref UInt16 ltc_mode);


            //专用信号设置函数

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_INP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_INP_PIN(UInt16 axis, UInt16 enable, UInt16 inp_logic);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_ERC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_ERC_PIN(UInt16 axis, UInt16 enable, UInt16 erc_logic,
                            UInt16 erc_width, UInt16 erc_off_time);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_EMG_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_EMG_PIN(UInt16 cardno, UInt16 option, UInt16 emg_logic);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_EMG_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_EMG_PIN(UInt16 cardno, ref UInt16 enbale, ref UInt16 emg_logic);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_ALM_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_ALM_PIN(UInt16 axis, UInt16 enable, UInt16 alm_logic, UInt16 alm_action);

            //new

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_EL_PIN(UInt16 axis, UInt16 el_logic, UInt16 el_mode);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_HOME_PIN_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_HOME_PIN_logic(UInt16 axis, UInt16 org_logic, UInt16 filter);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_write_ERC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_write_ERC_PIN(UInt16 axis, UInt16 sel);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_set_backlash", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_backlash(UInt16 axis, Int32 backlash);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_backlash", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_backlash(UInt16 axis, ref Int32 backlash);

            //通用输入/输出控制函数

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_read_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d2c80_read_inbit(UInt16 cardno, UInt16 bitno);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_write_outbit(UInt16 cardno, UInt16 bitno, UInt16 on_off);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_read_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d2c80_read_outbit(UInt16 cardno, UInt16 bitno);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_read_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_read_inport(UInt16 cardno);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_read_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_read_outport(UInt16 cardno);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_write_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_write_outport(UInt16 cardno, UInt32 port_value);

            //制动函数

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_decel_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_decel_stop(UInt16 axis, double dec);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_imd_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_imd_stop(UInt16 axis);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_emg_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_emg_stop();

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_simultaneous_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_simultaneous_stop(UInt16 axis);

            //位置设置和读取函数

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_get_position(UInt16 axis);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_set_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_position(UInt16 axis, Int32 current_position);


            //状态检测函数

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d2c80_check_done(UInt16 axis);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_axis_io_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_axis_io_status(UInt16 axis);


            //速度设置

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_read_current_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern double d2c80_read_current_speed(UInt16 axis);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_read_vector_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern double d2c80_read_vector_speed(UInt16 card);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_change_speed(UInt16 axis, double Curr_Vel);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_set_vector_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_vector_profile(UInt16 cardno, double s_para, double Max_Vel, double acc, double dec);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_vector_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_vector_profile(UInt16 cardno, ref double s_para, ref double Max_Vel, ref double acc, ref double dec);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_set_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_profile(UInt16 axis, double option, double Max_Vel, double acc, double dec);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_s_profile(UInt16 axis, double s_para);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_profile(UInt16 axis, ref double option, ref double Max_Vel, ref double acc, ref double dec);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_s_profile(UInt16 axis, ref double s_para);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_reset_target_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_reset_target_position(UInt16 axis, Int32 dist);

            //单轴定长运动

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);

            //单轴连续运动

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_vmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_vmove(UInt16 axis, UInt16 dir, double vel);

            //线性插补

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_line2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_line2(UInt16 axis1, Int32 Dist1, UInt16 axis2, Int32 Dist2, UInt16 posi_mode);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_line3", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_line3(ref UInt16 axis, Int32 Dist1, Int32 Dist2, Int32 Dist3, UInt16 posi_mode);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_line4", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_line4(UInt16 cardno, Int32 Dist1, Int32 Dist2, Int32 Dist3, Int32 Dist4, UInt16 posi_mode);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_lineN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_lineN(UInt16 axisNum, ref UInt16 piaxisList, ref Int32 pPosList, UInt16 posi_mode);

            //手轮运动

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_set_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_handwheel_inmode(UInt16 axis, UInt16 inmode, double multi);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_handwheel_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_handwheel_move(UInt16 axis);

            //找原点

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_home_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_home_mode(UInt16 axis, UInt16 home_dir, double vel, UInt16 mode, UInt16 EZ_count);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_home_move(UInt16 axis);

            //圆弧插补

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_arc_move(ref UInt16 axis, ref Int32 target_pos, ref Int32 cen_pos, UInt16 arc_dir);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_rel_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_rel_arc_move(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir);

            //设置和读取位置比较信号

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_compare_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_compare_config(UInt16 card, UInt16 enable, UInt16 axis, UInt16 cmp_source);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_compare_get_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_compare_get_config(UInt16 card, ref UInt16 enable, ref UInt16 axis, ref UInt16 cmp_source);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_compare_clear_points", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_compare_clear_points(UInt16 card);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_compare_add_point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_compare_add_point(UInt16 card, Int32 pos, UInt16 dir, UInt16 action, Int32 actpara);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_compare_get_current_point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_compare_get_current_point(UInt16 card);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_compare_get_points_runned", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_compare_get_points_runned(UInt16 card);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_compare_get_points_remained", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_compare_get_points_remained(UInt16 card);


            //---------------------   编码器计数功能  ----------------------//

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_get_encoder(UInt16 axis);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_set_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_encoder(UInt16 axis, Int32 encoder_value);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_EZ_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_EZ_PIN(UInt16 axis, UInt16 ez_logic, UInt16 ez_mode);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_EZ_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_EZ_PIN(UInt16 axis, ref UInt16 ez_logic, ref UInt16 ez_mode);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_LTC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_LTC_PIN(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_latch_mode(UInt16 cardno, UInt16 all_enable);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_latch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_get_latch_value(UInt16 axis);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_get_latch_flag(UInt16 cardno);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_reset_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_reset_latch_flag(UInt16 cardno);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_get_counter_flag(UInt16 cardno);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_reset_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_reset_counter_flag(UInt16 cardno);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_reset_clear_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_reset_clear_flag(UInt16 cardno);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_triger_chunnel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_triger_chunnel(UInt16 cardno, UInt16 num);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_set_speaker_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_speaker_logic(UInt16 cardno, UInt16 logic);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_speaker_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_speaker_logic(UInt16 cardno, ref UInt16 logic);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_latch_mode(UInt16 cardno, ref UInt16 all_enable);

            //软件限位功能

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_softlimit(UInt16 axis, UInt16 ON_OFF, UInt16 source_sel, UInt16 SL_action, Int32 N_limit, Int32 P_limit);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_softlimit(UInt16 axis, ref UInt16 ON_OFF, ref UInt16 source_sel, ref UInt16 SL_action, ref Int32 N_limit, ref Int32 P_limit);


            //连续插补函数

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_lines", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_lines(UInt16 axisNum, ref UInt16 piaxisList,
                ref Int32 pPosList, UInt16 posi_mode);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_arc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_arc(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir, UInt16 posi_mode);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_restrain_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_restrain_speed(UInt16 card, double v);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_change_speed_ratio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_change_speed_ratio(UInt16 card, double percent);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_get_current_speed_ratio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern double d2c80_conti_get_current_speed_ratio(UInt16 card);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_set_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_set_mode(UInt16 card, UInt16 conti_mode, double conti_vl, double conti_para, double filter);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_get_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_get_mode(UInt16 card, ref UInt16 conti_mode, ref double conti_vl, ref double conti_para, ref double filter);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_open_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_open_list(UInt16 axisNum, ref UInt16 piaxisList);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_close_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_close_list(UInt16 card);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_check_remain_space", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_check_remain_space(UInt16 card);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_decel_stop_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_decel_stop_list(UInt16 card);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_sudden_stop_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_sudden_stop_list(UInt16 card);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_pause_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_pause_list(UInt16 card);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_start_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_start_list(UInt16 card);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_read_current_mark", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_conti_read_current_mark(UInt16 card);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_extern_lines", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_extern_lines(UInt16 axisNum, ref UInt16 piaxisListw,
                                                           ref Int32 pPosList, UInt16 posi_mode, Int32 imark);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_conti_extern_arc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_extern_arc(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir, UInt16 posi_mode, Int32 imark);


            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_Enable_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_Enable_EL_PIN(UInt16 axis, UInt16 enable);

            [DllImport(@"\DMC2410\x64\DMC2C80.dll", EntryPoint = "d2c80_get_Enable_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_Enable_EL_PIN(UInt16 axis, ref UInt16 enable);

            //PC库错误码
            enum ERR_CODE_DMC
            {
                ERR_NOERR = 0,          //成功      
                ERR_UNKNOWN = 1,
                ERR_PARAERR = 2,

                ERR_TIMEOUT = 3,
                ERR_CONTROLLERBUSY = 4,
                ERR_CONNECT_TOOMANY = 5,

                ERR_CONTILINE = 40,
                ERR_CANNOT_CONNECTETH = 8,
                ERR_HANDLEERR = 9,
                ERR_SENDERR = 10,
                ERR_FIRMWAREERR = 12, //固件文件错误
                ERR_FIRMWAR_MISMATCH = 14, //固件不匹配

                ERR_FIRMWARE_INVALID_PARA = 20,  //固件参数错误
                ERR_FIRMWARE_PARA_ERR = 20,  //固件参数错误2
                ERR_FIRMWARE_STATE_ERR = 22, //固件当前状态不允许操作
                ERR_FIRMWARE_LIB_STATE_ERR = 22, //固件当前状态不允许操作2
                ERR_FIRMWARE_CARD_NOT_SUPPORT = 24,  //固件不支持的功能 控制器不支持的功能
                ERR_FIRMWARE_LIB_NOTSUPPORT = 24,    //固件不支持的功能2
            };

        }
        private class x86
        {
            //---------------------   板卡初始和配置函数  ----------------------
            /********************************************************************************
            ** 函数名称: d2c80_board_init
            ** 功能描述: 控制板初始化，设置初始化和速度等设置
            ** 输　  入: 无
            ** 返 回 值: 0：无卡； 1-8：成功(实际卡数) 
            **     
            *********************************************************************************/
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_board_init();


            /********************************************************************************
            ** 函数名称: d2c80_board_close
            ** 功能描述: 关闭所有卡
            ** 输　  入: 无
            ** 返 回 值: 无
            ** 日    期: 2007.02.1
            *********************************************************************************/
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern void d2c80_board_close();

            /********************************************************************************
            ** 函数名称: 控制卡复位
            ** 功能描述: 复位所有卡，只能在初始化完成之后调用．
            ** 输　  入: 卡号
            ** 返 回 值: 错误码
            ** 日    期: 
            *********************************************************************************/
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_board_reset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_board_reset(UInt16 card);
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_card_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_card_version(UInt16 card);
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_card_soft_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_card_soft_version(UInt16 card, ref UInt16 firm_id, ref UInt32 sub_firm_id);
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_client_ID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_client_ID(UInt16 card);
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_lib_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_lib_version();
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_card_ID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_card_ID(UInt16 card);
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_total_axes", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_total_axes(UInt16 card);
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_test_software", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_test_software(UInt16 card, UInt16 testid, UInt16 para1, UInt16 para2, UInt16 para3);
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_test_hardware(UInt16 card);
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_download_firmware", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_download_firmware(UInt16 card, ref char pfilename);

            //脉冲输入输出配置
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_set_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_pulse_outmode(UInt16 axis, UInt16 outmode);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_counter_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_counter_config(UInt16 axis, UInt16 mode);


            //添加配置读
            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_pulse_outmode(UInt16 axis, ref UInt16 outmode);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_counter_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_counter_config(UInt16 axis, ref UInt16 mode);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_INP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_INP_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 inp_logic);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_ERC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_ERC_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 erc_logic,
                            ref UInt16 erc_width, ref UInt16 erc_off_time);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_ALM_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_ALM_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 alm_logic, ref UInt16 alm_action);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_EL_PIN(UInt16 axis, ref UInt16 el_logic, ref UInt16 el_mode);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_HOME_PIN_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_HOME_PIN_logic(UInt16 axis, ref UInt16 org_logic, ref UInt16 filter);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_home_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_home_mode(UInt16 axis, ref UInt16 home_dir, ref double vel, ref UInt16 home_mode, ref UInt16 EZ_count);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_handwheel_inmode(UInt16 axis, ref UInt16 inmode, ref double multi);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_LTC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_LTC_PIN(UInt16 axis, ref UInt16 ltc_logic, ref UInt16 ltc_mode);


            //专用信号设置函数

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_INP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_INP_PIN(UInt16 axis, UInt16 enable, UInt16 inp_logic);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_ERC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_ERC_PIN(UInt16 axis, UInt16 enable, UInt16 erc_logic,
                            UInt16 erc_width, UInt16 erc_off_time);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_EMG_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_EMG_PIN(UInt16 cardno, UInt16 option, UInt16 emg_logic);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_EMG_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_EMG_PIN(UInt16 cardno, ref UInt16 enbale, ref UInt16 emg_logic);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_ALM_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_ALM_PIN(UInt16 axis, UInt16 enable, UInt16 alm_logic, UInt16 alm_action);

            //new

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_EL_PIN(UInt16 axis, UInt16 el_logic, UInt16 el_mode);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_HOME_PIN_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_HOME_PIN_logic(UInt16 axis, UInt16 org_logic, UInt16 filter);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_write_ERC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_write_ERC_PIN(UInt16 axis, UInt16 sel);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_set_backlash", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_backlash(UInt16 axis, Int32 backlash);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_backlash", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_backlash(UInt16 axis, ref Int32 backlash);

            //通用输入/输出控制函数

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_read_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d2c80_read_inbit(UInt16 cardno, UInt16 bitno);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_write_outbit(UInt16 cardno, UInt16 bitno, UInt16 on_off);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_read_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d2c80_read_outbit(UInt16 cardno, UInt16 bitno);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_read_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_read_inport(UInt16 cardno);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_read_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_read_outport(UInt16 cardno);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_write_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_write_outport(UInt16 cardno, UInt32 port_value);

            //制动函数

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_decel_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_decel_stop(UInt16 axis, double dec);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_imd_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_imd_stop(UInt16 axis);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_emg_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_emg_stop();

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_simultaneous_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_simultaneous_stop(UInt16 axis);

            //位置设置和读取函数

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_get_position(UInt16 axis);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_set_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_position(UInt16 axis, Int32 current_position);


            //状态检测函数

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int d2c80_check_done(UInt16 axis);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_axis_io_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_axis_io_status(UInt16 axis);


            //速度设置

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_read_current_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern double d2c80_read_current_speed(UInt16 axis);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_read_vector_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern double d2c80_read_vector_speed(UInt16 card);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_change_speed(UInt16 axis, double Curr_Vel);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_set_vector_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_vector_profile(UInt16 cardno, double s_para, double Max_Vel, double acc, double dec);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_vector_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_vector_profile(UInt16 cardno, ref double s_para, ref double Max_Vel, ref double acc, ref double dec);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_set_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_profile(UInt16 axis, double option, double Max_Vel, double acc, double dec);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_s_profile(UInt16 axis, double s_para);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_profile(UInt16 axis, ref double option, ref double Max_Vel, ref double acc, ref double dec);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_s_profile(UInt16 axis, ref double s_para);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_reset_target_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_reset_target_position(UInt16 axis, Int32 dist);

            //单轴定长运动

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);

            //单轴连续运动

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_vmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_vmove(UInt16 axis, UInt16 dir, double vel);

            //线性插补

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_line2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_line2(UInt16 axis1, Int32 Dist1, UInt16 axis2, Int32 Dist2, UInt16 posi_mode);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_line3", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_line3(ref UInt16 axis, Int32 Dist1, Int32 Dist2, Int32 Dist3, UInt16 posi_mode);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_line4", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_line4(UInt16 cardno, Int32 Dist1, Int32 Dist2, Int32 Dist3, Int32 Dist4, UInt16 posi_mode);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_lineN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_lineN(UInt16 axisNum, ref UInt16 piaxisList, ref Int32 pPosList, UInt16 posi_mode);

            //手轮运动

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_set_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_handwheel_inmode(UInt16 axis, UInt16 inmode, double multi);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_handwheel_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_handwheel_move(UInt16 axis);

            //找原点

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_home_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_home_mode(UInt16 axis, UInt16 home_dir, double vel, UInt16 mode, UInt16 EZ_count);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_home_move(UInt16 axis);

            //圆弧插补

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_arc_move(ref UInt16 axis, ref Int32 target_pos, ref Int32 cen_pos, UInt16 arc_dir);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_rel_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_rel_arc_move(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir);

            //设置和读取位置比较信号

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_compare_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_compare_config(UInt16 card, UInt16 enable, UInt16 axis, UInt16 cmp_source);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_compare_get_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_compare_get_config(UInt16 card, ref UInt16 enable, ref UInt16 axis, ref UInt16 cmp_source);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_compare_clear_points", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_compare_clear_points(UInt16 card);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_compare_add_point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_compare_add_point(UInt16 card, Int32 pos, UInt16 dir, UInt16 action, Int32 actpara);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_compare_get_current_point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_compare_get_current_point(UInt16 card);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_compare_get_points_runned", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_compare_get_points_runned(UInt16 card);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_compare_get_points_remained", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_compare_get_points_remained(UInt16 card);


            //---------------------   编码器计数功能  ----------------------//

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_get_encoder(UInt16 axis);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_set_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_encoder(UInt16 axis, Int32 encoder_value);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_EZ_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_EZ_PIN(UInt16 axis, UInt16 ez_logic, UInt16 ez_mode);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_EZ_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_EZ_PIN(UInt16 axis, ref UInt16 ez_logic, ref UInt16 ez_mode);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_LTC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_LTC_PIN(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_latch_mode(UInt16 cardno, UInt16 all_enable);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_latch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_get_latch_value(UInt16 axis);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_get_latch_flag(UInt16 cardno);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_reset_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_reset_latch_flag(UInt16 cardno);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_get_counter_flag(UInt16 cardno);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_reset_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_reset_counter_flag(UInt16 cardno);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_reset_clear_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_reset_clear_flag(UInt16 cardno);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_triger_chunnel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_triger_chunnel(UInt16 cardno, UInt16 num);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_set_speaker_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_set_speaker_logic(UInt16 cardno, UInt16 logic);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_speaker_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_speaker_logic(UInt16 cardno, ref UInt16 logic);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_latch_mode(UInt16 cardno, ref UInt16 all_enable);

            //软件限位功能

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_config_softlimit(UInt16 axis, UInt16 ON_OFF, UInt16 source_sel, UInt16 SL_action, Int32 N_limit, Int32 P_limit);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_config_softlimit(UInt16 axis, ref UInt16 ON_OFF, ref UInt16 source_sel, ref UInt16 SL_action, ref Int32 N_limit, ref Int32 P_limit);


            //连续插补函数

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_lines", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_lines(UInt16 axisNum, ref UInt16 piaxisList,
                ref Int32 pPosList, UInt16 posi_mode);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_arc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_arc(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir, UInt16 posi_mode);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_restrain_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_restrain_speed(UInt16 card, double v);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_change_speed_ratio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_change_speed_ratio(UInt16 card, double percent);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_get_current_speed_ratio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern double d2c80_conti_get_current_speed_ratio(UInt16 card);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_set_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_set_mode(UInt16 card, UInt16 conti_mode, double conti_vl, double conti_para, double filter);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_get_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_get_mode(UInt16 card, ref UInt16 conti_mode, ref double conti_vl, ref double conti_para, ref double filter);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_open_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_open_list(UInt16 axisNum, ref UInt16 piaxisList);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_close_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_close_list(UInt16 card);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_check_remain_space", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_check_remain_space(UInt16 card);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_decel_stop_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_decel_stop_list(UInt16 card);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_sudden_stop_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_sudden_stop_list(UInt16 card);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_pause_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_pause_list(UInt16 card);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_start_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_start_list(UInt16 card);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_read_current_mark", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2c80_conti_read_current_mark(UInt16 card);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_extern_lines", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_extern_lines(UInt16 axisNum, ref UInt16 piaxisListw,
                                                           ref Int32 pPosList, UInt16 posi_mode, Int32 imark);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_conti_extern_arc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_conti_extern_arc(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir, UInt16 posi_mode, Int32 imark);


            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_Enable_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_Enable_EL_PIN(UInt16 axis, UInt16 enable);

            [DllImport(@"\DMC2410\x86\DMC2C80.dll", EntryPoint = "d2c80_get_Enable_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2c80_get_Enable_EL_PIN(UInt16 axis, ref UInt16 enable);

            //PC库错误码
            enum ERR_CODE_DMC
            {
                ERR_NOERR = 0,          //成功      
                ERR_UNKNOWN = 1,
                ERR_PARAERR = 2,

                ERR_TIMEOUT = 3,
                ERR_CONTROLLERBUSY = 4,
                ERR_CONNECT_TOOMANY = 5,

                ERR_CONTILINE = 40,
                ERR_CANNOT_CONNECTETH = 8,
                ERR_HANDLEERR = 9,
                ERR_SENDERR = 10,
                ERR_FIRMWAREERR = 12, //固件文件错误
                ERR_FIRMWAR_MISMATCH = 14, //固件不匹配

                ERR_FIRMWARE_INVALID_PARA = 20,  //固件参数错误
                ERR_FIRMWARE_PARA_ERR = 20,  //固件参数错误2
                ERR_FIRMWARE_STATE_ERR = 22, //固件当前状态不允许操作
                ERR_FIRMWARE_LIB_STATE_ERR = 22, //固件当前状态不允许操作2
                ERR_FIRMWARE_CARD_NOT_SUPPORT = 24,  //固件不支持的功能 控制器不支持的功能
                ERR_FIRMWARE_LIB_NOTSUPPORT = 24,    //固件不支持的功能2
            };

        }
    }
}
