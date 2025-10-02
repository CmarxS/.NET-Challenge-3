using Microsoft.AspNetCore.Mvc;
using MottoMap.Data.Repository;
using MottoMap.DTOs.Filial;
using MottoMap.DTOs.Common;
using MottoMap.Mappers;
using MottoMap.Models;

namespace MottoMap.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de filiais
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [Tags("?? Filiais")]
    public class FiliaisController : ControllerBase
    {
        private readonly IFilialRepository _filialRepository;

        public FiliaisController(IFilialRepository filialRepository)
        {
            _filialRepository = filialRepository;
        }

        /// <summary>
        /// Obt�m todas as filiais com pagina��o
        /// </summary>
        /// <param name="paginationParameters">Par�metros de pagina��o e filtros</param>
        /// <returns>Lista paginada de filiais</returns>
        /// <remarks>
        /// Exemplo de requisi��o:
        /// 
        ///     GET /api/v1/filiais?pageNumber=1&amp;pageSize=10&amp;searchTerm=s�o paulo&amp;sortBy=cidade
        /// 
        /// Par�metros de busca dispon�veis:
        /// - **searchTerm**: Busca por nome, endere�o, cidade, estado ou CEP
        /// - **sortBy**: Campos dispon�veis: nome, endereco, cidade, estado, cep
        /// </remarks>
        /// <response code="200">Lista de filiais retornada com sucesso</response>
        /// <response code="400">Par�metros de pagina��o inv�lidos</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponseDto<FilialResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResponseDto<FilialResponseDto>>> GetAllAsync(
            [FromQuery] PaginationParameters paginationParameters)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value!.Errors)
                    .Select(x => new ValidationErrorDto { Message = x.ErrorMessage })
                    .ToList();
                return BadRequest(PaginationMapper.CreateValidationError(errors));
            }

            var dataPage = await _filialRepository.GetAllAsync(paginationParameters);
            var response = FilialMapper.ToPagedResponseDto(dataPage);

            // Adicionar links HATEOAS
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var queryParams = PaginationMapper.ToQueryParameters(paginationParameters);
            PaginationMapper.AddNavigationLinks(response, baseUrl, queryParams);
            FilialMapper.AddHateoasLinks(response.Data, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }

        /// <summary>
        /// Obt�m uma filial espec�fica por ID
        /// </summary>
        /// <param name="id">ID da filial</param>
        /// <returns>Dados da filial</returns>
        /// <response code="200">Filial encontrada</response>
        /// <response code="404">Filial n�o encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FilialResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FilialResponseDto>> GetByIdAsync(int id)
        {
            var filial = await _filialRepository.GetByIdAsync(id);
            if (filial == null)
            {
                return NotFound(PaginationMapper.CreateError(
                    "FILIAL_NOT_FOUND", 
                    $"Filial com ID {id} n�o foi encontrada"));
            }

            var response = FilialMapper.ToResponseDto(filial);
            FilialMapper.AddHateoasLinks(response, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }

        /// <summary>
        /// Obt�m uma filial com todos os relacionamentos (funcion�rios e motos)
        /// </summary>
        /// <param name="id">ID da filial</param>
        /// <returns>Dados completos da filial</returns>
        /// <response code="200">Filial encontrada com relacionamentos</response>
        /// <response code="404">Filial n�o encontrada</response>
        [HttpGet("{id}/detalhes")]
        [ProducesResponseType(typeof(FilialDetailResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FilialDetailResponseDto>> GetDetailsByIdAsync(int id)
        {
            var filial = await _filialRepository.GetWithRelationsAsync(id);
            if (filial == null)
            {
                return NotFound(PaginationMapper.CreateError(
                    "FILIAL_NOT_FOUND", 
                    $"Filial com ID {id} n�o foi encontrada"));
            }

            var response = FilialMapper.ToDetailResponseDto(filial);
            FilialMapper.AddHateoasLinks(response, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }

        /// <summary>
        /// Cria uma nova filial
        /// </summary>
        /// <param name="createDto">Dados da filial a ser criada</param>
        /// <returns>Filial criada</returns>
        /// <response code="201">Filial criada com sucesso</response>
        /// <response code="400">Dados inv�lidos</response>
        [HttpPost]
        [ProducesResponseType(typeof(FilialResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FilialResponseDto>> CreateAsync(
            [FromBody] CreateFilialDto createDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value!.Errors)
                    .Select(x => new ValidationErrorDto { Message = x.ErrorMessage })
                    .ToList();
                return BadRequest(PaginationMapper.CreateValidationError(errors));
            }

            var entity = FilialMapper.ToEntity(createDto);
            var createdEntity = await _filialRepository.AddAsync(entity);
            var response = FilialMapper.ToResponseDto(createdEntity);
            
            FilialMapper.AddHateoasLinks(response, $"{Request.Scheme}://{Request.Host}/api/v1");

            return StatusCode(201, response);
        }

        /// <summary>
        /// Atualiza uma filial existente
        /// </summary>
        /// <param name="id">ID da filial</param>
        /// <param name="updateDto">Dados atualizados da filial</param>
        /// <returns>Filial atualizada</returns>
        /// <response code="200">Filial atualizada com sucesso</response>
        /// <response code="400">Dados inv�lidos</response>
        /// <response code="404">Filial n�o encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FilialResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FilialResponseDto>> UpdateAsync(
            int id, 
            [FromBody] UpdateFilialDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value!.Errors)
                    .Select(x => new ValidationErrorDto { Message = x.ErrorMessage })
                    .ToList();
                return BadRequest(PaginationMapper.CreateValidationError(errors));
            }

            var existingEntity = await _filialRepository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return NotFound(PaginationMapper.CreateError(
                    "FILIAL_NOT_FOUND", 
                    $"Filial com ID {id} n�o foi encontrada"));
            }

            FilialMapper.UpdateEntity(existingEntity, updateDto);
            var updatedEntity = await _filialRepository.UpdateAsync(existingEntity);
            var response = FilialMapper.ToResponseDto(updatedEntity);
            
            FilialMapper.AddHateoasLinks(response, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }

        /// <summary>
        /// Remove uma filial
        /// </summary>
        /// <param name="id">ID da filial</param>
        /// <returns>Confirma��o de remo��o</returns>
        /// <response code="204">Filial removida com sucesso</response>
        /// <response code="404">Filial n�o encontrada</response>
        /// <response code="409">Filial possui relacionamentos que impedem a remo��o</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await _filialRepository.ExistsAsync(id))
            {
                return NotFound(PaginationMapper.CreateError(
                    "FILIAL_NOT_FOUND", 
                    $"Filial com ID {id} n�o foi encontrada"));
            }

            try
            {
                await _filialRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return Conflict(PaginationMapper.CreateError(
                    "FILIAL_HAS_DEPENDENCIES", 
                    "N�o � poss�vel remover esta filial pois ela possui funcion�rios ou motos associados"));
            }
        }

        /// <summary>
        /// Obt�m filiais por cidade
        /// </summary>
        /// <param name="cidade">Nome da cidade</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>Lista paginada de filiais da cidade</returns>
        /// <response code="200">Lista de filiais da cidade</response>
        [HttpGet("cidade/{cidade}")]
        [ProducesResponseType(typeof(PagedResponseDto<FilialResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDto<FilialResponseDto>>> GetByCidadeAsync(
            string cidade,
            [FromQuery] PaginationParameters paginationParameters)
        {
            var dataPage = await _filialRepository.GetByCidadeAsync(cidade, paginationParameters);
            var response = FilialMapper.ToPagedResponseDto(dataPage);

            // Adicionar links HATEOAS
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var queryParams = PaginationMapper.ToQueryParameters(paginationParameters);
            PaginationMapper.AddNavigationLinks(response, baseUrl, queryParams);
            FilialMapper.AddHateoasLinks(response.Data, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }

        /// <summary>
        /// Obt�m filiais por estado
        /// </summary>
        /// <param name="estado">Sigla do estado (2 caracteres)</param>
        /// <param name="paginationParameters">Par�metros de pagina��o</param>
        /// <returns>Lista paginada de filiais do estado</returns>
        /// <response code="200">Lista de filiais do estado</response>
        /// <response code="400">Estado inv�lido</response>
        [HttpGet("estado/{estado}")]
        [ProducesResponseType(typeof(PagedResponseDto<FilialResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResponseDto<FilialResponseDto>>> GetByEstadoAsync(
            string estado,
            [FromQuery] PaginationParameters paginationParameters)
        {
            if (string.IsNullOrWhiteSpace(estado) || estado.Length != 2)
            {
                return BadRequest(PaginationMapper.CreateError(
                    "INVALID_STATE", 
                    "Estado deve conter exatamente 2 caracteres"));
            }

            var dataPage = await _filialRepository.GetByEstadoAsync(estado.ToUpper(), paginationParameters);
            var response = FilialMapper.ToPagedResponseDto(dataPage);

            // Adicionar links HATEOAS
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var queryParams = PaginationMapper.ToQueryParameters(paginationParameters);
            PaginationMapper.AddNavigationLinks(response, baseUrl, queryParams);
            FilialMapper.AddHateoasLinks(response.Data, $"{Request.Scheme}://{Request.Host}/api/v1");

            return Ok(response);
        }

        /// <summary>
        /// Obt�m estat�sticas de uma filial
        /// </summary>
        /// <param name="id">ID da filial</param>
        /// <returns>Estat�sticas da filial (quantidade de funcion�rios e motos)</returns>
        /// <response code="200">Estat�sticas da filial</response>
        /// <response code="404">Filial n�o encontrada</response>
        [HttpGet("{id}/estatisticas")]
        [ProducesResponseType(typeof(DTOs.Filial.FilialStatsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DTOs.Filial.FilialStatsDto>> GetStatsAsync(int id)
        {
            if (!await _filialRepository.ExistsAsync(id))
            {
                return NotFound(PaginationMapper.CreateError(
                    "FILIAL_NOT_FOUND", 
                    $"Filial com ID {id} n�o foi encontrada"));
            }

            var repositoryStats = await _filialRepository.GetStatsAsync(id);
            
            // Converter do DTO do repository para o DTO da API
            var stats = new DTOs.Filial.FilialStatsDto
            {
                TotalFuncionarios = repositoryStats.TotalFuncionarios,
                TotalMotos = repositoryStats.TotalMotos
            };
            
            return Ok(stats);
        }
    }
}