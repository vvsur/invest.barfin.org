using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TradingBot.Data.Models
{
    public class TestModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => JsonSerializer.Serialize<TestModel>(this);
    }
}
