namespace Reference.Common.Contracts
{
    public interface ILog
    {
        void Debug(string message, params object[] args);
    }
}