namespace MottoMap.DTOs.Common
{
    /// <summary>
    /// DTO para resposta paginada gen�rica
    /// </summary>
    /// <typeparam name="T">Tipo dos dados paginados</typeparam>
    public class PagedResponseDto<T>
    {
        /// <summary>
        /// Dados da p�gina atual
        /// </summary>
        public IEnumerable<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Informa��es de pagina��o
        /// </summary>
        public PaginationInfoDto Pagination { get; set; } = new PaginationInfoDto();

        /// <summary>
        /// Links HATEOAS para navega��o
        /// </summary>
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Construtor para criar resposta paginada
        /// </summary>
        /// <param name="data">Dados da p�gina</param>
        /// <param name="pageNumber">N�mero da p�gina atual</param>
        /// <param name="pageSize">Tamanho da p�gina</param>
        /// <param name="totalItems">Total de itens</param>
        public PagedResponseDto(IEnumerable<T> data, int pageNumber, int pageSize, int totalItems)
        {
            Data = data;
            Pagination = new PaginationInfoDto
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalItems > 0 ? (int)Math.Ceiling((double)totalItems / pageSize) : 0,
                HasPreviousPage = pageNumber > 1,
                HasNextPage = pageNumber < (totalItems > 0 ? (int)Math.Ceiling((double)totalItems / pageSize) : 0)
            };
        }

        /// <summary>
        /// Construtor padr�o
        /// </summary>
        public PagedResponseDto()
        {
        }
    }

    /// <summary>
    /// DTO para informa��es de pagina��o
    /// </summary>
    public class PaginationInfoDto
    {
        /// <summary>
        /// N�mero da p�gina atual
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Tamanho da p�gina
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total de itens
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Total de p�ginas
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Indica se existe p�gina anterior
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Indica se existe pr�xima p�gina
        /// </summary>
        public bool HasNextPage { get; set; }
    }

    /// <summary>
    /// DTO para resposta de erro padronizada
    /// </summary>
    public class ErrorResponseDto
    {
        /// <summary>
        /// C�digo do erro
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Mensagem do erro
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Detalhes adicionais do erro (opcional)
        /// </summary>
        public string? Details { get; set; }

        /// <summary>
        /// Lista de erros de valida��o (quando aplic�vel)
        /// </summary>
        public List<ValidationErrorDto> ValidationErrors { get; set; } = new List<ValidationErrorDto>();

        /// <summary>
        /// Timestamp do erro
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// DTO para erro de valida��o
    /// </summary>
    public class ValidationErrorDto
    {
        /// <summary>
        /// Nome do campo com erro
        /// </summary>
        public string Field { get; set; } = string.Empty;

        /// <summary>
        /// Mensagem do erro de valida��o
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Valor que causou o erro (opcional)
        /// </summary>
        public object? Value { get; set; }
    }

    /// <summary>
    /// DTO para resposta de sucesso simples
    /// </summary>
    public class SuccessResponseDto
    {
        /// <summary>
        /// Mensagem de sucesso
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Dados adicionais (opcional)
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Timestamp da opera��o
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}