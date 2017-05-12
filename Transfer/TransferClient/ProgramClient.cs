using System;
using System.Threading.Tasks;

namespace TransferClient
{
    class ProgramClient
    {
        static void Main(string[] args)
        {
            Client cl1 = new Client();
            int quantity = 0;
            Console.WriteLine("how many?");
            var quantityString = Console.ReadLine();
            int.TryParse(quantityString, out quantity);
            Task.Run(async () => await cl1.Start(quantity));
            Client cl2 = new Client();
            Console.WriteLine("how many?");
            quantityString = Console.ReadLine();
            int.TryParse(quantityString, out quantity);
            Task.Run(async () => await cl2.Start(quantity));
            Client cl3 = new Client();
            Console.WriteLine("how many?");
            quantityString = Console.ReadLine();
            int.TryParse(quantityString, out quantity);
            Task.Run(async () => await cl3.Start(quantity));
            Client cl4 = new Client();
            Console.WriteLine("how many?");
            quantityString = Console.ReadLine();
            int.TryParse(quantityString, out quantity);
            Task.Run(async () => await cl4.Start(quantity));
            Console.ReadLine();
        }
    }
}