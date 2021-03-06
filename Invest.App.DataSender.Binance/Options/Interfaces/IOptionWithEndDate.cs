using CommandLine;

namespace TradingBot.TradeDataSender.Options.Interfaces
{
    public interface IOptionWithEndDate
    {
        [Option("to", HelpText = "End date to filter fetched data (Format: yyyymmdd)")]
        string ToDateString { get; set; }
    }
}
