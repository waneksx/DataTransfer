using System;
using System.Threading.Tasks;

namespace TransferClient
{
    class ProgramClient
    {
        static void Main(string[] args)
        {
            int quantity = 0;
           
            while (true)
            {
                Client cl1 = new Client();
                Console.WriteLine("how many?");
                var quantityString = Console.ReadLine();
                int.TryParse(quantityString, out quantity);
                Task.Run(async () => await cl1.Start(quantity));
            }
            
            Console.ReadLine();
        }
    }
}