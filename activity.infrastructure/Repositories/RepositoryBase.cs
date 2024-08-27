using activity.domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity.infrastructure.Repositories
{
    public class RepositoryBase<Entitie>where Entitie : RepositoryEntitie
    {
        private DataContext _context;
        private DbSet<Entitie> _entitie;
        public RepositoryBase(DataContext context)
        {
            _context = context; 
            _entitie = _context.Set<Entitie>();
        }
        public IQueryable<Entitie> GetAll()
        {
            return _entitie.AsQueryable();
        }
        public  async Task<Entitie> GetAsync(Guid id)
        {
           var entitie =await  _entitie.FirstOrDefaultAsync(x => x.Id == id);
           if (entitie is null)
                throw new NotFoundException();

            return entitie;
        }


    }
}
