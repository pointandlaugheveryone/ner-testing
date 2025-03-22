namespace CVtesting;

class Program
{
    static void Main(string[] args)
    {
        //string parsedText = InputParser.ExtractTextFromDocx("/home/roni/repos/CVtesting/test.docx");
        //string entext = await Translate.GetText("cs","en", parsedText);
        //Dictionary<string,string> regexres = Utils.MatchContacts(parsedText);

        //Console.WriteLine(parsedText);
        //Console.WriteLine(entext);
        //Console.WriteLine($"{regexres["phone"]}\n{regexres["email"]}\n{regexres["linkedin"]}");
        Data.MakeFiles();
    }

}