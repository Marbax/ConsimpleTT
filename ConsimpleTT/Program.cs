using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsimpleTT
{

    //      Тестовое задание
    /// <summary>
    ///  Реализовать простое консольное приложение для работы с удаленным rest api.
    /// </summary>
    //     Требования/функционал:
    //  Возможность отправить запрос на удаленный API (адрес и описание приведено ниже)
    //  Метод API возвращает 2 списка - продукты и категории
    //  Полученный ответ приложение должно вывести в консоль согласно примеру:
    //  
    //  Product name    Category name
    //  Laptop          Computers
    //  Bread           Food
    //  Примитивный консольный командный интерфейс(команды отправить и выйти)
    //  адрес: http://tester.consimple.pro

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            string url = @"http://tester.consimple.pro";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            IHttpService httpService = new HttpService(client);

            bool runing = true;
            while (runing)
            {
                Console.WriteLine("1 - to get info | 2 - to exit | 3 - clear console");
                var input = Console.ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        Draw(await httpService.Get<RespModel>(url));
                        break;
                    case ConsoleKey.D2:
                        runing = false;
                        break;
                    default:
                    case ConsoleKey.D3:
                        Console.Clear();
                        break;
                }
            }
        }

        private static void Draw(RespModel data)
        {
            for (int i = 0; i < data.Products.Length; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine($"\t\t--------------------------------------------");
                    Console.WriteLine($"\t\t|{"Product name",-20} |{"Category name",-20}|");
                    Console.WriteLine($"\t\t--------------------------------------------");
                }
                Console.WriteLine($"\t\t|{$"{data.Products?[i]?.Name }",-20} |{$"{data.Categories.FirstOrDefault(item => item.Id == data.Products?[i]?.CategoryId)?.Name}",-20}|");
            }
            Console.WriteLine($"\t\t--------------------------------------------");
            Console.WriteLine("\n\n");
        }
    }
}
