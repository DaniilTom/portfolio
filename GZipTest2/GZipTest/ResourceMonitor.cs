using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.IO;
using System.IO.Compression;

namespace GZipTest
{
    class ResourceMonitor  //инкапсулирует и обновляет необходимые для функционирования данные
    {
        //static Int64 availableRAM;       // текущее коичество свободной оперативной памяти (но будет использоваться 80%)
        //const double percentage = 0.80;
        //public static bool EnoughRAM = true;
        public static Int32 coreNum;       // количество ядер цпу (нет смысла в вычислениях запускать потоков больше чем ядер)


        public static FileInfo inputFile;
        public static CompressionMode compMode;
        public static string outputFileName;

        //public static readonly Int32 minBlockSize = 1 * 1024 * 1024; // байт
        //public static readonly Int32 maxBlockSize = 200 * 1024 * 1024; // байт

        // теперь размер блока постоянен 10 Мб
        public static Int32 currentBlockSize = 10 * 1024 * 1024;

        public static Int64 bytesRead = 0;            //количество считанных из файла байт
        //public static readonly Int32 Delta = 5 * 1024 * 1024; // обозначает изменение размера блока за раз 

        public static void ResourceMonitorInit(string[] args) //принимает аргументы, заданные при запуске приложения
        {
            compMode = (args[0] == "compress") ? CompressionMode.Compress : CompressionMode.Decompress;

            DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] fi = currentDirectory.GetFiles();

            foreach (FileInfo f in fi)
            {
                if (f.Name == args[1])
                {
                    inputFile = f;
                    break;
                }
            }

            outputFileName = args[2];
            
            // получение количества ядер
            /*foreach (var item in new ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreNum += int.Parse(item["NumberOfCores"].ToString());
            }*/

            coreNum = Environment.ProcessorCount;

            //RamSpaceUpdate();

            //currentBlockSize = minBlockSize;
        }

        /*public static void RamSpaceUpdate()        // обновляет показатель availableRAM
        {
            foreach (var item in new ManagementObjectSearcher("Select * from Win32_OperatingSystem").Get())
            {
                availableRAM = (Int64) (1024 * percentage * int.Parse(item["FreePhysicalMemory"].ToString()));
            }   //умножение на 1024 чтобы получить значение в байтах

            // есть ли возможность увеличить размер блоков (чтоб не переполнить озу)
            // количество требуемой памяти в худшем случае = размеру очередей I/O * размер блока
            // вместимость выходной очереди ограничивается 5 элементами
            // во входной очереди coreNum * 2 блоков, чем есть потоков для них
            EnoughRAM = (GC.GetTotalMemory(true) + Delta * (coreNum * 2 + 5)) > availableRAM ? false : true;
        }*/
    }
}
