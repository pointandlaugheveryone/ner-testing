namespace CVtesting;

class Program
{
    static async Task Main(string[] args)
    {   
        string parsedText = String.Empty;
        List<string> segments = InputParser.ExtractTextFromDocx("/home/roni/repos/CVtesting/test.docx", ref parsedText);
        
        string cs = await Translate.GetText("en","cs", parsedText);
        Console.WriteLine(cs);
    }

}