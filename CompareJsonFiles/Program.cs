using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CompareJsonFiles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CompareJsonFiles();
        }

        private static void CompareJsonFiles()
        {
            string json1 = File.ReadAllText(@"./SharedResource.Shq.json");
            string json2 = File.ReadAllText(@"./AFK_Alb.json");

            var DeserializeObject = JsonConvert.DeserializeObject<dynamic>(json1);
            var DeserializeObject1 = JsonConvert.DeserializeObject<dynamic>(json2);

            var count = 1;
            var sameNames = 0;

            foreach(var data in DeserializeObject1)
            {
                foreach(var data1 in DeserializeObject)
                {
                    if(data.Name == data1.Name)
                    {
                        Console.WriteLine(data[0].Value);
                    }
                }
            }

            Console.WriteLine(sameNames);
        }
    }
}