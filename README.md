# ??? MottoMap API

Sistema de Gest�o de Frota de Motos - API REST completa desenvolvida em .NET 8.0 com Entity Framework Core e Oracle Database.

## ?? Vis�o Geral

A **MottoMap API** � uma solu��o completa para gerenciamento de frotas de motocicletas, oferecendo recursos avan�ados de CRUD, pagina��o, filtros e HATEOAS para navegabilidade da API.

### ?? Funcionalidades Principais

- ? **Gest�o de Filiais**: Controle de unidades por cidade/estado
- ? **Gest�o de Funcion�rios**: Cadastro com valida��o de email �nico
- ? **Gest�o de Motos**: Controle de frota com placas antigas e Mercosul
- ? **Relacionamentos**: Filiais ? Funcion�rios e Motos
- ? **Filtros Avan�ados**: Busca por m�ltiplos crit�rios
- ? **Estat�sticas**: Relat�rios de ocupa��o por filial

## ??? Arquitetura

### Padr�es Implementados
- **Repository Pattern** para acesso a dados
- **DTO Pattern** para transfer�ncia de dados
- **Mapper Pattern** para convers�es
- **HATEOAS** para descoberta de recursos
- **RESTful API** com status codes apropriados

### Tecnologias Utilizadas
- **.NET 8.0** - Framework principal
- **Entity Framework Core 9.0** - ORM
- **Oracle Database** - Banco de dados
- **Swagger/OpenAPI** - Documenta��o da API
- **C# 12** - Linguagem de programa��o

## ?? Modelo de Dados

### Entidades Principais

#### ?? Filiais (`NET_C3_Filial`)
```csharp
- IdFilial (PK, Identity)
- Nome (required, max 100 chars)
- Endereco (required, max 200 chars)
- Cidade (required, max 80 chars)
- Estado (required, 2 chars, mai�sculo)
- CEP (optional, max 10 chars, formato: 00000-000)
```

#### ????? Funcion�rios (`NET_C3_Funcionario`)
```csharp
- IdFuncionario (PK, Identity)
- Nome (required, max 100 chars)
- Email (required, max 150 chars, unique, email format)
- IdFilial (FK to Filial)
- Funcao (required, max 80 chars)
```

#### ??? Motos (`NET_C3_Motos`)
```csharp
- IdMoto (PK, Identity)
- Marca (required, max 50 chars)
- Modelo (required, max 80 chars)
- Ano (required, range 1900-2030)
- Placa (required, max 10 chars, unique, formato ABC-1234 ou ABC1D23)
- IdFilial (FK to Filial)
- Cor (optional, max 30 chars)
- Quilometragem (optional, >= 0)
```

### Relacionamentos
- **Filial** 1:N **Funcion�rios**
- **Filial** 1:N **Motos**

## ?? Endpoints da API

### Base URL
- **Development**: `https://localhost:7001/api/v1`

### ?? Filiais (`/filiais`)

| M�todo | Endpoint | Descri��o |
|--------|----------|-----------|
| GET | `/filiais` | Lista paginada de filiais |
| GET | `/filiais/{id}` | Busca filial por ID |
| GET | `/filiais/{id}/detalhes` | Filial com relacionamentos |
| GET | `/filiais/{id}/estatisticas` | Estat�sticas da filial |
| GET | `/filiais/cidade/{cidade}` | Filiais por cidade |
| GET | `/filiais/estado/{estado}` | Filiais por estado |
| POST | `/filiais` | Criar nova filial |
| PUT | `/filiais/{id}` | Atualizar filial |
| DELETE | `/filiais/{id}` | Remover filial |

### ????? Funcion�rios (`/funcionarios`)

| M�todo | Endpoint | Descri��o |
|--------|----------|-----------|
| GET | `/funcionarios` | Lista paginada de funcion�rios |
| GET | `/funcionarios/{id}` | Busca funcion�rio por ID |
| GET | `/funcionarios/email/{email}` | Busca por email |
| GET | `/funcionarios/filial/{idFilial}` | Funcion�rios por filial |
| POST | `/funcionarios` | Criar funcion�rio |
| PUT | `/funcionarios/{id}` | Atualizar funcion�rio |
| DELETE | `/funcionarios/{id}` | Remover funcion�rio |

### ??? Motos (`/motos`)

| M�todo | Endpoint | Descri��o |
|--------|----------|-----------|
| GET | `/motos` | Lista paginada com filtros avan�ados |
| GET | `/motos/{id}` | Busca moto por ID |
| GET | `/motos/placa/{placa}` | Busca por placa |
| GET | `/motos/filial/{idFilial}` | Motos por filial |
| GET | `/motos/marca/{marca}` | Motos por marca |
| GET | `/motos/ano/{ano}` | Motos por ano |
| POST | `/motos` | Criar moto |
| PUT | `/motos/{id}` | Atualizar moto |
| DELETE | `/motos/{id}` | Remover moto |

## ?? Par�metros de Consulta

### Pagina��o (Todos os endpoints GET de lista)
```
?pageNumber=1&pageSize=10&searchTerm=termo&sortBy=campo&sortDirection=asc
```

### Filtros Espec�ficos (Motos)
```
?marca=Honda&ano=2023&quilometragemMin=1000&quilometragemMax=50000&idFilial=1
```

## ?? Exemplos de Uso

