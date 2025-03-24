using Newtonsoft.Json;

namespace CVtesting;

public class Data
{
    public static void MakeFiles()
    {
        using (StreamReader sr = new("/home/roni/repos/CVtesting/dataset_raw.json"))
        {
            string line;
            int i = 1;
            while ((line = sr.ReadLine()!) != null)
            {
                using (StreamWriter sw = new($"/home/roni/repos/CVtesting/datajson/{i}.json"))
                    sw.WriteLine(line);
                i++;
            }
        }
    }

    public static void ProcessText()
    {
        for (int i = 1; i <= 220; i++)
        {
            string pathread = $"/home/roni/repos/CVtesting/datajson/{i}.json";
            string pathwrite = $"/home/roni/repos/CVtesting/DATASET/{i}.txt";

            using (StreamReader sr = new(pathread))
            {
                string jsonString = sr.ReadLine()!;
                Resume dataRoot = JsonConvert.DeserializeObject<Resume>(jsonString)!;

                using StreamWriter sw = new(pathwrite);
                sw.WriteLine(dataRoot.content);
            }
        }
    }

    public static void Label() // creates an azureAI-formatted json label document  
    {
        Root root = new()
        {
            projectFileVersion = "2023-04-01",
            stringIndexType = "Utf16CodeUnit"
        };

        Metadata meta = new("customNERattempt", "nerblob", "CustomEntityRecognition", "please work.", "en", false)
        {
            settings = new()
        };
        root.metadata = meta;

        Assets assets = new("CustomEntityRecognition")
        {
            documents = [],
            entities = []
        };


        for (int i = 1; i <= 220; i++)
        {
            string jsonPath = $"/home/roni/repos/CVtesting/datajson/{i}.json";
            string textPath = $"/home/roni/repos/CVtesting/DATASET/{i}.txt";

            using StreamReader sr = new(jsonPath);
            string jsonString = sr.ReadLine()!;

            // data from original dataset
            Resume inputData = JsonConvert.DeserializeObject<Resume>(jsonString)!;

            Document doc = new()
            {
                location = $"{i}.txt",
                language = "en",
                entities = []
            };


            foreach (var annotation in inputData.annotation)
            {
                var mergedPoints = annotation.points
                    .OrderBy(p => p.start)
                    .Aggregate(new List<Point>(), (acc, p) =>
                    {
                        if (acc.Count == 0)
                        {
                            acc.Add(p);
                        }
                        else
                        {
                            var last = acc[^1];
                            if (p.start <= last.end)
                            {
                                last.end = Math.Max(last.end, p.end); // merge overlapping entity ranges
                            }
                            else
                            {
                                acc.Add(p); //create Entity from label
                            }
                        }
                        return acc;
                    });

                foreach (var point in mergedPoints)
                {
                    int offset = point.start;
                    int length = point.end - point.start;

                    // add entities to output doc
                    foreach (var labelValue in annotation.label)
                    {
                        Entity entity = new()
                        {
                            category = labelValue,
                            regionOffset = offset,
                            regionLength = length,
                            labels =
                            [
                                new()
                                {
                                    category = labelValue,
                                    offset = offset,
                                    length = length
                                }
                            ]
                        };
                        doc.entities.Add(entity);
                    }
                }
            }
            assets.documents.Add(doc);
        }

        root.assets = assets;
        string outputFile = "/home/roni/repos/CVtesting/labels2.json";
        string result = JsonConvert.SerializeObject(root, Formatting.Indented);
        File.WriteAllText(outputFile, result);
    }
}