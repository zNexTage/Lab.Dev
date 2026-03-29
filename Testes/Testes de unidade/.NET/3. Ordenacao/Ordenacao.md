# Ordenação

É possível realizar a ordenação dos testes, especificando um ordem que eles irão rodar. Apesar da existência dessa prática, não é recomendado aplicá-la. Um teste não deve depender de outro para funcionar.

## Implementação

- Necessário criar um annotation `TestPriority`;
```c#
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Lab.Dev.Features.Tests.Order
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TestPriorityAttribute : Attribute
    {
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; }
    }

    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

            foreach (var testCase in testCases)
            {
                var priority = 0;

                foreach (var attr in testCase.TestMethod.Method.GetCustomAttributes((typeof(TestPriorityAttribute).AssemblyQualifiedName)))
                    priority = attr.GetNamedArgument<int>("Priority");

                GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
            {
                list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
                foreach (var testCase in list)
                    yield return testCase;
            }
        }

        private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            if (dictionary.TryGetValue(key, out var result)) return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }
}
```
- Após a criação, podemos utilizar o annotation da seguinte forma:

```c#
namespace Lab.Dev.Features.Tests.Order
{
    [TestCaseOrderer("Lab.Dev.Features.Tests.Order.PriorityOrderer", "Lab.Dev.Features.Tests")]
    public class OrderTests
    {
        public static bool Teste1Chamado;
        public static bool Teste2Chamado;
        public static bool Teste3Chamado;
        public static bool Teste4Chamado;

        [Fact(DisplayName = "Teste 04"), TestPriority(3)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste04()
        {
            Teste4Chamado = true;

            Assert.True(Teste3Chamado);
            Assert.True(Teste1Chamado);
            Assert.False(Teste2Chamado);
        }

        [Fact(DisplayName = "Teste 01"), TestPriority(2)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste01()
        {
            Teste1Chamado = true;

            Assert.True(Teste3Chamado);
            Assert.False(Teste4Chamado);
            Assert.False(Teste2Chamado);
        }

        [Fact(DisplayName = "Teste 03"), TestPriority(1)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste03()
        {
            Teste3Chamado = true;

            Assert.False(Teste1Chamado);
            Assert.False(Teste2Chamado);
            Assert.False(Teste4Chamado);
        }

        [Fact(DisplayName = "Teste 02"), TestPriority(4)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste02()
        {
            Teste2Chamado = true;

            Assert.True(Teste3Chamado);
            Assert.True(Teste4Chamado);
            Assert.True(Teste1Chamado);
        }
    }
}

```
