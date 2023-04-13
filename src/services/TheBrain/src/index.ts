import IncidentMessagePostgreesListener from '@infrastructure/databaseMessageListener/IncidentMessagePostgreesListener';
import SocketMessageProducer from '@infrastructure/messageProducer/SocketMessageProducer';
import NetSocketConnection from '@infrastructure/Socket/NetSocketConnection';
import NetClientSocketConnection from '@infrastructure/Socket/NetClientSocketConnection';

const listener = new IncidentMessagePostgreesListener(new SocketMessageProducer(new NetSocketConnection()));
listener.listen();
const client = new NetClientSocketConnection();
