using Storage;
using Models;
using LobbyWars; 

namespace Utils;

class Tools
{
    public static Boolean contractValidator(string contract)
    {
        if (string.IsNullOrEmpty(contract)) return false;
        
        char[] storedSigns = Program.database.getStoredSigns().Select(sign => sign.Identifier).ToArray();
        char[] contractSigns = contract.ToCharArray();

        return contractSigns.Except(storedSigns).ToArray().Length > 0 
            ? false 
            : true;
    }
}