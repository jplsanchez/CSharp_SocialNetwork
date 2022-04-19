
# To run: 
# bash -i ./Setup.sh

docker-compose down -v

docker-compose up -d 

conda run set_postgres_database_migrations.py

conda run set_mysql_database.py