#  import os
from getpass import getpass
from mysql.connector import connect, Error

try:
    with connect(host="localhost", user="root",password=getpass("Enter password: ")) as connection:
        create_db_query =   "USE  mysqldb; CREATE TABLE User( Id varchar(36),Name varchar(255),Age int,Gender varchar(10),isEnabled bool,Created datetime);"
        with connection.cursor() as cursor:
            cursor.execute(create_db_query)
        print("Query successfully executed!")
except Error as e:
    print(e)


# os.system("dotnet --version");