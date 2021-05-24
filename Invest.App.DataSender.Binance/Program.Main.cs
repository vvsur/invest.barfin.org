using ExchangeSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Security;

namespace TradingBot.TradeDataSender
{
    public partial class Program
    {
        internal const int ExitCodeError = -99;
        internal const int ExitCodeOk = 0;
        internal const int ExitCodeErrorParsing = -1;

        public static Program Instance { get; } = new Program();


        //public string ExchangeName { get; set; }

        //public IEnumerable<string> MarketSymbols { get; set; }

        static ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };


        static void SentData(string queueName, string message)
        {

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);


                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: Encoding.UTF8.GetBytes(message));

                Console.WriteLine(" [x] Sent " + queueName);
                //Console.WriteLine(" [x] message: " + message);
            }
        }


        public static async Task<int> Main(string[] args)
        {

            // create a web socket connection to Binance. Note you can Dispose the socket anytime to shut it down.
            using var api = new ExchangeBinanceAPI();

            var privateApiKeyString = "";
            var privateApiKey = new SecureString();
            Array.ForEach(privateApiKeyString.ToArray(), privateApiKey.AppendChar);
            privateApiKey.MakeReadOnly();

            var publicApiKeyString = "";
            var publicApiKey = new SecureString();
            Array.ForEach(publicApiKeyString.ToArray(), publicApiKey.AppendChar);
            publicApiKey.MakeReadOnly();


            api.PrivateApiKey = privateApiKey;
            api.PublicApiKey = publicApiKey;

            //api.Ke
            // the web socket will handle disconnects and attempt to re-connect automatically.
            //using var socket = await api.GetTradesWebSocketAsync

            //var symbols = await api.ValidateMarketSymbolsAsync(api, MarketSymbols.ToArray());




            var marketSymbols = await api.GetMarketSymbolsAsync();
            var marketSymbols1 = marketSymbols.ToArray().Where(x => x.IndexOf("USDT") > -1 || x.IndexOf("ETH") > -1 || x.IndexOf("BTC") > -1);
            //var marketSymbols2 = marketSymbols.ToArray().Where(x => x.IndexOf("BNB") > -1);

            //SentData("Binance_GetMarketSymbols", JsonSerializer.Serialize(marketSymbols));
            ///////////////////////



            ///////////////////////
            //var tickers = await api.GetTickersWebSocketAsync(message =>
            //{
            //    SentData("Binance_GetTickers", JsonSerializer.Serialize(message));
            //});
            ///////////////////////



            /////////////////////////
            var repository = new Data.Repositories.ExchangeRepository();
            //var symbols = repository.GetMarketExchangeSymbols("Binance");

            using var trades = await api.GetTradesWebSocketAsync(message =>
            {
                //Console.WriteLine("socket!!!! start");
                //Console.WriteLine($"{message.Key}: {message.Value}");
                Console.WriteLine(JsonSerializer.Serialize(message).ToString());
                SentData("GetTrades.Binance", JsonSerializer.Serialize(message));
                return Task.CompletedTask;

                //}, marketSymbols1.ToArray());
            }, new String[] { "ADADOWNUSDT" });
            ///////////////////////



            //using var trades2 = await api.GetTradesWebSocketAsync(message =>
            //{
            //    Console.WriteLine("socket2 start");
            //    Console.WriteLine($"{message.Key}: {message.Value}");
            //    return Task.CompletedTask;

            //}, marketSymbols2.ToArray());









            api.CancelOrderAsync("12").Sync();















            //using var trades2 = await api.GetTradesWebSocketAsync(message =>
            //{
            //    Console.WriteLine("socket2 start");
            //    Console.WriteLine($"{message.Key}: {message.Value}");
            //    return Task.CompletedTask;

            //}, marketSymbols.TakeLast(950).ToArray());



            //var repository = new Models.TestModelRepository();
            //Binance

            //    var marketSymbols = new String[] { "ETHBTC" };

            //    marketSymbols = marketSymbols.ToArray().Take(950).ToArray();


            //    using var socket2 = await api.GetTradesWebSocketAsync(message =>
            //    {
            //        Console.WriteLine("socket2 start");
            //        Console.WriteLine($"{message.Key}: {message.Value}");
            //        return Task.CompletedTask;

            //    }, marketSymbols);
            //}, socketMarketSymbols.ToArray());

            //    Console.WriteLine("socket2");



            //using var socket = await api.GetTickersWebSocketAsync(tickers =>
            //{
            //    using (var connection = factory.CreateConnection())
            //    using (var channel = connection.CreateModel())
            //    {

            //        channel.QueueDeclare(queue: "Binance_GetTickers",
            //                             durable: false,
            //                             exclusive: false,
            //                             autoDelete: false,
            //                             arguments: null);


            //        string message = JsonSerializer.Serialize(tickers);
            //        var body = Encoding.UTF8.GetBytes(message);

            //        channel.BasicPublish(exchange: "",
            //                             routingKey: "Binance_GetTickers",
            //                             basicProperties: null,
            //                             body: body);
            //        Console.WriteLine(" [x] Sent {0}", tickers.Count);


            //    }










            //    //[BAKEBUSD, Bid: 2,0451, Ask: 2,0491, Last: 2,0491]
            //    //foreach (var ticker in tickers)
            //    //{
            //    //	Console.WriteLine(ticker);

            //    //}
            //    //Console.WriteLine("{0} tickers, first: {1}", tickers.Count, tickers.All());
            //    //Console.WriteLine("{0} tickers, first: {1}", tickers.Count, tickers.First().Key);
            //    //Console.WriteLine("tickers.First().Key:{0}", tickers.First().Key);
            //    //Console.WriteLine("tickers.First().Value:{0}", tickers.First().Value);
            //    //Console.WriteLine("tickers.First().Value:{0}", tickers.Last().Value.MarketSymbol);
            //    //Console.WriteLine("tickers.First().Value:{0}", JsonSerializer.Serialize(tickers.Last().Value));

            //    //{ "Id":null,"MarketSymbol":"BAKEBUSD","Bid":2.1152,"Ask":2.1195,"Last":2.1195,"Volume":{ "Timestamp":"2021-02-22T21:29:55.165Z","QuoteCurrency":"BUSD","QuoteCurrencyVolume":69265821.214462,"BaseCurrency":"BAKE","BaseCurrencyVolume":35204148.25}




            //});

            Console.WriteLine("Press ENTER to shutdown.");
            Console.ReadLine();


            //var program = Instance;
            //var (error, help) = program.ParseArguments(args, out var options);

            //if (help)
            //	return ExitCodeOk;

            //if (error)
            //	return ExitCodeErrorParsing;

            //try
            //{
            //	await program.Run(options);
            //}
            //catch (Exception ex)
            //{
            //	Console.Error.WriteLine(ex);
            //	return ExitCodeError;
            //}

            return ExitCodeOk;
        }
    }
}

