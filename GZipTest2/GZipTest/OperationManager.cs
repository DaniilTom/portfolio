using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
//using ResMonitor = GZipTest.ResourceMonitor;

namespace GZipTest
{
    class OperationManager
    {
        IDeCompressor De_Compressor;
        //ResourceMonitor ResourceMonitor;
        ReadWriteVault vault;
        //ConsoleLog ConsoleLog;
        // группа потоков для выполнения соответствующих методов
        NamedThread[] ExecutionThreadPool;
        public Thread FileStreamInOutThread;
        static Int32 NowWaitingForThreadName = 1; // надо для сохранения порядка извлечении и записи в очереди
        static Int32 LastThreadName = 1;
        bool IsWorking; // true - если работает хоть один поток комп/декомп, false в противном случае

        ManualResetEvent mreIn;
        ManualResetEvent mreOut;


        FileStream outputFileStream;
        FileStream inputFileStream;
                

        public OperationManager(ReadWriteVault rwv)
        {
            //System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            ExecutionContext.SuppressFlow();

            //ResourceMonitor = rm;
            vault = rwv;
            mreIn = new ManualResetEvent(true);
            mreOut = new ManualResetEvent(true);

            if (ResourceMonitor.compMode == System.IO.Compression.CompressionMode.Compress)
                De_Compressor = new Compressor(out inputFileStream);
            else De_Compressor = new Decompressor(out inputFileStream);

            ExecutionThreadPool = new NamedThread[ResourceMonitor.coreNum];

            outputFileStream = new FileStream(ResourceMonitor.outputFileName, FileMode.Create);

            FileStreamInOutThread = new Thread(FileStreamInOutRoutine);
            FileStreamInOutThread.Priority = ThreadPriority.Highest;

            IsWorking = true;

            FileStreamInOutThread.Start();
            ThreadBuilder();
        }

        /// <summary>
        /// создает потоки
        /// </summary>
        void ThreadBuilder()
        {
            
            for (int i = 0; (i < ResourceMonitor.coreNum ); i++) // теперь потоки создаются один раз и работают до конца
            {
                ExecutionThreadPool[i] = new NamedThread(vault, De_Compressor, this);
            }

            ConsoleLog.UpdateInfo(ConsoleLog.Params.T, ResourceMonitor.coreNum);
            ConsoleLog.UpdateInfo(ConsoleLog.Params.blockSize, ResourceMonitor.currentBlockSize); 
        }

        /// <summary>
        /// Метод для управления I/O потоками
        /// </summary>
        void FileStreamInOutRoutine()
        {
            while (vault.outQueueCount > 0 || IsWorking)
            {
                // контроль входной очереди
                /*if*/while ((vault.inQueueCount < ResourceMonitor.coreNum)
                    && (ResourceMonitor.bytesRead != ResourceMonitor.inputFile.Length))
                {
                    De_Compressor.GetNewBlockFromFile(vault);
                    //ConsoleLog.UpdateInfo(ConsoleLog.Params.inQueue, vault.inQueueCount);
                    ConsoleLog.UpdateInfo(ConsoleLog.Params.curentPos, ResourceMonitor.bytesRead,
                        ResourceMonitor.inputFile.Length);
                }
                mreIn.Set();

                // контроль выходной очереди
                /*if*/
                while (vault.outQueueCount > 0)
                {
                    byte[] bt = vault.get_outQueue();
                    outputFileStream.Write(bt, 0, bt.Length);
                    //ConsoleLog.UpdateInfo(ConsoleLog.Params.outQueue, vault.outQueueCount);
                }
                mreOut.Set();
            }

            inputFileStream.Close();
            outputFileStream.Flush();
            outputFileStream.Close();
        }

        /// <summary>
        /// Вспомогательный метод, предотвращающий преждевременное завершение FileStreamInOutThread.
        /// </summary>
        /// <returns></returns>
        void IsWorkingChecker()
        {
            //if (ResourceMonitor.bytesRead != ResourceMonitor.inputFile.Length) return true;

            for (int i = 0; i < ResourceMonitor.coreNum ; i++) // проверка состояния потоков
            {
                if (ExecutionThreadPool[i].Name != 0) return;
            }

            IsWorking = false;
        }

        // Для oганизации порядка доступа потоку будет присваиваться
        // определенный идентификатор (если бы свойтво Thread.Name можно было менять
        // во время выполнения потока, этот класс был бы не нужен)

        class NamedThread 
        {
            Thread thread;
            public Int32 Name { set; get; }
            static Object lockOn = new Object();
            static Object threadLockOn = new Object();

            ManualResetEvent mreIn;
            ManualResetEvent mreOut;

            ReadWriteVault vault;
            OperationManager om;
            IDeCompressor De_Compressor;

            public NamedThread(ReadWriteVault _rwv, IDeCompressor _dc, OperationManager _om)
            {
                vault = _rwv;
                De_Compressor = _dc;
                om = _om;
                mreIn = om.mreIn;
                mreOut = om.mreOut;

                thread = new Thread(DoWork);
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }

            void DoWork()
            {
                while (vault.inQueueCount > 0 || ResourceMonitor.bytesRead != ResourceMonitor.inputFile.Length)
                {
                    
                    Monitor.Enter(NamedThread.lockOn);

                    if (vault.inQueueCount > 0) // повторная проверка на случай внезапно неуместного переключения потоков
                    {

                        byte[] byteMas = vault.get_inQueue();
                        this.Name = LastThreadName++;

                        Monitor.Exit(NamedThread.lockOn);

                        // используется тоже имя, чтобы не плодить переменных
                        byteMas = De_Compressor.Execution(byteMas);

                        // ограничение выходной очереди
                        if(vault.outQueueCount > ResourceMonitor.coreNum)
                        {
                            mreOut.Reset();
                            mreOut.WaitOne();
                        }


                        // синхронизация доступа к выходной очереди
                        while (Name != NowWaitingForThreadName) { Thread.Sleep(1); }
                        
                        vault.set_outQueue(byteMas);
                        NowWaitingForThreadName++;
                    }
                    else
                    {
                        Monitor.Exit(NamedThread.lockOn);
                        mreIn.Reset();
                        mreIn.WaitOne();
                    }

                    //ConsoleLog.UpdateInfo(ConsoleLog.Params.inQueue, vault.inQueueCount);
                }

                Name = 0; // когда поток подходит к концу исполнения, он обнуляет св-во Name 
                                                            // текущего экземпляра (оно будет отслеживаться далее)
                lock(NamedThread.lockOn) om.IsWorkingChecker(); //замена методу IsWorking(), теперь вызывается только
                                                                // в конце потоков комп/декомп
            }
        }
    }
}
