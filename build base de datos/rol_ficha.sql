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
-- Table structure for table `ficha`
--

DROP TABLE IF EXISTS `ficha`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ficha` (
  `Nombre` varchar(45) NOT NULL,
  `nom_usuario` varchar(45) NOT NULL,
  `Clase` varchar(45) NOT NULL,
  `subclase` varchar(45) NOT NULL,
  `Raza` varchar(45) NOT NULL,
  `subraza` varchar(45) NOT NULL,
  `STR` int NOT NULL,
  `nivel` int NOT NULL DEFAULT '1',
  `DEX` int NOT NULL,
  `CON` int NOT NULL,
  `WIS` int NOT NULL,
  `INTE` int NOT NULL,
  `CHA` int NOT NULL,
  PRIMARY KEY (`Nombre`),
  KEY `subclase` (`subclase`),
  KEY `subraza` (`subraza`),
  KEY `nom_usuario` (`nom_usuario`),
  KEY `Raza` (`Raza`),
  KEY `Clase` (`Clase`),
  CONSTRAINT `ficha_ibfk_1` FOREIGN KEY (`subclase`) REFERENCES `subclase` (`nombre`) ON DELETE CASCADE,
  CONSTRAINT `ficha_ibfk_2` FOREIGN KEY (`subraza`) REFERENCES `subraza` (`nombre`) ON DELETE CASCADE,
  CONSTRAINT `ficha_ibfk_3` FOREIGN KEY (`nom_usuario`) REFERENCES `usuarios` (`nom_usuario`) ON DELETE CASCADE,
  CONSTRAINT `ficha_ibfk_4` FOREIGN KEY (`Raza`) REFERENCES `raza` (`nombre`) ON DELETE CASCADE,
  CONSTRAINT `ficha_ibfk_5` FOREIGN KEY (`Clase`) REFERENCES `clase` (`Nombre`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ficha`
--

LOCK TABLES `ficha` WRITE;
/*!40000 ALTER TABLE `ficha` DISABLE KEYS */;
INSERT INTO `ficha` VALUES ('Dodap','Admin','Paladin','Devotion','Gnome','Rock Gnome',15,4,15,9,8,10,15),('Irmiel','Admin','Barbarian','Berserker','Elf','High Elf',15,5,17,15,8,9,8),('prueba','Admin','Cleric','Life','Half-Elf','None',8,1,15,8,15,15,10);
/*!40000 ALTER TABLE `ficha` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-06  9:57:40
