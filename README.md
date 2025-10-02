
# 🚗 MottoMap API

Sistema de Gestão de Frota de Motos - API REST completa desenvolvida em .NET 8.0 com Entity Framework Core e Oracle Database.

## 👨‍💻 Equipe de Desenvolvimento

- **RM555997** - Caio Marques
- **RM558640** - Caio Amarante
- **RM556325** - Felipe Camargo

## 📋 Visão Geral

A **MottoMap API** é uma solução completa para gerenciamento de frotas de motocicletas, oferecendo recursos avançados de CRUD, paginação, filtros e HATEOAS para navegabilidade da API.

### 🔑 Funcionalidades Principais

- 🚧 **Gestão de Filiais**: Controle de unidades por cidade/estado
- 👩‍💻 **Gestão de Funcionários**: Cadastro com validação de email único
- 🏍️ **Gestão de Motos**: Controle de frota com placas antigas e Mercosul
- 🔗 **Relacionamentos**: Filiais ↔ Funcionários e Motos
- 🔍 **Filtros Avançados**: Busca por múltiplos critérios
- 📊 **Estatísticas**: Relatórios de ocupação por filial

## 🏛️ Arquitetura

### Padrões Implementados
- **Repository Pattern** para acesso a dados
- **DTO Pattern** para transferência de dados
- **Mapper Pattern** para conversões
- **HATEOAS** para descoberta de recursos
- **RESTful API** com status codes apropriados

### Tecnologias Utilizadas
- **.NET 8.0** - Framework principal
- **Entity Framework Core 9.0** - ORM
- **Oracle Database** - Banco de dados
- **Swagger/OpenAPI** - Documentação da API
- **C# 12** - Linguagem de programação

## 📚 Modelo de Dados

### Entidades Principais

#### 🏢 Filiais (`NET_C3_Filial`)
```csharp
- IdFilial (PK, Identity)
- Nome (required, max 100 chars)
- Endereco (required, max 200 chars)
- Cidade (required, max 80 chars)
- Estado (required, 2 chars, maiúsculo)
- CEP (optional, max 10 chars, formato: 00000-000)
```

#### 🧑‍💼 Funcionários (`NET_C3_Funcionario`)
```csharp
- IdFuncionario (PK, Identity)
- Nome (required, max 100 chars)
- Email (required, max 150 chars, unique, email format)
- IdFilial (FK to Filial)
- Funcao (required, max 80 chars)
```

#### 🏍️ Motos (`NET_C3_Motos`)
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
- **Filial** 1:N **Funcionários**
- **Filial** 1:N **Motos**

## 🌐 Endpoints da API

### Base URL
- **Development**: `https://localhost:7001/api/v1`

### 🏢 Filiais (`/filiais`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/filiais` | Lista paginada de filiais |
| GET | `/filiais/{id}` | Busca filial por ID |
| GET | `/filiais/{id}/detalhes` | Filial com relacionamentos |
| GET | `/filiais/{id}/estatisticas` | Estatísticas da filial |
| GET | `/filiais/cidade/{cidade}` | Filiais por cidade |
| GET | `/filiais/estado/{estado}` | Filiais por estado |
| POST | `/filiais` | Criar nova filial |
| PUT | `/filiais/{id}` | Atualizar filial |
| DELETE | `/filiais/{id}` | Remover filial |

### 🧑‍💼 Funcionários (`/funcionarios`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/funcionarios` | Lista paginada de funcionários |
| GET | `/funcionarios/{id}` | Busca funcionário por ID |
| GET | `/funcionarios/email/{email}` | Busca por email |
| GET | `/funcionarios/filial/{idFilial}` | Funcionários por filial |
| POST | `/funcionarios` | Criar funcionário |
| PUT | `/funcionarios/{id}` | Atualizar funcionário |
| DELETE | `/funcionarios/{id}` | Remover funcionário |

### 🏍️ Motos (`/motos`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/motos` | Lista paginada com filtros avançados |
| GET | `/motos/{id}` | Busca moto por ID |
| GET | `/motos/placa/{placa}` | Busca por placa |
| GET | `/motos/filial/{idFilial}` | Motos por filial |
| GET | `/motos/marca/{marca}` | Motos por marca |
| GET | `/motos/ano/{ano}` | Motos por ano |
| POST | `/motos` | Criar moto |
| PUT | `/motos/{id}` | Atualizar moto |
| DELETE | `/motos/{id}` | Remover moto |

## 🔧 Parâmetros de Consulta

