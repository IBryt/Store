# Store

An application representing an online store. This small application is designed for learning

## Install
This application is installed using Docker Compose.

1 SQL installation
```
 docker-compose -f docker-compose.services.yml up -d
```
2 Backend and frontend installation.
```
 docker-compose -f docker-compose.apps.yml up -d
```
If you want to use demo data, you can copy the images from the demo/productImages folder to the backend/WebAPI/wwwroot/productImages folder before step 2. Afterward, go to SSMS and execute the demo/insert.sql script.

Now connect using SSMS:
```    
    Server name: localhost,1433
    Authentication: SQL Server Authentication
    Login: sa
    Password: MySecretPassword!
```