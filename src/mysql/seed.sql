CREATE DATABASE IF NOT EXISTS WebApiDB;

USE WebApiDB;

GRANT SELECT ON *.* TO 'garrard' IDENTIFIED BY 'password';
GRANT INSERT ON *.* TO 'garrard' IDENTIFIED BY 'password';
GRANT UPDATE ON *.* TO 'garrard' IDENTIFIED BY 'password';

CREATE TABLE IF NOT EXISTS Users (
    Firstname VARCHAR (50),
    Surname VARCHAR (50),
    Email VARCHAR(320),
    HashedPassword VARCHAR(128),
    CONSTRAINT pk_email PRIMARY KEY (Email)
) ENGINE='InnoDB';



-- DROP PROCEDURE IF EXISTS `sp_ins_user`;
-- DROP PROCEDURE IF EXISTS `sp_upd_user`;
-- DROP PROCEDURE IF EXISTS `sp_sel_user`;

DELIMITER $$
CREATE PROCEDURE sp_ins_user(IN firstname VARCHAR(50), IN surname VARCHAR(50), IN email VARCHAR(320), IN password VARCHAR(128))
BEGIN
    INSERT INTO `Users` (Firstname, Surname, Email, HashedPassword) VALUES (firstname, surname, email, password);
    SELECT Firstname,
           Surname,
           Email
    FROM `Users`
    WHERE Email = email;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE sp_upd_user(IN firstname VARCHAR(50), IN surname VARCHAR(50), IN email VARCHAR(320), IN password VARCHAR(128))
BEGIN
    UPDATE `Users`
    SET
        Firstname = firstname,
        Surname = surname,
        HashedPassword = password
    WHERE
        Email = Email;

    SELECT Firstname,
           Surname,
           Email
    FROM `Users`
    WHERE Email = email;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE sp_sel_user(IN email VARCHAR(320))
BEGIN
    SELECT Firstname,
           Surname,
           Email
    FROM `Users`
    WHERE Email = email;
END$$
DELIMITER ;

GRANT EXECUTE ON PROCEDURE sp_sel_user TO 'garrard';
GRANT EXECUTE ON PROCEDURE sp_upd_user TO 'garrard';
GRANT EXECUTE ON PROCEDURE sp_ins_user TO 'garrard';

