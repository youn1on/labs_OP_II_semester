using System;

namespace TextFilesProcessing.Model
{
    public class NoFilesException : Exception
    {
        public NoFilesException() {}

        public NoFilesException(string message): base(message)
        {
            
        }
       
    }

    public class AllInvalidFilesException : Exception
    {
        public AllInvalidFilesException() {}

        public AllInvalidFilesException(string message) : base(message)
        {
            
        }
    }

    public class InvalidFileException : Exception
    {
        public InvalidFileException() {}

        public InvalidFileException(string message) : base(message)
        {
            
        }
    }
}
