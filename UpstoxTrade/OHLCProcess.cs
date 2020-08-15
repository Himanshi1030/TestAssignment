using System;
using System.Collections.Generic;
using System.Text;
using UpstoxTrade.Model;
using Newtonsoft;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace UpstoxTrade
{
    class OHLCProcess
    {
        static int barInterval = 15;
        static DateTime barStarttime = new DateTime();
        static DateTime barEndtime = new DateTime();
        static int barNum = 1;
        static List<OhlcData> ohlcOutputPerTrade = new List<OhlcData>();
        static List<Trade> tradesList = new List<Trade>();
        static double tradeClose = 0;
        
        public List<Trade> ReadInputJson(string stockName)
        {
            var jsonData = File.ReadAllText(@"..\..\..\Input\trades.json");
                var trades = JsonConvert.DeserializeObject<List<Trade>>(jsonData);

            foreach(Trade t in trades)
            {
                if(stockName.Equals(t.StockName))
                Queue.tradesqueue.Enqueue(t);
            }
            return trades;
        }
       

        public void ProcessTrades()
        {
            List<Trade> currentIntervalTrades = new List<Trade>();
            
            while(Queue.tradesqueue.Count() > 0)
            {
                Trade newtrade = Queue.tradesqueue.Dequeue();
                if(currentIntervalTrades.Count() == 0)
                {
                    barStarttime = newtrade.TradeDateTime;
                    barEndtime = barStarttime.AddSeconds(barInterval);
                    currentIntervalTrades.Add(newtrade);
                }
                else
                {
                    if(newtrade.TradeDateTime >=  barStarttime && newtrade.TradeDateTime <= barEndtime)
                    {
                        currentIntervalTrades.Add(newtrade);
                    }
                    else if (newtrade.TradeDateTime > barEndtime)
                    {
                        ohlcOutputPerTrade.Add( ComputeOHLC(currentIntervalTrades , barNum));
                        barStarttime = barEndtime.AddSeconds(1);
                        barEndtime = barEndtime.AddSeconds(barInterval);
                        ++barNum;
                        currentIntervalTrades = new List<Trade>();
                        currentIntervalTrades.Add(newtrade);
                    }
                }
            }
            Consoleoutput();
           
        }
        private OhlcData ComputeOHLC(List<Trade> trades, int barNum)
        {
            OhlcData data = new OhlcData();
            data.Open = barNum==1 ? trades.OrderBy(x => x.Id).Select(x => x.TradePrice).FirstOrDefault() : tradeClose;
            data.High = trades.Max(x => x.TradePrice) < data.Open ? data.Open : trades.Max(x => x.TradePrice);
            data.Low = trades.Min(x => x.TradePrice)> data.Open ? data.Open : trades.Min(x => x.TradePrice);
            data.Close = trades.OrderByDescending(x => x.Id).Select(x => x.TradePrice).FirstOrDefault();
            data.Volume = trades.Sum(x => x.QuantityTraded);
            data.Symbol = trades.Select(x => x.StockName).FirstOrDefault();
            data.Bar_num = barNum;
            tradeClose = data.Close;
            return data;
        }
     
        public void Consoleoutput()
        {
            if(ohlcOutputPerTrade.Count()>0)
            {
                Console.WriteLine("\n===Stock:{0}====\n", ohlcOutputPerTrade.Select(x => x.Symbol).FirstOrDefault());
                foreach(var output in ohlcOutputPerTrade.Take(100))
                {
                    var result = JsonConvert.SerializeObject(output);
                    Console.WriteLine("\n{0}",result);
                }
            }
        }
    }
}
