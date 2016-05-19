namespace DynamicHandleGeneratorsConsumer
{
    public interface IPeople
    {
        string GetDetails();
        string GetDetails(uint age);
        string GetDetails(string name);

        int Calculate(int x);
    }
}