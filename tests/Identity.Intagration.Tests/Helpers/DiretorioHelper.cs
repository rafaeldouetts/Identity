
namespace Identity.Intagration.Tests.Helpers
{
    public static class DiretorioHelper
    {
        public static void LimparDiretorio(string diretorio)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = GetProjectDirectory(currentDirectory);
            string evidenciasDirectory = Path.Combine(projectDirectory, "Evidencias", diretorio);

            foreach (string arquivo in Directory.GetFiles(evidenciasDirectory))
            {
                File.Delete(arquivo);
            }
        }

        public static string GetProjectDirectory(string directory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);

            while (directoryInfo != null && !directoryInfo.GetFiles("*.csproj").Any())
            {
                directoryInfo = directoryInfo.Parent;
            }

            return directoryInfo?.FullName;
        }

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