### Criar uma Filial
```bash
POST /api/v1/filiais
Content-Type: application/json

{
  "nome": "Filial S�o Paulo - Centro",
  "endereco": "Rua Augusta, 1000",
  "cidade": "S�o Paulo",
  "estado": "SP",
  "cep": "01310-100"
}
```

### Criar um Funcion�rio
```bash
POST /api/v1/funcionarios
Content-Type: application/json

{
  "nome": "Jo�o Silva Santos",
  "email": "joao.silva@mottomap.com",
  "idFilial": 1,
  "funcao": "Gerente Operacional"
}
```

### Criar uma Moto
```bash
POST /api/v1/motos
Content-Type: application/json

{
  "marca": "Honda",
  "modelo": "CG 160 Titan",
  "ano": 2023,
  "placa": "ABC-1234",
  "idFilial": 1,
  "cor": "Vermelha",
  "quilometragem": 5000
}
```

### Buscar Motos com Filtros
```bash
GET /api/v1/motos?marca=Honda&ano=2023&pageSize=5&sortBy=modelo
```

## ?? Configura��o e Instala��o

### Pr�-requisitos
- .NET 8.0 SDK
- Oracle Database (11g ou superior)
- Visual Studio 2022 ou VS Code

### Configura��o do Banco de Dados

1. **Editar `appsettings.json`**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=seu_usuario;Password=sua_senha;Data Source=servidor:porta/sid;"
  }
}
```

2. **Executar Migrations**:
```bash
dotnet ef database update
```

### Executar a Aplica��o

```bash
# Restaurar pacotes
dotnet restore

# Compilar
dotnet build

# Executar
dotnet run
```

A API estar� dispon�vel em:
- **HTTPS**: https://localhost:7001
- **HTTP**: http://localhost:5001
- **Swagger**: https://localhost:7001 (raiz)

## ?? Documenta��o

### Swagger/OpenAPI
A documenta��o interativa est� dispon�vel na raiz da aplica��o quando executada em modo de desenvolvimento.

**Recursos do Swagger**:
- ?? Documenta��o completa de todos os endpoints
- ?? Interface "Try It Out" para testes
- ?? Schemas detalhados dos DTOs
- ?? Exemplos de payloads
- ??? Organiza��o por tags com emojis

## ??? Valida��es e Regras de Neg�cio

### Valida��es Autom�ticas
- **Email �nico** por funcion�rio
- **Placa �nica** por moto
- **Formato de email** v�lido
- **Formato de placa** (ABC-1234 ou ABC1D23 Mercosul)
- **Estado** sempre em mai�sculo (2 caracteres)
- **CEP** no formato 00000-000
- **Ano** entre 1900-2030
- **Quilometragem** >= 0

### Relacionamentos
- **Funcion�rios** devem pertencer a uma filial existente
- **Motos** devem estar alocadas a uma filial existente
- **Filiais** n�o podem ser removidas se possuem funcion�rios ou motos

## ?? HATEOAS

Todas as respostas incluem links de navega��o:

```json
{
  "idFuncionario": 1,
  "nome": "Jo�o Silva",
  "links": {
    "self": "/api/v1/funcionarios/1",
    "update": "/api/v1/funcionarios/1",
    "delete": "/api/v1/funcionarios/1",
    "filial": "/api/v1/filiais/1",
    "all": "/api/v1/funcionarios"
  }
}
```

## ?? Status Codes

| C�digo | Significado | Uso |
|--------|-------------|-----|
| 200 | OK | Busca/Atualiza��o bem-sucedida |
| 201 | Created | Recurso criado com sucesso |
| 204 | No Content | Remo��o bem-sucedida |
| 400 | Bad Request | Dados inv�lidos |
| 404 | Not Found | Recurso n�o encontrado |
| 409 | Conflict | Conflito (email/placa duplicados) |

## ?? Estrutura do Projeto

```
MottoMap/
??? Controllers/           # Controladores da API
??? Models/               # Entidades do banco de dados
??? DTOs/                # Objetos de transfer�ncia de dados
??? Mappers/             # Conversores Entity ? DTO
??? Data/
?   ??? AppData/         # Contexto do banco
?   ??? Repository/      # Reposit�rios de acesso a dados
??? Migrations/          # Migra��es do Entity Framework
??? wwwroot/            # Arquivos est�ticos (CSS do Swagger)
```

## ?? Testando a API

### Usar Swagger UI
1. Execute a aplica��o
2. Acesse https://localhost:7001
3. Use a interface "Try It Out"

### Usar cURL
```bash
# Listar filiais
curl -X GET "https://localhost:7001/api/v1/filiais" -H "accept: application/json"

# Criar funcion�rio
curl -X POST "https://localhost:7001/api/v1/funcionarios" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Maria Santos",
    "email": "maria@mottomap.com",
    "idFilial": 1,
    "funcao": "Analista"
  }'
```

## ?? Contribuindo

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudan�as (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## ?? Licen�a

Este projeto est� sob a licen�a MIT. Veja o arquivo `LICENSE` para mais detalhes.

## ?? Equipe de Desenvolvimento

- **MottoMap Development Team**
- **Email**: dev@mottomap.com
- **GitHub**: https://github.com/mottomap/api

---

<div align="center">

**??? MottoMap API - Gest�o Inteligente de Frotas de Motos**

*Desenvolvido com ?? usando .NET 8.0 & Oracle Database*

</div>