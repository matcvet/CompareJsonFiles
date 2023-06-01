using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace CompareJsonFiles
{
    public class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            watch.Start();
            Console.WriteLine("Process started...");

            var DObjectToEdit = ReadAndDeserializeObject(@"./SharedResource.Shq.json");
            var DObjectToEditFrom = ReadAndDeserializeObject(@"./AFK_En.json");

            var wordsTranslated = 0;

            foreach (var dataToEdit in DObjectToEdit)
            {
                //string word = dataToEdit.Name;
                //if (word.Contains("_original"))
                //{
                //    word = word.Replace("_original", "");
                //}

                foreach (var dataToInsert in DObjectToEditFrom)
                {
                    if (dataToEdit.Name.ToLower() == dataToInsert.Name.ToLower())
                    {
                        if (dataToInsert.Value != null && dataToEdit.Value != null)
                        {
                            dataToEdit.Value = dataToInsert.Value;
                        }
                        Console.Write("\r{0} words translated", wordsTranslated);
                        wordsTranslated++;
                        continue;
                    }
                }
            }

            var SObject = JsonConvert.SerializeObject(DObjectToEdit, Formatting.Indented);
            File.WriteAllText(@"./SharedResource.Shq.json", SObject);
            watch.Stop();
            Console.WriteLine($"Process finished in: {watch.Elapsed}");
        }

        public static dynamic ReadAndDeserializeObject(string path)
        {
            string jsonString = File.ReadAllText(path);
            var DesObject = JsonConvert.DeserializeObject<dynamic>(jsonString);

            return DesObject;
        }
    }
}