﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.Exceptions
{
    class MoveException : Exception
    {
        public MoveException(string message) : base(message + "\n")
        {

        }
    }
}
