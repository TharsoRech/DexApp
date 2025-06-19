namespace DexApp.Repository.Interfaces;

public interface IDEXRepository
{
    Task<bool> SendDEXFile(string reportContent, string machineName);
}