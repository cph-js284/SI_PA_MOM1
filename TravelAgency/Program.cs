using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace TravelAgency
{
    class Program
    {
        static void Main(string[] args)
        {
            //TravelAgency sending adds every 2 secs(!) format: <season><continent><budget>

            var HostName = "SomeAddRabbit";
            var factory = new ConnectionFactory(){HostName = HostName};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "addspam", type: ExchangeType.Topic);

                    System.Console.WriteLine("Addspammer running - press Ctrl+C to terminate");

                    while (true)
                    {
                        Thread.Sleep(2000);

                        var routingKey = CreateAddMessage();
                        var subjects = routingKey.Split(".");
                        var body = Encoding.UTF8.GetBytes($"Congraz!!! you have won! SUBJECT : {subjects[0]}-{subjects[1]}-{subjects[2]} (routingkey used : {routingKey})" );

                        channel.BasicPublish(exchange: "addspam", routingKey: routingKey, body:body);
                        System.Console.WriteLine($"Add spam sent to queue with routing-key {routingKey}");

                    }
                }                
            }

        }

        //(overkill-)helper to randomize topics
        static string CreateAddMessage(){
            string res="";
            var rng = new Random();

            var seasonSwitch = rng.Next(1,5); //ex. upperbound
            switch (seasonSwitch)
            {
                case 1:
                    res = res + "winter";
                    break;
                case 2:
                    res = res + "spring";
                    break;
                case 3:
                    res = res + "summer";
                    break;
                case 4:
                    res = res + "fall";
                    break;
                default:
                break;
            }

            var continentSwitch = rng.Next(1,4); //ex. upperbound
            switch (continentSwitch)
            {
                case 1:
                    res = res + ".europe";
                    break;
                case 2:
                    res = res + ".asia";
                    break;
                case 3:
                    res = res + ".africa";
                    break;
                default:
                break;
            }

            var budgetSwitch = rng.Next(1,4); //ex. upperbound
            switch (budgetSwitch)
            {
                case 1:
                    res = res + ".prima";
                    break;
                case 2:
                    res = res + ".middle";
                    break;
                case 3:
                    res = res + ".lowfi";
                    break;
                default:
                break;
            }
            return res;
        }

    }



}
