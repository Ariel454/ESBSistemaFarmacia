using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SistemaFarmacia.Infraestructure;
using System;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Sistema de Farmacia: Esperando recetas médicas...");

        var serviceBusReceiver = new ServiceBusReceiver("Endpoint=sb://arquitecturaar.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=liz2Rdnrrk71JmuXcI3xyWnanI36FTj2h+ASbNqRNXI="); // Reemplazar con tu cadena de conexión
        await serviceBusReceiver.RecibirRecetasAsync();

        Console.WriteLine("Presiona cualquier tecla para salir.");
        Console.ReadKey();
    }
}
