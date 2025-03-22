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
}