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
        public List<Trade> ReadInputJson()
        {
            var jsonData = File.ReadAllText(@"..\..\..\Input\trades.json");
                var trades = JsonConvert.DeserializeObject<List<Trade>>(jsonData);
            return trades;
        }

        public List<OhlcData> ProcessTrades(List<Trade> trades)
        {
            List<OhlcData> ohlcOutputPerTrade = new List<OhlcData>();
            List<Trade> currentIntervalTrades = new List<Trade>();
            foreach (Trade trade in trades)
            {
               
                if(currentIntervalTrades.Count() == 0)
                {
                    barStarttime = trade.TradeDateTime;
                    barEndtime = barStarttime.AddSeconds(barInterval);
                    currentIntervalTrades.Add(trade);
                }
                else
                {
                    if(trade.TradeDateTime >=  barStarttime && trade.TradeDateTime <= barEndtime)
                    {
                        currentIntervalTrades.Add(trade);
                    }
                    else if (trade.TradeDateTime > barEndtime)
                    {
                        ohlcOutputPerTrade.Add( ComputeOHLC(currentIntervalTrades , barNum));
                        barStarttime = barEndtime.AddSeconds(1);
                        barEndtime = barEndtime.AddSeconds(barInterval);
                        ++barNum;
                        currentIntervalTrades = new List<Trade>();
                        currentIntervalTrades.Add(trade);
                    }
                }
            }
            return ohlcOutputPerTrade;
        }
        public OhlcData ComputeOHLC(List<Trade> trades, int barNum)
        {
            OhlcData data = new OhlcData();
            data.Open = trades.OrderBy(x => x.Id).Select(x => x.TradePrice).FirstOrDefault();
            data.High = trades.Max(x => x.TradePrice);
            data.Low = trades.Min(x => x.TradePrice);
            data.Close = trades.OrderByDescending(x => x.Id).Select(x => x.TradePrice).FirstOrDefault();
            data.Volume = trades.Sum(x => x.QuantityTraded);
            data.Symbol = trades.Select(x => x.StockName).FirstOrDefault();
            data.Bar_num = barNum;
            return data;
        }
     
    }
}
