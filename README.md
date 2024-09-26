# Contracheque API - Documentação

A API permite visualizar o extrato de pagamento mensal de um funcionário, incluindo salários, descontos e lançamentos financeiros.

---

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
  - **Id**: Identificador único do funcionário.

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
