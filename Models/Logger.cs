using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Models
{
    public abstract class LogManager
    {
        private ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();
        public virtual void Write(string _fileName, string _event)
        {
            lock_.EnterWriteLock();
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(System.IO.Directory.GetCurrentDirectory() + @"\" + @"Logs\" + _fileName + ".txt", append: true, encoding: Encoding.GetEncoding(1251)))
                {
                    streamWriter.WriteLine(DateTime.Now + " : " + _event);
                }
            }
            finally
            {
                lock_.ExitWriteLock();
            }

        }
    }
}
