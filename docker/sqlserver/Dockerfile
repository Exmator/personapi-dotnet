FROM mcr.microsoft.com/mssql/server:2022-latest
USER root
COPY db/init.sql /init.sql
COPY docker/sqlserver/entrypoint.sh /entrypoint.sh
RUN sed -i 's/\r//' /entrypoint.sh && chmod +x /entrypoint.sh
USER mssql
ENTRYPOINT ["/entrypoint.sh"]
