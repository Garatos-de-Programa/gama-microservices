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

  subscribe() : void {
    const encoder = new TextEncoder();
    const message = "{ 'type': 'subscribe', 'latitude'-22.7533418:, 'longitude':-47.3309513, 'radius': 20 }";
    const bytes = encoder.encode(message);
    this.socket.write(bytes);
  }

  unsubscribe() : void {
    const encoder = new TextEncoder();
    const message = "{ 'type': 'unsubscribe' }";
    const bytes = encoder.encode(message);
    this.socket.write(bytes);
  }

}

