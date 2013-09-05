namespace JsBuild.Core
{
    public interface IWorker
    {
        void Configure(Configuration cfg);
        void Run();
    }
}
