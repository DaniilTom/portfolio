using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace GZipTest
{
    class Compressor : IDeCompressor
    {
        //ReadWriteVault vault;
        //ResourceMonitor resourceMonitor;
        FileStream inputFS;

        public Compressor(out FileStream fs)
        {
            //vault = rwv;
            //resourceMonitor = rm;
            fs = new FileStream(ResourceMonitor.inputFile.Name, FileMode.Open);
            inputFS = fs;
        }

        public byte[] Execution(byte[] bt)
        {
            return Compress(bt);
        }

        public void GetNewBlockFromFile(ReadWriteVault rwv)   // добавляет новый блок в общую очередь
        {
            //ResourceMonitor.RamSpaceUpdate();

            // код изменения размера блока (для выравнивания производительноти жесткого диска и процессора)
            // работает только при компрессии
        
            /*if ((rwv.outQueueCount - rwv.inQueueCount < 2) &&
                (resourceMonitor.currentBlockSize < resourceMonitor.maxBlockSize) && resourceMonitor.EnoughRAM)
                resourceMonitor.currentBlockSize += resourceMonitor.Delta;
            else if (rwv.outQueueCount == rwv.inQueueCount) { }
            else if ((rwv.inQueueCount - rwv.outQueueCount < 2) && (
                resourceMonitor.currentBlockSize > resourceMonitor.minBlockSize))
                resourceMonitor.currentBlockSize -= resourceMonitor.Delta;*/


            /*if ((rwv.outQueueCount == rwv.inQueueCount) &&
                (ResourceMonitor.currentBlockSize < ResourceMonitor.maxBlockSize) && ResourceMonitor.EnoughRAM)
                ResourceMonitor.currentBlockSize += ResourceMonitor.Delta;*/


            Int32 length = 0;

            //длинна будет не больше количества оставшихся байтов
            if (inputFS.Length - inputFS.Position >= ResourceMonitor.currentBlockSize)
                length = ResourceMonitor.currentBlockSize;
            else length = (int) (inputFS.Length - inputFS.Position);  //безопасное преобразование,
                                        //потерь не будет, т.к. разница не больше resourceMonitor.currentBlockSize

            byte[] temp = new byte[length];
            int i = inputFS.Read(temp, 0, length);
            rwv.set_inQueue(temp);
            ResourceMonitor.bytesRead += i;

            inputFS.Flush();
        }

        byte[] Compress(byte[] bt)
        {
            byte[] temp;
            using (MemoryStream reciever = new MemoryStream())
            {
                using (GZipStream compressor = new GZipStream(reciever, CompressionMode.Compress))
                {
                    compressor.Write(bt, 0, bt.Length);
                }
                    
                temp = reciever.ToArray();
            }

            //перед блоком добавляется размер этого блока
            //хотя формат несколько нарушается, декомпрессия может быть выполнена в разы быстрее

            byte[] tLength = BitConverter.GetBytes(temp.Length);
            byte[] result = new byte[temp.Length + sizeof(int)];

            Buffer.BlockCopy(tLength, 0, result, 0, tLength.Length);
            Buffer.BlockCopy(temp,0,result,sizeof(int),temp.Length);

            return result;

        }
    }
}
