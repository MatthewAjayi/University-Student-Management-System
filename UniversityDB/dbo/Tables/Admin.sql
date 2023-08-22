CREATE TABLE [dbo].[Admin]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UniversityName] VARCHAR(50) NOT NULL, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [EmailAddress] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL
)
