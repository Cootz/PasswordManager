﻿using Microsoft.EntityFrameworkCore;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Model.IO;
using System.Data;

namespace PasswordManager.Model.DB;

public class DBController : DbContext, IController
{
    private static readonly string DBPath = Path.Combine(AppDirectoryManager.AppData, "Psw.db");
    private static readonly string Connection = $"Filename=\"{DBPath}\"";
    
    public DbSet<Profile> Profiles { get; set; }

    public async Task Initialize()
    {
        await Database.EnsureCreatedAsync();
    }

    public async Task Add(Profile profile)
    {
        Profiles.Add(profile);
        SaveChanges();
    }

    public async Task<IEnumerable<Profile>> Select(Func<Profile, bool> predicate) 
    {
        return Profiles.Where(predicate);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(Connection);
    }
}
