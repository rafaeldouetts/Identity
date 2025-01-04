using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Benchmark.Tests
{
    public class CustomSummaryExporter : IExporter
    {
        public void ExportToLog(Summary summary, ILogger logger)
        {
            // Logando o início do relatório
            logger.WriteLine("Relatório de Benchmark:");

            // Logando os resultados dos benchmarks com colunas personalizadas
            foreach (var report in summary.Reports)
            {
                var benchmarkName = report.BenchmarkCase.DisplayInfo;
                var mean = report.ResultStatistics.Mean;
                var median = report.ResultStatistics.Median;
                var standardDeviation = report.ResultStatistics.StandardDeviation;
                var count = report.Metrics.Count;

                // Logando no formato desejado
                logger.WriteLine($"{benchmarkName} | Média: {mean:F2} ms | Mediana: {median:F2} ms | Erro Padrão: {standardDeviation:F2} ms | Execuções: {count}");
            }
        }

        public IEnumerable<string> ExportToFiles(Summary summary, ILogger consoleLogger)
        {
            var filePath = "benchmark_resultados.txt";  // Caminho do arquivo de saída

            // Criando ou sobrescrevendo o arquivo
            using (var writer = new StreamWriter(filePath, append: false))
            {
                // Escrevendo o cabeçalho
                writer.WriteLine("Benchmark | Média | Mediana | Erro Padrão | Execuções");

                // Escrevendo os resultados
                foreach (var report in summary.Reports)
                {
                    var benchmarkName = report.BenchmarkCase.DisplayInfo;
                    var mean = report.ResultStatistics.Mean;
                    var median = report.ResultStatistics.Median;
                    var standardDeviation = report.ResultStatistics.StandardDeviation;
                    var count = report.Metrics.Count;

                    // Escrevendo a linha no arquivo
                    writer.WriteLine($"{benchmarkName} | {mean:F2} ms | {median:F2} ms | {standardDeviation:F2} ms | {count}");
                }
            }

            // Retorna o caminho do arquivo gerado para confirmar
            return new[] { filePath };
        }

        public string Name => "CustomSummaryExporter"; // Nome do exportador
    }
}
