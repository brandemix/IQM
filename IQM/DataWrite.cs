using System;

namespace IQM
{
    public interface IDataWrite
    {
        void Write(string state);
    }

    class ConsoleWriter: IDataWrite {
        void IDataWrite.Write(string state)
        {
            Console.WriteLine(state);
        }
    }
}