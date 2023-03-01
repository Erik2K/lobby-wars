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

    // Prints app title
    private static void title() 
    {
        Console.Clear();

        Console.WriteLine("-------------------------");
        Console.WriteLine("        LOBBY WARS");
        Console.WriteLine("-------------------------\n");
    }

    // Print all Signs available
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
        string plaintiff;
        string defendant;

        string emptyContractName = "";
        string emptyContract = "";
        string fullContract = "";
        char result;


        Boolean invalidContract = false;

        do
        {
            title();
            SignOptions();

            Console.WriteLine("In this part one of the contracts needs to contain an empty sign ('#')");
            Console.WriteLine("use it as a normal sign, like: 'N#V'\n");

            do
            {
                do
                {
                    invalidContract = false;

                    Console.Write("Enter the Plaintiff contract: ");
                    plaintiff = Console.ReadLine()!;

                    if (Utils.Tools.emptyContractValidator(plaintiff))
                    {
                        emptyContractName = "Plaintiff";
                        emptyContract = plaintiff;
                    } else fullContract = plaintiff;

                    if (emptyContractName == "" && (invalidContract = !Utils.Tools.contractValidator(plaintiff)))
                    {
                        Console.WriteLine("The contract was INVALID!\n");
                    }
                } while(invalidContract);

                do
                {
                    invalidContract = false;

                    Console.Write("Enter the Defendant contract: ");
                    defendant = Console.ReadLine()!;

                    if (emptyContractName == "" && Utils.Tools.emptyContractValidator(defendant))
                    {
                        emptyContractName = "Defendant";
                        emptyContract = defendant;
                    }

                    if (defendant.Contains('#'))
                    {
                        if (!Utils.Tools.emptyContractValidator(defendant))
                            Console.WriteLine("The contract was INVALID!\n");
                    }
                    else
                    {
                        if (invalidContract = !Utils.Tools.contractValidator(defendant)) Console.WriteLine("The contract was INVALID!\n");
                        else fullContract = defendant;
                    }

                } while(invalidContract);

                if (emptyContractName == "")
                {
                    Console.WriteLine("No contract has an empty sign!\n");
                    fullContract = "";
                    emptyContract = "";
                    emptyContractName = "";
                }
                else
                {
                    if (fullContract == "")
                    {
                        Console.WriteLine("Only one contract can have an empty sign!\n");
                        fullContract = "";
                        emptyContract = "";
                        emptyContractName = "";
                    }
                }
            } while (emptyContractName == "" || fullContract == "");

            result = Utils.Tools.SignGuesser(emptyContract, fullContract);

            if (result == '#')
            {
                Console.WriteLine($"{emptyContractName} can't win");
            }
            else
            {
                Console.WriteLine($"{emptyContractName} needs ('{result}') sign to win");
            }

            fullContract = "";
            emptyContract = "";
            emptyContractName = "";
        } while(repeatQuestion());


    }
}