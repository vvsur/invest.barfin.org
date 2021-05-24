using CommandLine;

namespace TradingBot.TradeDataSender.Options.Interfaces
{
    public interface IOptionWithDryRun
    {
        [Option("dry-run", Default = false,
            HelpText = "Prevents any requests from being performed.")]
        bool IsDryRun { get; set; }
    }
}
