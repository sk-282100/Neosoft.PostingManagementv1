﻿using System.Runtime.Serialization;

namespace PostingManagement.UI.Exceptions
{
    [Serializable]
    public class NotFoundException :ApplicationException    
    {
        public NotFoundException(string name, object key)
            : base($"{name} ({key}) is not found")
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
