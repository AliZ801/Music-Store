using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MStore.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        IGenresRepo Genres { get; }

        ISinglesRepo Singles { get; }

        void Save();
    }
}
