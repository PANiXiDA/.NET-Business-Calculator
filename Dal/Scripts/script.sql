create database BusinessCalculator
alter database BusinessCalculator set recovery simple
go

use BusinessCalculator
go

CREATE TABLE Payment (
    Id INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    PaymentMethod VARCHAR(50) NULL,
    Status VARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NULL,
);

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Login VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    RoleId INT NOT NULL,
    IsBlocked BIT NOT NULL DEFAULT 0,
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE()
)

INSERT INTO Users (Login, Password, RoleId, IsBlocked, RegistrationDate)
VALUES ('admin', 'd61d004c03457bac7b90c1e8d4f51113be162346b27af5307caffe21ef88597ff15ab1569e07155302ff7b0af29f7f0431531004568da3849a5708176815a70f', 1, 0, GETDATE());