-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: rol
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `imagenes`
--

DROP TABLE IF EXISTS `imagenes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `imagenes` (
  `Clase` varchar(45) NOT NULL,
  `url` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Clase`),
  CONSTRAINT `Clase` FOREIGN KEY (`Clase`) REFERENCES `clase` (`Nombre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagenes`
--

LOCK TABLES `imagenes` WRITE;
/*!40000 ALTER TABLE `imagenes` DISABLE KEYS */;
INSERT INTO `imagenes` VALUES ('Barbarian','https://www.dndbeyond.com/avatars/thumbnails/6/342/420/618/636272680339895080.png'),('Bard','https://www.dndbeyond.com/avatars/thumbnails/6/369/420/618/636272705936709430.png'),('Cleric','https://www.dndbeyond.com/avatars/thumbnails/6/371/420/618/636272706155064423.png'),('Druid','https://www.dndbeyond.com/avatars/thumbnails/6/346/420/618/636272691461725405.png'),('Fighter','https://www.dndbeyond.com/avatars/thumbnails/6/359/420/618/636272697874197438.png'),('Monk','https://www.dndbeyond.com/avatars/thumbnails/6/489/420/618/636274646181411106.png'),('Paladin','https://www.dndbeyond.com/avatars/thumbnails/6/365/420/618/636272701937419552.png'),('Ranger','https://www.dndbeyond.com/avatars/thumbnails/6/367/420/618/636272702826438096.png'),('Rogue','https://www.dndbeyond.com/avatars/thumbnails/6/384/420/618/636272820319276620.png'),('Sorcerer','https://www.dndbeyond.com/avatars/thumbnails/6/485/420/618/636274643818663058.png'),('Warlock','https://www.dndbeyond.com/avatars/thumbnails/6/375/420/618/636272708661726603.png'),('Wizard','https://www.dndbeyond.com/avatars/thumbnails/6/357/420/618/636272696881281556.png');
/*!40000 ALTER TABLE `imagenes` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-06  9:57:41
