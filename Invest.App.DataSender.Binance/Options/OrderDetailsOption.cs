using System;
using System.Threading.Tasks;
using CommandLine;
using TradingBot.TradeDataSender.Options.Interfaces;

namespace TradingBot.TradeDataSender.Options
{
    [Verb("order-details", HelpText = "Fetch the order details from the exchange.")]
    public class OrderDetailsOption : BaseOption, IOptionPerExchange, IOptionPerOrderId, IOptionWithMarketSymbol
    {
        public override async Task RunCommand()
        {
            using var api = GetExchangeInstance(ExchangeName);

            Authenticate(api);

            var orderDetails = await api.GetOrderDetailsAsync(OrderId, MarketSymbol);
            Console.WriteLine(orderDetails);

            WaitInteractively();
        }

        public string ExchangeName { get; set; }

        public string OrderId { get; set; }

        public string MarketSymbol { get; set; }
    }
}
