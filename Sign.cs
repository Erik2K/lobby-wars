namespace Objects;

public class Sign
{
    public string Name { get; set; }
    public char Identifier { get; set; }
    public int Value { get; set; }
    public Sign(string name, char identifier, int value) 
    {
        Name = name;
        Identifier = identifier;
        Value = value;
    }
}