# Ejemplo de Flujo de Eventos

El ejemplo define 3 componentes:

- RabbitMQ: Un container que expone RabbitMQ a los demás componentes.
- WebAppProductor: Una aplicación web que permite enviar mensajes a través de RabbitMQ. Para ingresar el mensaje expone un endpoint REST.
- WebAppConsumidor: Una aplicación web que permite recibir mensajes a través de RabbitMQ. Los mensajes son visibles a través del log de la aplicación.

Los 3 componentes son instanciados utilizando Docker Compose.

## Technologías usadas

- [.NET 6.0](https://dotnet.microsoft.com/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Docker](https://www.docker.com/)

## ¿Cómo ejecutar?

### En Docker Desktop

- Crear volumen "rabbitmqdata"

### En Visual Studio

- Seleccionar docker-compose como proyecto de inicio.
- Ejecutar el proyecto Docker Compose.

### En línea de comandos

- En la carpeta raíz del repositorio, ejecutar el comando `docker-compose up -d` para crear las imágenes y posteriormente ejecutar los containers.
- En la carpeta raíz del repositorio, ejecutar el comando `docker-compose down` para borrar los containers. Este comando no borra las imágenes.
