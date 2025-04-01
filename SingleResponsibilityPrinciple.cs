using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsusingC_
{
    internal class SingleResponsibilityPrinciple
    {
        public static bool WriteFile(string fileName, string data)
        {
            try
            {
                File.WriteAllText(@"D:\" + fileName, data);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        // Against Single Responsibility Principle, we shall keep reading and persistence logic separate
        public static string ReadFile(string fileName)
        {
            return string.Empty;
        }        
    }    
}
