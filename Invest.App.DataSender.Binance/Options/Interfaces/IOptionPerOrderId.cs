using CommandLine;

namespace TradingBot.TradeDataSender.Options.Interfaces
{
    public interface IOptionPerOrderId
    {
        [Option("order-id", HelpText = "The order id.", Required = true)]
        string OrderId { get; set; }
    }
}
