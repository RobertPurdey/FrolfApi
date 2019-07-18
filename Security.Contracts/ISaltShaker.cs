namespace Security.Contracts
{
    public interface ISaltShaker
    {
        string Shake(int duration);
    }
}
