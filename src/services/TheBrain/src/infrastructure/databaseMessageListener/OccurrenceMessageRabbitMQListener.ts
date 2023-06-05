import IDatabaseMessageListener from "@domain/messenger/IDatabaseMessageListener";
import IMessageProducer from "@domain/messenger/IMessageProducer";
import OccurrenceEventMessage from "@domain/occurrences/OccurrenceEventMessage";
import { Channel, connect, Connection, Message } from 'amqplib';

export default class OccurrenceMessageRabbitMQListener 
    implements IDatabaseMessageListener {

    private readonly _messageProducer : IMessageProducer;

    constructor(messageProducer : IMessageProducer) {
        this._messageProducer = messageProducer;
    }

    listen(): void {
        const connection : Promise<Connection> = connect('amqp://guest:guest@localhost:5672');
        const channel : Promise<Channel> = connection.then((connection) => connection.createChannel());
        
        const queueName = 'the-brain:occurrence-notifier';
        const exchangeName = 'gama.api:events-exchange';
        channel.then((channel) => {
            channel.assertQueue(queueName, { durable:true })
            channel.assertExchange(exchangeName, 'topic', { durable: true });
            channel.bindQueue(queueName, exchangeName, 'gama.api:event-occurrences');
            console.log('RabbitMQ listener started. Waiting for messages...');
            try{
                channel.consume(queueName, (message: Message | null) => {
                    if (message) {
                        const content = message.content.toString();
                        const occurrenceMessage : OccurrenceEventMessage = Object.assign(new OccurrenceEventMessage(), JSON.parse(content));
                        console.log(`Received message: ${occurrenceMessage.OccurrenceId}`);
                        const incidentMessage = occurrenceMessage.toIncidentMessage();
                        this._messageProducer.produce(incidentMessage);
                        channel.ack(message);
                    }
                  });
            } catch (error) {
              console.error('Error occurred in RabbitMQ listener:', error);
            }
        });

        
    }
}