namespace N.Extensions
{
    public static class DirectoryExtensions
    {
        public static long DirSize(this DirectoryInfo dir)
        {
            return dir.EnumerateFiles().Sum(fi => fi.Length) +
                   dir.EnumerateDirectories().Sum(di => DirSize(di));
        }

        public static bool CheckDirectory(string? path)
        {
            if (Directory.Exists(path))
                return true;
            Directory.CreateDirectory(path);
            return false;
        }

        public static IEnumerable<FileInfo> EnumerateFilesAll(this DirectoryInfo dir)
        {
            return dir.EnumerateFiles().Concat(dir.EnumerateDirectories().SelectMany(x => x.EnumerateFilesAll()));
        }
    }
}
