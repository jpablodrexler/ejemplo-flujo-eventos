using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

internal class Consumidor : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        ConnectionFactory fabrica;
        IConnection conexion;
        IModel canal = null;
        int reintentos = 0;
        bool conectado = false;

        while (!conectado && reintentos < 5)
        {
            Console.WriteLine("Conectando...");

            try
            {
                fabrica = new ConnectionFactory { HostName = "rabbitmq" };
                conexion = fabrica.CreateConnection();
                canal = conexion.CreateModel();

                canal.QueueDeclare(queue: "cola",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                conectado = true;
                Console.WriteLine("Conectado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                await Task.Delay(5000, stoppingToken).ConfigureAwait(true);
                reintentos++;
            }
        }

        stoppingToken.ThrowIfCancellationRequested();

        try
        {
            var consumidor = new EventingBasicConsumer(canal);

            consumidor.Received += (model, ea) =>
            {
                var contenido = ea.Body.ToArray();
                var mensaje = Encoding.UTF8.GetString(contenido);
                canal.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                Console.WriteLine($"Se recibio {mensaje}");
            };

            var tag = canal.BasicConsume(
                queue: "cola",
                autoAck: false,
                consumidor);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex);
        }
    }
}
