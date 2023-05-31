using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace CompareJsonFiles
{
    public class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();

            watch.Start();

            string jsonString1 = File.ReadAllText(@"./SharedResource.Shq.json");
            string jsonString2 = File.ReadAllText(@"./AFK_Alb.json");

            JObject? DObjectToEdit = JsonConvert.DeserializeObject(jsonString1) as JObject;
            JObject? DObjectToEditFrom = JsonConvert.DeserializeObject(jsonString2) as JObject;

            if(DObjectToEditFrom == null || DObjectToEdit == null) 
            {
                throw new Exception("One of the JObjets are null");
            }

            foreach (var dataToEdit in DObjectToEdit)
            {

                foreach (var dataToInsert in DObjectToEditFrom)
                {
                    if (dataToEdit.Key.ToLower() == dataToInsert.Key.ToLower())
                    {
                        if(dataToInsert.Value != null && dataToEdit.Value != null)
                        {
                            dataToEdit.Value.Replace(dataToInsert.Value);
                        }
                    }
                }
            }

            var SObject = JsonConvert.SerializeObject(DObjectToEdit, Formatting.Indented);

            File.WriteAllText(@"./SharedResource.Shq.json", SObject);

            watch.Stop();
        }
    }
}