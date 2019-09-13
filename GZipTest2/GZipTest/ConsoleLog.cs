using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZipTest
{
    static class ConsoleLog
    {
        static readonly int threadPosition;
        //static readonly int inQueuePosition;
        //static readonly int outQueuePosition;
        static readonly int bytePosition;
        static readonly int endPosition;
        static readonly int blockSizePosition;
        static Object lockOn = new Object();

        static ConsoleLog()
        {
            //Console.WriteLine("ConsoleLoger was created.");
            Console.WriteLine();
            threadPosition = Console.CursorTop;
            //inQueuePosition = Console.CursorTop + 1;
            //outQueuePosition = Console.CursorTop + 2;
            bytePosition = Console.CursorTop + 1;
            blockSizePosition = Console.CursorTop + 2;
            endPosition = Console.CursorTop + 3;

            Console.WriteLine("Threads was created: ");
            //Console.WriteLine("Input queue count: ");
            //Console.WriteLine("Output queue count: ");
            Console.WriteLine("Bytes Converted: ");
            Console.WriteLine("Current block size: ");
        }

        static public void UpdateInfo(Params p, long value, long totalFileSize = 0)
        {
            lock (lockOn)
            { 
                switch (p)
                {
                    case Params.T:
                        Console.SetCursorPosition(0, threadPosition);
                        Console.WriteLine("Threads totaly was created: {0:4}", value.ToString());
                        break;

                    /*case Params.inQueue:
                        Console.SetCursorPosition(0, inQueuePosition);
                        Console.WriteLine("Input queue count: {0:4}", value.ToString());
                        break;

                    case Params.outQueue:
                        Console.SetCursorPosition(0, outQueuePosition);
                        Console.WriteLine("Output queue count: {0:4}", value.ToString());
                        break;*/

                    case Params.curentPos:
                        Console.SetCursorPosition(0, bytePosition);
                        Console.WriteLine("Bytes read: {0} / {1}", value.ToString(), totalFileSize.ToString());
                        break;

                    case Params.blockSize:
                        Console.SetCursorPosition(0, blockSizePosition);
                        Console.WriteLine("Current block size: {0:00000} KB", value / 1024);
                        break;

                }
                Console.SetCursorPosition(0, endPosition);
            }
        }
        
        public enum Params
        {
            T = 0,
            inQueue = 1,
            outQueue = 2,
            curentPos = 3,
            blockSize = 4,
        }
    }
}
