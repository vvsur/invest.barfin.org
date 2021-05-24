using System;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ExchangeSharp;
using TradingBot.Data.Models;


namespace TradingBot.App.Deposit.Binance
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new TestModelRepository();


            //Console.WriteLine(" repository.GetTestModels():" + repository.GetTestModels().First().ToString());
            //Console.WriteLine(" repository.GetExchangeTickers():" + JsonSerializer.Serialize(repository.GetExchangeTickers()));


            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "GetTrades.Binance",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    //JsonSerializer.Deserialize<ExchangeTicker>(message);


                    //.........Binance_GetTickers
                    //....................................
                    //MarketSymbol varchar(25)
                    //Bid numeric
                    //Ask numeric
                    //Last numeric
                    //Volume_Timestamp datetime
                    //Volume_QuoteCurrency varchar(10)
                    //Volume_QuoteCurrencyVolume numeric
                    //Volume_BaseCurrency varchar(10)
                    //Volume_BaseCurrencyVolume numeric








                    //"MarketSymbol":"SFPBUSD",
                    //"Bid":2.5514,
                    //"Ask":2.5594,
                    //"Last":2.5515,
                    //"Volume":{
                    //    "Timestamp":"2021 - 02 - 22T22: 07:33.091Z",
                    //        "QuoteCurrency":"BUSD",
                    //        "QuoteCurrencyVolume":8616432.890597,
                    //        "BaseCurrency":"SFP",
                    //        "BaseCurrencyVolume":3352504.82



                    //Console.WriteLine(" [x] Received {0}", message);
                    var instrument = JsonSerializer.Deserialize<KeyValuePair<string, ExchangeTrade>>(message);
                    Console.WriteLine(instrument.ToString());



                    if (instrument.Value.Price > (decimal)0.11)
                    {
                        Console.WriteLine("!!!!!");
                    }

                    //JsonSerializer.Deserialize<ICollection<KeyValuePair<string, ExchangeTicker>>>(message)

                    //Console.WriteLine(" [x] Received {0}", tickersCollection.First());

                    //foreach (KeyValuePair<string, ExchangeTicker> keyValue in tickersCollection)
                    //{
                    //    repository.CreateExchangeTicker(keyValue.Value);
                    //}




                };
                channel.BasicConsume(queue: "GetTrades.Binance",
                                             autoAck: true,
                                             consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
