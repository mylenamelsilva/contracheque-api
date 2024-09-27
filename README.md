# Contracheque API - Documentação

A API permite visualizar o extrato de pagamento mensal de um funcionário, incluindo salários, descontos e lançamentos financeiros.

Você pode ver o funcionamento da API <a href="https://contracheque-app.azurewebsites.net/swagger" target="_blank">clicando aqui.</a>

![Imagem da documentação da API no Swagger](https://github.com/user-attachments/assets/b61ce38c-7ade-439e-aa17-7f14821f7932)


---

## Tecnologias utilizadas

- **.NET 8** com uso do **ADO.NET** puro para interação direta com o banco de dados.
- **Secret Manager**: Durante o desenvolvimento local, usei o User Secrets para armazenar de forma segura as configurações sensíveis, como strings de conexão.
- **SQL Server**: Utilizei o SQL Server como banco de dados localmente.
- **Azure Web App**: Utilizei o Azure Web App para hospedar e gerenciar a API na nuvem da Azure.
- **Azure SQL Database**: Após o desenvolvimento, migrei o banco de dados para o Azure SQL Database, permitindo que a aplicação rodasse em um ambiente na nuvem.
- **Azure Key Vault**: No ambiente de produção, estou utilizando o Azure Key Vault para armazenar de forma segura as chaves, segredos e strings de conexão.

----

## Entidades

### Funcionário

A entidade **Funcionário** armazena as seguintes informações:

- **Nome**: Nome do funcionário.
- **Sobrenome**: Sobrenome do funcionário.
- **Documento (CPF)**: Número do CPF do funcionário.
- **Setor**: Setor onde o funcionário trabalha.
- **Salário bruto**: Salário bruto do funcionário.
- **Data de admissão**: Data de admissão do funcionário.
- **Plano de saúde**: Indica se o funcionário optou pelo plano de saúde.
- **Plano dental**: Indica se o funcionário optou pelo plano dental.
- **Vale transporte**: Indica se o funcionário optou pelo vale transporte.

---

## Estrutura de Retorno

### Estrutura do JSON do Extrato

- **mesReferencia**: Mês de referência do pagamento.
- **salarioBruto**: Valor total do salário antes dos descontos.
- **salarioLiquido**: Valor final do salário após todos os descontos.
- **totalDescontos**: Soma total dos descontos aplicados.
- **lancamentos**: Lista de lançamentos financeiros:
  - **tipo**: Tipo do lançamento (desconto ou remuneração).
  - **descricao**: Descrição do lançamento.
  - **valor**: Valor do lançamento.

#### Tipos de Descontos:

- **INSS**: Baseado na tabela de 2024.
- **Imposto**: Calculado com base na tabela de 2024.
- **Plano de saúde**: Desconto fixo de R$ 10,00.
- **Plano dental**: Desconto fixo de R$ 5,00.
- **Vale transporte**: Desconto de 6% sobre o salário bruto para salários iguais ou superiores a R$ 1.500,00.

#### Exemplo de Estrutura JSON:

```json
{
  "mesReferencia": "setembro",
  "salarioBruto": "R$ 1.500,00",
  "salarioLiquido": "R$ 1.386,18",
  "totalDescontos": "R$ 113,82",
  "lancamentos": [
    {
      "tipo": "Remuneracao",
      "descricao": "Salário mês",
      "valor": "R$ 1.500,00"
    },
    {
      "tipo": "Desconto",
      "descricao": "INSS mês",
      "valor": "R$ 113,82"
    }
  ]
}
```
----

## Result Pattern

A aplicação utiliza um padrão de resposta para todos os endpoints.

```json
{
  "erro": boolean,
  "mensagem": string,
  "data": object
}
```

- **erro**: Indica se houve erro na requisição.
- **mensagem**: Mensagem de retorno personalizada.
- **data**: Dados retornados, podendo ser de diferentes tipos (objetos, boolean, inteiro, etc.).

----

## Endpoints

### Extrato Mensal

- **Endpoint**: `{URL_AMBIENTE}/api/Extratos/ExtratoMensal/{id}`
- **Método**: GET
- **Parâmetros**:
  - **id**: Identificador único do funcionário.

#### Exemplo de Retorno:

```json
{
  "erro": false,
  "mensagem": "Extrato do mês atual",
  "data": {
    "mesReferencia": "setembro",
    "salarioBruto": "R$ 1.500,00",
    "salarioLiquido": "R$ 1.286,18",
    "totalDescontos": "R$ 213,82",
    "lancamentos": [
      {
        "tipo": "Remuneracao",
        "descricao": "Salário mês",
        "valor": "R$ 1.500,00"
      },
      {
        "tipo": "Desconto",
        "descricao": "INSS mês",
        "valor": "R$ 113,82"
      },
      {
        "tipo": "Desconto",
        "descricao": "Plano de Saúde mês",
        "valor": "R$ 10,00"
      },
      {
        "tipo": "Desconto",
        "descricao": "Vale Transporte mês",
        "valor": "R$ 90,00"
      }
    ]
  }
}
```

### Criar funcionário

- **Endpoint**: `{URL_AMBIENTE}/api/Funcionarios/CriarFuncionario`
- **Método**: POST
- **Parâmetros**:
  - **nome**: O primeiro nome do funcionário. (Tipo: `string`)
  - **sobrenome**: O sobrenome do funcionário. (Tipo: `string`)
  - **documento**: O número do documento de identificação do funcionário (exemplo: CPF ou RG). (Tipo: `string`)
  - **setor**: O setor de trabalho do funcionário (exemplo: TI, RH, Vendas, etc.). (Tipo: `string`)
  - **salarioBruto**: O salário bruto do funcionário. (Tipo: `decimal`)
  - **dataAdmissao**: A data em que o funcionário foi admitido na empresa. (Tipo: `string`, formato: `dd/MM/yyyy`)
  - **descontoPlanoSaude**: Indica se o funcionário terá desconto do plano de saúde. (Tipo: `boolean`)
  - **descontoPlanoDental**: Indica se o funcionário terá desconto do plano dental. (Tipo: `boolean`)
  - **descontoValeTransporte**: Indica se o funcionário terá desconto do vale-transporte. (Tipo: `boolean`)

#### Exemplo de request:

```json
{
  "nome": "Lucas",
  "sobrenome": "Paulo",
  "documento": "222222322222",
  "setor": "TI",
  "salarioBruto": 4590,
  "dataAdmissao": "11/03/2024",
  "descontoPlanoSaude": true,
  "descontoPlanoDental": false,
  "descontoValeTransporte": true
}
```

#### Exemplo de response:

```json
{
  "erro": false,
  "mensagem": "Funcionário criado.",
  "data": 6
}
```

O valor no data é o id gerado para poder usar no endpoint de extrato.

----

## Endpoints secundários

Há os endpoints secundários para gerenciamento de funcionário, caso queira atualizar, visualizar suas informações ou deletar.

----

## Como rodar localmente

### **1. Pré-requisitos**
  
Antes de começar, certifique-se de ter as seguintes ferramentas instaladas:

  - Visual Studio ou Visual Studio Code
  - .NET SDK
  - SQL Server
  - SQL Server Management Studio (SSMS) ou outra ferramenta para gerenciar o banco de dados SQL Server

### **2. Clone o repositório**

`https://github.com/mylenamelsilva/contracheque-api.git` 

### **3. Configure o banco de dados**

- Como a API não usa migrations, será necessário executar o script SQL manualmente, com base no que foi descrito anteriormente, para configurar o banco de dados.

### **4. Configure o secrets**

A API usa o [Secret Manager](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows) para armazenar as credenciais do banco de dados.

- Vá para o diretório do projeto
- Inicie a configuração do user-secrets: `dotnet user-secrets init`
  *   Você pode ver o id criado no arquivo `.csproj`.
  *   Ex: `<UserSecretsId>79a3edd0-2092-40a2-a04d-dcb46d5ca9ed</UserSecretsId>`
- Adicione a chave e o valor do secret do banco de dados.
  * Ex: `dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=xxx;Database=yyy;User Id=zzz;Password=bbb;"`
  * Nota: No Windows, use `:` como separador. No Linux, substitua `:` por `__`.      

### **5. Rode a aplicação**

- Restaure as dependências do projeto: `dotnet restore`
- Compile o projeto: `dotnet build`
- Execute a API: `dotnet run`
- Abra o navegador e acesse a documentação Swagger: `https://localhost:7281/swagger/` 
