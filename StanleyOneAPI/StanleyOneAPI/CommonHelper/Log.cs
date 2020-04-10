using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StanleyOneAPI.CommonHelper
    {
    public  class Log
        {
        public void WriteErrorLog(string Message)
            {
            StreamWriter sw = null;
            try
                {
                sw = new StreamWriter(GetBasePath() + "\\LogFile.txt", true);
                sw.WriteLine();
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
                }
            catch
                {
                }
            }

        public  void WriteExceptionLog(Exception ex)
            {
            StreamWriter sw = null;
            try
                {
                sw = new StreamWriter(GetBasePath()+ "\\LogFile.txt", true);
                sw.WriteLine();
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                
                sw.Flush();
                sw.Close();
                }
            catch
                {
                }
            }

        public static string GetBasePath()
            {
            if (System.Web.HttpContext.Current == null) return AppDomain.CurrentDomain.BaseDirectory;
            else return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            }

        }
    }