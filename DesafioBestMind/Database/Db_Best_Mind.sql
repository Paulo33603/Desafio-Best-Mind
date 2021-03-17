create database db_best_mind;

use db_best_mind;

create table `user`(
	`id` INT(11) AUTO_INCREMENT NOT NULL PRIMARY KEY,
    `name` VARCHAR(255) NOT NULL,
    `email` VARCHAR(255) NOT NULL,
    `cpf` VARCHAR(255) NOT NULL,
    `cellPhone` VARCHAR(255) NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    `removed` TINYINT(4) DEFAULT 0
);