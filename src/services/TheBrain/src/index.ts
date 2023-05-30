import IncidentMessagePostgreesListener from '@infrastructure/databaseMessageListener/IncidentMessagePostgreesListener';
import SocketMessageProducer from '@infrastructure/messageProducer/SocketMessageProducer';
import NetSocketConnection from '@infrastructure/Socket/NetSocketConnection';
import NetClientSocketConnection from '@infrastructure/Socket/NetClientSocketConnection';
import OccurrenceMessageRabbitMQListener from '@infrastructure/databaseMessageListener/OccurrenceMessageRabbitMQListener';

const listener = new OccurrenceMessageRabbitMQListener(new SocketMessageProducer(new NetSocketConnection()));
listener.listen();


// var connection = new NetClientSocketConnection();

