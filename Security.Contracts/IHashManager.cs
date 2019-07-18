namespace Security.Contracts
{
    public interface IHashManager
    {
        string Hash(string input);
        string Hash(string input, string salt);
    }
}
