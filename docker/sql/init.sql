-- =============================================
-- Setup Database and User for TaranSoft.MyGarage
-- =============================================

-- Create SQL Login for application user
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'MyGarageAPI')
BEGIN
    CREATE LOGIN MyGarageAPI WITH PASSWORD = 'Passw0rd123';
    PRINT 'Login MyGarageAPI created successfully.';
END
ELSE
BEGIN
    PRINT 'Login MyGarageAPI already exists.';
END

-- Create application database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CarsDb')
BEGIN
    CREATE DATABASE CarsDb;
    PRINT 'Database CarsDb created successfully.';
END
ELSE
BEGIN
    PRINT 'Database CarsDb already exists.';
END

-- Switch to the application database
USE CarsDb;

-- Create database user
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'MyGarageAPI')
BEGIN
    CREATE USER MyGarageAPI FOR LOGIN MyGarageAPI;
    PRINT 'User MyGarageAPI created successfully in CarsDb.';
END
ELSE
BEGIN
    PRINT 'User MyGarageAPI already exists in CarsDb.';
END

-- Grant necessary permissions to the application user
ALTER ROLE db_owner ADD MEMBER MyGarageAPI;
PRINT 'Granted db_owner permissions to MyGarageAPI in CarsDb.';

-- Additional specific permissions if needed
GRANT CREATE TABLE TO MyGarageAPI;
GRANT ALTER ON SCHEMA::dbo TO MyGarageAPI;
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO MyGarageAPI;
PRINT 'Granted additional permissions to MyGarageAPI in CarsDb.';

-- Verify the setup
SELECT 
    'Login Status' as Check_Type,
    CASE 
        WHEN EXISTS (SELECT * FROM sys.server_principals WHERE name = 'MyGarageAPI') 
        THEN 'MyGarageAPI login exists' 
        ELSE 'MyGarageAPI login missing' 
    END as Status
UNION ALL
SELECT 
    'Database Status',
    CASE 
        WHEN EXISTS (SELECT * FROM sys.databases WHERE name = 'CarsDb') 
        THEN 'CarsDb database exists' 
        ELSE 'CarsDb database missing' 
    END
UNION ALL
SELECT 
    'User Status',
    CASE 
        WHEN EXISTS (SELECT * FROM sys.database_principals WHERE name = 'MyGarageAPI') 
        THEN 'MyGarageAPI user exists in CarsDb' 
        ELSE 'MyGarageAPI user missing in CarsDb' 
    END;

PRINT 'Database setup completed successfully!'; 