using LobbyWars;
using Models;

namespace App;
class Terminal
{
    // Start the Terminal App
    public static void start()
    {
        Boolean end = false;

        do
        {
            int option = menu();

            switch(option) 
            {
                case 1: 
                    winnerSelector();
                    break;
                
                case 2:
                    SignGuesser();
                    break;
                
                default:
                    end = true;
                    break;
            }

        } while(!end);
    }

    private static void title() 
    {
        Console.Clear();

        Console.WriteLine("-------------------------");
        Console.WriteLine("        LOBBY WARS");
        Console.WriteLine("-------------------------\n");
    }

    private static void SignOptions() 
    {
        List<Sign> storedSigns = Program.database.getStoredSigns();

        Console.WriteLine("***************************");
        Console.WriteLine("Available Signs:\n");

        foreach (Sign sign in storedSigns)
        {
            Console.WriteLine($"{sign.Name} ({sign.Identifier}) - {sign.Value} points");
        }

        Console.WriteLine("***************************\n");
    }

    // Repeat question
    private static Boolean repeatQuestion()
    {
        Console.Write("\nDo you want to repeat? (y/N): ");
        string option = Console.ReadLine()!;

        return option.ToUpper() == "Y";
    } 

    // Interactive menu
    private static int menu()
    {
        int option;
        Boolean validOption;

        do
        {
            title();

            Console.WriteLine("1. Winner selector");    
            Console.WriteLine("2. Sign guesser");
            Console.WriteLine("0. Exit\n");
            Console.Write("Select an option: ");

            validOption = 
                int.TryParse(Console.ReadLine(), out option) && 
                option >= 0 && 
                option <= 2;

            if (!validOption) Console.WriteLine("Please enter a valid option");
        } while(!validOption);

        return option;
    }

    // Runs Winner selector
    private static void winnerSelector()
    {        
        string plaintiff;
        string defendant;

        Boolean invalidContract;

        do 
        {
            title();

            SignOptions();

            do
            {
                Console.Write("Enter the Plaintiff contract: ");
                plaintiff = Console.ReadLine()!;

                if (invalidContract = !Utils.Tools.contractValidator(plaintiff)) 
                    Console.WriteLine("The contract was INVALID!\n");
            } while(invalidContract);

            do
            {
                Console.Write("Enter the Defendant contract: ");
                defendant = Console.ReadLine()!;

                if (invalidContract = !Utils.Tools.contractValidator(defendant)) 
                    Console.WriteLine("The contract was INVALID!\n");
            } while(invalidContract);

            Console.WriteLine($"{Utils.Tools.winnerSelector(plaintiff, defendant)} wins !!!");

        } while(repeatQuestion());
    }

    //Runs SignGuesser
    private static void SignGuesser()
    {
        Console.WriteLine("SignGuesser");
    }
}