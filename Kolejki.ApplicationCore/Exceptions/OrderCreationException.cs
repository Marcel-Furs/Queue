﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolejki.ApplicationCore.Exceptions
{
    public class OrderCreationException : Exception
    {
        public OrderCreationException(string message) : base(message)
        {
            
        }
    }
}
