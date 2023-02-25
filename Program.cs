using Models;
using Storage;
using Utils;
namespace LobbyWars;

class Program 
{
    // Create database globally
    public static Database database = new Database();
    static void Main(string[] args)
    {
        // Create and store default signs
        database.storeSign(new Sign("King", 'K', 5));
        database.storeSign(new Sign("Notary", 'N', 2));
        database.storeSign(new Sign("Validator", 'V', 1));

        Console.Write("Enter the contract: ");
        string contract = Console.ReadLine();

        Console.WriteLine(Tools.contractValidator(contract));
    }
}

