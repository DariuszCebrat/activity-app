using activity.domain.Interfaces.Repository;
using activity.infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace activity.infrastructure.Repositories
{
    public class RepositoryBase<Entity>:IRepositoryBase<Entity>where Entity : class
    {
        private DataContext _context;
        //private readonly ILogger _logger;
        private DbSet<Entity> _entity;
        public RepositoryBase(DataContext context)//,ILogger logger)
        {
            _context = context;
            //_logger = logger;
            _entity = _context.Set<Entity>();
        }
        public IQueryable<Entity> GetAll()
        {
            return _entity.AsQueryable();
        }
        public  async Task<Entity> GetAsync(object id)
        {
            var entity = await _entity.FindAsync(id);
           if (entity is null)
                throw new NotFoundException();

            return entity;
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await _entity.FindAsync(id);
            if (entity is null)
                throw new NotFoundException();

            _entity.Remove(entity);
           await SaveChangesAsync();
        }
        public async Task<object> CreateAsync(Entity newEntity)
        {
            await _entity.AddAsync(newEntity);
            await SaveChangesAsync();
            var type =newEntity.GetType();
            PropertyInfo idProp = type.GetProperty("Id");
            var id = idProp?.GetValue(newEntity);
            return id;
        }
        public async Task UpdateAsync(Entity updatedEntity)
        {
            var entityEntry = _context.Entry<Entity>(updatedEntity);
            entityEntry.State = EntityState.Modified;
           var result = await SaveChangesAsync();
            if (result < 1)
            {
               // LogEntityProperties(updatedEntity,"update");
                throw new Exception("Coulnd not update entity");
            }
        }
        private async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

        //private void LogEntityProperties(Entity entity,string action)
        //{
        //    List<string> values = new List<string>();
        //    Type type = typeof(Entity);

        //    // Pobieranie wszystkich właściwości obiektu
        //    PropertyInfo[] properties = type.GetProperties();

        //    // Pętla przez właściwości
        //    foreach (var property in properties)
        //    {
        //        // Pobranie wartości właściwości dla bieżącego obiektu
        //        object value = property.GetValue(entity);

        //        values.Add($"{property.Name}: {value}");

        //    }
        //    _logger.LogWarning($"Błąd {action} dla {entity.GetType()} obiekt z wartościami {String.Join("/n",values)} ");
        //}
    }
}
