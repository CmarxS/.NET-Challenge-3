using Microsoft.EntityFrameworkCore;
using MottoMap.Data.AppData;
using MottoMap.Models;
using System.Linq.Expressions;

namespace MottoMap.Data.Repository
{
    /// <summary>
    /// Implementa��o base do reposit�rio gen�rico
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<DataPage<T>> GetAllAsync(PaginationParameters paginationParameters)
        {
            var query = _dbSet.AsQueryable();

            // Aplicar filtro de busca se fornecido
            if (!string.IsNullOrEmpty(paginationParameters.SearchTerm))
            {
                query = ApplySearch(query, paginationParameters.SearchTerm);
            }

            // Aplicar ordena��o se fornecida
            if (!string.IsNullOrEmpty(paginationParameters.SortBy))
            {
                query = ApplySort(query, paginationParameters.SortBy, paginationParameters.SortDirection);
            }

            // Contar total de itens
            var totalItems = await query.CountAsync();

            // Aplicar pagina��o
            var items = await query
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .ToListAsync();

            return new DataPage<T>(items, paginationParameters.PageNumber, paginationParameters.PageSize, totalItems);
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.FindAsync(id) != null;
        }

        /// <summary>
        /// M�todo virtual para aplicar busca/filtro - deve ser sobrescrito pelas classes filhas
        /// </summary>
        /// <param name="query">Query base</param>
        /// <param name="searchTerm">Termo de busca</param>
        /// <returns>Query com filtro aplicado</returns>
        protected virtual IQueryable<T> ApplySearch(IQueryable<T> query, string searchTerm)
        {
            // Implementa��o padr�o - retorna query sem filtro
            return query;
        }

        /// <summary>
        /// M�todo virtual para aplicar ordena��o - deve ser sobrescrito pelas classes filhas
        /// </summary>
        /// <param name="query">Query base</param>
        /// <param name="sortBy">Campo para ordena��o</param>
        /// <param name="sortDirection">Dire��o da ordena��o</param>
        /// <returns>Query com ordena��o aplicada</returns>
        protected virtual IQueryable<T> ApplySort(IQueryable<T> query, string sortBy, string sortDirection)
        {
            // Implementa��o padr�o - retorna query sem ordena��o espec�fica
            return query;
        }
    }
}