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

namespace UpstoxTrade
{
    class Program
    {
        static void Main(string[] args)
        {
            OHLCProcess ohlc = new OHLCProcess();
            List<Trade> trades = ohlc.ReadInputJson();

            ohlc.ProcessTrades(trades.Where(x => x.StockName == "XETHZUSD").ToList());
        }
       
    }
}
