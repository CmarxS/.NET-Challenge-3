# ?? Guia de Testes - MottoMap API

Este guia fornece exemplos pr�ticos para testar todos os endpoints da API MottoMap.

## ?? Iniciando os Testes

### 1. Executar a Aplica��o
```bash
dotnet run --urls="https://localhost:7001;http://localhost:5001"
```

### 2. Acessar o Swagger
- URL: https://localhost:7001
- Interface interativa com todos os endpoints

## ?? Sequ�ncia de Testes Recomendada

### Passo 1: Criar Filiais (Base)

```bash
# Criar Filial S�o Paulo
curl -X POST "https://localhost:7001/api/v1/filiais" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Filial S�o Paulo - Centro",
    "endereco": "Rua Augusta, 1000",
    "cidade": "S�o Paulo",
    "estado": "SP",
    "cep": "01310-100"
  }'

# Criar Filial Rio de Janeiro
curl -X POST "https://localhost:7001/api/v1/filiais" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Filial Rio de Janeiro - Copacabana",
    "endereco": "Av. Atl�ntica, 2000",
    "cidade": "Rio de Janeiro",
    "estado": "RJ",
    "cep": "22021-001"
  }'

# Criar Filial Belo Horizonte
curl -X POST "https://localhost:7001/api/v1/filiais" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Filial Belo Horizonte - Savassi",
    "endereco": "Rua Pernambuco, 500",
    "cidade": "Belo Horizonte",
    "estado": "MG",
    "cep": "30130-151"
  }'
```

### Passo 2: Criar Funcion�rios

```bash
# Funcion�rio SP - Gerente
curl -X POST "https://localhost:7001/api/v1/funcionarios" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Jo�o Silva Santos",
    "email": "joao.silva@mottomap.com",
    "idFilial": 1,
    "funcao": "Gerente Operacional"
  }'

# Funcion�rio SP - Analista
curl -X POST "https://localhost:7001/api/v1/funcionarios" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Maria Santos Oliveira",
    "email": "maria.santos@mottomap.com",
    "idFilial": 1,
    "funcao": "Analista de Frota"
  }'

# Funcion�rio RJ - Coordenador
curl -X POST "https://localhost:7001/api/v1/funcionarios" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Pedro Costa Lima",
    "email": "pedro.costa@mottomap.com",
    "idFilial": 2,
    "funcao": "Coordenador Regional"
  }'

# Funcion�rio BH - Supervisor
curl -X POST "https://localhost:7001/api/v1/funcionarios" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Ana Oliveira Ferreira",
    "email": "ana.oliveira@mottomap.com",
    "idFilial": 3,
    "funcao": "Supervisora de Opera��es"
  }'
```

### Passo 3: Criar Motos

```bash
# Moto Honda SP
curl -X POST "https://localhost:7001/api/v1/motos" \
  -H "Content-Type: application/json" \
  -d '{
    "marca": "Honda",
    "modelo": "CG 160 Titan",
    "ano": 2023,
    "placa": "ABC-1234",
    "idFilial": 1,
    "cor": "Vermelha",
    "quilometragem": 5000
  }'

# Moto Yamaha SP
curl -X POST "https://localhost:7001/api/v1/motos" \
  -H "Content-Type: application/json" \
  -d '{
    "marca": "Yamaha",
    "modelo": "Factor 125",
    "ano": 2022,
    "placa": "XYZ-5678",
    "idFilial": 1,
    "cor": "Azul",
    "quilometragem": 8500
  }'

# Moto Honda RJ (Mercosul)
curl -X POST "https://localhost:7001/api/v1/motos" \
  -H "Content-Type: application/json" \
  -d '{
    "marca": "Honda",
    "modelo": "CB 600F Hornet",
    "ano": 2023,
    "placa": "BRA2E19",
    "idFilial": 2,
    "cor": "Preta",
    "quilometragem": 2000
  }'

# Moto Kawasaki RJ (Mercosul)
curl -X POST "https://localhost:7001/api/v1/motos" \
  -H "Content-Type: application/json" \
  -d '{
    "marca": "Kawasaki",
    "modelo": "Ninja 400",
    "ano": 2024,
    "placa": "BRA3F45",
    "idFilial": 2,
    "cor": "Verde",
    "quilometragem": 1200
  }'

# Moto BMW BH
curl -X POST "https://localhost:7001/api/v1/motos" \
  -H "Content-Type: application/json" \
  -d '{
    "marca": "BMW",
    "modelo": "F 850 GS",
    "ano": 2024,
    "placa": "MNO-2468",
    "idFilial": 3,
    "cor": "Cinza",
    "quilometragem": 800
  }'
```

## ?? Testes de Consulta

