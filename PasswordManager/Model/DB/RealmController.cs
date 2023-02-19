﻿using PasswordManager.Model.DB.Schema;
using PasswordManager.Model.IO;
using Realms;
using System.Diagnostics;

namespace PasswordManager.Model.DB
{
    /// <summary>
    /// Handles data transferring to/from realm
    /// </summary>
    public sealed class RealmController : IController
    {
        private const ulong schema_version = 3;

        private Storage dataStorage { get; set; }

        private Realm realm;

        private bool isInitialized = false;

        public bool IsInitialized() => isInitialized;

        public RealmController(Storage storage) 
        {
            dataStorage = storage.GetStorageForDirectory("data");

            Initialize().Wait();
        }

        public async Task Add(ProfileInfo profile)
        {
            await realm.WriteAsync(() =>
            {
                realm.Add(profile);
            });
        }

        public void Dispose()
        {
            realm.Dispose();
        }

        public async Task Initialize()
        {
            //Prevent double initialization
            if (IsInitialized())
                return;

            var config = new RealmConfiguration(Path.Combine(dataStorage.WorkingDirectory, "Psw.realm"))
            {
                SchemaVersion = schema_version,
                MigrationCallback = OnMigration
            };

            try
            {
                realm = Realm.GetInstance(config);
            }
            catch
            {
                string databasePath = config.DatabasePath;

                BackupManager.Backup(new FileInfo(databasePath));
                File.Delete(databasePath);

                realm = Realm.GetInstance(config);
            }

            //Preparing default values
            var services = realm.All<ServiceInfo>();
            var servicesToAdd = new List<ServiceInfo>();

            foreach (var service in ServiceInfo.DefaultServices)
                if (realm.Find<ServiceInfo>(service.ID) is null)
                    servicesToAdd.Add(service);

            if (servicesToAdd.Count > 0)
                await realm.WriteAsync(() => servicesToAdd.ForEach(s => realm.Add(s)));

            Debug.Assert(realm.All<ServiceInfo>().ToArray().Intersect(ServiceInfo.DefaultServices).Count() == ServiceInfo.DefaultServices.Length);

            isInitialized = true;
        }

        private void OnMigration(Migration migration, ulong lastSchemaVersion)
        {
            for (ulong i = 1; i <= schema_version; i++)
                applyMigrationsForVestions(migration, (ulong)i);
        }

        private void applyMigrationsForVestions(Migration migration, ulong targetVersion)
        {
            switch (targetVersion)
            {
                case 2:
                    var newProfiles = migration.NewRealm.All<ProfileInfo>();

                    for (int i = 0; i < newProfiles.Count(); i++)
                        newProfiles.ElementAt(i).ID = Guid.NewGuid();

                    break;
            }
        }

        public IQueryable<T> Select<T>() where T : IRealmObject => realm?.All<T>();

        public Task Remove(ProfileInfo profile) => realm?.WriteAsync(() => realm.Remove(profile));
    }
}
