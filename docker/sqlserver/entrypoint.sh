#!/bin/bash
set -e

/opt/mssql/bin/sqlservr &
MSSQL_PID=$!

echo "[init] Waiting for SQL Server to accept connections..."
for i in $(seq 1 60); do
    if /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" -C -l 1 >/dev/null 2>&1; then
        echo "[init] SQL Server is up."
        break
    fi
    sleep 2
done

echo "[init] Running init.sql..."
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i /init.sql -C
echo "[init] Database ready."

wait $MSSQL_PID
