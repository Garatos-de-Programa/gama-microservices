import IDatabaseMessageListener from "@domain/messenger/IDatabaseMessageListener";
import IMessageProducer from "@domain/messenger/IMessageProducer";
import IncidentMessage from "@domain/messenger/IncidentMessage";
import IDatabaseConnection from "@infrastructure/persistence/IDatabaseConnection";
import { Client, Notification } from 'pg';

export default class IncidentMessagePostgreesListener 
    implements IDatabaseMessageListener {

      private readonly _messageProducer : IMessageProducer;
      private readonly _databaseConnection : IDatabaseConnection

      constructor(messageProducer : IMessageProducer, databaseConnection : IDatabaseConnection) {
        this._messageProducer = messageProducer;
        this._databaseConnection = databaseConnection;
      }

      listen(): void {
        const connection = this._databaseConnection.getPostgressConnection();

        connection.connect();
        
        connection.query('LISTEN occurrence_notification');

        connection.on('notification', (notification : Notification) => {
          console.log('Received notification' + notification.payload);
          const message = new IncidentMessage(notification.payload!);
          this._messageProducer.produce(message);
        });
      }
}