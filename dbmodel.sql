-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema prosupply
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema prosupply
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `prosupply` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `prosupply` ;

-- -----------------------------------------------------
-- Table `prosupply`.`category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `prosupply`.`category` ;

CREATE TABLE IF NOT EXISTS `prosupply`.`category` (
  `category_id` INT NOT NULL AUTO_INCREMENT,
  `category_name` VARCHAR(45) NOT NULL,
  `category_description` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`category_id`),
  UNIQUE INDEX `category_name_UNIQUE` (`category_name` ASC) VISIBLE,
  UNIQUE INDEX `category_id_UNIQUE` (`category_id` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `prosupply`.`industry`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `prosupply`.`industry` ;

CREATE TABLE IF NOT EXISTS `prosupply`.`industry` (
  `industry_id` INT NOT NULL AUTO_INCREMENT,
  `industry_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`industry_id`),
  UNIQUE INDEX `industry_name_UNIQUE` (`industry_name` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `prosupply`.`user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `prosupply`.`user` ;

CREATE TABLE IF NOT EXISTS `prosupply`.`user` (
  `user_id` INT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(45) NOT NULL,
  `user_name` VARCHAR(100) NOT NULL,
  `user_lastName` VARCHAR(100) NOT NULL,
  `user_email` VARCHAR(45) NOT NULL,
  `user_password` VARCHAR(250) NOT NULL,
  `user_level` ENUM('1', '2', '3', '4') NOT NULL DEFAULT '1',
  PRIMARY KEY (`user_id`),
  UNIQUE INDEX `username_UNIQUE` (`username` ASC) VISIBLE,
  UNIQUE INDEX `user_id_UNIQUE` (`user_id` ASC) VISIBLE,
  UNIQUE INDEX `user_email_UNIQUE` (`user_email` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 13
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `prosupply`.`company`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `prosupply`.`company` ;

CREATE TABLE IF NOT EXISTS `prosupply`.`company` (
  `company_id` INT NOT NULL AUTO_INCREMENT,
  `company_name` VARCHAR(70) NOT NULL,
  `company_phone` VARCHAR(45) NOT NULL,
  `company_email` VARCHAR(45) NOT NULL,
  `company_address` TEXT NOT NULL,
  `company_description` VARCHAR(100) NULL DEFAULT NULL,
  `industry_id` INT NOT NULL,
  `user_id` INT NOT NULL,
  PRIMARY KEY (`company_id`),
  UNIQUE INDEX `company_id_UNIQUE` (`company_id` ASC) VISIBLE,
  UNIQUE INDEX `company_email_UNIQUE` (`company_email` ASC) VISIBLE,
  INDEX `fk_company_industry1_idx` (`industry_id` ASC) VISIBLE,
  INDEX `fk_company_user1_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `fk_company_industry1`
    FOREIGN KEY (`industry_id`)
    REFERENCES `prosupply`.`industry` (`industry_id`),
  CONSTRAINT `fk_company_user1`
    FOREIGN KEY (`user_id`)
    REFERENCES `prosupply`.`user` (`user_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `prosupply`.`supplier`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `prosupply`.`supplier` ;

CREATE TABLE IF NOT EXISTS `prosupply`.`supplier` (
  `supplier_id` INT NOT NULL AUTO_INCREMENT,
  `category_id` INT NOT NULL,
  `supplier_name` VARCHAR(45) NOT NULL,
  `supplier_phone` VARCHAR(45) NOT NULL,
  `supplier_email` VARCHAR(45) NOT NULL,
  `supplier_address` VARCHAR(45) NOT NULL,
  `supplier_description` VARCHAR(45) NOT NULL,
  `user_id` INT NOT NULL,
  `company_id` INT NULL DEFAULT NULL,
  PRIMARY KEY (`supplier_id`),
  INDEX `fk_suppliers_category1_idx` (`category_id` ASC) VISIBLE,
  INDEX `fk_supplier_user1_idx` (`user_id` ASC) VISIBLE,
  INDEX `fk_supplier_company1_idx` (`company_id` ASC) VISIBLE,
  CONSTRAINT `fk_supplier_company1`
    FOREIGN KEY (`company_id`)
    REFERENCES `prosupply`.`company` (`company_id`),
  CONSTRAINT `fk_supplier_user1`
    FOREIGN KEY (`user_id`)
    REFERENCES `prosupply`.`user` (`user_id`),
  CONSTRAINT `fk_suppliers_category1`
    FOREIGN KEY (`category_id`)
    REFERENCES `prosupply`.`category` (`category_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `prosupply`.`order`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `prosupply`.`order` ;

CREATE TABLE IF NOT EXISTS `prosupply`.`order` (
  `order_id` INT NOT NULL AUTO_INCREMENT,
  `supplier_id` INT NOT NULL,
  `user_id` INT NOT NULL,
  `order_date` DATE NOT NULL,
  `order_status` ENUM('pending', 'approved', 'shipped', 'completed', 'canceled') NOT NULL,
  `order_amount` DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (`order_id`),
  UNIQUE INDEX `order_id_UNIQUE` (`order_id` ASC) VISIBLE,
  INDEX `fk_order_suppliers1_idx` (`supplier_id` ASC) VISIBLE,
  INDEX `fk_order_user1_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `fk_order_suppliers1`
    FOREIGN KEY (`supplier_id`)
    REFERENCES `prosupply`.`supplier` (`supplier_id`),
  CONSTRAINT `fk_order_user1`
    FOREIGN KEY (`user_id`)
    REFERENCES `prosupply`.`user` (`user_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `prosupply`.`quote`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `prosupply`.`quote` ;

CREATE TABLE IF NOT EXISTS `prosupply`.`quote` (
  `quotes_id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `company_id` INT NULL DEFAULT NULL,
  `supplier_id` INT NOT NULL,
  `quotes_date` DATE NOT NULL,
  `quote_details` TEXT NOT NULL,
  `quote_price` DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (`quotes_id`),
  UNIQUE INDEX `quotes_id_UNIQUE` (`quotes_id` ASC) VISIBLE,
  INDEX `fk_quotes_user1_idx` (`user_id` ASC) VISIBLE,
  INDEX `fk_quotes_company1_idx` (`company_id` ASC) VISIBLE,
  INDEX `fk_quotes_suppliers1_idx` (`supplier_id` ASC) VISIBLE,
  CONSTRAINT `fk_quotes_company1`
    FOREIGN KEY (`company_id`)
    REFERENCES `prosupply`.`company` (`company_id`),
  CONSTRAINT `fk_quotes_suppliers1`
    FOREIGN KEY (`supplier_id`)
    REFERENCES `prosupply`.`supplier` (`supplier_id`),
  CONSTRAINT `fk_quotes_user1`
    FOREIGN KEY (`user_id`)
    REFERENCES `prosupply`.`user` (`user_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 11
DEFAULT CHARACTER SET = utf8mb3;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
