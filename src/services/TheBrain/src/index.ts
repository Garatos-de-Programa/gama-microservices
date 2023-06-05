import IncidentMessagePostgreesListener from '@infrastructure/databaseMessageListener/IncidentMessagePostgreesListener';
import SocketMessageProducer from '@infrastructure/messageProducer/SocketMessageProducer';
import NetSocketConnection from '@infrastructure/Socket/NetSocketConnection';
import NetClientSocketConnection from '@infrastructure/Socket/NetClientSocketConnection';
import OccurrenceMessageRabbitMQListener from '@infrastructure/databaseMessageListener/OccurrenceMessageRabbitMQListener';
import SubscriberNetSocketConnection from '@infrastructure/Socket/SubscriberNetSocketConnection';
import PostgresOccurrenceRepository from '@infrastructure/persistence/PostgresOccurrenceRepository';
import PostgresConnection from '@infrastructure/persistence/PostgresConnection';
import GeoLocationCalculator from '@domain/geolocation/GeoLocationCalculator';

const postgresConnection = new PostgresConnection();
const geolocationCalculator = new GeoLocationCalculator();
const occurrenceRepository = new PostgresOccurrenceRepository(postgresConnection, geolocationCalculator);
const listener = new OccurrenceMessageRabbitMQListener(new SocketMessageProducer(new SubscriberNetSocketConnection(occurrenceRepository, geolocationCalculator)));
listener.listen();


// var connection = new NetClientSocketConnection();
// connection.subscribe();
