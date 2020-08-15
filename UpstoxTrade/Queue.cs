using System;
using System.Collections.Generic;
using System.Text;
using UpstoxTrade.Model;

namespace UpstoxTrade
{
    static class Queue
    {
        public static Queue<Trade> tradesqueue = new Queue<Trade>();
    }
}
