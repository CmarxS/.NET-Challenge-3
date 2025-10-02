using System.ComponentModel.DataAnnotations;

namespace MottoMap.DTOs.Filial
{
    /// <summary>
    /// DTO para cria��o de uma nova filial
    /// </summary>
    public class CreateFilialDto
    {
        /// <summary>
        /// Nome da filial
        /// </summary>
        [Required(ErrorMessage = "Nome da filial � obrigat�rio")]
        [MaxLength(100, ErrorMessage = "Nome deve ter no m�ximo 100 caracteres")]
        public required string Nome { get; set; }

        /// <summary>
        /// Endere�o da filial
        /// </summary>
        [Required(ErrorMessage = "Endere�o � obrigat�rio")]
        [MaxLength(200, ErrorMessage = "Endere�o deve ter no m�ximo 200 caracteres")]
        public required string Endereco { get; set; }

        /// <summary>
        /// Cidade da filial
        /// </summary>
        [Required(ErrorMessage = "Cidade � obrigat�ria")]
        [MaxLength(80, ErrorMessage = "Cidade deve ter no m�ximo 80 caracteres")]
        public required string Cidade { get; set; }

        /// <summary>
        /// Estado da filial (sigla)
        /// </summary>
        [Required(ErrorMessage = "Estado � obrigat�rio")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Estado deve ter exatamente 2 caracteres")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Estado deve conter apenas letras mai�sculas")]
        public required string Estado { get; set; }

        /// <summary>
        /// CEP da filial (opcional)
        /// </summary>
        [MaxLength(10, ErrorMessage = "CEP deve ter no m�ximo 10 caracteres")]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP deve estar no formato 00000-000 ou 00000000")]
        public string? CEP { get; set; }
    }

    /// <summary>
    /// DTO para atualiza��o de uma filial existente
    /// </summary>
    public class UpdateFilialDto
    {
        /// <summary>
        /// Nome da filial
        /// </summary>
        [Required(ErrorMessage = "Nome da filial � obrigat�rio")]
        [MaxLength(100, ErrorMessage = "Nome deve ter no m�ximo 100 caracteres")]
        public required string Nome { get; set; }

        /// <summary>
        /// Endere�o da filial
        /// </summary>
        [Required(ErrorMessage = "Endere�o � obrigat�rio")]
        [MaxLength(200, ErrorMessage = "Endere�o deve ter no m�ximo 200 caracteres")]
        public required string Endereco { get; set; }

        /// <summary>
        /// Cidade da filial
        /// </summary>
        [Required(ErrorMessage = "Cidade � obrigat�ria")]
        [MaxLength(80, ErrorMessage = "Cidade deve ter no m�ximo 80 caracteres")]
        public required string Cidade { get; set; }

        /// <summary>
        /// Estado da filial (sigla)
        /// </summary>
        [Required(ErrorMessage = "Estado � obrigat�rio")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Estado deve ter exatamente 2 caracteres")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Estado deve conter apenas letras mai�sculas")]
        public required string Estado { get; set; }

        /// <summary>
        /// CEP da filial (opcional)
        /// </summary>
        [MaxLength(10, ErrorMessage = "CEP deve ter no m�ximo 10 caracteres")]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP deve estar no formato 00000-000 ou 00000000")]
        public string? CEP { get; set; }
    }

    /// <summary>
    /// DTO para resposta de filial (dados b�sicos)
    /// </summary>
    public class FilialResponseDto
    {
        /// <summary>
        /// ID �nico da filial
        /// </summary>
        public int IdFilial { get; set; }

        /// <summary>
        /// Nome da filial
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Endere�o da filial
        /// </summary>
        public string Endereco { get; set; } = string.Empty;

        /// <summary>
        /// Cidade da filial
        /// </summary>
        public string Cidade { get; set; } = string.Empty;

        /// <summary>
        /// Estado da filial
        /// </summary>
        public string Estado { get; set; } = string.Empty;

        /// <summary>
        /// CEP da filial
        /// </summary>
        public string? CEP { get; set; }

        /// <summary>
        /// Links HATEOAS para opera��es relacionadas
        /// </summary>
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// DTO para resposta detalhada da filial com relacionamentos
    /// </summary>
    public class FilialDetailResponseDto : FilialResponseDto
    {
        /// <summary>
        /// Funcion�rios da filial
        /// </summary>
        public List<FuncionarioSummaryDto> Funcionarios { get; set; } = new List<FuncionarioSummaryDto>();

        /// <summary>
        /// Motos da filial
        /// </summary>
        public List<MotoSummaryDto> Motos { get; set; } = new List<MotoSummaryDto>();

        /// <summary>
        /// Estat�sticas da filial
        /// </summary>
        public FilialStatsDto Stats { get; set; } = new FilialStatsDto();
    }

    /// <summary>
    /// DTO resumido do funcion�rio para ser usado em FilialDetailResponseDto
    /// </summary>
    public class FuncionarioSummaryDto
    {
        /// <summary>
        /// ID do funcion�rio
        /// </summary>
        public int IdFuncionario { get; set; }

        /// <summary>
        /// Nome do funcion�rio
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Email do funcion�rio
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Fun��o do funcion�rio
        /// </summary>
        public string Funcao { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO resumido da moto para ser usado em FilialDetailResponseDto
    /// </summary>
    public class MotoSummaryDto
    {
        /// <summary>
        /// ID da moto
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
        /// Placa da moto
        /// </summary>
        public string Placa { get; set; } = string.Empty;

        /// <summary>
        /// Ano da moto
        /// </summary>
        public int Ano { get; set; }
    }

    /// <summary>
    /// DTO para estat�sticas da filial
    /// </summary>
    public class FilialStatsDto
    {
        /// <summary>
        /// Total de funcion�rios na filial
        /// </summary>
        public int TotalFuncionarios { get; set; }

        /// <summary>
        /// Total de motos na filial
        /// </summary>
        public int TotalMotos { get; set; }
    }
}