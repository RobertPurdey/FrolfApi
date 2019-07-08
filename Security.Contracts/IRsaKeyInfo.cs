namespace Security.Contracts
{
    public interface IRsaKeyInfo
    {
        string GetPrivateKeyXml();
        string GetPublicKeyXml();
    }
}
