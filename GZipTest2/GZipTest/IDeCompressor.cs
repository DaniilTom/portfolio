using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZipTest
{
    interface IDeCompressor //наличие внутри классов
    {
        /// <summary>
        /// интерфейсный метод для обобщения вызова методов Compress и Decompress
        /// </summary>
        byte[] Execution(byte[] bt);

        /// <summary>
        /// способ получения блока из файла отличается у компрессора и декомпрессора
        /// </summary>
        void GetNewBlockFromFile(ReadWriteVault rwv);
    }
}
