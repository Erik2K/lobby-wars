using Models;

namespace Storage;

public class Database
{
    private List<Sign> Signs;

    // Constructor
    public Database()
    { 
        Signs = new List<Sign>();
    }

    // Store a sign in database
    public void storeSign(Sign sign)
    {
        Signs.Add(sign);
    }

    // return all stored signs
    public List<Sign> getStoredSigns()
    {
        return Signs;
    }
}