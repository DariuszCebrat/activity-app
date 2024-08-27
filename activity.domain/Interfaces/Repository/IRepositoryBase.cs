using activity.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity.domain.Interfaces.Repository
{
    public interface IRepositoryBase<Entitie> where Entitie : RepositoryEntitie
    {
    }
}
