using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab1;
using Lab1.Forms;

namespace Lab1
{
    public static class Saver { 
    public static void SaveToFile(string content, string path, bool isDeposit)
        {
            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
            }
            using (StreamWriter w = new StreamWriter (path, true)) 
            {
                if (isDeposit)
                    w.WriteLine("Deposit");
                else
                    w.WriteLine("Credit");
                w.WriteLine(DateTime.Now.ToString());
                w.WriteLine(content);
                w.WriteLine("########################################################");
                
            }
        }
    
    
    }
}

