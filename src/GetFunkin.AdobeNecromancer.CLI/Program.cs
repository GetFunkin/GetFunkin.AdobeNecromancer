using System.Threading.Tasks;
using CliFx;

namespace GetFunkin.AdobeNecromancer.CLI
{
    internal static class Program
    {
        internal static async Task Main() => await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .Build()
            .RunAsync();
    }
}