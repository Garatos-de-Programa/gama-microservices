export default interface ISocketConnection {
  write(data : Uint8Array) : void;
}