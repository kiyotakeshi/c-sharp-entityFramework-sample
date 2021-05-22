# entityFramework sample

## Run local

- start mssql server using Docker

```shell
$ docker-compose up -d
```

- create user in mssql server
    - to use [Azure Data Studio](https://docs.microsoft.com/ja-jp/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15)

```sql
create LOGIN CommanderAPI with PASSWORD = '1qazxsw2!';

-- role の確認
-- SELECT * FROM sys.fn_builtin_permissions('SERVER') ORDER BY permission_name;

-- login を再作成する場合(session を kill して drop & create)
-- SELECT session_id FROM sys.dm_exec_sessions WHERE login_name = 'CommanderAPI'
-- KILL 55
-- drop login CommanderAPI;

-- role を追加しないと entityFramework の migration に失敗する
-- CREATE DATABASE permission denied in database 'master'.
ALTER SERVER ROLE sysadmin ADD MEMBER CommanderAPI; 
```

- To set login user into [appsettings.json](./appsettings.json)

- migrate

```shell
dotnet ef migrations add InitialMigration

dotnet ef database update
```

```sql
-- SELECT top(1000) [MigrationId], [ProductVersion] from CommanderDB.dbo.__EFMigrationsHistory;

-- サンプルデータを投入する場合
INSERT CommanderDB.dbo.Commands VALUES ('how to create migrations', 'dotnet ef migrations add', 'EF Core');
INSERT CommanderDB.dbo.Commands VALUES ('how to run migrations', 'dotnet ef database update', 'EF Core');
SELECT * from CommanderDB.dbo.Commands;
```

- run application

```shell
dotnet run
```

- CRUD

```shell
# POST
curl --location --request POST 'http://localhost:5000/api/commands/' \
--header 'Content-Type: application/json' \
--data-raw '{
    "howTo": "how to create migrations5",
    "line": "dotnet ef migrations add",
    "platform": ".NET Core CLI"
}' -k

# GET
curl --location --request GET 'http://localhost:5000/api/commands/' -k

curl --location --request GET 'http://localhost:5000/api/commands/1' -k

# DELETE
curl --location --request DELETE 'http://localhost:5000/api/commands/1' -k

# PUT
curl --location --request PUT 'http://localhost:5000/api/commands/1' \
--header 'Content-Type: application/json' \
--data-raw '{
    "howTo": "update",
    "line": "dotnet ef migrations add",
    "platform": ".NET Core CLI"
}' -k

# PATCH
curl --location --request PATCH 'http://localhost:5000/api/commands/1' \
--header 'Content-Type: application/json' \
--data-raw '[
    {
        "op": "replace",
        "path": "/howto",
        "value": "new value from patch replace"
    }
]' -k
```
