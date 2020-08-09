using MStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MStore.Repository.IRepository
{
    public interface ISinglesRepo : IRepository<Singles>
    {
        void Update(Singles singles);
    }
}
