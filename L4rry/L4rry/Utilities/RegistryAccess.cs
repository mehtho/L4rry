using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4rry.Utilities
{
    class RegistryAccess
    {
        public static String GetToken()
        {
            /*using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\L4rry\Key", true))
            {
                if (key != null)
                {
                    Object o = key.GetValue("Key");
                    if (o != null && !o.Equals("KEY HERE"))
                    {
                        return o.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Registry entry was null");
                    }
                }
                else
                {
                    WriteKey("KEY HERE");
                    return key.ToString();
                }
            }
            throw new KeyNotFoundException();*/
            return "NDg1MTI2NjcwNTA0Njg5Njg4.DzhIUg.AstqR0gX0Tt6_9tD-lfG6CXsWDc";
        }

        private static void WriteKey(String value)
        {
            RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).CreateSubKey(@"SOFTWARE", true);

            key.CreateSubKey("L4rry");
            key = key.OpenSubKey("L4rry", true);

            if (key.GetValue("Key") == null)
            {
                key.SetValue("Key", value);
            }
        }
    }
}
