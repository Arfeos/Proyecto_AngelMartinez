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
-- Table structure for table `ficha_rasgo`
--

DROP TABLE IF EXISTS `ficha_rasgo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ficha_rasgo` (
  `nombre_personaje` varchar(45) DEFAULT NULL,
  `indice_rasgo` varchar(45) DEFAULT NULL,
  KEY `ficha_rasgo_ibfk_1` (`nombre_personaje`),
  KEY `ras_idx` (`indice_rasgo`),
  CONSTRAINT `ficha_rasgo_ibfk_1` FOREIGN KEY (`nombre_personaje`) REFERENCES `ficha` (`Nombre`) ON DELETE CASCADE,
  CONSTRAINT `ras` FOREIGN KEY (`indice_rasgo`) REFERENCES `rasgos` (`Nombre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ficha_rasgo`
--

LOCK TABLES `ficha_rasgo` WRITE;
/*!40000 ALTER TABLE `ficha_rasgo` DISABLE KEYS */;
INSERT INTO `ficha_rasgo` VALUES ('Irmiel','Darkvision'),('Irmiel','Fey Ancestry'),('Irmiel','Trance'),('Irmiel','Keen Senses'),('Irmiel','Elf Weapon Training'),('Irmiel','High Elf Cantrip'),('Irmiel','Extra Language'),('Irmiel','Rage'),('Irmiel','Unarmored Defense'),('Irmiel','Reckless Attack'),('Irmiel','Danger Sense'),('Irmiel','Frenzy'),('Irmiel','Primal Path'),('Irmiel','Extra Attack'),('Irmiel','Fast Movement'),('Dodap','Darkvision'),('Dodap','Gnome Cunning'),('Dodap','Artificer\'s Lore'),('Dodap','Tinker'),('Dodap','Divine Sense'),('Dodap','Lay on Hands'),('Dodap','Fighting Style'),('Dodap','Spellcasting: Paladin'),('Dodap','Divine Smite'),('Dodap','Channel Divinity: Sacred Weapon'),('Dodap','Channel Divinity: Turn the Unholy'),('Dodap','Divine Health'),('Dodap','Sacred Oath'),('Dodap','Oath Spells'),('Dodap','Channel Divinity'),('prueba','Darkvision'),('prueba','Fey Ancestry'),('prueba','Skill Versatility');
/*!40000 ALTER TABLE `ficha_rasgo` ENABLE KEYS */;
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
