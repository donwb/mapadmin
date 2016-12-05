using Nancy;
using Nancy.Configuration;

namespace NancyApplication
{

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public Bootstrapper()
        {
            
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(
                enabled: true,
                displayErrorTraces: true
            );
        }
    }
}