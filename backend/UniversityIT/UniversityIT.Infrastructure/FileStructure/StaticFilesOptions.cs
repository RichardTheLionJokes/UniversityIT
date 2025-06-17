namespace UniversityIT.Infrastructure.FileStructure
{
    public class StaticFilesOptions
    {
        public string RawPath { get; set; } = string.Empty;

        public string GeneratePath()
        {
            string path = System.IO.Path.Combine(new string[] { Directory.GetCurrentDirectory() }
                .Concat(RawPath.Split("*")).ToArray());

            return path;
        }
    }
}