using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TradingBot.Data.Models
{
    public class MarketSymbol
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public override string ToString() => JsonSerializer.Serialize<MarketSymbol>(this);
    }
}
