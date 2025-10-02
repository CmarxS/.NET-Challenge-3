using MottoMap.Models;

namespace MottoMap.Data.Repository
{
    /// <summary>
    /// Interface espec�fica para opera��es do reposit�rio de Funcion�rios
    /// </summary>
    public interface IFuncionarioRepository : IRepository<FuncionarioEntity>
    {
        /// <summary>
        /// Busca funcion�rios por filial
        /// </summary>
        /// <param name="idFilial">ID da filial</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>P�gina de funcion�rios da filial</returns>
        Task<DataPage<FuncionarioEntity>> GetByFilialAsync(int idFilial, PaginationParameters paginationParameters);
        
        /// <summary>
        /// Busca funcion�rio por email
        /// </summary>
        /// <param name="email">Email do funcion�rio</param>
        /// <returns>Funcion�rio encontrado ou null</returns>
        Task<FuncionarioEntity?> GetByEmailAsync(string email);
        
        /// <summary>
        /// Busca funcion�rios por fun��o
        /// </summary>
        /// <param name="funcao">Fun��o do funcion�rio</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>P�gina de funcion�rios com a fun��o especificada</returns>
        Task<DataPage<FuncionarioEntity>> GetByFuncaoAsync(string funcao, PaginationParameters paginationParameters);
        
        /// <summary>
        /// Verifica se email j� existe para outro funcion�rio
        /// </summary>
        /// <param name="email">Email a ser verificado</param>
        /// <param name="idFuncionarioAtual">ID do funcion�rio atual (para updates)</param>
        /// <returns>True se email j� existe</returns>
        Task<bool> EmailExistsAsync(string email, int? idFuncionarioAtual = null);
    }
}