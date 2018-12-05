namespace inc.core
{
    public interface IAddressableEncoder : IEncoder
    {
        bool FillAddress(string address);
    }
}
