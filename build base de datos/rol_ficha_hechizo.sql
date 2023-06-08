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
-- Table structure for table `ficha_hechizo`
--

DROP TABLE IF EXISTS `ficha_hechizo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ficha_hechizo` (
  `nom_per` varchar(45) NOT NULL,
  `nom_hechizo` varchar(45) NOT NULL,
  `niv` int DEFAULT NULL,
  KEY `ficha_idx` (`nom_per`),
  KEY `hechizo_idx` (`nom_hechizo`),
  CONSTRAINT `fich` FOREIGN KEY (`nom_per`) REFERENCES `ficha` (`Nombre`) ON UPDATE CASCADE,
  CONSTRAINT `hech` FOREIGN KEY (`nom_hechizo`) REFERENCES `hechizos` (`Nombre`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ficha_hechizo`
--

LOCK TABLES `ficha_hechizo` WRITE;
/*!40000 ALTER TABLE `ficha_hechizo` DISABLE KEYS */;
INSERT INTO `ficha_hechizo` VALUES ('Dodap','Protection from Evil and Good',1),('Dodap','Sanctuary',1),('Dodap','Lesser Restoration',2),('Dodap','Zone of Truth',2),('Dodap','Beacon of Hope',3),('Dodap','Dispel Magic',3),('Dodap','Freedom of Movement',4),('Dodap','Guardian of Faith',4),('Dodap','Commune',5),('Dodap','Flame Strike',5),('Dodap','Bless',1),('Dodap','Command',1),('Dodap','Cure Wounds',1),('Dodap','Detect Evil and Good',1),('Dodap','Detect Magic',1),('Dodap','Detect Poison and Disease',1),('Dodap','Divine Favor',1),('Dodap','Heroism',1),('Dodap','Hunter\'s Mark',1),('Dodap','Protection from Evil and Good',1),('Dodap','Purify Food and Drink',1),('Dodap','Shield of Faith',1),('Dodap','Aid',2),('Dodap','Branding Smite',2),('Dodap','Find Steed',2),('Dodap','Lesser Restoration',2),('Dodap','Locate Object',2),('Dodap','Magic Weapon',2),('Dodap','Protection from Poison',2),('Dodap','Zone of Truth',2),('Dodap','Create Food and Water',3),('Dodap','Daylight',3),('Dodap','Dispel Magic',3),('Dodap','Magic Circle',3),('Dodap','Remove Curse',3),('Dodap','Revivify',3),('Dodap','Banishment',4),('Dodap','Death Ward',4),('Dodap','Guardian of Faith',4),('Dodap','Locate Creature',4),('Dodap','Dispel Evil and Good',5),('Dodap','Geas',5),('Dodap','Raise Dead',5),('prueba','Bless',1),('prueba','Cure Wounds',1),('prueba','Lesser Restoration',2),('prueba','Spiritual Weapon',2),('prueba','Beacon of Hope',3),('prueba','Revivify',3),('prueba','Death Ward',4),('prueba','Mass Cure Wounds',5),('prueba','Raise Dead',5),('prueba','Guidance',0),('prueba','Light',0),('prueba','Mending',0),('prueba','Resistance',0),('prueba','Sacred Flame',0),('prueba','Spare the Dying',0),('prueba','Thaumaturgy',0),('prueba','Animal Friendship',1),('prueba','Bane',1),('prueba','Bless',1),('prueba','Command',1),('prueba','Create or Destroy Water',1),('prueba','Cure Wounds',1),('prueba','Detect Evil and Good',1),('prueba','Detect Magic',1),('prueba','Detect Poison and Disease',1),('prueba','Guiding Bolt',1),('prueba','Healing Word',1),('prueba','Inflict Wounds',1),('prueba','Protection from Evil and Good',1),('prueba','Purify Food and Drink',1),('prueba','Sanctuary',1),('prueba','Shield of Faith',1),('prueba','Aid',2),('prueba','Augury',2),('prueba','Blindness/Deafness',2),('prueba','Calm Emotions',2),('prueba','Continual Flame',2),('prueba','Enhance Ability',2),('prueba','Find Traps',2),('prueba','Gentle Repose',2),('prueba','Hold Person',2),('prueba','Lesser Restoration',2),('prueba','Locate Object',2),('prueba','Prayer of Healing',2),('prueba','Protection from Poison',2),('prueba','Silence',2),('prueba','Spiritual Weapon',2),('prueba','Warding Bond',2),('prueba','Zone of Truth',2),('prueba','Animate Dead',3),('prueba','Beacon of Hope',3),('prueba','Bestow Curse',3),('prueba','Clairvoyance',3),('prueba','Create Food and Water',3),('prueba','Daylight',3),('prueba','Dispel Magic',3),('prueba','Glyph of Warding',3),('prueba','Magic Circle',3),('prueba','Mass Healing Word',3),('prueba','Meld Into Stone',3),('prueba','Protection From Energy',3);
/*!40000 ALTER TABLE `ficha_hechizo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-06  9:57:39
