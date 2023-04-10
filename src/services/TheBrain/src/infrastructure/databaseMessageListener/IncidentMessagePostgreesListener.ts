import IDatabaseMessageListener from "@application/Contracts/Infrastructure/IDatabaseMessageListener";
import IMessageProducer from "@application/Contracts/Infrastructure/IMessageProducer";
import IncidentMessage from "@domain/messenger/IncidentMessage";
import { Client, Notification } from 'pg';

export default class IncidentMessagePostgreesListener 
    implements IDatabaseMessageListener {

      private readonly _messageProducer : IMessageProducer;

      constructor(messageProducer : IMessageProducer) {
        this._messageProducer = messageProducer;
      }

      listen(): void {
        const client = new Client({
          user: 'yourusername',
          password: 'yourpassword',
          database: 'yourdatabase',
          host: 'localhost',
          port: 5432,
        });
      
        client.connect();
        
        client.query('LISTEN your_event');

        client.on('notification', (notification : Notification) => {
          console.log('Received notification');
          const message = new IncidentMessage(notification.payload!);
          this._messageProducer.produce(message);
        });
      }
}