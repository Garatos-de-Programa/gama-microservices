import { PoolClient } from "pg";

export default interface IDatabaseConnection {
    getPostgressConnectionAsync() : Promise<PoolClient>
}