using System.Collections.Generic;
using CommandLine;

namespace TradingBot.TradeDataSender.Options.Interfaces
{
    public interface IOptionWithMultipleMarketSymbol
    {
        [Option(
            's', "symbols",
            HelpText = "Symbol (currency pair) to be fetched from the exchange.\n" +
                       "Comma delimited list.",
            Separator = ','
        )]
        IEnumerable<string> MarketSymbols { get; set; }
    }
}
