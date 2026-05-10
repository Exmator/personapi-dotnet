IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'persona_db')
BEGIN
    CREATE DATABASE persona_db;
END
GO

USE persona_db;
GO

IF OBJECT_ID('estudios', 'U') IS NULL
BEGIN
    CREATE TABLE estudios (
        id_prof INT         NOT NULL,
        cc_per  INT         NOT NULL,
        fecha   DATE        NULL,
        univer  VARCHAR(50) NULL,
        CONSTRAINT PK_estudios PRIMARY KEY (id_prof, cc_per)
    );
END
GO

IF OBJECT_ID('profesion', 'U') IS NULL
BEGIN
    CREATE TABLE profesion (
        id  INT          NOT NULL,
        nom VARCHAR(90)  NULL,
        des VARCHAR(MAX) NULL,
        CONSTRAINT PK_profesion PRIMARY KEY (id)
    );
END
GO

IF OBJECT_ID('persona', 'U') IS NULL
BEGIN
    CREATE TABLE persona (
        cc       INT        NOT NULL,
        nombre   VARCHAR(45) NULL,
        apellido VARCHAR(45) NULL,
        genero   CHAR(1)    NULL,
        edad     INT        NULL,
        CONSTRAINT PK_persona PRIMARY KEY (cc)
    );
END
GO

IF OBJECT_ID('telefono', 'U') IS NULL
BEGIN
    CREATE TABLE telefono (
        num    VARCHAR(15) NOT NULL,
        oper   VARCHAR(45) NULL,
        duenio INT         NULL,
        CONSTRAINT PK_telefono PRIMARY KEY (num)
    );
END
GO

IF OBJECT_ID('FK_estudios_per', 'F') IS NULL
BEGIN
    ALTER TABLE estudios
        ADD CONSTRAINT FK_estudios_per FOREIGN KEY (cc_per) REFERENCES persona (cc);
END
GO

IF OBJECT_ID('FK_estudios_prof', 'F') IS NULL
BEGIN
    ALTER TABLE estudios
        ADD CONSTRAINT FK_estudios_prof FOREIGN KEY (id_prof) REFERENCES profesion (id);
END
GO

IF OBJECT_ID('FK_telefono_per', 'F') IS NULL
BEGIN
    ALTER TABLE telefono
        ADD CONSTRAINT FK_telefono_per FOREIGN KEY (duenio) REFERENCES persona (cc);
END
GO
