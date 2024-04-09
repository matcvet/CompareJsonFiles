using Newtonsoft.Json;
using System.Diagnostics;

namespace CompareJsonFiles;

public class Program
{
    private static void Main()
    {
        var watch = Stopwatch.StartNew();
        watch.Start();

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Process started...");

        var jsonToEditFrom = DeserializeAndReadObject("../../../Unprocessed/TestJson1.json");
        var jsonToEdit = DeserializeAndReadObject("../../../Unprocessed/TestJson2.json");

        CompareAndTransferValues(jsonToEditFrom, jsonToEdit);

        SerializeAndSaveObject(jsonToEdit);

        watch.Stop();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Process finished in: {watch.Elapsed}");
    }

    private static dynamic DeserializeAndReadObject(string path)
    {
        var jsonString = File.ReadAllText(path);
        var DesObject = JsonConvert.DeserializeObject<dynamic>(jsonString);

        return DesObject;
    }

    private static void SerializeAndSaveObject(dynamic? dObject)
    {
        var processedObject = JsonConvert.SerializeObject(dObject, Formatting.Indented);
        File.WriteAllText("../../../Processed/Final.json", processedObject);
    }

    private static void CompareAndTransferValues(dynamic source, dynamic destination)
    {
        var sourceDictionary = source.ToObject<Dictionary<string, string>>();

        var wordsTranslated = 0;

        foreach (var kvp in destination)
        {
            string key = kvp.Name.ToLower();
            if (sourceDictionary.TryGetValue(key, out string value))
            {
                kvp.Value = value;
                wordsTranslated++;
            }
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Number of words translated: {0} \n", wordsTranslated);
    }
}