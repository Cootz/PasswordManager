using PasswordManager.Model;
using PasswordManager.Model.DB.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Services
{
    public interface IDatabaseService : IInitializable, IDisposable
    {
        public void SavePasswords(Profile[] data);
        
        public Task Remove(Profile profile);
        
        public void Add(Profile profile);
        
        public IQueryable<Profile> GetProfiles();
    }
}
