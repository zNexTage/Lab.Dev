# Árvore binária

Estruturas de dados hierárquicas usadas para armazenar informações de forma organizada e eficiente.

É composta por nós, onde cada nó pode ou não ter até dois filhos, esquerda e direita.

O nó principal é chamado de Raiz.

## Termos importantes

- Nó: Elemento onde armazena os dados;
- Raiz: o primeiro nó da árvore;
- Filho: nó conectado abaixo de outro nó;
- Pai: nó que tem um ou mais filho;
- Folha: nó que não possui filhos.

## Onde é usado?

- Algoritmo de busca de dados;
- banco de dados, índices, busca de dados;
- compiladores, ordem de cálculos;
- Sistemas de arquivos do SO;
- Usado em HTML
    - Html é a tag raiz;

## Altura

Comprimento do caminho entre a raiz e a folha mais profunda.

# Percurso

![alt text](01Percurso.png)

## Pré-Ordem
Na pré-ordem, o nó é processado antes dos seus filhos.

Você visita o nó atual primeiro, depois percorre:

- subárvore esquerda
- subárvore direita

Ordem:

- Nó;
- Esquerda;
- Direita.

**"Cheguei no nó? Já imprimo ele. Depois vou para a esquerda e depois para a direita."**

## Em-Ordem
Primeiro vejo tudo da esquerda, depois o nó atual, depois tudo da direita.

Na em-ordem, o nó é processado entre a subárvore esquerda e a direita.

Você percorre:

- subárvore esquerda;
- visita o nó atual;
- subárvore direita.

## Pós-Ordem
Só imprimo o nó depois de terminar tudo que está abaixo dele.

Na pós-ordem, o nó é processado depois dos dois filhos.

Você percorre:

- subárvore esquerda;
- subárvore direita;
- visita o nó atual.

Ordem:

- Esquerda;
- Direita;
- Nó.