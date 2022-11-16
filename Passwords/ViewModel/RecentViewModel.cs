using Passwords.Model;
using Passwords.PasData;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passwords.ViewModel
{
    internal class RecentViewModel : AbstractViewModel
    {
        public RecentModel Model { get { return _model; } set { _model = value; OnPorpertyChanged(); } }
        private RecentModel _model;

        public RecentViewModel() => _model = new();
    }
}
