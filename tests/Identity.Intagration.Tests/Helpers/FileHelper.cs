using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Intagration.Tests.Helpers
{
    public static class FileHelper
    {
        public static string GetSolutionDirectory()
        {
            // Obtém o diretório de trabalho atual
            string currentDirectory = Directory.GetCurrentDirectory();

            // Navega pelos diretórios pai até encontrar um arquivo .sln
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            while (directory != null)
            {
                FileInfo[] solutionFiles = directory.GetFiles("*.sln");
                if (solutionFiles.Length > 0)
                {
                    return directory.FullName;
                }
                directory = directory.Parent;
            }

            // Se nenhum arquivo .sln for encontrado, retorna null ou uma mensagem de erro
            throw new InvalidOperationException("Diretório da solução não encontrado.");
        }
    }
}
