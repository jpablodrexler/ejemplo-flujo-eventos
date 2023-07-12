using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace WebAppProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductorController : ControllerBase
    {
        private readonly ILogger<ProductorController> _logger;

        public ProductorController(ILogger<ProductorController> logger)
        {
            _logger = logger;
        }

        [HttpPost()]
        public IActionResult Post(string mensaje)
        {
            var fabrica = new ConnectionFactory { HostName = "rabbitmq" };
            using var conexion = fabrica.CreateConnection();
            using var canal = conexion.CreateModel();

            canal.QueueDeclare(queue: "cola",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            mensaje = $"{mensaje} - {DateTime.Now}";
            var contenido = Encoding.UTF8.GetBytes(mensaje);

            canal.BasicPublish(exchange: string.Empty,
                     routingKey: "cola",
                     basicProperties: null,
                     body: contenido);

            return Ok();
        }
    }
}