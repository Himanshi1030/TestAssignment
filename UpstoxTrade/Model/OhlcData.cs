using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UpstoxTrade.Model
{
    class OhlcData
    {

        [DisplayName("o")]
        public double Open { get; set; }

        [DisplayName("c")]
        public double Close { get; set; }
        [DisplayName("h")]
        public double High { get; set; }
        [DisplayName("l")]
        public double Low { get; set; }
        [DisplayName("volume")]
        public double Volume { get; set; }
        [DefaultValue("ohlc_notify")]
        [DisplayName("event")]
        public string Event { get; set; }
        [DisplayName("symbol")]

        public String Symbol { get; set; }
        [DisplayName("bar_num")]

        public int Bar_num { get; set; }
    }
}
