using BenchmarkDotNet.Running;
using Identity.Benchmark.Tests.Benchmarks;

namespace Identity.Benchmark.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<RegistroBenchmark>();
        }
    }
}
