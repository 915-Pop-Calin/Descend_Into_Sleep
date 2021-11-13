using System;

namespace ConsoleApp12.Exceptions
{
    public class EmptyQueueException: Exception
    {
        public EmptyQueueException(string queueType): base(queueType + " queue is empty and cannot be dequeued\n")
        {
            
        }

        public EmptyQueueException()
        {
            
        }
    }
}