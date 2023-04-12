import IncidentMessagePostgreesListener from '@infrastructure/databaseMessageListener/IncidentMessagePostgreesListener';
import SocketMessageProducer from '@infrastructure/messageProducer/SocketMessageProducer';
import NetSocketConnection from '@infrastructure/Socket/NetSocketConnection';

const listener = new IncidentMessagePostgreesListener(new SocketMessageProducer(new NetSocketConnection()));

listener.listen();