using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZipTest
{
    class ReadWriteVault
    {
        // синхронизация доступа непосредственно к очереди переносится в OperationManager.DoWork()

        Queue<byte[]> inQueue = new Queue<byte[]>();
        Queue<byte[]> outQueue = new Queue<byte[]>();

        public int inQueueCount
        {
            get
            {
                return inQueue.Count;
            }
        }

        public int outQueueCount
        {
            get
            {
                return outQueue.Count;
            }
        }

        public byte[] get_inQueue()
        {
            return inQueue.Dequeue();
        }

        public void set_inQueue(byte[] bt)
        {
            inQueue.Enqueue(bt);
        }

        public byte[] get_outQueue()
        {
            return outQueue.Dequeue();
        }

        public void set_outQueue(byte[] bt)
        {
            outQueue.Enqueue(bt);
        }
    }
}
