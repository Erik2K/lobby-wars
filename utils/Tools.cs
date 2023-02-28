using Storage;
using Models;
using LobbyWars; 

namespace Utils;

class Tools
{
    // Cheks the contract and return if it's valid 
    public static Boolean contractValidator(string contract)
    {
        if (string.IsNullOrEmpty(contract)) return false;
        
        char[] storedSigns = Program.database.getStoredSigns().Select(sign => sign.Identifier).ToArray();
        char[] contractSigns = contract.ToUpper().ToCharArray();

        return contractSigns.Except(storedSigns).ToArray().Length == 0;
    }

    public static Boolean emptyContractValidator(string contract) 
    {
        if (string.IsNullOrEmpty(contract)) return false;
        
        char[] storedSigns = Program.database.getStoredSigns().Select(sign => sign.Identifier).ToArray();
        char[] contractSigns = contract.ToUpper().ToCharArray();
        char[] exceptSigns = contractSigns.Except(storedSigns).ToArray();

       
        return exceptSigns.Contains('#') && exceptSigns.Length == 1;
    }

    // Returns the winner between two contracts
    public static String winnerSelector(string plaintiff, string defendant)
    {
        return
            signsValueCalculator(contractRuleApplier(plaintiff)) > 
            signsValueCalculator(contractRuleApplier(defendant))
                ? "plaintiff"
                : "defendant";

    }

    public static char[] contractRuleApplier(string contract)
    {
        char[] signs = contract.ToUpper().Where(sign => sign != '#').ToArray();

        if (signs.Contains('K'))
        {
            signs = signs.Where(sign => sign != 'V').ToArray();
        }

        return signs;
    }

    public static int signsValueCalculator(char[] signs)
    {
        List<Sign> storedSigns = Program.database.getStoredSigns();
        int counter = 0;

        foreach (var sign in signs)
        {
            counter += storedSigns.Find(s => s.Identifier == sign)!.Value;
        }

        return counter;
    }

    public static char SignGuesser(string emptyContract, string fullContract)
    {
        char winningSign = '#';

        List<Sign> storedSigns = Program.database.getStoredSigns();
        int fullContractPoints = signsValueCalculator(contractRuleApplier(fullContract));

        if (
            fullContractPoints -
            signsValueCalculator(contractRuleApplier(emptyContract)) > 5
        ) return '#';

        foreach (var sign in storedSigns.OrderBy(sign => sign.Value))
        {
            if (
                signsValueCalculator(
                    contractRuleApplier(
                        emptyContract.Replace('#', sign.Identifier)
                    )
                ) > fullContractPoints
            ) 
            {
                winningSign = sign.Identifier;
                break;
            }
        }

        return winningSign;
    }
}