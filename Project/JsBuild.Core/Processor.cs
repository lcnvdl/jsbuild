
namespace JsBuild.Core
{
    public class Processor
    {
        public Processor()
        {

        }

        public bool Start(string file)
        {
            Configuration cfg = new Configuration();
            if (cfg.Load(file))
            {

                foreach (var worker in cfg.Workers)
                {
                    worker.Configure(cfg);
                    worker.Run();
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
