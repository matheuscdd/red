#!/usr/bin/env bash

until nc -z -v -w30 localhost 1433; do
  echo "Waiting MSSQL..."
  sleep 5
done

readonly cmd="
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'red')
BEGIN
  CREATE DATABASE red;
END;
GO
"

sqlcmd=$(find / -name sqlcmd 2>/dev/null)
"$sqlcmd" -S 'localhost,1433' -U sa -P "$MSSQL_SA_PASSWORD" -Q "$cmd" -C
"$sqlcmd" -S 'localhost,1433' -U sa -P "$MSSQL_SA_PASSWORD" -Q "SELECT name FROM sys.databases;" -C