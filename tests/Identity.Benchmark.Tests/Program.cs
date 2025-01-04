using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Validators;
using Identity.Benchmark.Tests.Benchmarks;
using System.Globalization;

namespace Identity.Benchmark.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator) // Para evitar otimizações
            .WithSummaryStyle(SummaryStyle.Default.WithCultureInfo(new CultureInfo("pt-BR"))) // Configura o idioma para Português
            .AddExporter(HtmlExporter.Default) // Exporta para HTML
            .AddExporter(CsvExporter.Default) // Exporta para CSV, se precisar
            .AddExporter(MarkdownExporter.GitHub) // Exporta para Markdown (opcional)
            .AddValidator(JitOptimizationsValidator.DontFailOnError) // Evita erro caso haja otimizações JIT faltando
            .AddExporter(MarkdownExporter.GitHub) // Exporta para Markdown (opcional)
            .AddExporter(new CustomSummaryExporter())
            .AddJob(Job.MediumRun // Definindo configurações de execução
                .WithLaunchCount(1) // Número de execuções
                .WithWarmupCount(1)) // Número de warmups
            .AddDiagnoser(MemoryDiagnoser.Default); // Agora adicionamos o diagnosador ao config


            var summary = BenchmarkRunner.Run<RegistroBenchmark>(config);

        }
    }
}
