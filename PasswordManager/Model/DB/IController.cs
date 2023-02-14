﻿using PasswordManager.Model.DB.Schema;
using Realms;

namespace PasswordManager.Model.DB;

/// <summary>
/// Provides database logic. Makes database migration easier
/// </summary>
public interface IController: IInitializable, IDisposable
{
    public IQueryable<T> Select<T>() where T : IRealmObject;
    public Task Add(Profile profile);
    public Task Remove(Profile profile);
}
