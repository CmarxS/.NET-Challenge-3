using MottoMap.Models;

namespace MottoMap.Data.Repository
{
    /// <summary>
    /// Interface espec�fica para opera��es do reposit�rio de Motos
    /// </summary>
    public interface IMotosRepository : IRepository<MotosEntity>
    {
        /// <summary>
        /// Busca motos por filial
        /// </summary>
        /// <param name="idFilial">ID da filial</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>P�gina de motos da filial</returns>
        Task<DataPage<MotosEntity>> GetByFilialAsync(int idFilial, PaginationParameters paginationParameters);
        
        /// <summary>
        /// Busca moto por placa
        /// </summary>
        /// <param name="placa">Placa da moto</param>
        /// <returns>Moto encontrada ou null</returns>
        Task<MotosEntity?> GetByPlacaAsync(string placa);
        
        /// <summary>
        /// Busca motos por marca
        /// </summary>
        /// <param name="marca">Marca da moto</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>P�gina de motos da marca</returns>
        Task<DataPage<MotosEntity>> GetByMarcaAsync(string marca, PaginationParameters paginationParameters);
        
        /// <summary>
        /// Busca motos por ano
        /// </summary>
        /// <param name="ano">Ano da moto</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>P�gina de motos do ano</returns>
        Task<DataPage<MotosEntity>> GetByAnoAsync(int ano, PaginationParameters paginationParameters);
        
        /// <summary>
        /// Busca motos por faixa de quilometragem
        /// </summary>
        /// <param name="quilometragemMin">Quilometragem m�nima</param>
        /// <param name="quilometragemMax">Quilometragem m�xima</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>P�gina de motos na faixa de quilometragem</returns>
        Task<DataPage<MotosEntity>> GetByQuilometragemRangeAsync(int quilometragemMin, int quilometragemMax, PaginationParameters paginationParameters);
        
        /// <summary>
        /// Verifica se placa j� existe para outra moto
        /// </summary>
        /// <param name="placa">Placa a ser verificada</param>
        /// <param name="idMotoAtual">ID da moto atual (para updates)</param>
        /// <returns>True se placa j� existe</returns>
        Task<bool> PlacaExistsAsync(string placa, int? idMotoAtual = null);
    }
}