using System.ComponentModel.DataAnnotations;

namespace MottoMap.DTOs.Motos
{
    /// <summary>
    /// DTO para cria��o de uma nova moto
    /// </summary>
    public class CreateMotoDto
    {
        /// <summary>
        /// Marca da moto
        /// </summary>
        [Required(ErrorMessage = "Marca � obrigat�ria")]
        [MaxLength(50, ErrorMessage = "Marca deve ter no m�ximo 50 caracteres")]
        public required string Marca { get; set; }

        /// <summary>
        /// Modelo da moto
        /// </summary>
        [Required(ErrorMessage = "Modelo � obrigat�rio")]
        [MaxLength(80, ErrorMessage = "Modelo deve ter no m�ximo 80 caracteres")]
        public required string Modelo { get; set; }

        /// <summary>
        /// Ano de fabrica��o da moto
        /// </summary>
        [Required(ErrorMessage = "Ano � obrigat�rio")]
        [Range(1900, 2030, ErrorMessage = "Ano deve estar entre 1900 e 2030")]
        public int Ano { get; set; }

        /// <summary>
        /// Placa da moto
        /// </summary>
        [Required(ErrorMessage = "Placa � obrigat�ria")]
        [MaxLength(10, ErrorMessage = "Placa deve ter no m�ximo 10 caracteres")]
        [RegularExpression(@"^[A-Z]{3}-\d{4}$|^[A-Z]{3}\d[A-Z]\d{2}$", 
            ErrorMessage = "Placa deve estar no formato ABC-1234 ou ABC1D23 (Mercosul)")]
        public required string Placa { get; set; }

        /// <summary>
        /// ID da filial onde a moto est� alocada
        /// </summary>
        [Required(ErrorMessage = "ID da Filial � obrigat�rio")]
        [Range(1, int.MaxValue, ErrorMessage = "ID da Filial deve ser maior que 0")]
        public int IdFilial { get; set; }

        /// <summary>
        /// Cor da moto (opcional)
        /// </summary>
        [MaxLength(30, ErrorMessage = "Cor deve ter no m�ximo 30 caracteres")]
        public string? Cor { get; set; }

        /// <summary>
        /// Quilometragem atual da moto (opcional)
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Quilometragem deve ser maior ou igual a zero")]
        public int? Quilometragem { get; set; }
    }

    /// <summary>
    /// DTO para atualiza��o de uma moto existente
    /// </summary>
    public class UpdateMotoDto
    {
        /// <summary>
        /// Marca da moto
        /// </summary>
        [Required(ErrorMessage = "Marca � obrigat�ria")]
        [MaxLength(50, ErrorMessage = "Marca deve ter no m�ximo 50 caracteres")]
        public required string Marca { get; set; }

        /// <summary>
        /// Modelo da moto
        /// </summary>
        [Required(ErrorMessage = "Modelo � obrigat�rio")]
        [MaxLength(80, ErrorMessage = "Modelo deve ter no m�ximo 80 caracteres")]
        public required string Modelo { get; set; }

        /// <summary>
        /// Ano de fabrica��o da moto
        /// </summary>
        [Required(ErrorMessage = "Ano � obrigat�rio")]
        [Range(1900, 2030, ErrorMessage = "Ano deve estar entre 1900 e 2030")]
        public int Ano { get; set; }

        /// <summary>
        /// Placa da moto
        /// </summary>
        [Required(ErrorMessage = "Placa � obrigat�ria")]
        [MaxLength(10, ErrorMessage = "Placa deve ter no m�ximo 10 caracteres")]
        [RegularExpression(@"^[A-Z]{3}-\d{4}$|^[A-Z]{3}\d[A-Z]\d{2}$", 
            ErrorMessage = "Placa deve estar no formato ABC-1234 ou ABC1D23 (Mercosul)")]
        public required string Placa { get; set; }

        /// <summary>
        /// ID da filial onde a moto est� alocada
        /// </summary>
        [Required(ErrorMessage = "ID da Filial � obrigat�rio")]
        [Range(1, int.MaxValue, ErrorMessage = "ID da Filial deve ser maior que 0")]
        public int IdFilial { get; set; }

        /// <summary>
        /// Cor da moto (opcional)
        /// </summary>
        [MaxLength(30, ErrorMessage = "Cor deve ter no m�ximo 30 caracteres")]
        public string? Cor { get; set; }

        /// <summary>
        /// Quilometragem atual da moto (opcional)
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Quilometragem deve ser maior ou igual a zero")]
        public int? Quilometragem { get; set; }
    }

    /// <summary>
    /// DTO para resposta de moto (dados completos)
    /// </summary>
    public class MotoResponseDto
    {
        /// <summary>
        /// ID �nico da moto
        /// </summary>
        public int IdMoto { get; set; }

        /// <summary>
        /// Marca da moto
        /// </summary>
        public string Marca { get; set; } = string.Empty;

        /// <summary>
        /// Modelo da moto
        /// </summary>
        public string Modelo { get; set; } = string.Empty;

        /// <summary>
        /// Ano de fabrica��o da moto
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// Placa da moto
        /// </summary>
        public string Placa { get; set; } = string.Empty;

        /// <summary>
        /// ID da filial onde a moto est� alocada
        /// </summary>
        public int IdFilial { get; set; }

        /// <summary>
        /// Cor da moto
        /// </summary>
        public string? Cor { get; set; }

        /// <summary>
        /// Quilometragem atual da moto
        /// </summary>
        public int? Quilometragem { get; set; }

        /// <summary>
        /// Informa��es da filial (se inclu�da)
        /// </summary>
        public MotoFilialSummaryDto? Filial { get; set; }

        /// <summary>
        /// Links HATEOAS para opera��es relacionadas
        /// </summary>
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// DTO resumido da filial para ser usado em MotoResponseDto
    /// </summary>
    public class MotoFilialSummaryDto
    {
        /// <summary>
        /// ID da filial
        /// </summary>
        public int IdFilial { get; set; }

        /// <summary>
        /// Nome da filial
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Cidade da filial
        /// </summary>
        public string Cidade { get; set; } = string.Empty;

        /// <summary>
        /// Estado da filial
        /// </summary>
        public string Estado { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para filtros espec�ficos de motos
    /// </summary>
    public class MotoFilterDto
    {
        /// <summary>
        /// Filtrar por marca
        /// </summary>
        public string? Marca { get; set; }

        /// <summary>
        /// Filtrar por ano espec�fico
        /// </summary>
        [Range(1900, 2030, ErrorMessage = "Ano deve estar entre 1900 e 2030")]
        public int? Ano { get; set; }

        /// <summary>
        /// Filtrar por faixa de quilometragem m�nima
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Quilometragem m�nima deve ser maior ou igual a zero")]
        public int? QuilometragemMin { get; set; }

        /// <summary>
        /// Filtrar por faixa de quilometragem m�xima
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Quilometragem m�xima deve ser maior ou igual a zero")]
        public int? QuilometragemMax { get; set; }

        /// <summary>
        /// Filtrar por filial espec�fica
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "ID da Filial deve ser maior que 0")]
        public int? IdFilial { get; set; }
    }
}