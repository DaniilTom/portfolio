using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
//using ResMonitor = GZipTest.ResourceMonitor;

namespace GZipTest
{
    class MainClass
    {
        static void Main(string[] args)
        {
            try // каждое из исключений является критическим и приводит к выходу из приложния
            {
                InputArgsChecker(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Приложение будет закрыто.");
                Console.WriteLine("Для продложения нажмите Enter...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("***************************");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            ReadWriteVault rwv = new ReadWriteVault();
            ResourceMonitor.ResourceMonitorInit(args);
            OperationManager om = new OperationManager(rwv);

            while (om.FileStreamInOutThread.ThreadState == System.Threading.ThreadState.Unstarted) { }
            om.FileStreamInOutThread.Join();

            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            Console.WriteLine("\n*****************************************\n");
            Console.WriteLine($"Процесс {args[0]} файла {args[1]} завершен.");
            Console.WriteLine("Прошло времени: {0:00}:{1:00}:{2:00}.{3:00}",
                                                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            FileInfo inFile = new FileInfo(Directory.GetCurrentDirectory() + "\\" + args[1]);
            FileInfo outFile = new FileInfo(Directory.GetCurrentDirectory() + "\\" + args[2]);
            Console.WriteLine("Процент сжатия составил: {0}% (было: {1} bytes, стало: {2} bytes)",
                (inFile.Length - outFile.Length)*100/inFile.Length, inFile.Length, outFile.Length);
            Console.WriteLine("Размер блока: {0}", ResourceMonitor.currentBlockSize);

            Console.ReadLine();
        }

        static void InputArgsChecker(string[] args)
        {
            // проверка правильности указания параметров

            //1. Проверка кол-ва (всего 3)                          (+)
            //2. Проверка на наличие compress/decompress            (+)
            //3. Провекра названия файлов (c учетом формата):       (+)
            //      3.1 Наличие исходного файла                     (+)
            //      3.2 Наличие/отсутствие результирующего файла    (+)


            if (args.Length != 3) throw new IndexOutOfRangeException("Количество параметров не равно 3...");

            if          (args.Contains("compress")) Console.WriteLine("Compress");
            else if     (args.Contains("decompress")) Console.WriteLine("Decompress");
            else        throw new ArgumentException("Параемтр compress/decompress не обнаружен");

                     
            DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] fi = directoryInfo.GetFiles();

            string[] filesName = new string[fi.Length];

            int i = 0;
            foreach(FileInfo f in fi) // отделение имен файлов (удобно испоьзовать Contains)
            {
                //Console.WriteLine("{0}", f.Name);
                filesName[i++] = f.Name;
            }
            
            if (!filesName.Contains(args[1])) throw new FileNotFoundException("Исходный файл " + args[1] +
                                                                                    " не обнаружен...");

            if (args[0] == "compress" && (args[2].Substring(args[2].IndexOf('.')) != ".gz"))
                throw new FileNotFoundException("Неверное расширение результрующего файла (ожидается *.gz).");

            if (args[0] == "decompress" && (args[1].Substring(args[1].IndexOf('.')) != ".gz"))
                throw new FileNotFoundException("Неверное расширение исходного файла (ожидается *.gz).");


            if (filesName.Contains(args[2]))
            {
                Console.WriteLine($"Результирующий файл {args[2]} уже существует" +
                " и будет перезаписан." + Environment.NewLine + "Продолжить? [Y/N]");

                string ans = Console.ReadLine();

                while (!((ans == "Y") || (ans == "N")))
                {
                    Console.WriteLine("Попробуйте еще раз");
                    ans = Console.ReadLine();
                }

                if (ans == "N") throw new Exception("В перезаписи отказано...");
            } 
        }
    }
}
