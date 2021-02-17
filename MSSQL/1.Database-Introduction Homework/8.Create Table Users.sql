CREATE TABLE Users
(
	Id BIGINT PRIMARY KEY IDENTITY,
	[Username] VARCHAR(30) NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARCHAR(MAX),
	LastLoginTime DATETIME,
	IsDeleted BIT
)

INSERT INTO Users ([Username], [Password], ProfilePicture, LastLoginTime, IsDeleted) VALUES
('krasiari', 'skajs123', 'https://en.wikipedia.org/wiki/File:FullMoon2010.jpg', '6/4/2020', 0),
('ivank', 'kajshs', 'https://en.wikipedia.org/wiki/File:FullMoon2010.jpg', '1/3/2020', 0),
('pavelkr', 'sdasdd', 'https://en.wikipedia.org/wiki/File:FullMoon2010.jpg', '8/1/2020', 0),
('anitaar', 'asass', 'https://en.wikipedia.org/wiki/File:FullMoon2010.jpg', '4/12/2020', 0),
('margim', 'sdsad', 'https://en.wikipedia.org/wiki/File:FullMoon2010.jpg', '6/1/2020', 0)