import IMessage from '@domain/messenger/IMessage';

export default interface IMessageProducer {
  produce(message : IMessage) : void;
}