### Listar com Pagina��o
```bash
# Todas as filiais (p�gina 1, 10 itens)
curl -X GET "https://localhost:7001/api/v1/filiais?pageNumber=1&pageSize=10"

# Funcion�rios ordenados por nome
curl -X GET "https://localhost:7001/api/v1/funcionarios?sortBy=nome&sortDirection=asc"

# Motos com busca
curl -X GET "https://localhost:7001/api/v1/motos?searchTerm=honda&pageSize=5"
```

### Buscar por ID
```bash
# Filial espec�fica
curl -X GET "https://localhost:7001/api/v1/filiais/1"

# Funcion�rio espec�fico
curl -X GET "https://localhost:7001/api/v1/funcionarios/1"

# Moto espec�fica
curl -X GET "https://localhost:7001/api/v1/motos/1"
```

### Filiais - Endpoints Especiais
```bash
# Filial com detalhes completos
curl -X GET "https://localhost:7001/api/v1/filiais/1/detalhes"

# Estat�sticas da filial
curl -X GET "https://localhost:7001/api/v1/filiais/1/estatisticas"

# Filiais por cidade
curl -X GET "https://localhost:7001/api/v1/filiais/cidade/S�o Paulo"

# Filiais por estado
curl -X GET "https://localhost:7001/api/v1/filiais/estado/SP"
```

### Funcion�rios - Endpoints Especiais
```bash
# Funcion�rio por email
curl -X GET "https://localhost:7001/api/v1/funcionarios/email/joao.silva@mottomap.com"

# Funcion�rios de uma filial
curl -X GET "https://localhost:7001/api/v1/funcionarios/filial/1"
```

### Motos - Endpoints Especiais e Filtros
```bash
# Moto por placa
curl -X GET "https://localhost:7001/api/v1/motos/placa/ABC-1234"

# Motos por marca
curl -X GET "https://localhost:7001/api/v1/motos/marca/Honda"

# Motos por ano
curl -X GET "https://localhost:7001/api/v1/motos/ano/2023"

# Motos de uma filial
curl -X GET "https://localhost:7001/api/v1/motos/filial/1"

# Filtros avan�ados combinados
curl -X GET "https://localhost:7001/api/v1/motos?marca=Honda&ano=2023&quilometragemMin=1000&quilometragemMax=10000"

# Filtro por faixa de quilometragem
curl -X GET "https://localhost:7001/api/v1/motos?quilometragemMin=5000&quilometragemMax=15000"
```

## ?? Testes de Atualiza��o

### Atualizar Filial
```bash
curl -X PUT "https://localhost:7001/api/v1/filiais/1" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Filial S�o Paulo - Centro Expandida",
    "endereco": "Rua Augusta, 1000 - Sala 101",
    "cidade": "S�o Paulo",
    "estado": "SP",
    "cep": "01310-100"
  }'
```

### Atualizar Funcion�rio
```bash
curl -X PUT "https://localhost:7001/api/v1/funcionarios/1" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Jo�o Silva Santos Jr.",
    "email": "joao.silva@mottomap.com",
    "idFilial": 1,
    "funcao": "Gerente Operacional S�nior"
  }'
```

### Atualizar Moto
```bash
curl -X PUT "https://localhost:7001/api/v1/motos/1" \
  -H "Content-Type: application/json" \
  -d '{
    "marca": "Honda",
    "modelo": "CG 160 Titan",
    "ano": 2023,
    "placa": "ABC-1234",
    "idFilial": 1,
    "cor": "Vermelha",
    "quilometragem": 6500
  }'
```

## ? Testes de Valida��o (Devem Falhar)

### Email Duplicado
```bash
# Deve retornar 409 Conflict
curl -X POST "https://localhost:7001/api/v1/funcionarios" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Outro Funcion�rio",
    "email": "joao.silva@mottomap.com",
    "idFilial": 1,
    "funcao": "Analista"
  }'
```

### Placa Duplicada
```bash
# Deve retornar 409 Conflict
curl -X POST "https://localhost:7001/api/v1/motos" \
  -H "Content-Type: application/json" \
  -d '{
    "marca": "Suzuki",
    "modelo": "GSX-R 750",
    "ano": 2023,
    "placa": "ABC-1234",
    "idFilial": 1,
    "cor": "Branca"
  }'
```

### Filial Inexistente
```bash
# Deve retornar 400 Bad Request
curl -X POST "https://localhost:7001/api/v1/funcionarios" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Funcion�rio Teste",
    "email": "teste@mottomap.com",
    "idFilial": 999,
    "funcao": "Teste"
  }'
```

