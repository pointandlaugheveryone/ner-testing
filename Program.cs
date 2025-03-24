
namespace CVtesting;

class Program
{
    static async Task Main(string[] args)
    {
        
        string parsedText = InputParser.ExtractTextFromDocx("/home/roni/repos/CVtesting/test.docx");
        string entext = await AzureAI.Translate("cs","en", parsedText);


        Dictionary<string,string> regexres = Utils.MatchContacts(parsedText);

        AzureAI.GetSkills(entext);

        //Console.WriteLine(parsedText);
        Console.WriteLine(entext);
        //Console.WriteLine($"{regexres["phone"]}\n{regexres["email"]}\n{regexres["linkedin"]}");
        
    }

}