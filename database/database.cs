using Models;

namespace Storage;

public class Database
{
    private List<Sign> Signs;
    public Database()
    { 
        Signs = new List<Sign>();
    }

    public void storeSign(Sign sign)
    {
        Signs.Add(sign);
    }

    public List<Sign> getStoredSigns()
    {
        return Signs;
    }
}