### Dados Inv�lidos
```bash
# Email inv�lido - deve retornar 400 Bad Request
curl -X POST "https://localhost:7001/api/v1/funcionarios" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Teste",
    "email": "email-invalido",
    "idFilial": 1,
    "funcao": "Teste"
  }'

# Estado inv�lido - deve retornar 400 Bad Request
curl -X POST "https://localhost:7001/api/v1/filiais" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Filial Teste",
    "endereco": "Rua Teste, 123",
    "cidade": "Cidade Teste",
    "estado": "SAO",
    "cep": "12345-678"
  }'

# Ano inv�lido - deve retornar 400 Bad Request
curl -X POST "https://localhost:7001/api/v1/motos" \
  -H "Content-Type: application/json" \
  -d '{
    "marca": "Honda",
    "modelo": "CG 160",
    "ano": 1850,
    "placa": "TST-1234",
    "idFilial": 1
  }'
```

## ??? Testes de Remo��o

### Remover Moto
```bash
# Deve retornar 204 No Content
curl -X DELETE "https://localhost:7001/api/v1/motos/1"
```

### Remover Funcion�rio
```bash
# Deve retornar 204 No Content
curl -X DELETE "https://localhost:7001/api/v1/funcionarios/1"
```

### Tentar Remover Filial com Depend�ncias
```bash
# Deve retornar 409 Conflict (se ainda houver funcion�rios/motos)
curl -X DELETE "https://localhost:7001/api/v1/filiais/1"
```

### Buscar Recurso Removido
```bash
# Deve retornar 404 Not Found
curl -X GET "https://localhost:7001/api/v1/funcionarios/1"
```

## ?? Verificar HATEOAS

Todas as respostas devem incluir links de navega��o. Exemplo de resposta:

```json
{
  "idFuncionario": 1,
  "nome": "Jo�o Silva Santos",
  "email": "joao.silva@mottomap.com",
  "idFilial": 1,
  "funcao": "Gerente Operacional",
  "filial": {
    "idFilial": 1,
    "nome": "Filial S�o Paulo - Centro",
    "cidade": "S�o Paulo",
    "estado": "SP"
  },
  "links": {
    "self": "https://localhost:7001/api/v1/funcionarios/1",
    "update": "https://localhost:7001/api/v1/funcionarios/1",
    "delete": "https://localhost:7001/api/v1/funcionarios/1",
    "filial": "https://localhost:7001/api/v1/filiais/1",
    "all": "https://localhost:7001/api/v1/funcionarios"
  }
}
```

## ?? Verificar Pagina��o

Resposta de lista paginada deve incluir:

```json
{
  "data": [...],
  "pagination": {
    "pageNumber": 1,
    "pageSize": 10,
    "totalItems": 25,
    "totalPages": 3,
    "hasPreviousPage": false,
    "hasNextPage": true
  },
  "links": {
    "self": "https://localhost:7001/api/v1/funcionarios?pageNumber=1&pageSize=10",
    "first": "https://localhost:7001/api/v1/funcionarios?pageNumber=1&pageSize=10",
    "last": "https://localhost:7001/api/v1/funcionarios?pageNumber=3&pageSize=10",
    "next": "https://localhost:7001/api/v1/funcionarios?pageNumber=2&pageSize=10"
  }
}
```

## ?? Checklist de Testes

### ? Funcionalidades B�sicas
- [ ] Criar filiais
- [ ] Criar funcion�rios
- [ ] Criar motos
- [ ] Buscar por ID
- [ ] Listar com pagina��o
- [ ] Atualizar registros
- [ ] Remover registros

### ? Valida��es
- [ ] Email �nico
- [ ] Placa �nica
- [ ] Formato de email v�lido
- [ ] Estado com 2 caracteres
- [ ] Ano dentro do range
- [ ] Filial existente

### ? Filtros e Buscas
- [ ] Busca textual (searchTerm)
- [ ] Ordena��o (sortBy/sortDirection)
- [ ] Filtros por marca/ano
- [ ] Filtros por quilometragem
- [ ] Busca por cidade/estado

### ? HATEOAS
- [ ] Links em respostas individuais
- [ ] Links de navega��o em listas
- [ ] Links contextuais por tipo

### ? Pagina��o
- [ ] Metadados corretos
- [ ] Links de navega��o
- [ ] Preserva��o de filtros nos links

### ? Status Codes
- [ ] 200 OK para buscas
- [ ] 201 Created para cria��o
- [ ] 204 No Content para remo��o
- [ ] 400 Bad Request para dados inv�lidos
- [ ] 404 Not Found para recursos inexistentes
- [ ] 409 Conflict para duplicatas

---

**?? Dica**: Use o Swagger UI (https://localhost:7001) para uma experi�ncia de teste mais visual e interativa!