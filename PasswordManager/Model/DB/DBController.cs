using Microsoft.EntityFrameworkCore;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Model.IO;
using System.Data;

namespace PasswordManager.Model.DB;

[Obsolete]
public class DBController : DbContext
{
    private static readonly string DBPath = Path.Combine(AppDirectoryManager.AppData, "Psw.db");
    private static readonly string Connection = $"Filename=\"{DBPath}\"";
    
    private DbSet<ProfileInfo> Profiles { get; set; }

    public async Task Initialize()
    {
        await Database.EnsureCreatedAsync();
    }

    public async Task Add(ProfileInfo profile)
    {
        await Profiles.AddAsync(profile);
        await SaveChangesAsync();
    }

    public IEnumerable<ProfileInfo> Select(Func<ProfileInfo, bool> predicate) 
    {
        return Profiles.Where(predicate);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(Connection);
    }
}
