# 1. Nomenclatura de testes de unidade

**Testes precisam ser expressivos.**

# ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado


Cada aspecto separado por underline

ObjetoEmTeste -> Classe;
MetodoComportamentoEmTeste -> Método da classe;
ComportamentoEsperado -> O que o método deve fazer.

Ex: 
- Pedido_AdicionarPedidoItem_DeveIncrementarUnidadesSeItemJaExisente
- Estoque_RetirarItem_DeveEnviarEmailSeAbaixoDe10Unidades

# MetodoEmTeste_EstadoEmTeste_ComportamentoEsperado

As vezes fica claro o objeto de teste, portanto podemos omiti-lo. 

MetodoEmTeste ->  Método da classe;
EstadoEmTeste -> Condição da ação testada (item existente no carrinho);
ComportamentoEsperado -> O que o método deve fazer.

Ex:
- AdicionarPedidoItem_ItemExistenteCarrinho_DeveIncrementarUnidadesDoItem




