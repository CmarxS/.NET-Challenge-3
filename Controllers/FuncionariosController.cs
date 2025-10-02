using Microsoft.AspNetCore.Mvc;
using MottoMap.Data.Repository;
using MottoMap.DTOs.Funcionario;
using MottoMap.DTOs.Common;
using MottoMap.Mappers;
using MottoMap.Models;
using System.ComponentModel.DataAnnotations;

namespace MottoMap.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de funcion�rios
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [Tags("????? Funcion�rios")]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IFilialRepository _filialRepository;

        public FuncionariosController(
            IFuncionarioRepository funcionarioRepository,
            IFilialRepository filialRepository)
        {
            _funcionarioRepository = funcionarioRepository;
            _filialRepository = filialRepository;
        }

        /// <summary>
        /// Obt�m todos os funcion�rios com pagina��o
        /// </summary>
        /// <param name="paginationParameters">Par�metros de pagina��o e filtros</param>
        /// <returns>Lista paginada de funcion�rios</returns>
        /// <remarks>
        /// Exemplo de requisi��o:
        /// 
        ///     GET /api/v1/funcionarios?pageNumber=1&amp;pageSize=10&amp;searchTerm=jo�o&amp;sortBy=nome&amp;sortDirection=asc
        /// 
        /// Par�metros de busca dispon�veis:
        /// - **searchTerm**: Busca por nome, email, fun��o ou nome da filial
        /// - **sortBy**: Campos dispon�veis: nome, email, funcao, filial
        /// - **sortDirection**: asc ou desc
        /// </remarks>
        /// <response code="200">Lista de funcion�rios retornada com sucesso</response>
        /// <response code="400">Par�metros de pagina��o inv�lidos</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponseDto<FuncionarioResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResponseDto<FuncionarioResponseDto>>> GetAllAsync(
            [FromQuery] PaginationParameters paginationParameters)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value!.Errors)
                    .Select(x => new ValidationErrorDto { Message = x.ErrorMessage })
                    .ToList();
                return BadRequest(PaginationMapper.CreateValidationError(errors));
            }

            var dataPage = await _funcionarioRepository.GetAllAsync(paginationParameters);
            var response = FuncionarioMapper.ToPagedResponseDto(dataPage);

            // Adicionar links HATEOAS
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var queryParams = PaginationMapper.ToQueryParameters(paginationParameters);
            PaginationMapper.AddNavigationLinks(response, baseUrl, queryParams);
            FuncionarioMapper.AddHateoasLinks(response.Data, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }

        /// <summary>
        /// Obt�m um funcion�rio espec�fico por ID
        /// </summary>
        /// <param name="id">ID do funcion�rio</param>
        /// <returns>Dados do funcion�rio</returns>
        /// <response code="200">Funcion�rio encontrado</response>
        /// <response code="404">Funcion�rio n�o encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FuncionarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FuncionarioResponseDto>> GetByIdAsync(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null)
            {
                return NotFound(PaginationMapper.CreateError(
                    "FUNCIONARIO_NOT_FOUND", 
                    $"Funcion�rio com ID {id} n�o foi encontrado"));
            }

            var response = FuncionarioMapper.ToResponseDto(funcionario);
            FuncionarioMapper.AddHateoasLinks(response, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }

        /// <summary>
        /// Cria um novo funcion�rio
        /// </summary>
        /// <param name="createDto">Dados do funcion�rio a ser criado</param>
        /// <returns>Funcion�rio criado</returns>
        /// <response code="201">Funcion�rio criado com sucesso</response>
        /// <response code="400">Dados inv�lidos</response>
        /// <response code="409">Email j� existe</response>
        [HttpPost]
        [ProducesResponseType(typeof(FuncionarioResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<FuncionarioResponseDto>> CreateAsync(
            [FromBody] CreateFuncionarioDto createDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value!.Errors)
                    .Select(x => new ValidationErrorDto { Message = x.ErrorMessage })
                    .ToList();
                return BadRequest(PaginationMapper.CreateValidationError(errors));
            }

            // Verificar se a filial existe
            if (!await _filialRepository.ExistsAsync(createDto.IdFilial))
            {
                return BadRequest(PaginationMapper.CreateError(
                    "FILIAL_NOT_FOUND", 
                    $"Filial com ID {createDto.IdFilial} n�o foi encontrada"));
            }

            // Verificar se o email j� existe
            if (await _funcionarioRepository.EmailExistsAsync(createDto.Email))
            {
                return Conflict(PaginationMapper.CreateError(
                    "EMAIL_ALREADY_EXISTS", 
                    $"Email {createDto.Email} j� est� em uso"));
            }

            var entity = FuncionarioMapper.ToEntity(createDto);
            var createdEntity = await _funcionarioRepository.AddAsync(entity);
            var response = FuncionarioMapper.ToResponseDto(createdEntity);
            
            FuncionarioMapper.AddHateoasLinks(response, $"{Request.Scheme}://{Request.Host}/api/v1");

            return StatusCode(201, response);
        }

        /// <summary>
        /// Atualiza um funcion�rio existente
        /// </summary>
        /// <param name="id">ID do funcion�rio</param>
        /// <param name="updateDto">Dados atualizados do funcion�rio</param>
        /// <returns>Funcion�rio atualizado</returns>
        /// <response code="200">Funcion�rio atualizado com sucesso</response>
        /// <response code="400">Dados inv�lidos</response>
        /// <response code="404">Funcion�rio n�o encontrado</response>
        /// <response code="409">Email j� existe</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FuncionarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<FuncionarioResponseDto>> UpdateAsync(
            int id, 
            [FromBody] UpdateFuncionarioDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value!.Errors)
                    .Select(x => new ValidationErrorDto { Message = x.ErrorMessage })
                    .ToList();
                return BadRequest(PaginationMapper.CreateValidationError(errors));
            }

            var existingEntity = await _funcionarioRepository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return NotFound(PaginationMapper.CreateError(
                    "FUNCIONARIO_NOT_FOUND", 
                    $"Funcion�rio com ID {id} n�o foi encontrado"));
            }

            // Verificar se a filial existe
            if (!await _filialRepository.ExistsAsync(updateDto.IdFilial))
            {
                return BadRequest(PaginationMapper.CreateError(
                    "FILIAL_NOT_FOUND", 
                    $"Filial com ID {updateDto.IdFilial} n�o foi encontrada"));
            }

            // Verificar se o email j� existe para outro funcion�rio
            if (await _funcionarioRepository.EmailExistsAsync(updateDto.Email, id))
            {
                return Conflict(PaginationMapper.CreateError(
                    "EMAIL_ALREADY_EXISTS", 
                    $"Email {updateDto.Email} j� est� em uso"));
            }

            FuncionarioMapper.UpdateEntity(existingEntity, updateDto);
            var updatedEntity = await _funcionarioRepository.UpdateAsync(existingEntity);
            var response = FuncionarioMapper.ToResponseDto(updatedEntity);
            
            FuncionarioMapper.AddHateoasLinks(response, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }

        /// <summary>
        /// Remove um funcion�rio
        /// </summary>
        /// <param name="id">ID do funcion�rio</param>
        /// <returns>Confirma��o de remo��o</returns>
        /// <response code="204">Funcion�rio removido com sucesso</response>
        /// <response code="404">Funcion�rio n�o encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await _funcionarioRepository.ExistsAsync(id))
            {
                return NotFound(PaginationMapper.CreateError(
                    "FUNCIONARIO_NOT_FOUND", 
                    $"Funcion�rio com ID {id} n�o foi encontrado"));
            }

            await _funcionarioRepository.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Obt�m funcion�rios por filial
        /// </summary>
        /// <param name="idFilial">ID da filial</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>Lista paginada de funcion�rios da filial</returns>
        /// <response code="200">Lista de funcion�rios da filial</response>
        /// <response code="404">Filial n�o encontrada</response>
        [HttpGet("filial/{idFilial}")]
        [ProducesResponseType(typeof(PagedResponseDto<FuncionarioResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedResponseDto<FuncionarioResponseDto>>> GetByFilialAsync(
            int idFilial,
            [FromQuery] PaginationParameters paginationParameters)
        {
            if (!await _filialRepository.ExistsAsync(idFilial))
            {
                return NotFound(PaginationMapper.CreateError(
                    "FILIAL_NOT_FOUND", 
                    $"Filial com ID {idFilial} n�o foi encontrada"));
            }

            var dataPage = await _funcionarioRepository.GetByFilialAsync(idFilial, paginationParameters);
            var response = FuncionarioMapper.ToPagedResponseDto(dataPage);

            // Adicionar links HATEOAS
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var queryParams = PaginationMapper.ToQueryParameters(paginationParameters);
            PaginationMapper.AddNavigationLinks(response, baseUrl, queryParams);
            FuncionarioMapper.AddHateoasLinks(response.Data, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }

        /// <summary>
        /// Busca funcion�rio por email
        /// </summary>
        /// <param name="email">Email do funcion�rio</param>
        /// <returns>Dados do funcion�rio</returns>
        /// <response code="200">Funcion�rio encontrado</response>
        /// <response code="404">Funcion�rio n�o encontrado</response>
        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(FuncionarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FuncionarioResponseDto>> GetByEmailAsync(string email)
        {
            var funcionario = await _funcionarioRepository.GetByEmailAsync(email);
            if (funcionario == null)
            {
                return NotFound(PaginationMapper.CreateError(
                    "FUNCIONARIO_NOT_FOUND", 
                    $"Funcion�rio com email {email} n�o foi encontrado"));
            }

            var response = FuncionarioMapper.ToResponseDto(funcionario);
            FuncionarioMapper.AddHateoasLinks(response, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }
    }
}