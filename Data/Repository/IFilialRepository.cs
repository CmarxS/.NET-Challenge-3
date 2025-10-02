using MottoMap.Models;

namespace MottoMap.Data.Repository
{
    /// <summary>
    /// Interface espec�fica para opera��es do reposit�rio de Filiais
    /// </summary>
    public interface IFilialRepository : IRepository<FilialEntity>
    {
        /// <summary>
        /// Busca filiais por cidade
        /// </summary>
        /// <param name="cidade">Nome da cidade</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>P�gina de filiais da cidade</returns>
        Task<DataPage<FilialEntity>> GetByCidadeAsync(string cidade, PaginationParameters paginationParameters);
        
        /// <summary>
        /// Busca filiais por estado
        /// </summary>
        /// <param name="estado">Sigla do estado</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>P�gina de filiais do estado</returns>
        Task<DataPage<FilialEntity>> GetByEstadoAsync(string estado, PaginationParameters paginationParameters);
        
        /// <summary>
        /// Obt�m filial com seus funcion�rios e motos
        /// </summary>
        /// <param name="id">ID da filial</param>
        /// <returns>Filial com funcion�rios e motos</returns>
        Task<FilialEntity?> GetWithRelationsAsync(int id);
        
        /// <summary>
        /// Obt�m estat�sticas da filial (quantidade de funcion�rios e motos)
        /// </summary>
        /// <param name="id">ID da filial</param>
        /// <returns>Objeto com estat�sticas da filial</returns>
        Task<FilialStatsDto> GetStatsAsync(int id);
    }

    /// <summary>
    /// DTO para estat�sticas da filial
    /// </summary>
    public class FilialStatsDto
    {
        public int IdFilial { get; set; }
        public string NomeFilial { get; set; } = string.Empty;
        public int TotalFuncionarios { get; set; }
        public int TotalMotos { get; set; }
    }
}