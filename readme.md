### Effortless Docker Setup

To get started, simply launch your services using Docker with the following command:
```
docker-compose up -d
```

Upon successful setup, the services will be accessible on the following ports:

- **Port 5340**: REST API (Access the Swagger UI by navigating to localhost:5340 in your browser)
- **Port 5341**: gRPC Service
- **Port 5342**: gRPC Code-First Approach (Specifically for C# implementations)

#### Managing PostgreSQL with pgAdmin in Docker

To facilitate easy management of your PostgreSQL database, we've included pgAdmin in our Docker setup. pgAdmin provides a web-based interface for database administration, making it easier to manage your databases directly from your browser.

##### Accessing pgAdmin

After starting your Docker services with docker-compose up -d, pgAdmin will be available at http://localhost:5050. Use the following default credentials to log in, and remember to change them to more secure ones:

- Email: admin@admin.com
= Password: admin

##### Connecting pgAdmin to PostgreSQL

To connect pgAdmin to your PostgreSQL service:

1. Navigate to the pgAdmin dashboard.
2. Right-click on 'Servers' in the left sidebar and choose 'Create' > 'Server'.
3. In the 'Create Server' dialog, go to the 'Connection' tab.
4. For 'Hostname/address', enter postgres (the name of your PostgreSQL service in Docker Compose).
5. Fill in the PostgreSQL credentials (POSTGRES_USER, POSTGRES_PASSWORD, and POSTGRES_DB from your docker-compose.yml).
    ```
    postgres:
        image: postgres:latest
        environment:
            POSTGRES_DB: yourapp
            POSTGRES_USER: youruser
            POSTGRES_PASSWORD: yourpassword
    ```
6. Click 'Save' to establish the connection.

### Database Configuration

Our services support two types of databases: SQLite and PostgreSQL. By default:

- Docker-Compose automatically opts for PostgreSQL.
- Launching the services through Visual Studio defaults to using an SQLite database.

#### Customizing Database Selection

To select your preferred database, utilize the DATABASE_TYPE environment variable with either SQLite or PostgreSQL as the value. Additionally, the DB_CONNECTION_STRING environment variable allows for specifying a custom connection string. In the absence of a provided connection string, the service defaults to using the ConnectionString specified in appsettings.json.

Fallback Behavior:

- If DATABASE_TYPE is not explicitly set, SQLite is used as the default database.

This approach provides flexibility in database management, catering to different development and deployment scenarios.