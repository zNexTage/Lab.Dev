# RabbitMQ

- Trabalha com Publish / Subscribe
    - Publish / Subscribe = Design Pattern

- Um app publica algo para o RabbitMQ;
    - Todo mundo que fica ouvindo (inscrito) a fila do RabbitMQ é notificado (consumer);
    - O consumer pode estar dentro da própria API que escreve no RabbitMQ;
- Para .NET, podemos usar o `MassTransit.RabbitMQ`.
- `O protocolo usado é amqp`;

## Exchange

- O RabbitMQ possui exchanges que direcionam as mensagens para as filas;
- Ela não armazena as mensagens, quem normalmente faz isso são as filas;

A exchange decide o destino da mensagem com base em regras de roteamento. Essas regras dependem do tipo da exchange e, em muitos casos, da routing key.

### Direct exchange

Encaminha a mensagem para a fila cuja binding key seja exatamente igual à routing key da mensagem.
**Use quando você quer um roteamento mais direto e específico.**

### Fanout exchange

Envia a mensagem para todas as filas ligadas a ela, sem considerar routing key.
**Use quando quiser distribuir a mesma mensagem para vários consumidores.**

### Topic exchange

**Encaminha com base em padrões na routing key.**
Permite algo mais flexível, como pedido.* ou pedido.#.

### Headers exchange

**Faz o roteamento com base nos headers da mensagem, e não na routing key.**
É menos comum, mas pode ser útil em cenários específicos.