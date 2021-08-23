USE testdatabasex;
SHOW TABLES;
-- SHOW COLLATION;
-- SHOW CHARACTER SET;

DROP TABLE IF EXISTS ArticlesCategories;
DROP TABLE IF EXISTS Categories;
DROP TABLE IF EXISTS Comments;
DROP TABLE IF EXISTS Articles;
DROP TABLE IF EXISTS Accounts;
DROP TABLE IF EXISTS Roles;
-- --------------------------------
CREATE TABLE Roles (
	Id TINYINT UNSIGNED NOT NULL,
	Priveledge VARCHAR(50) UNIQUE NOT NULL,
	CONSTRAINT PRIMARY KEY (Id)
);

INSERT INTO Roles (Id, Priveledge) VALUES (1, 'Admin'), (2, 'User');

CREATE TABLE Accounts (
	Id INT UNSIGNED AUTO_INCREMENT,
	Name VARCHAR(40) UNIQUE NOT NULL,
	-- PasswordHash VARCHAR(100) NOT NULL,
	-- PasswordSalt VARCHAR(100) NOT NULL,
	PasswordHash VARBINARY(100) NOT NULL,
	PasswordSalt VARBINARY(100) NOT NULL,
	RolesId TINYINT UNSIGNED NOT NULL DEFAULT 2,
	CONSTRAINT PRIMARY KEY (Id),
	CONSTRAINT FOREIGN KEY (RolesId) REFERENCES Roles(Id)
);
-- TODO: Ändra PasswordHash och PasswordSalt-längderna.

CREATE TABLE Articles (
	Id INT UNSIGNED AUTO_INCREMENT,
	Title VARCHAR(200) NOT NULL,
	CreateDate DATETIME DEFAULT NOW(),
	EditDate DATETIME,
	CONSTRAINT PRIMARY KEY (Id)
);

CREATE TABLE Categories (
	Id TINYINT UNSIGNED AUTO_INCREMENT,
	Category VARCHAR(100) NOT NULL,
	CONSTRAINT PRIMARY KEY (Id)
);

CREATE TABLE ArticlesCategories (
	ArticlesId INT UNSIGNED,
	CategoriesId TINYINT UNSIGNED,
	CONSTRAINT FOREIGN KEY (ArticlesId) REFERENCES Articles(Id) ON DELETE CASCADE,
	CONSTRAINT FOREIGN KEY (CategoriesId) REFERENCES Categories(Id) ON DELETE CASCADE
);

CREATE TABLE Comments (
	Id INT UNSIGNED AUTO_INCREMENT,
	Comment VARCHAR(200),		-- Är NULL ifall kommentaren är borttagen och har sub-kommentarer.
	CreateDate DATETIME DEFAULT NOW(),
	EditDate DATETIME,
	AccountsId INT UNSIGNED NOT NULL,
	CommentsId INT UNSIGNED,
	ArticlesId INT UNSIGNED NOT NULL,
	CONSTRAINT PRIMARY KEY (Id),
	CONSTRAINT FOREIGN KEY (AccountsId) REFERENCES Accounts(Id),
	CONSTRAINT FOREIGN KEY (ArticlesId) REFERENCES Articles(Id) ON DELETE CASCADE,
	CONSTRAINT FOREIGN KEY (CommentsId) REFERENCES Comments(Id) ON DELETE CASCADE
);

-- --------------------------------
-- TEST Accounts
INSERT INTO Accounts (Name, PasswordHash, PasswordSalt) VALUES ('ACCOUNT1', 'PWD', 'SALT'), ('ACCOUNT2', 'PWD', 'SALT'), ('ACCOUNT3', 'PWD', 'SALT');
SELECT * FROM Accounts;
DELETE FROM Accounts WHERE Id = 11;
-- TEST END

-- TEST Articles
INSERT INTO Articles (Title) VALUES ('Testarticle1'), ('Testarticle2'), ('Testarticle3');
UPDATE Articles SET Title='TestarticleUPDATE' WHERE Id=1;
SELECT * FROM Articles;
-- TEST END

-- TEST Categories
INSERT INTO Categories (Category) VALUES ('Category1'), ('Category2'), ('Category3');
SELECT * FROM Categories;
-- TEST END

-- TEST ArticlesCategories
INSERT INTO ArticlesCategories (ArticlesId, CategoriesId) VALUES (1, 1), (1, 2), (2, 3), (3, 1);
SELECT * FROM ArticlesCategories;
SELECT * FROM Articles;
SELECT * FROM Categories;
DELETE FROM Articles WHERE Id=1;
DELETE FROM Categories WHERE Id=1;
-- TEST END

-- TEST --
INSERT INTO Comments (Comment, AccountsId, ArticlesId) VALUES ('COMMENT1', 4, 2), ('COMMENT2', 5, 3);
INSERT INTO Comments (Comment, AccountsId, CommentsId, ArticlesId) VALUES ('COMMENT3', 6, 3, 2), ('COMMENT4', 5, 3, 2), ('COMMENT5', 4, 4, 2);
DELETE FROM Comments WHERE Id = 1;
DELETE FROM Articles WHERE Id = 1;
SELECT * FROM Comments;
-- TEST END --