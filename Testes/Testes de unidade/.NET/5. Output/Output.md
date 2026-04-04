# Output

É possível configurar saída para os testes, permitindo escrever mensagens para saber valores de variáveis, etc.

## Configurando

Necessário utilizar o `ITestOutputHelper` do `Xunit.Abstractions`.

```c#
public MinhaClasseTeste(ITestOutputHelper outputHelper) {
    _outputHelper = outputHelper;
}
```

Depois é possível fazer:

```c#
_outputHelper.WriteLine($"Minha mensagem");
```