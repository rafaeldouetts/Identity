using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
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
                    .AddValidator(JitOptimizationsValidator.DontFailOnError) // Garantir que não falte configurações do JIT
                    .AddJob(Job
                        .MediumRun
                        .WithLaunchCount(1)
                        .WithToolchain(InProcessEmitToolchain.Instance));

            var summary = BenchmarkRunner.Run<RegistroBenchmark>(config);

        }
    }
}
