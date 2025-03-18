namespace CVtesting;

class Program
{
    static async Task Main(string[] args)
    {   
        string parsedtext = String.Empty;
        List<string> segments = InputParser.ExtractTextFromDocx("/home/roni/repos/CVtesting/test.docx", ref parsedtext);
        Console.WriteLine(parsedtext);
        //string cs = await Translate.GetText("en","cs", text);
        //Console.WriteLine();
    }

}