CREATE TABLE [dbo].[Student]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AdminID] INT NOT NULL, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Address] VARCHAR(50) NOT NULL, 
    [DegreeName] VARCHAR(50) NOT NULL, 
    [AdmissionYear] VARCHAR(50) NOT NULL, 
    [AdmissionFee] VARCHAR(50) NOT NULL, 
    [StudentEmail] VARCHAR(50) NOT NULL
)
