using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
namespace Basic
{
    static public class LicenseLib
    {
        public static string GetComputerInfo()
        {
            string info = string.Empty;
            info = string.Format($"{GetMACAddress()}#{GetCPUInfo()}");
            return info;
        }

        private static string GetCPUInfo()
        {
            string info = "";
            ManagementClass mo;
            ManagementObjectCollection moc;
            mo = new ManagementClass("Win32_Processor");
            moc = mo.GetInstances();
            foreach (ManagementObject value in moc)
            {
                info = value.Properties["ProcessorId"].Value.ToString();
                break;
            }
            return info;
        }
        private static string GetMACAddress()
        {
            string macAddress = string.Empty;

            try
            {
                ManagementClass networkAdapterClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection networkAdapterCollection = networkAdapterClass.GetInstances();

                foreach (ManagementObject networkAdapter in networkAdapterCollection)
                {
                    if ((bool)networkAdapter["IPEnabled"])
                    {
                        macAddress = networkAdapter["MacAddress"].ToString();
                        break; // 如果只需获取一个可用的 MAC 地址，可以使用 break 结束循环
                    }
                }
            }
            catch (ManagementException ex)
            {
                // 处理异常情况
                Console.WriteLine("An error occurred while retrieving MAC address: " + ex.Message);
            }

            return macAddress;
        }
        private static string GetUserAccount()
        {
            string info = "";
            ManagementClass mo;
            ManagementObjectCollection moc;
            mo = new ManagementClass("Win32_UserAccount");
            moc = mo.GetInstances();
            foreach (ManagementObject value in moc)
            {
                info = value.Properties["Name"].Value.ToString();
                break;
            }
            return info;
        }
        private static string GetBIOSInfo()
        {
            string info = "";
            ManagementClass mo;
            ManagementObjectCollection moc;
            mo = new ManagementClass("Win32_BIOS");
            moc = mo.GetInstances();
            foreach (ManagementObject value in moc)
            {
                info = value.Properties["SerialNumber"].Value.ToString();
                break;
            }
            return info;
        }
        private static string GetBaseBoardInfo()
        {
            string info = "";
            ManagementClass mo;
            ManagementObjectCollection moc;
            mo = new ManagementClass("Win32_BaseBoard");
            moc = mo.GetInstances();
            foreach (ManagementObject value in moc)
            {
                info = value.Properties["Product"].Value.ToString();
                break;
            }
            return info;
        }
    }
}
