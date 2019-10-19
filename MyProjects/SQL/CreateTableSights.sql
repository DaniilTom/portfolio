/* 10.15.2019 */
USE Test;
CREATE TABLE Sights (
	Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	ShortDescription VARCHAR(100),
	FullDescription VARCHAR(500),
	PhotoPath VARCHAR(60),
	[Type] INT FOREIGN KEY REFERENCES SightTypes(Id)
)

ALTER TABLE Sights ADD AuthorId INT;

ALTER TABLE Sights ALTER COLUMN FullDescription TEXT;
ALTER TABLE Sights ALTER COLUMN PhotoPath VARCHAR(250);

/* 10.16.2019 */
USE Test;
ALTER TABLE Sights ALTER COLUMN AuthorId INT NOT NULL;