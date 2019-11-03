using System;
using System.IO;

namespace IQM
{
    public interface IDataRead
    {
        string Read();
    }

    public class FileRead: IDataRead, IDisposable
    {
        StreamReader reader;
        
        string IDataRead.Read()
        {
            return this.reader.ReadLine();
        }

        void IDisposable.Dispose()
        {
            this.reader.Dispose();
        }

        public FileRead(string filePath)
        {
            this.reader = new StreamReader(filePath);
        }
    }
}