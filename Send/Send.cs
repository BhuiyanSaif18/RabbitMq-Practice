using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Send
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare(exchange: "hello", type : "fanout", durable : false, autoDelete : false, arguments : null);
                    channel.QueueBind(queue : "hello",exchange : "hello",routingKey : "hello", arguments : null);
                    string message = "Hello queue";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "hello",
                        routingKey: "hello",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine(" [x] Sent {0} ", message);
                }
                Console.WriteLine("Press Enter To Exit");
                Console.ReadKey();
            }
        }
    }
}
