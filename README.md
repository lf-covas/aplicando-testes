# Ponderada Testes de Integração

Este tutorial mostra como implementar e executar testes unitários em uma aplicação .NET utilizando xUnit, NUnit ou MSTest. Utilizamos o Codespaces do GitHub para simplificar o ambiente de desenvolvimento.

Abra o Codespaces no repositório do GitHub para configurar seu ambiente automaticamente.

![alt text](img/codespace.png)

Figura 1: Iniciando Codespace no repositório do GitHub.

Após abrir o Codespace, você verá o ambiente de desenvolvimento integrado (IDE) diretamente no navegador.

![alt text](img/IDE_git.png)

Figura 2: Interface do IDE no GitHub Codespaces.

### Criando o Projeto Principal

No terminal integrado, crie o projeto principal com o comando:

```
dotnet new classlib -n Temperatura
```

Isso criará uma biblioteca de classes chamada Temperatura, que conterá a lógica da nossa aplicação.

![alt text](img/projeto_principal.png)

Figura 3: Criando o projeto principal.

### Implementando a Classe Principal

No arquivo `ConversorTemperatura.cs`, implemente a lógica da aplicação. Por exemplo, para converter temperaturas de Fahrenheit para Celsius:

```C#
using System;

namespace Temperatura
{
    public static class ConversorTemperatura
    {
        public static double FahrenheitParaCelsius(double temperatura)
            //=> (temperatura - 32) / 1.8; // Simulação de falha
            => Math.Round((temperatura - 32) / 1.8, 2);
    }
}
```

### Criando o Projeto de Testes

Volte ao diretório raiz e crie o projeto de testes utilizando o framework de sua escolha:

**Usando xUnit**

`dotnet new xunit -n TestesTemperaturaXUnit`

**Usando NUnit**

`dotnet new nunit -n TestesTemperaturaNUnit`

**Usando MSTest**

`dotnet new mstest -n TestesTemperaturaMSTest`

Esses comandos criam um novo projeto de testes chamado TestesTemperatura(framework preferido).

### Adicionando Referência ao Projeto Principal

Adicione uma referência ao projeto principal (Temperatura) no projeto de testes:

```
dotnet add reference ../Temperatura/Temperatura.csproj
```

Nota: Faça isso para todos os projetos de testes. Após executar este comando, os projetos de testes poderam acessar as classes e métodos do projeto principal.

### Implementando os Testes

No arquivo TesteConversorTemperatura.cs dentro do projeto de testes com xUnit, implemente o teste conforme código abaixo:

```C#
using System;
using Xunit;

namespace Temperatura.Testes
{
    public class TestesConversorTemperatura
    {
        [Theory]
        [InlineData(32, 0)]
        [InlineData(47, 8.33)]
        [InlineData(86, 30)]
        [InlineData(90.5, 32.5)]
        [InlineData(120.18, 48.99)]
        [InlineData(212, 100)]
        public void TestarConversaoTemperatura(
            double fahrenheit, double celsius)
        {
            double valorCalculado =
                ConversorTemperatura.FahrenheitParaCelsius(fahrenheit);
            Assert.Equal(celsius, valorCalculado);
        }
    }
}
```
No arquivo TesteConversorTemperatura.cs dentro do projeto de testes com NUnit, implemente o teste conforme código abaixo:

```C#
using NUnit.Framework;

namespace Temperatura.Testes
{
    public class TestesConversorTemperatura
    {
        [TestCase(32, 0)]
        [TestCase(47, 8.33)]
        [TestCase(86, 30)]
        [TestCase(90.5, 32.5)]
        [TestCase(120.18, 48.99)]
        [TestCase(212, 100)]
        public void TesteConversaoTemperatura(
            double tempFahrenheit, double tempCelsius)
        {
            double valorCalculado =
                ConversorTemperatura.FahrenheitParaCelsius(tempFahrenheit);
            Assert.AreEqual(tempCelsius, valorCalculado);
        }
    }
}
```
No arquivo TesteConversorTemperatura.cs dentro do projeto de testes com MSTest, implemente o teste conforme código abaixo:

```C#
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Temperatura.Testes
{
    [TestClass]
    public class TestesConversorTemperatura
    {
        [DataRow(32, 0)]
        [DataRow(47, 8.33)]
        [DataRow(86, 30)]
        [DataRow(90.5, 32.5)]
        [DataRow(120.18, 48.99)]
        [DataRow(212, 100)]
        [DataTestMethod]
        public void TesteConversaoTemperatura(
            double tempFahrenheit, double tempCelsius)
        {
            double valorCalculado =
                ConversorTemperatura.FahrenheitParaCelsius(tempFahrenheit);
            Assert.AreEqual(tempCelsius, valorCalculado);
        }
    }
}
```

### Executando os Testes

Compile e execute os testes para garantir que tudo está funcionando corretamente:

Navegue até a pasta do projeto de cada teste e execute o comando:

```
dotnet test
```

O terminal exibirá os resultados para cada teste conforme imagens abaixo:

Realizando testes com MSTest:

![alt text](img/mstest.png)

Figura 4: Terminal com resultados do MSTest

Realizando testes com NUnit:

![alt text](img/NUnit.png)

Figura 5: Terminal com resultado do NUnit

Realizando testes com xUnit:

![alt text](img/xUnit.png)

Figura 6: Terminal com resultado do Xunit

# Testes Implementados

Este repositório contém testes implementados para garantir a qualidade e a funcionalidade do projeto. Abaixo estão descrições detalhadas para cada tipo de teste implementado, incluindo exemplos de cenários práticos conforme soliticato.

## 1. Testes Unitários

Os testes unitários garantem que os menores blocos de código da aplicação funcionem corretamente de forma isolada. Eles verificam a precisão das funções ou métodos individuais.

**Objetivo:** Os testes unitários foram aplicados para verificar se a lógica de conversão de temperaturas de Fahrenheit para Celsius está correta.

No arquivo TestesConversorTemperatura.cs em qualquer tipo de framework de testes, os testes utilizam diferentes valores de entrada para validar a precisão do método FahrenheitParaCelsius.

![alt text](img/dados_teste.png)

Podemos verificar se o ponto de congelamento e ebulição da água está correta para ambas as medidas.

Congelamento da água:

- Entrada: 32°F
- Saída esperada: 0°C
- Resultado do teste: Passou.

Ponto de ebulição da água:

- Entrada: 212°F
- Saída esperada: 100°C
- Resultado do teste: Passou.