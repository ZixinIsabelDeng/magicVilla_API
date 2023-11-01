// Required namespaces for the repository
using magicVilla_VillaAPI.Data;
using magicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;  // Required for using 'Task'

// Define the namespace for the repository
namespace magicVilla_VillaAPI.Repository
{
    // Define a generic repository class that implements the IRepository interface.
    // The class is constrained to work only with classes ('where T : class').
    public class Repository<T> : IRepository<T> where T : class
    {
        // Declare an instance of the ApplicationDbContext to interact with the database.
        private readonly ApplicationDbContext _db;

        // Declare a DbSet to work with the entities of type T.
        internal DbSet<T> dbSet;

        // Constructor to initialize the database context and the DbSet.
        public Repository(ApplicationDbContext db)
        {
            _db = db;  // Initialize the database context
            this.dbSet = db.Set<T>();  // Initialize the DbSet for entities of type T
        }

        // Asynchronous method to create a new entity of type T.
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);  // Asynchronously add the entity to the DbSet.
            await SaveAsync();  // Asynchronously save changes to the database.
        }

        // Asynchronous method to get a single entity based on a filter, tracking option, and properties to include.
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;  // Start with a base query over the DbSet.

            // If tracking is not required, turn it off.
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            // If a filter is provided, apply it to the query.
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // If there are properties to include, add them to the query.
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);  // Include each property in the query.
                }
            }

            // Execute the query and return the first entity that satisfies the conditions.
            return await query.FirstOrDefaultAsync();
        }

        // Asynchronous method to get a list of entities based on a filter and properties to include.
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;  // Start with a base query over the DbSet.

            // If a filter is provided, apply it to the query.
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // If there are properties to include, add them to the query.
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);  // Include each property in the query.
                }
            }

            // Execute the query and return a list of entities that satisfy the conditions.
            return await query.ToListAsync();
        }

        // Asynchronous method to save changes to the database.
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();  // Commit the changes to the database.
        }

        // Asynchronous method to remove an entity of type T.
        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);  // Remove the entity from the DbSet.
            await SaveAsync();  // Asynchronously save changes to the database.
        }

        // Asynchronous method to update an existing entity of type T.
        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);  // Update the entity in the DbSet.
            await SaveAsync();  // Asynchronously save changes to the database.
        }
    }
}
