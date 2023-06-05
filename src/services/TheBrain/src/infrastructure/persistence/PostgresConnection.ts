import { Pool, PoolClient } from "pg";
import IDatabaseConnection from "./IDatabaseConnection";

export default class PostgresConnection implements IDatabaseConnection {
    private _pool : Pool | null = null;

    async getPostgressConnectionAsync(): Promise<PoolClient> {
        if(!this._pool) {
            this._pool = new Pool({
                user: 'admin',
                password: 'admin1234',
                database: 'NationalGeographicDb',
                host: 'localhost',
                port: 5433,
              });    
        }
        
        return await this._pool.connect();
    }
}