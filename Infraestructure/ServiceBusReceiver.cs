using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SistemaFarmacia.Application;
using SistemaFarmacia.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFarmacia.Infraestructure
{
    public class ServiceBusReceiver
    {
        private readonly string _serviceBusConnectionString;
        private const string QueueNameFarmacia = "farmacia";

        public ServiceBusReceiver(string serviceBusConnectionString)
        {
            _serviceBusConnectionString = serviceBusConnectionString;
        }

        public async Task RecibirRecetasAsync()
        {
            var client = new QueueClient(_serviceBusConnectionString, QueueNameFarmacia);

            RegisterOnMessageHandlerAndReceiveMessages(client);

            Console.ReadKey();

            await client.CloseAsync();
        }

        private void RegisterOnMessageHandlerAndReceiveMessages(QueueClient queueClient)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(async (message, token) =>
            {
                var recetaMedica = JsonConvert.DeserializeObject<RecetaMedica>(Encoding.UTF8.GetString(message.Body));

                // Procesar la receta médica en el sistema de farmacia
                var farmaciaApplicationService = new FarmaciaApplicationService(new FarmaciaService());
                farmaciaApplicationService.ProcesarRecetaEnFarmacia(recetaMedica);

                Console.WriteLine($"Receta procesada en Farmacia: {recetaMedica.Medicamento}");

                await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            }, messageHandlerOptions);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Mensaje de error: {exceptionReceivedEventArgs.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}
