using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Services
{
    public interface ISettingsService
    {
        IQueryable<ProfileInfo> ProfileInfos { get; }
        AppTheme CurrentTheme { get; set; }
    }
}
