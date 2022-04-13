from subprocess import Popen

try: 
    Popen('dotnet-ef database update',cwd='../UserAuthentication/User.API')
except Error as e:
    print(e)