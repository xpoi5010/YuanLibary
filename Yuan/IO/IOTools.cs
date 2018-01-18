using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
namespace Yuan.IO
{
    public static class IOTools
    {

        /// <summary>
        /// 讀取一個目錄底下的所有目錄(包含目錄底下的目錄的子目錄的子目錄)
        /// </summary>
        /// <param name="path">要讀取的目錄</param>
        /// <returns></returns>
        public static string[] GetAllDirectories(string path)
        {
            List<string> temp = new List<string>() ;
            temp.AddRange(Directory.GetDirectories(path));
            for (int i = 0; i < temp.Count; i++)
            {
                temp.AddRange(Directory.GetDirectories(temp[i]));
            }
            return temp.ToArray();
        }

        /// <summary>
        /// 讀取一個目錄底下的所有檔案(包含目錄底下的目錄的子目錄的檔案)，但不包含目錄。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetAllFiles(string path)
        {
            List<string> temp = new List<string>();
            List<string> Files = new List<string>();
            Files.AddRange(Directory.GetFiles(path));
            temp.AddRange(Directory.GetDirectories(path));
            for (int i = 0; i < temp.Count; i++)
            {
                temp.AddRange(Directory.GetDirectories(temp[i]));
                Files.AddRange(Directory.GetFiles(temp[i]));
            }
            return Files.ToArray();
        }

        /// <summary>
        /// 取得一個目錄底下的所有檔案、資料夾(包含子目錄的檔案、資料夾、子目路的子目錄的檔案、資料夾)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetAll(string path)
        {
            List<string> temp = new List<string>();
            List<string> output = new List<string>();
            output.AddRange(Directory.GetFiles(path));
            output.AddRange(Directory.GetDirectories(path));
            temp.AddRange(Directory.GetDirectories(path));
            for (int i = 0; i < temp.Count; i++)
            {
                temp.AddRange(Directory.GetDirectories(temp[i]));
                output.AddRange(Directory.GetFiles(temp[i]));
                output.AddRange(Directory.GetDirectories(temp[i]));
            }
            return output.ToArray();
        }
    }
}
