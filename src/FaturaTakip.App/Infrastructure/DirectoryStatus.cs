namespace FaturaTakip.App.Infrastructure;

public sealed record DirectoryStatus(string Name, string FullPath, bool Created)
{
    public string State => Created ? "Oluşturuldu" : "Hazır";
}
