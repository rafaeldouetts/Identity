using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using Identity.Benchmark.Tests.Benchmarks;

namespace Identity.Benchmark.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance)
                    .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                    .WithSummaryStyle(SummaryStyle.Default.WithCultureInfo(System.Globalization.CultureInfo.InvariantCulture))
                    .AddExporter(HtmlExporter.Default) // Adicionando o exporter HTML
                    .AddValidator(JitOptimizationsValidator.DontFailOnError); // Garantir que não falte configurações do JIT


            var summary = BenchmarkRunner.Run<RegistroBenchmark>();
        }
    }
}