### Paginação (Todos os endpoints GET de lista)
```
?pageNumber=1&pageSize=10&searchTerm=termo&sortBy=campo&sortDirection=asc
```

### Filtros Específicos (Motos)
```
?marca=Honda&ano=2023&quilometragemMin=1000&quilometragemMax=50000&idFilial=1
```

## 💻 Exemplos de Uso

### Criar uma Filial
```bash
POST /api/v1/filiais
Content-Type: application/json

{
  "nome": "Filial São Paulo - Centro",
  "endereco": "Rua Augusta, 1000",
  "cidade": "São Paulo",
  "estado": "SP",
  "cep": "01310-100"
}
```

### Criar um Funcionário
```bash
POST /api/v1/funcionarios
Content-Type: application/json

{
  "nome": "João Silva Santos",
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

## ⚙️ Configuração e Instalação

### Pré-requisitos
- .NET 8.0 SDK
- Oracle Database (11g ou superior)
- Visual Studio 2022 ou VS Code

### Configuração do Banco de Dados

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

### Executar a Aplicação

```bash
# Restaurar pacotes
dotnet restore

# Compilar
dotnet build

# Executar
dotnet run
```

A API estará disponível em:
- **HTTPS**: https://localhost:7001
- **HTTP**: http://localhost:5001
- **Swagger**: https://localhost:7001 (raiz)

## 📄 Documentação

### Swagger/OpenAPI
A documentação interativa está disponível na raiz da aplicação quando executada em modo de desenvolvimento.

**Recursos do Swagger**:
- 📚 Documentação completa de todos os endpoints
- 🧪 Interface "Try It Out" para testes
- 🗂️ Schemas detalhados dos DTOs
- 📝 Exemplos de payloads
- 🏷️ Organização por tags com emojis

## 🛠️ Validações e Regras de Negócio

### Validações Automáticas
- **Email único** por funcionário
- **Placa única** por moto
- **Formato de email** válido
- **Formato de placa** (ABC-1234 ou ABC1D23 Mercosul)
- **Estado** sempre em maiúsculo (2 caracteres)
- **CEP** no formato 00000-000
- **Ano** entre 1900-2030
- **Quilometragem** >= 0

### Relacionamentos
- **Funcionários** devem pertencer a uma filial existente
- **Motos** devem estar alocadas a uma filial existente
- **Filiais** não podem ser removidas se possuem funcionários ou motos

## 🔗 HATEOAS

Todas as respostas incluem links de navegação:

```json
{
  "idFuncionario": 1,
  "nome": "João Silva",
  "links": {
    "self": "/api/v1/funcionarios/1",
    "update": "/api/v1/funcionarios/1",
    "delete": "/api/v1/funcionarios/1",
    "filial": "/api/v1/filiais/1",
    "all": "/api/v1/funcionarios"
  }
}
```

## 📈 Status Codes

| Código | Significado | Uso |
|--------|-------------|-----|
| 200 | OK | Busca/Atualização bem-sucedida |
| 201 | Created | Recurso criado com sucesso |
| 204 | No Content | Remoção bem-sucedida |
| 400 | Bad Request | Dados inválidos |
| 404 | Not Found | Recurso não encontrado |
| 409 | Conflict | Conflito (email/placa duplicados) |

## 📂 Estrutura do Projeto

```
MottoMap/
    Controllers/           # Controladores da API
    Models/                # Entidades do banco de dados
    DTOs/                  # Objetos de transferência de dados
    Mappers/               # Conversores Entity ↔ DTO
    Data/
        AppData/           # Contexto do banco
        Repository/        # Repositórios de acesso a dados
    Migrations/            # Migrações do Entity Framework
    wwwroot/               # Arquivos estáticos (CSS do Swagger)
```

## 🧪 Testando a API

### Usar Swagger UI
1. Execute a aplicação
2. Acesse https://localhost:7001
3. Use a interface "Try It Out"

### Usar cURL
```bash
# Listar filiais
curl -X GET "https://localhost:7001/api/v1/filiais" -H "accept: application/json"

# Criar funcionário
curl -X POST "https://localhost:7001/api/v1/funcionarios"   -H "Content-Type: application/json"   -d '{
    "nome": "Maria Santos",
    "email": "maria@mottomap.com",
    "idFilial": 1,
    "funcao": "Analista"
  }'
```

## 📜 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

---

<div align="center">

**🚀 MottoMap API - Gestão Inteligente de Frotas de Motos**

*Desenvolvido com 💻 usando .NET 8.0 & Oracle Database*

</div>
