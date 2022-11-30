using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LeadShineUI
{
    public class Dmc2410
    {
        public static UInt16 d2410_board_init()
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_board_init();
            else return x86.d2410_board_init();
        }
        public static UInt32 d2410_board_close()
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_board_close();
            else return x86.d2410_board_close();
        }
        public static UInt32 d2410_board_rest()
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_board_rest();
            else return x86.d2410_board_rest();
        }
        public static UInt32 d2410_set_pulse_outmode(UInt16 axis, UInt16 outmode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_pulse_outmode(axis, outmode);
            else return x86.d2410_set_pulse_outmode(axis, outmode);
        }
        public static UInt32 d2410_config_ALM_PIN(UInt16 axis, UInt16 alm_logic, UInt16 alm_action)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_ALM_PIN(axis, alm_logic, alm_action);
            else return x86.d2410_config_ALM_PIN(axis, alm_logic, alm_action);
        }
        public static UInt32 d2410_config_ALM_PIN_Extern(UInt16 axis, UInt16 alm_enbale, UInt16 alm_logic, UInt16 alm_all, UInt16 alm_action)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_ALM_PIN_Extern(axis, alm_enbale, alm_logic, alm_all, alm_action);
            else return x86.d2410_config_ALM_PIN_Extern(axis, alm_enbale, alm_logic, alm_all, alm_action);
        }
        public static UInt32 d2410_config_EL_MODE(UInt16 axis, UInt16 el_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_EL_MODE(axis, el_mode);
            else return x86.d2410_config_EL_MODE(axis, el_mode);
        }
        public static UInt32 d2410_set_HOME_pin_logic(UInt16 axis, UInt16 org_logic, UInt16 filter)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_HOME_pin_logic(axis, org_logic, filter);
            else return x86.d2410_set_HOME_pin_logic(axis, org_logic, filter);
        }
        public static UInt32 d2410_write_SEVON_PIN(UInt16 axis, UInt16 on_off)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_write_SEVON_PIN(axis, on_off);
            else return x86.d2410_write_SEVON_PIN(axis, on_off);
        }
        public static Int32 d2410_read_SEVON_PIN(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_read_SEVON_PIN(axis);
            else return x86.d2410_read_SEVON_PIN(axis);
        }
        public static Int32 d2410_read_RDY_PIN(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_read_RDY_PIN(axis);
            else return x86.d2410_read_RDY_PIN(axis);
        }
        public static UInt32 d2410_Enable_EL_PIN(UInt16 axis, UInt16 enable)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_Enable_EL_PIN(axis, enable);
            else return x86.d2410_Enable_EL_PIN(axis, enable);
        }
        public static Int32 d2410_read_inbit(UInt16 cardno, UInt16 bitno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_read_inbit(cardno, bitno);
            else return x86.d2410_read_inbit(cardno, bitno);
        }
        public static Int32 d2410_read_inport(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_read_inport(cardno);
            else return x86.d2410_read_inport(cardno);
        }       
        public static UInt32 d2410_write_outbit(UInt16 cardno, UInt16 bitno, UInt16 on_off)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_write_outbit(cardno, bitno, on_off);
            else return x86.d2410_write_outbit(cardno, bitno, on_off);
        }
        public static Int32 d2410_read_outbit(UInt16 cardno, UInt16 bitno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_read_outbit(cardno, bitno);
            else return x86.d2410_read_outbit(cardno, bitno);
        }
        public static Int32 d2410_read_outport(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_read_outport(cardno);
            else return x86.d2410_read_outport(cardno);
        }
        public static UInt32 d2410_write_outport(UInt16 cardno, UInt32 port_value)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_write_outport(cardno, port_value);
            else return x86.d2410_write_outport(cardno, port_value);
        }
        public static UInt32 d2410_decel_stop(UInt16 axis, double Tdec)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_decel_stop(axis, Tdec);
            else return x86.d2410_decel_stop(axis, Tdec);
        }
        public static UInt32 d2410_imd_stop(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_imd_stop(axis);
            else return x86.d2410_imd_stop(axis);
        }
        public static UInt32 d2410_emg_stop()
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_emg_stop();
            else return x86.d2410_emg_stop();
        }
        public static Int32 d2410_get_position(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_position(axis);
            else return x86.d2410_get_position(axis);
        }
        public static UInt32 d2410_set_position(UInt16 axis, Int32 current_position)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_position(axis, current_position);
            else return x86.d2410_set_position(axis, current_position);
        }
        public static UInt16 d2410_check_done(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_check_done(axis);
            else return x86.d2410_check_done(axis);
        }
        public static UInt16 d2410_axis_io_status(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_axis_io_status(axis);
            else return x86.d2410_axis_io_status(axis);
        }
        public static UInt32 d2410_get_rsts(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_rsts(axis);
            else return x86.d2410_get_rsts(axis);
        }
        public static UInt32 d2410_set_vector_profile(double Min_Vel, double Max_Vel, double Tacc, double Tdec)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_vector_profile(Min_Vel, Max_Vel, Tacc, Tdec);
            else return x86.d2410_set_vector_profile(Min_Vel, Max_Vel, Tacc, Tdec);
        }
        public static UInt32 d2410_set_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_profile(axis, Min_Vel, Max_Vel, Tacc, Tdec);
            else return x86.d2410_set_profile(axis, Min_Vel, Max_Vel, Tacc, Tdec);
        }
        public static UInt32 d2410_set_profile_Extern(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Stop_Vel)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_profile_Extern(axis, Min_Vel, Max_Vel, Tacc, Tdec, Stop_Vel);
            else return x86.d2410_set_profile_Extern(axis, Min_Vel, Max_Vel, Tacc, Tdec, Stop_Vel);
        }
        public static UInt32 d2410_set_s_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, Int32 Sacc, Int32 Sdec)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_s_profile(axis, Min_Vel, Max_Vel, Tacc, Tdec, Sacc, Sdec);
            else return x86.d2410_set_s_profile(axis, Min_Vel, Max_Vel, Tacc, Tdec, Sacc, Sdec);
        }
        public static UInt32 d2410_set_st_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Tsacc, double Tsdec)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_st_profile(axis, Min_Vel, Max_Vel, Tacc, Tdec, Tsacc, Tsdec);
            else return x86.d2410_set_st_profile(axis, Min_Vel, Max_Vel, Tacc, Tdec, Tsacc, Tsdec);
        }
        public static double d2410_read_current_speed(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_read_current_speed(axis);
            else return x86.d2410_read_current_speed(axis);
        }
        public static double d2410_read_vector_speed(UInt16 card)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_read_vector_speed(card);
            else return x86.d2410_read_vector_speed(card);
        }
        public static UInt32 d2410_set_st_profile_Extern(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Tsacc, double Tsdec, double Stop_Vel)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_st_profile_Extern(axis, Min_Vel, Max_Vel, Tacc, Tdec, Tsacc, Tsdec, Stop_Vel);
            else return x86.d2410_set_st_profile_Extern(axis, Min_Vel, Max_Vel, Tacc, Tdec, Tsacc, Tsdec, Stop_Vel);
        }
        public static UInt32 d2410_change_speed(UInt16 axis, double Curr_Vel)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_change_speed(axis, Curr_Vel);
            else return x86.d2410_change_speed(axis, Curr_Vel);
        }
        public static UInt32 d2410_reset_target_position(UInt16 axis, Int32 dist)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_reset_target_position(axis, dist);
            else return x86.d2410_reset_target_position(axis, dist);
        }
        public static UInt32 d2410_t_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_t_pmove(axis, Dist, posi_mode);
            else return x86.d2410_t_pmove(axis, Dist, posi_mode);
        }
        public static UInt32 d2410_ex_t_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_ex_t_pmove(axis, Dist, posi_mode);
            else return x86.d2410_ex_t_pmove(axis, Dist, posi_mode);
        }
        public static UInt32 d2410_s_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_s_pmove(axis, Dist, posi_mode);
            else return x86.d2410_s_pmove(axis, Dist, posi_mode);
        }
        public static UInt32 d2410_ex_s_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_ex_s_pmove(axis, Dist, posi_mode);
            else return x86.d2410_ex_s_pmove(axis, Dist, posi_mode);
        }
        public static UInt32 d2410_s_vmove(UInt16 axis, UInt16 dir)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_s_vmove(axis, dir);
            else return x86.d2410_s_vmove(axis, dir);
        }
        public static UInt32 d2410_t_vmove(UInt16 axis, UInt16 dir)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_t_vmove(axis, dir);
            else return x86.d2410_t_vmove(axis, dir);
        }
        public static UInt32 d2410_t_line2(UInt16 axis1, Int32 Dist1, UInt16 axis2, Int32 Dist2, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_t_line2(axis1, Dist1, axis2, Dist2, posi_mode);
            else return x86.d2410_t_line2(axis1, Dist1, axis2, Dist2, posi_mode);
        }
        public static UInt32 d2410_t_line3(UInt16[] axis, Int32 Dist1, Int32 Dist2, Int32 Dist3, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_t_line3(axis, Dist1, Dist2, Dist3, posi_mode);
            else return x86.d2410_t_line3(axis, Dist1, Dist2, Dist3, posi_mode);
        }
        public static UInt32 d2410_t_line4(UInt16 cardno, Int32 Dist1, Int32 Dist2, Int32 Dist3, Int32 Dist4, UInt16 posi_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_t_line4(cardno, Dist1, Dist2, Dist3, Dist4, posi_mode);
            else return x86.d2410_t_line4(cardno, Dist1, Dist2, Dist3, Dist4, posi_mode);
        }
        public static UInt32 d2410_arc_move(UInt16[] axis, Int32[] target_pos, Int32[] cen_pos, UInt16 arc_dir)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_arc_move(axis, target_pos, cen_pos, arc_dir);
            else return x86.d2410_arc_move(axis, target_pos, cen_pos, arc_dir);
        }
        public static UInt32 d2410_rel_arc_move(UInt16[] axis, Int32[] rel_pos, Int32[] rel_cen, UInt16 arc_dir)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_rel_arc_move(axis, rel_pos, rel_cen, arc_dir);
            else return x86.d2410_rel_arc_move(axis, rel_pos, rel_cen, arc_dir);
        }
        public static UInt32 d2410_set_handwheel_inmode(UInt16 axis, UInt16 inmode, double multi)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_handwheel_inmode(axis, inmode, multi);
            else return x86.d2410_set_handwheel_inmode(axis, inmode, multi);
        }
        public static UInt32 d2410_handwheel_move(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_handwheel_move(axis);
            else return x86.d2410_handwheel_move(axis);
        }
        public static UInt32 d2410_config_home_mode(UInt16 axis, UInt16 mode, UInt16 EZ_count)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_home_mode(axis, mode, EZ_count);
            else return x86.d2410_config_home_mode(axis, mode, EZ_count);
        }
        public static UInt32 d2410_home_move(UInt16 axis, UInt16 home_mode, UInt16 vel_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_home_move(axis, home_mode, vel_mode);
            else return x86.d2410_home_move(axis, home_mode, vel_mode);
        }
        public static UInt32 d2410_set_homelatch_mode(UInt16 axis, UInt16 enable, UInt16 logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_homelatch_mode(axis, enable, logic);
            else return x86.d2410_set_homelatch_mode(axis, enable, logic);
        }
        public static UInt32 d2410_get_homelatch_mode(UInt16 axis, ref UInt16 enable, ref UInt16 logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_homelatch_mode(axis, ref enable, ref logic);
            else return x86.d2410_get_homelatch_mode(axis, ref enable, ref logic);
        }
        public static Int32 d2410_get_homelatch_flag(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_homelatch_flag(axis);
            else return x86.d2410_get_homelatch_flag(axis);
        }
        public static UInt32 d2410_reset_homelatch_flag(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_reset_homelatch_flag(axis);
            else return x86.d2410_reset_homelatch_flag(axis);
        }
        public static Int32 d2410_get_homelatch_value(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_homelatch_value(axis);
            else return x86.d2410_get_homelatch_value(axis);
        }
        public static UInt32 d2410_compare_config_Extern(UInt16 card, UInt16 queue, UInt16 enable, UInt16 axis, UInt16 cmp_source)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_compare_config_Extern(card, queue, enable, axis, cmp_source);
            else return x86.d2410_compare_config_Extern(card, queue, enable, axis, cmp_source);
        }
        public static UInt32 d2410_compare_get_config_Extern(UInt16 card, UInt16 queue, ref UInt16 enable, ref UInt16 axis, ref UInt16 cmp_source)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_compare_get_config_Extern(card, queue, ref enable, ref axis, ref cmp_source);
            else return x86.d2410_compare_get_config_Extern(card, queue, ref enable, ref axis, ref cmp_source);
        }
        public static UInt32 d2410_compare_clear_points_Extern(UInt16 card, UInt16 queue)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_compare_clear_points_Extern(card, queue);
            else return x86.d2410_compare_clear_points_Extern(card, queue);
        }
        public static UInt32 d2410_compare_add_point_Extern(UInt16 card, UInt16 queue, UInt32 pos, UInt16 dir, UInt16 action, UInt32 actpara)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_compare_add_point_Extern(card, queue, pos, dir, action, actpara);
            else return x86.d2410_compare_add_point_Extern(card, queue, pos, dir, action, actpara);
        }
        public static Int32 d2410_compare_get_current_point_Extern(UInt16 card, UInt16 queue)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_compare_get_current_point_Extern(card, queue);
            else return x86.d2410_compare_get_current_point_Extern(card, queue);
        }
        public static Int32 d2410_compare_get_points_runned_Extern(UInt16 card, UInt16 queue)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_compare_get_points_runned_Extern(card, queue);
            else return x86.d2410_compare_get_points_runned_Extern(card, queue);
        }
        public static Int32 d2410_compare_get_points_remained_Extern(UInt16 card, UInt16 queue)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_compare_get_points_remained_Extern(card, queue);
            else return x86.d2410_compare_get_points_remained_Extern(card, queue);
        }    
        public static UInt32 d2410_config_CMP_PIN(UInt16 axis, UInt16 cmp_enable, Int32 cmp_pos, UInt16 CMP_logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_CMP_PIN(axis, cmp_enable, cmp_pos, CMP_logic);
            else return x86.d2410_config_CMP_PIN(axis, cmp_enable, cmp_pos, CMP_logic);
        }
        public static UInt32 d2410_get_config_CMP_PIN(UInt16 axis, ref UInt16 cmp_enable, ref Int32 cmp_pos, ref UInt16 CMP_logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_config_CMP_PIN(axis, ref cmp_enable, ref cmp_pos, ref CMP_logic);
            else return x86.d2410_get_config_CMP_PIN(axis, ref cmp_enable, ref cmp_pos, ref CMP_logic);
        }
        public static Int32 d2410_read_CMP_PIN(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_read_CMP_PIN(axis);
            else return x86.d2410_read_CMP_PIN(axis);
        }
        public static UInt32 d2410_write_CMP_PIN(UInt16 axis, UInt16 on_off)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_write_CMP_PIN(axis, on_off);
            else return x86.d2410_write_CMP_PIN(axis, on_off);
        }
        public static Int32 d2410_get_encoder(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_encoder(axis);
            else return x86.d2410_get_encoder(axis);
        }
        public static UInt32 d2410_set_encoder(UInt16 axis, UInt32 encoder_value)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_encoder(axis, encoder_value);
            else return x86.d2410_set_encoder(axis, encoder_value);
        }
        public static UInt32 d2410_config_EZ_PIN(UInt16 axis, UInt16 ez_logic, UInt16 ez_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_EZ_PIN(axis, ez_logic, ez_mode);
            else return x86.d2410_config_EZ_PIN(axis, ez_logic, ez_mode);
        }
        public static UInt32 d2410_get_counter_flag(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_counter_flag(cardno);
            else return x86.d2410_get_counter_flag(cardno);
        }
        public static UInt32 d2410_reset_counter_flag(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_reset_counter_flag(cardno);
            else return x86.d2410_reset_counter_flag(cardno);
        }
        public static UInt32 d2410_reset_clear_flag(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_reset_clear_flag(cardno);
            else return x86.d2410_reset_clear_flag(cardno);
        }
        public static UInt32 d2410_config_LTC_PIN(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_LTC_PIN(axis, ltc_logic, ltc_mode);
            else return x86.d2410_config_LTC_PIN(axis, ltc_logic, ltc_mode);
        }
        public static UInt32 d2410_config_LTC_PIN_Extern(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode, double ltc_filter)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_LTC_PIN_Extern(axis, ltc_logic, ltc_mode, ltc_filter);
            else return x86.d2410_config_LTC_PIN_Extern(axis, ltc_logic, ltc_mode, ltc_filter);
        }
        public static UInt32 d2410_config_latch_mode(UInt16 cardno, UInt16 all_enable)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_latch_mode(cardno, all_enable);
            else return x86.d2410_config_latch_mode(cardno, all_enable);
        }
        public static UInt32 d2410_counter_config(UInt16 axis, UInt16 mode)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_counter_config(axis, mode);
            else return x86.d2410_counter_config(axis, mode);
        }
        public static UInt32 d2410_get_latch_value(UInt16 axis)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_latch_value(axis);
            else return x86.d2410_get_latch_value(axis);
        }
        public static UInt32 d2410_get_latch_flag(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_latch_flag(cardno);
            else return x86.d2410_get_latch_flag(cardno);
        }
        public static UInt32 d2410_reset_latch_flag(UInt16 cardno)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_reset_latch_flag(cardno);
            else return x86.d2410_reset_latch_flag(cardno);
        }
        public static UInt32 d2410_triger_chunnel(UInt16 cardno, UInt16 num)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_triger_chunnel(cardno, num);
            else return x86.d2410_triger_chunnel(cardno, num);
        }
        public static UInt32 d2410_set_speaker_logic(UInt16 cardno, UInt16 logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_set_speaker_logic(cardno, logic);
            else return x86.d2410_set_speaker_logic(cardno, logic);
        }
        public static UInt32 d2410_config_EMG_PIN(UInt16 cardno, UInt16 enable, UInt16 emg_logic)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_EMG_PIN(cardno, enable, emg_logic);
            else return x86.d2410_config_EMG_PIN(cardno, enable, emg_logic);
        }
        public static UInt32 d2410_config_softlimit(UInt16 axis, UInt16 ON_OFF, UInt16 source_sel, UInt16 SL_action, Int32 N_limit, Int32 P_limit)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_config_softlimit(axis, ON_OFF, source_sel, SL_action, N_limit, P_limit);
            else return x86.d2410_config_softlimit(axis, ON_OFF, source_sel, SL_action, N_limit, P_limit);
        }
        public static UInt32 d2410_get_config_softlimit(UInt16 axis, ref UInt16 ON_OFF, ref UInt16 source_sel, ref UInt16 SL_action, ref Int32 N_limit, ref Int32 P_limit)
        {
            if (Basic.MySystem.IsSystem_x64()) return x64.d2410_get_config_softlimit(axis, ref ON_OFF, ref source_sel, ref SL_action, ref N_limit, ref P_limit);
            else return x86.d2410_get_config_softlimit(axis, ref ON_OFF, ref source_sel, ref SL_action, ref N_limit, ref P_limit);
        }
        private class x64
        {
            //---------------------板卡初始和配置函数DMC2480 ----------------------
            /********************************************************************************
            ** 函数名称: d2410_board_init
            ** 功能描述: 控制板初始化，设置初始化和速度等设置
            ** 输　  入: 无
            ** 返 回 值: 0：无卡； 1-8：成功(实际卡数) 
            **           1001 + j: j号卡初始化出错 从1001开始。
            ** 修    改:  
            ** 修改日期: 
            *********************************************************************************/
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt16 d2410_board_init();

            /********************************************************************************
            ** 函数名称: d2410_board_close
            ** 功能描述: 关闭所有卡
            ** 输　  入: 无
            ** 返 回 值: 无
            ** 日    期: 
            *********************************************************************************/
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_board_close();

            /********************************************************************************
            ** 函数名称: 控制卡复位
            ** 功能描述: 复位所有卡，只能在初始化完成之后调用．
            ** 输　  入: 无
            ** 返 回 值: 无
            ** 日    期: 
            *********************************************************************************/
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_board_rest", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_board_rest();

            //脉冲输入输出配置
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_pulse_outmode(UInt16 axis, UInt16 outmode);

            //专用信号设置函数        
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_ALM_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_ALM_PIN(UInt16 axis, UInt16 alm_logic, UInt16 alm_action);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_ALM_PIN_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_ALM_PIN_Extern(UInt16 axis, UInt16 alm_enbale, UInt16 alm_logic, UInt16 alm_all, UInt16 alm_action);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_EL_MODE", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_EL_MODE(UInt16 axis, UInt16 el_mode);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_HOME_pin_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_HOME_pin_logic(UInt16 axis, UInt16 org_logic, UInt16 filter);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_write_SEVON_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_write_SEVON_PIN(UInt16 axis, UInt16 on_off);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_read_SEVON_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_SEVON_PIN(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_read_RDY_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_RDY_PIN(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_Enable_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_Enable_EL_PIN(UInt16 axis, UInt16 enable);


            //通用输入/输出控制函数
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_read_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_inbit(UInt16 cardno, UInt16 bitno);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_write_outbit(UInt16 cardno, UInt16 bitno, UInt16 on_off);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_read_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_outbit(UInt16 cardno, UInt16 bitno);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_read_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_inport(UInt16 cardno);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_read_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_outport(UInt16 cardno);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_write_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_write_outport(UInt16 cardno, UInt32 port_value);

            //制动函数
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_decel_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_decel_stop(UInt16 axis, double Tdec);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_imd_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_imd_stop(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_emg_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_emg_stop();

            //位置设置和读取函数
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_get_position(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_position(UInt16 axis, Int32 current_position);

            //状态检测函数
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt16 d2410_check_done(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_axis_io_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt16 d2410_axis_io_status(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_rsts", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_rsts(UInt16 axis);

            //速度设置和读取函数              
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_vector_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_vector_profile(double Min_Vel, double Max_Vel, double Tacc, double Tdec);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_profile_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_profile_Extern(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Stop_Vel);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_s_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, Int32 Sacc, Int32 Sdec);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_st_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_st_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Tsacc, double Tsdec);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_read_current_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern double d2410_read_current_speed(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_read_vector_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern double d2410_read_vector_speed(UInt16 card);

            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_st_profile_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_st_profile_Extern(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Tsacc, double Tsdec, double Stop_Vel);

            //在线变速/变位
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_change_speed(UInt16 axis, double Curr_Vel);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_reset_target_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_reset_target_position(UInt16 axis, Int32 dist);

            //单轴定长运动
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_t_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_t_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_ex_t_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_ex_t_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_s_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_s_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_ex_s_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_ex_s_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);

            //单轴连续运动
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_s_vmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_s_vmove(UInt16 axis, UInt16 dir);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_t_vmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_t_vmove(UInt16 axis, UInt16 dir);

            //直线插补
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_t_line2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_t_line2(UInt16 axis1, Int32 Dist1, UInt16 axis2, Int32 Dist2, UInt16 posi_mode);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_t_line3", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_t_line3(UInt16[] axis, Int32 Dist1, Int32 Dist2, Int32 Dist3, UInt16 posi_mode);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_t_line4", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_t_line4(UInt16 cardno, Int32 Dist1, Int32 Dist2, Int32 Dist3, Int32 Dist4, UInt16 posi_mode);

            //圆弧插补
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_arc_move(UInt16[] axis, Int32[] target_pos, Int32[] cen_pos, UInt16 arc_dir);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_rel_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_rel_arc_move(UInt16[] axis, Int32[] rel_pos, Int32[] rel_cen, UInt16 arc_dir);

            //手轮运动
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_handwheel_inmode(UInt16 axis, UInt16 inmode, double multi);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_handwheel_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_handwheel_move(UInt16 axis);

            //找原点
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_home_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_home_mode(UInt16 axis, UInt16 mode, UInt16 EZ_count);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_home_move(UInt16 axis, UInt16 home_mode, UInt16 vel_mode);

            //原点锁存
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_homelatch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_homelatch_mode(UInt16 axis, UInt16 enable, UInt16 logic);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_homelatch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_homelatch_mode(UInt16 axis, ref UInt16 enable, ref UInt16 logic);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_homelatch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_get_homelatch_flag(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_reset_homelatch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_reset_homelatch_flag(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_homelatch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_get_homelatch_value(UInt16 axis);

            //多组位置比较函数
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_compare_config_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_compare_config_Extern(UInt16 card, UInt16 queue, UInt16 enable, UInt16 axis, UInt16 cmp_source);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_compare_get_config_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_compare_get_config_Extern(UInt16 card, UInt16 queue, ref UInt16 enable, ref UInt16 axis, ref UInt16 cmp_source);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_compare_clear_points_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_compare_clear_points_Extern(UInt16 card, UInt16 queue);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_compare_add_point_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_compare_add_point_Extern(UInt16 card, UInt16 queue, UInt32 pos, UInt16 dir, UInt16 action, UInt32 actpara);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_compare_get_current_point_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_compare_get_current_point_Extern(UInt16 card, UInt16 queue);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_compare_get_points_runned_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_compare_get_points_runned_Extern(UInt16 card, UInt16 queue);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_compare_get_points_remained_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_compare_get_points_remained_Extern(UInt16 card, UInt16 queue);

            //高速位置比较
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_CMP_PIN(UInt16 axis, UInt16 cmp_enable, Int32 cmp_pos, UInt16 CMP_logic);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_config_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_config_CMP_PIN(UInt16 axis, ref UInt16 cmp_enable, ref Int32 cmp_pos, ref UInt16 CMP_logic);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_read_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_CMP_PIN(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_write_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_write_CMP_PIN(UInt16 axis, UInt16 on_off);

            //编码器计数功能
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_get_encoder(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_encoder(UInt16 axis, UInt32 encoder_value);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_EZ_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_EZ_PIN(UInt16 axis, UInt16 ez_logic, UInt16 ez_mode);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_counter_flag(UInt16 cardno);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_reset_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_reset_counter_flag(UInt16 cardno);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_reset_clear_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_reset_clear_flag(UInt16 cardno);

            //高速锁存
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_LTC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_LTC_PIN(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_LTC_PIN_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_LTC_PIN_Extern(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode, double ltc_filter);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_latch_mode(UInt16 cardno, UInt16 all_enable);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_counter_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_counter_config(UInt16 axis, UInt16 mode);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_latch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_latch_value(UInt16 axis);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_latch_flag(UInt16 cardno);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_reset_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_reset_latch_flag(UInt16 cardno);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_triger_chunnel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_triger_chunnel(UInt16 cardno, UInt16 num);

            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_set_speaker_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_speaker_logic(UInt16 cardno, UInt16 logic);

            //EMG设置
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_EMG_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_EMG_PIN(UInt16 cardno, UInt16 enable, UInt16 emg_logic);

            //软件限位功能
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_softlimit(UInt16 axis, UInt16 ON_OFF, UInt16 source_sel, UInt16 SL_action, Int32 N_limit, Int32 P_limit);
            [DllImport(@"DMC2410\x64\Dmc2410.dll", EntryPoint = "d2410_get_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_config_softlimit(UInt16 axis, ref UInt16 ON_OFF, ref UInt16 source_sel, ref UInt16 SL_action, ref Int32 N_limit, ref Int32 P_limit);


        }
        private class x86
        {
            //---------------------板卡初始和配置函数DMC2480 ----------------------
            /********************************************************************************
            ** 函数名称: d2410_board_init
            ** 功能描述: 控制板初始化，设置初始化和速度等设置
            ** 输　  入: 无
            ** 返 回 值: 0：无卡； 1-8：成功(实际卡数) 
            **           1001 + j: j号卡初始化出错 从1001开始。
            ** 修    改:  
            ** 修改日期: 
            *********************************************************************************/
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt16 d2410_board_init();

            /********************************************************************************
            ** 函数名称: d2410_board_close
            ** 功能描述: 关闭所有卡
            ** 输　  入: 无
            ** 返 回 值: 无
            ** 日    期: 
            *********************************************************************************/
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_board_close();

            /********************************************************************************
            ** 函数名称: 控制卡复位
            ** 功能描述: 复位所有卡，只能在初始化完成之后调用．
            ** 输　  入: 无
            ** 返 回 值: 无
            ** 日    期: 
            *********************************************************************************/
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_board_rest", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_board_rest();

            //脉冲输入输出配置
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_pulse_outmode(UInt16 axis, UInt16 outmode);

            //专用信号设置函数        
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_ALM_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_ALM_PIN(UInt16 axis, UInt16 alm_logic, UInt16 alm_action);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_ALM_PIN_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_ALM_PIN_Extern(UInt16 axis, UInt16 alm_enbale, UInt16 alm_logic, UInt16 alm_all, UInt16 alm_action);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_EL_MODE", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_EL_MODE(UInt16 axis, UInt16 el_mode);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_HOME_pin_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_HOME_pin_logic(UInt16 axis, UInt16 org_logic, UInt16 filter);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_write_SEVON_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_write_SEVON_PIN(UInt16 axis, UInt16 on_off);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_read_SEVON_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_SEVON_PIN(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_read_RDY_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_RDY_PIN(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_Enable_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_Enable_EL_PIN(UInt16 axis, UInt16 enable);


            //通用输入/输出控制函数
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_read_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_inbit(UInt16 cardno, UInt16 bitno);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_write_outbit(UInt16 cardno, UInt16 bitno, UInt16 on_off);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_read_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_outbit(UInt16 cardno, UInt16 bitno);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_read_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_inport(UInt16 cardno);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_read_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_outport(UInt16 cardno);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_write_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_write_outport(UInt16 cardno, UInt32 port_value);

            //制动函数
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_decel_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_decel_stop(UInt16 axis, double Tdec);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_imd_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_imd_stop(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_emg_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_emg_stop();

            //位置设置和读取函数
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_get_position(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_position(UInt16 axis, Int32 current_position);

            //状态检测函数
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt16 d2410_check_done(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_axis_io_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt16 d2410_axis_io_status(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_rsts", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_rsts(UInt16 axis);

            //速度设置和读取函数              
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_vector_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_vector_profile(double Min_Vel, double Max_Vel, double Tacc, double Tdec);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_profile_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_profile_Extern(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Stop_Vel);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_s_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, Int32 Sacc, Int32 Sdec);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_st_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_st_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Tsacc, double Tsdec);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_read_current_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern double d2410_read_current_speed(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_read_vector_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern double d2410_read_vector_speed(UInt16 card);

            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_st_profile_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_st_profile_Extern(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Tsacc, double Tsdec, double Stop_Vel);

            //在线变速/变位
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_change_speed(UInt16 axis, double Curr_Vel);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_reset_target_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_reset_target_position(UInt16 axis, Int32 dist);

            //单轴定长运动
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_t_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_t_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_ex_t_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_ex_t_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_s_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_s_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_ex_s_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_ex_s_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);

            //单轴连续运动
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_s_vmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_s_vmove(UInt16 axis, UInt16 dir);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_t_vmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_t_vmove(UInt16 axis, UInt16 dir);

            //直线插补
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_t_line2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_t_line2(UInt16 axis1, Int32 Dist1, UInt16 axis2, Int32 Dist2, UInt16 posi_mode);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_t_line3", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_t_line3(UInt16[] axis, Int32 Dist1, Int32 Dist2, Int32 Dist3, UInt16 posi_mode);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_t_line4", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_t_line4(UInt16 cardno, Int32 Dist1, Int32 Dist2, Int32 Dist3, Int32 Dist4, UInt16 posi_mode);

            //圆弧插补
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_arc_move(UInt16[] axis, Int32[] target_pos, Int32[] cen_pos, UInt16 arc_dir);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_rel_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_rel_arc_move(UInt16[] axis, Int32[] rel_pos, Int32[] rel_cen, UInt16 arc_dir);

            //手轮运动
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_handwheel_inmode(UInt16 axis, UInt16 inmode, double multi);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_handwheel_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_handwheel_move(UInt16 axis);

            //找原点
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_home_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_home_mode(UInt16 axis, UInt16 mode, UInt16 EZ_count);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_home_move(UInt16 axis, UInt16 home_mode, UInt16 vel_mode);

            //原点锁存
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_homelatch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_homelatch_mode(UInt16 axis, UInt16 enable, UInt16 logic);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_homelatch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_homelatch_mode(UInt16 axis, ref UInt16 enable, ref UInt16 logic);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_homelatch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_get_homelatch_flag(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_reset_homelatch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_reset_homelatch_flag(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_homelatch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_get_homelatch_value(UInt16 axis);

            //多组位置比较函数
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_compare_config_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_compare_config_Extern(UInt16 card, UInt16 queue, UInt16 enable, UInt16 axis, UInt16 cmp_source);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_compare_get_config_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_compare_get_config_Extern(UInt16 card, UInt16 queue, ref UInt16 enable, ref UInt16 axis, ref UInt16 cmp_source);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_compare_clear_points_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_compare_clear_points_Extern(UInt16 card, UInt16 queue);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_compare_add_point_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_compare_add_point_Extern(UInt16 card, UInt16 queue, UInt32 pos, UInt16 dir, UInt16 action, UInt32 actpara);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_compare_get_current_point_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_compare_get_current_point_Extern(UInt16 card, UInt16 queue);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_compare_get_points_runned_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_compare_get_points_runned_Extern(UInt16 card, UInt16 queue);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_compare_get_points_remained_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_compare_get_points_remained_Extern(UInt16 card, UInt16 queue);

            //高速位置比较
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_CMP_PIN(UInt16 axis, UInt16 cmp_enable, Int32 cmp_pos, UInt16 CMP_logic);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_config_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_config_CMP_PIN(UInt16 axis, ref UInt16 cmp_enable, ref Int32 cmp_pos, ref UInt16 CMP_logic);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_read_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_read_CMP_PIN(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_write_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_write_CMP_PIN(UInt16 axis, UInt16 on_off);

            //编码器计数功能
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern Int32 d2410_get_encoder(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_encoder(UInt16 axis, UInt32 encoder_value);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_EZ_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_EZ_PIN(UInt16 axis, UInt16 ez_logic, UInt16 ez_mode);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_counter_flag(UInt16 cardno);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_reset_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_reset_counter_flag(UInt16 cardno);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_reset_clear_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_reset_clear_flag(UInt16 cardno);

            //高速锁存
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_LTC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_LTC_PIN(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_LTC_PIN_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_LTC_PIN_Extern(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode, double ltc_filter);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_latch_mode(UInt16 cardno, UInt16 all_enable);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_counter_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_counter_config(UInt16 axis, UInt16 mode);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_latch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_latch_value(UInt16 axis);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_latch_flag(UInt16 cardno);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_reset_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_reset_latch_flag(UInt16 cardno);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_triger_chunnel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_triger_chunnel(UInt16 cardno, UInt16 num);

            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_set_speaker_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_set_speaker_logic(UInt16 cardno, UInt16 logic);

            //EMG设置
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_EMG_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_EMG_PIN(UInt16 cardno, UInt16 enable, UInt16 emg_logic);

            //软件限位功能
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_config_softlimit(UInt16 axis, UInt16 ON_OFF, UInt16 source_sel, UInt16 SL_action, Int32 N_limit, Int32 P_limit);
            [DllImport(@"DMC2410\x86\Dmc2410.dll", EntryPoint = "d2410_get_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern UInt32 d2410_get_config_softlimit(UInt16 axis, ref UInt16 ON_OFF, ref UInt16 source_sel, ref UInt16 SL_action, ref Int32 N_limit, ref Int32 P_limit);


        }
    }
}
