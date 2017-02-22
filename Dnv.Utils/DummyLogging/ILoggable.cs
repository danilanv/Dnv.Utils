namespace Dnv.Utils.DummyLogging
{
    public interface ILoggable
    {
        int LogIndent { get; set; }
        string LogPrefix { get; set; }
    }
}