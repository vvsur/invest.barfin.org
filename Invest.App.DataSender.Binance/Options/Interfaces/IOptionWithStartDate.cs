using CommandLine;

namespace TradingBot.TradeDataSender.Options.Interfaces
{
    public interface IOptionWithStartDate
    {
        [Option("since", HelpText = "Start date to filter fetched data (Format: yyyymmdd)")]
        string SinceDateString { get; set; }
    }
}
