using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace FileSplittingAndMerging
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> fileCollection;
            string sourceFile = @"D:\testFolder\test.txt";
            int noOfFiles = 10;
            SplitFile(sourceFile, noOfFiles);
            fileCollection = ReadSplittedFiles();
            foreach (string file in fileCollection)
            {
                Thread thread = new Thread(() => Send(file));
                thread.Start();
            }
            Console.ReadKey();
        }
        public static List<string> ReadSplittedFiles()
        {
            string[] array1;
            array1 = Directory.GetFiles(@"D:\testFolder\", "*.tmp");
            List<string> list = array1.Cast<string>().ToList();
            return list;
        }

        public static void SplitFile(string source, int noOfFiles)
        {
            try
            {
                FileStream fs = new FileStream(source, FileMode.Open, FileAccess.Read);
                int sizeOFEachFile = (int)Math.Ceiling((double)fs.Length / noOfFiles);

                for (int i = 0; i < noOfFiles; i++)
                {
                    string baseFileName = Path.GetFileNameWithoutExtension(source);
                    string extension = Path.GetExtension(source);

                   FileStream outputFile = new FileStream(Path.GetDirectoryName(source) + "\\" + baseFileName + "." + i.ToString() + extension + ".tmp", FileMode.Create, FileAccess.Write);
                    
                     string mergeFolder;
                     mergeFolder = Path.GetDirectoryName(source);

                     int bytesRead = 0;
                     byte[] buffer = new byte[sizeOFEachFile];

                     if ((bytesRead = fs.Read(buffer, 0, sizeOFEachFile)) > 0)
                     {
                         outputFile.Write(buffer, 0, bytesRead);
                         string packet = baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + extension.ToString();
                         
                     }
                     outputFile.Close();
                }
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }   
        }

        public static void Send(string location)
        {
            Console.WriteLine("Hello");
        }

    }
}
