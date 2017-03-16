using Convert.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using static Convert.Facroty;

namespace Convert
{
    public  class OpenFile
    {
        /// <summary>  
        /// 写入文件   
        /// </summary>  
        /// <param name="Content"></param>  
        /// <param name="FileSavePath"></param>  
        public static void WriteFile(string Content, string FileSavePath)
        {
            if (File.Exists(FileSavePath))
            {
                File.Delete(FileSavePath);
            }
            int indexLen = FileSavePath.LastIndexOf("\\");
            string temString = FileSavePath.Substring(0, indexLen);
            if (!Directory.Exists(temString))
            {
                Directory.CreateDirectory(temString);
            }
            FileStream fs = new FileStream(FileSavePath, FileMode.Create, FileAccess.ReadWrite);
            Byte[] bContent;
            if (FileSavePath.IndexOf("cshtml") == -1)
            {
                 bContent = System.Text.Encoding.UTF8.GetBytes(Content);
            }
            else
            {
                Encoding en = new UTF8Encoding(false, true);
                bContent = en.GetBytes(Content);
            }

            fs.Write(bContent, 0, bContent.Length);
            fs.Close();
            fs.Dispose();
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <returns></returns>
        public static string CreateDir(Tem tem, TypeModel type,string fileName)
        {
            string path = tem.menu + "\\" + fileName;
            return System.Web.HttpContext.Current.Server.MapPath(path);      
        }
    }
}