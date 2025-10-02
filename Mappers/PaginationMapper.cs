using MottoMap.Models;
using MottoMap.DTOs.Common;

namespace MottoMap.Mappers
{
    /// <summary>
    /// Mapper utilit�rio para convers�es gen�ricas de pagina��o
    /// </summary>
    public static class PaginationMapper
    {
        /// <summary>
        /// Converte DataPage gen�rico para PagedResponseDto usando uma fun��o de mapeamento
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade</typeparam>
        /// <typeparam name="TDto">Tipo do DTO</typeparam>
        /// <param name="dataPage">P�gina de dados das entidades</param>
        /// <param name="mapFunction">Fun��o para mapear entidade para DTO</param>
        /// <returns>Resposta paginada de DTOs</returns>
        public static PagedResponseDto<TDto> ToPagedResponseDto<TEntity, TDto>(
            DataPage<TEntity> dataPage, 
            Func<TEntity, TDto> mapFunction)
        {
            var dtos = dataPage.Data.Select(mapFunction).ToList();
            
            var pagedResponse = new PagedResponseDto<TDto>(
                dtos,
                dataPage.PageNumber,
                dataPage.PageSize,
                dataPage.TotalItems
            );

            // Copiar links HATEOAS se existirem
            foreach (var link in dataPage.Links)
            {
                pagedResponse.Links[link.Key] = link.Value;
            }

            return pagedResponse;
        }

        /// <summary>
        /// Adiciona links de navega��o HATEOAS para pagina��o
        /// </summary>
        /// <typeparam name="T">Tipo do DTO</typeparam>
        /// <param name="pagedResponse">Resposta paginada</param>
        /// <param name="baseUrl">URL base do endpoint</param>
        /// <param name="additionalParams">Par�metros adicionais para manter na URL</param>
        public static void AddNavigationLinks<T>(
            PagedResponseDto<T> pagedResponse, 
            string baseUrl, 
            Dictionary<string, string>? additionalParams = null)
        {
            var paramString = BuildQueryString(additionalParams);
            var separator = string.IsNullOrEmpty(paramString) ? "?" : "&";

            // Link para a pr�pria p�gina
            pagedResponse.Links["self"] = $"{baseUrl}{paramString}{separator}pageNumber={pagedResponse.Pagination.PageNumber}&pageSize={pagedResponse.Pagination.PageSize}";
            
            if (pagedResponse.Pagination.TotalPages > 0)
            {
                // Link para a primeira p�gina
                pagedResponse.Links["first"] = $"{baseUrl}{paramString}{separator}pageNumber=1&pageSize={pagedResponse.Pagination.PageSize}";
                
                // Link para a �ltima p�gina
                pagedResponse.Links["last"] = $"{baseUrl}{paramString}{separator}pageNumber={pagedResponse.Pagination.TotalPages}&pageSize={pagedResponse.Pagination.PageSize}";
            }
            
            // Link para a p�gina anterior (se existir)
            if (pagedResponse.Pagination.HasPreviousPage)
            {
                pagedResponse.Links["prev"] = $"{baseUrl}{paramString}{separator}pageNumber={pagedResponse.Pagination.PageNumber - 1}&pageSize={pagedResponse.Pagination.PageSize}";
            }
            
            // Link para a pr�xima p�gina (se existir)
            if (pagedResponse.Pagination.HasNextPage)
            {
                pagedResponse.Links["next"] = $"{baseUrl}{paramString}{separator}pageNumber={pagedResponse.Pagination.PageNumber + 1}&pageSize={pagedResponse.Pagination.PageSize}";
            }
        }

        /// <summary>
        /// Constr�i query string a partir de par�metros adicionais
        /// </summary>
        /// <param name="parameters">Dicion�rio de par�metros</param>
        /// <returns>Query string formatada</returns>
        private static string BuildQueryString(Dictionary<string, string>? parameters)
        {
            if (parameters == null || !parameters.Any())
                return string.Empty;

            var validParams = parameters
                .Where(p => !string.IsNullOrWhiteSpace(p.Value))
                .Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}");

            var queryString = string.Join("&", validParams);
            return string.IsNullOrEmpty(queryString) ? string.Empty : $"?{queryString}";
        }

        /// <summary>
        /// Cria uma resposta de erro padronizada
        /// </summary>
        /// <param name="code">C�digo do erro</param>
        /// <param name="message">Mensagem do erro</param>
        /// <param name="details">Detalhes adicionais (opcional)</param>
        /// <returns>DTO de erro</returns>
        public static ErrorResponseDto CreateError(string code, string message, string? details = null)
        {
            return new ErrorResponseDto
            {
                Code = code,
                Message = message,
                Details = details,
                Timestamp = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Cria uma resposta de erro de valida��o
        /// </summary>
        /// <param name="validationErrors">Lista de erros de valida��o</param>
        /// <returns>DTO de erro com valida��es</returns>
        public static ErrorResponseDto CreateValidationError(List<ValidationErrorDto> validationErrors)
        {
            return new ErrorResponseDto
            {
                Code = "VALIDATION_ERROR",
                Message = "Um ou mais campos cont�m erros de valida��o",
                ValidationErrors = validationErrors,
                Timestamp = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Cria uma resposta de sucesso simples
        /// </summary>
        /// <param name="message">Mensagem de sucesso</param>
        /// <param name="data">Dados adicionais (opcional)</param>
        /// <returns>DTO de sucesso</returns>
        public static SuccessResponseDto CreateSuccess(string message, object? data = null)
        {
            return new SuccessResponseDto
            {
                Message = message,
                Data = data,
                Timestamp = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Converte PaginationParameters para Dictionary para uso em query strings
        /// </summary>
        /// <param name="parameters">Par�metros de pagina��o</param>
        /// <returns>Dictionary com par�metros</returns>
        public static Dictionary<string, string> ToQueryParameters(PaginationParameters parameters)
        {
            var dict = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
                dict["searchTerm"] = parameters.SearchTerm;

            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
                dict["sortBy"] = parameters.SortBy;

            if (!string.IsNullOrWhiteSpace(parameters.SortDirection) && parameters.SortDirection != "asc")
                dict["sortDirection"] = parameters.SortDirection;

            return dict;
        }
    }
}