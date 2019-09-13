using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace GZipTest
{
    class Decompressor : IDeCompressor
    {
        //ReadWriteVault vault;
        //ResourceMonitor resourceMonitor;
        FileStream inputFS;

        public Decompressor(out FileStream fs)
        {
            //vault = rwv;

            fs = new FileStream(ResourceMonitor.inputFile.Name, FileMode.Open);
            inputFS = fs;
        }

        public byte[] Execution(byte[] bt)
        {
            return Decompress(bt);
        }

        public void GetNewBlockFromFile(ReadWriteVault rwv)
        {
            // при декомпрессии размер блока выбрать не возможно,
            // поэтому просто 50 Мб буфера

            // старый вариант обнаружения блока - работает очень долго
            /*for (long i = 0; i < inputFS.Length; i++)
            {
                long BeginPos = inputFS.Position;
                // найдено начало блока
                if ((inputFS.ReadByte() == 0x1f) && (inputFS.ReadByte() == 0x8b) && (inputFS.ReadByte() == 0x08))
                {
                    for (long n = i; n < inputFS.Length; n++)
                    {
                        //найден конец блока
                        if (((inputFS.ReadByte() == 0x1f) && (inputFS.ReadByte() == 0x8b) && (inputFS.ReadByte() == 0x08)) ||
                            (inputFS.Position == inputFS.Length))
                        {
                            // считывание блока

                            byte[] temp;

                            if (inputFS.Position == inputFS.Length) temp = new byte[inputFS.Length - resourceMonitor.bytesRead];
                            else temp = new byte[inputFS.Position - 3 - BeginPos];

                            inputFS.Position = BeginPos;
                                
                            inputFS.Read(temp, 0, temp.Length);

                            while (rwv.inQueueCount() > 5) { }
                            rwv.set_inQueue(temp);

                            resourceMonitor.bytesRead += temp.Length;
                            inputFS.Flush();
                            break;
                        }
                    }
                    break;
                }
            }*/

            // теперь перед блоком указан его размер
            byte[] BlockSize = new byte[sizeof(int)];
            inputFS.Read(BlockSize, 0, sizeof(int));

            int size = BitConverter.ToInt32(BlockSize,0);

            byte[] temp = new byte[size];

            inputFS.Read(temp, 0, size);

            while (rwv.inQueueCount > 5) { }
            rwv.set_inQueue(temp);

            ResourceMonitor.bytesRead = ResourceMonitor.bytesRead + temp.Length + sizeof(int);
            inputFS.Flush();
            //if (resourceMonitor.bytesRead == inputFS.Length) inputFS.Close();

        }

        byte[] Decompress(byte[] bt)
        {
            
            
            int DecompressedBlockSize = BitConverter.ToInt32(bt, bt.Length - 4);
            byte[] temp = new byte[DecompressedBlockSize];

            using (MemoryStream source = new MemoryStream())
            {
                source.Write(bt, 0, bt.Length);
                source.Position = 0;

                    using (GZipStream decompressor = new GZipStream(source, CompressionMode.Decompress))
                    {
                        //temp = new byte[(int)decompressor.Length
                        decompressor.Read(temp, 0, DecompressedBlockSize); // ожидается длинна меньше 4 Гб
                    }
                
            }

            return temp;
        }
    }
}
