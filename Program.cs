namespace CVtesting;

class Program
{
    static async Task Main(string[] args)
    {   
        string parsedText = InputParser.ExtractTextFromDocx("/home/roni/repos/CVtesting/test.docx");
        
        string cstext = await Translate.GetText("en","cs", parsedText);
        
        string[] segments = cstext.Split("|");
        foreach (string s in segments) { Console.WriteLine(s); }
    }

}