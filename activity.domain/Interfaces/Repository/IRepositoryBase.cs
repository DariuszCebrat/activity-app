using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity.domain.Interfaces.Repository
{
    public interface IRepositoryBase<Entity> where Entity : class
    {
        public IQueryable<Entity> GetAll();
        public  Task<Entity> GetAsync(object id);
        public  Task DeleteAsync(object id);
        public  Task CreateAsync(Entity newEntity);
        public  Task UpdateAsync(Entity updatedEntity);
    }
}
