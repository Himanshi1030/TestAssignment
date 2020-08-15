using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UpstoxTrade.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpstoxTrade
{
    class Program
    {
        static void Main(string[] args)
        {
            Task _reader = Task.Run(Reader);
            _reader.Wait();
            Task _compute = Task.Run(Compute);
            _compute.Wait();

           
        }



        static async Task Reader()
        {
            OHLCProcess ohlc = new OHLCProcess();
            ohlc.ReadInputJson("XETHZUSD");
           
        }
        static async Task Compute()
        {
            OHLCProcess ohlc = new OHLCProcess();
            ohlc.ProcessTrades();
        }

    }
}
