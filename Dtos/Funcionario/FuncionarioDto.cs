using System.ComponentModel.DataAnnotations;

namespace MottoMap.DTOs.Funcionario
{
    /// <summary>
    /// DTO para cria��o de um novo funcion�rio
    /// </summary>
    public class CreateFuncionarioDto
    {
        /// <summary>
        /// Nome do funcion�rio
        /// </summary>
        [Required(ErrorMessage = "Nome � obrigat�rio")]
        [MaxLength(100, ErrorMessage = "Nome deve ter no m�ximo 100 caracteres")]
        public required string Nome { get; set; }

        /// <summary>
        /// Email do funcion�rio
        /// </summary>
        [Required(ErrorMessage = "Email � obrigat�rio")]
        [MaxLength(150, ErrorMessage = "Email deve ter no m�ximo 150 caracteres")]
        [EmailAddress(ErrorMessage = "Email deve ter formato v�lido")]
        public required string Email { get; set; }

        /// <summary>
        /// ID da filial onde o funcion�rio trabalha
        /// </summary>
        [Required(ErrorMessage = "ID da Filial � obrigat�rio")]
        [Range(1, int.MaxValue, ErrorMessage = "ID da Filial deve ser maior que 0")]
        public int IdFilial { get; set; }

        /// <summary>
        /// Fun��o do funcion�rio
        /// </summary>
        [Required(ErrorMessage = "Fun��o � obrigat�ria")]
        [MaxLength(80, ErrorMessage = "Fun��o deve ter no m�ximo 80 caracteres")]
        public required string Funcao { get; set; }
    }

    /// <summary>
    /// DTO para atualiza��o de um funcion�rio existente
    /// </summary>
    public class UpdateFuncionarioDto
    {
        /// <summary>
        /// Nome do funcion�rio
        /// </summary>
        [Required(ErrorMessage = "Nome � obrigat�rio")]
        [MaxLength(100, ErrorMessage = "Nome deve ter no m�ximo 100 caracteres")]
        public required string Nome { get; set; }

        /// <summary>
        /// Email do funcion�rio
        /// </summary>
        [Required(ErrorMessage = "Email � obrigat�rio")]
        [MaxLength(150, ErrorMessage = "Email deve ter no m�ximo 150 caracteres")]
        [EmailAddress(ErrorMessage = "Email deve ter formato v�lido")]
        public required string Email { get; set; }

        /// <summary>
        /// ID da filial onde o funcion�rio trabalha
        /// </summary>
        [Required(ErrorMessage = "ID da Filial � obrigat�rio")]
        [Range(1, int.MaxValue, ErrorMessage = "ID da Filial deve ser maior que 0")]
        public int IdFilial { get; set; }

        /// <summary>
        /// Fun��o do funcion�rio
        /// </summary>
        [Required(ErrorMessage = "Fun��o � obrigat�ria")]
        [MaxLength(80, ErrorMessage = "Fun��o deve ter no m�ximo 80 caracteres")]
        public required string Funcao { get; set; }
    }

    /// <summary>
    /// DTO para resposta de funcion�rio (dados completos)
    /// </summary>
    public class FuncionarioResponseDto
    {
        /// <summary>
        /// ID �nico do funcion�rio
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
        /// ID da filial onde o funcion�rio trabalha
        /// </summary>
        public int IdFilial { get; set; }

        /// <summary>
        /// Fun��o do funcion�rio
        /// </summary>
        public string Funcao { get; set; } = string.Empty;

        /// <summary>
        /// Informa��es da filial (se inclu�da)
        /// </summary>
        public FilialSummaryDto? Filial { get; set; }

        /// <summary>
        /// Links HATEOAS para opera��es relacionadas
        /// </summary>
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// DTO resumido da filial para ser usado em FuncionarioResponseDto
    /// </summary>
    public class FilialSummaryDto
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
}