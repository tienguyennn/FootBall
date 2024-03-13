namespace N.Service.Common.RequestFileService
{
    public interface IRequestFileService
    {
        Task<string> RequestFile(string? path);
        Task<string?> AccessFile(string? guid);

    }
}
