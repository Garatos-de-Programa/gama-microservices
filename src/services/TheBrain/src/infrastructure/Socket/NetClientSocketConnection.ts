import net from 'net';


export default class NetClientSocketConnection {

  private serverAddress = 'localhost';
  private serverPort = 8080;
  private socket;

  constructor() {
    this.socket = new net.Socket();

    this.socket.connect(this.serverPort, this.serverAddress, () => {
      console.log('Connected to server');
    });

    this.socket.on('error', (error) => {
      console.error('Socket error:', error);
    });

    this.socket.on('data', (data) => {
      console.log('Received data:', data.toString());
    });

    this.socket.on('close', () => {
      console.log('Connection closed');
    });
  }

}

