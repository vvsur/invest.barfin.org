using System;
using System.Text.Json;

namespace TradingBot.Data
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new Repositories.ExchangeRepository();

            //Console.WriteLine(" repository.GetTestModels():" + repository.GetTestModels().First().ToString());
            Console.WriteLine("repository.GetExchangeTickers():" + JsonSerializer.Serialize(repository.GetMarketExchangeSymbols("Binance")));
            //Console.WriteLine("repository.GetExchangeTickers():" + JsonSerializer.Serialize(repository.GetExchanges()));
            Console.ReadLine();
        }
    }
}
