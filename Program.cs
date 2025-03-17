namespace CVtesting;

class Program
{
    static async Task Main(string[] args)
    {   
        string cz = await Translate.ToCzech();
        Console.WriteLine(cz);
    }

}