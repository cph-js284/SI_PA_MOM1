using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Customer
{
    class Program
    {
        static void Main(string[] args)
        {
            string routingkey;

            if(args.Length == 0){
                routingkey = "winter.#";
                System.Console.WriteLine($"No args given, using default routing key topic :{routingkey}");
            }else
            {
                routingkey = args[0];
            }

            var HostName = "SomeAddRabbit";
            var factoory = new ConnectionFactory(){HostName=HostName};
            using (var connection = factoory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    System.Console.WriteLine("Waiting for adds - press enter to exit");
                    
                    
                    var RandomQueueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queue: RandomQueueName, exchange: "addspam", routingKey: routingkey);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) => {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        System.Console.WriteLine($"ADD RECEIVED : {message}");

                    };

                    channel.BasicConsume(queue: RandomQueueName, autoAck: true, consumer: consumer);

                    Console.ReadLine();

                }
            }
            System.Console.WriteLine("Terminating...");
        }
    }
}
