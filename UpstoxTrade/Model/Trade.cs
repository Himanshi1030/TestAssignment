using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Newtonsoft.Json;

namespace UpstoxTrade.Model
{
    class Trade
    {
        private static int trade_count = 0;
        Trade()
        {
            this.Id = ++trade_count;
        }
       
        [JsonProperty("sym")]
        public string StockName { get; set; }

        [JsonProperty("P")]
        public double TradePrice { get; set; }

        [JsonProperty("Q")]
        public double QuantityTraded { get; set; }

        [JsonProperty("TS2")]
        public Int64 TimeStamp { get; set; }

        public DateTime TradeDateTime
        {
            get
            {
                DateTime unixepocDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                //Convert Nano seconds  to Seconds
                DateTime datetime = unixepocDateTime.AddSeconds(TimeStamp / 1000000000);

                return DateTime.ParseExact(datetime.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", null);
            }
        }
        public int Id { get; set; }

    }
}
