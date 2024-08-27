using activity.domain.Common;
using activity.infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity.infrastructure.Repositories
{
    public class RepositoryBase<Entity>where Entity : RepositoryEntity
    {
        private DataContext _context;
        private DbSet<Entity> _entity;
        public RepositoryBase(DataContext context)
        {
            _context = context;
            _entity = _context.Set<Entity>();
        }
        public IQueryable<Entity> GetAll()
        {
            return _entity.AsQueryable();
        }
        public  async Task<Entity> GetAsync(Guid id)
        {
           var entity =await _entity.FirstOrDefaultAsync(x => x.Id == id);
           if (entity is null)
                throw new NotFoundException();

            return entity;
        }
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _entity.FirstOrDefaultAsync(x=>x.Id == id);
            if(entity is null)
                throw new NotFoundException();

            _entity.Remove(entity);
           await SaveChangesAsync();
        }
        public async Task CreateAsync(Entity newEntity)
        {
            await _entity.AddAsync(newEntity);
            await SaveChangesAsync();
        }
        public async Task UpdateAsync(Entity updatedEntity)
        {
            var entityEntry = _context.Entry<Entity>(updatedEntity);
            entityEntry.State = EntityState.Modified;
           await SaveChangesAsync();

        }
        private async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

    }
}
