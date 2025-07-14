 -- =============================================
-- Setup Test Database for TaranSoft.MyGarage Integration Tests
-- =============================================

-- Create SQL Login for test user
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'MyGarageAPITests')
BEGIN
    CREATE LOGIN MyGarageAPITests WITH PASSWORD = 'Passw0rd123';
    PRINT 'Login MyGarageAPITests created successfully.';
END
ELSE
BEGIN
    PRINT 'Login MyGarageAPITests already exists.';
END

-- Create test database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CarsDbTests')
BEGIN
    CREATE DATABASE CarsDbTests;
    PRINT 'Database CarsDbTests created successfully.';
END
ELSE
BEGIN
    PRINT 'Database CarsDbTests already exists.';
END

-- Switch to the test database
USE CarsDbTests;

-- Create database user
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'MyGarageAPITests')
BEGIN
    CREATE USER MyGarageAPITests FOR LOGIN MyGarageAPITests;
    PRINT 'User MyGarageAPITests created successfully in CarsDbTests.';
END
ELSE
BEGIN
    PRINT 'User MyGarageAPITests already exists in CarsDbTests.';
END

-- Grant necessary permissions to the test user
-- db_owner role for full access during testing
ALTER ROLE db_owner ADD MEMBER MyGarageAPITests;
PRINT 'Granted db_owner permissions to MyGarageAPITests in CarsDbTests.';

-- Additional specific permissions if needed
GRANT CREATE TABLE TO MyGarageAPITests;
GRANT ALTER ON SCHEMA::dbo TO MyGarageAPITests;
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO MyGarageAPITests;
PRINT 'Granted additional permissions to MyGarageAPITests in CarsDbTests.';

-- Verify the setup
SELECT 
    'Login Status' as Check_Type,
    CASE 
        WHEN EXISTS (SELECT * FROM sys.server_principals WHERE name = 'MyGarageAPITests') 
        THEN 'MyGarageAPITests login exists' 
        ELSE 'MyGarageAPITests login missing' 
    END as Status
UNION ALL
SELECT 
    'Database Status',
    CASE 
        WHEN EXISTS (SELECT * FROM sys.databases WHERE name = 'CarsDbTests') 
        THEN 'CarsDbTests database exists' 
        ELSE 'CarsDbTests database missing' 
    END
UNION ALL
SELECT 
    'User Status',
    CASE 
        WHEN EXISTS (SELECT * FROM sys.database_principals WHERE name = 'MyGarageAPITests') 
        THEN 'MyGarageAPITests user exists in CarsDbTests' 
        ELSE 'MyGarageAPITests user missing in CarsDbTests' 
    END;

PRINT 'Test database setup completed successfully!';
PRINT 'Connection string: Server=(localdb)\mssqllocaldb;Database=CarsDbTests;User Id=MyGarageAPITests;Password=Passw0rd123;MultipleActiveResultSets=true';