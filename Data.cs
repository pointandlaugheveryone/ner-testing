using Newtonsoft.Json;

namespace CVtesting;

public class Data {
    public static void MakeFiles() {
        using (StreamReader sr = new("/home/roni/repos/CVtesting/dataset_raw.json")) {
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

    public static void ProcessText() {
        for (int i = 1; i <= 220; i++) {
            string pathread = $"/home/roni/repos/CVtesting/datajson/{i}.json";
            string pathwrite = $"/home/roni/repos/CVtesting/dataset/{i}.txt";

            using (StreamReader sr = new(pathread)) {
                string jsonstring = sr.ReadLine()!;
                Root dataRoot = JsonConvert.DeserializeObject<Root>(jsonstring)!;
                
                using (StreamWriter sw = new(pathwrite)) {
                    sw.WriteLine(dataRoot.content);
                }
            }
        }
    }
}