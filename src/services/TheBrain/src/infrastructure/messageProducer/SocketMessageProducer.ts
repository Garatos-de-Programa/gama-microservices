import IMessageProducer from "@domain/messenger/IMessageProducer";
import IMessage from "@domain/messenger/IMessage";
import ISocketConnection from "@infrastructure/Socket/ISocketConnection";

export default class SocketMessageProducer 
  implements IMessageProducer {

    private readonly _socketConnection : ISocketConnection;

    constructor(socketConnection : ISocketConnection) {
      this._socketConnection = socketConnection;
    }

    produce(message: IMessage): void {
      this._socketConnection.write(message.getBytes());
    }
}