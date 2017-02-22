namespace Dnv.Utils.DummyLogging
{
    public interface ILogger
    {
        void Debug(int level, string message);
        void Trace(int level, string message);
        void Error(int level, string message);
        void Info(int level, string message);
    }
}