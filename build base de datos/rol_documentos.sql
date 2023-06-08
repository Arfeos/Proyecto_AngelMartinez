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
-- Table structure for table `documentos`
--

DROP TABLE IF EXISTS `documentos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `documentos` (
  `Nombre_ficha` varchar(45) NOT NULL,
  `Contenido` text,
  `nom_doc` varchar(45) NOT NULL,
  PRIMARY KEY (`nom_doc`),
  KEY `Ficha_idx` (`Nombre_ficha`),
  CONSTRAINT `Ficha` FOREIGN KEY (`Nombre_ficha`) REFERENCES `ficha` (`Nombre`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `documentos`
--

LOCK TABLES `documentos` WRITE;
/*!40000 ALTER TABLE `documentos` DISABLE KEYS */;
INSERT INTO `documentos` VALUES ('Dodap','TextBox:Tras:\r\r\r\r\r\r\r\r\r\r\r\r\r\nTextBox:niv:4\r\r\r\r\r\r\r\r\r\r\r\r\r\nRichTextBox:rtxtInv:\r\r\r\r\r\r\r\r\r\r\r\nCheckBox:rdFue:False\r\nCheckBox:rdDes:False\r\nCheckBox:rdCon:False\r\nCheckBox:rdInt:False\r\nCheckBox:rdSab:True\r\nCheckBox:rdCar:True\r\nCheckBox:rdCar:True\r\nCheckBox:rdAcro:False\r\nCheckBox:rdArca:False\r\nCheckBox:rdAtle:False\r\nCheckBox:rdEnga:False\r\nCheckBox:rdHist:True\r\nCheckBox:rdInte:False\r\nCheckBox:rdInti:False\r\nCheckBox:rdInve:False\r\nCheckBox:rdInve:False\r\nCheckBox:rdJuMa:False\r\nCheckBox:rdMedi:False\r\nCheckBox:rdNatu:False\r\nCheckBox:rdPerc:False\r\nCheckBox:rdPersp:False\r\nCheckBox:rdPersu:False\r\nCheckBox:rdReli:False\r\nCheckBox:rdSigi:False\r\nCheckBox:rdSupe:False\r\nCheckBox:rdTrAn:False\r\nRichTextBox:rtxtRasPer:\r\r\r\r\r\r\r\r\r\r\r\r\r\nRichTextBox:rtxtide:\r\r\r\r\r\r\r\r\r\r\r\r\r\nRichTextBox:rtxtVin:\r\r\r\r\r\r\r\r\r\r\r\r\r\nRichTextBox:rtxtDef:\r\r\r\r\r\r\r\r\r\r\r\r\r\nComboBox:Ali:0\r\nRichTextBox:rtxtcomp:\r\r\r\r\r\r\r\r\r\r\r\n','fichDodap'),('Irmiel','TextBox:Tras:\r\r\r\r\r\nTextBox:niv:5\r\r\r\r\r\nRichTextBox:rtxtInv:\r\r\r\r\r\nCheckBox:rdFue:True\r\nCheckBox:rdDes:False\r\nCheckBox:rdCon:True\r\nCheckBox:rdInt:False\r\nCheckBox:rdSab:False\r\nCheckBox:rdCar:False\r\nCheckBox:rdCar:False\r\nCheckBox:rdAcro:True\r\nCheckBox:rdArca:True\r\nCheckBox:rdAtle:True\r\nCheckBox:rdEnga:False\r\nCheckBox:rdHist:False\r\nCheckBox:rdInte:False\r\nCheckBox:rdInti:False\r\nCheckBox:rdInve:False\r\nCheckBox:rdInve:False\r\nCheckBox:rdJuMa:False\r\nCheckBox:rdMedi:False\r\nCheckBox:rdNatu:False\r\nCheckBox:rdPerc:False\r\nCheckBox:rdPersp:False\r\nCheckBox:rdPersu:False\r\nCheckBox:rdReli:False\r\nCheckBox:rdSigi:False\r\nCheckBox:rdSupe:False\r\nCheckBox:rdTrAn:False\r\nRichTextBox:rtxtRasPer:\r\r\r\r\r\nRichTextBox:rtxtide:\r\r\r\r\r\nRichTextBox:rtxtVin:\r\r\r\r\r\nRichTextBox:rtxtDef:\r\r\r\r\r\nComboBox:Ali:0\r\nRichTextBox:rtxtcomp:\r\r\r\r\r\n','fichIrmiel'),('Dodap','TextBox:act1:\r\nTextBox:act2:\r\nTextBox:act3:1\r\nTextBox:act4:\r\nTextBox:act5:\r\nTextBox:act6:\r\nTextBox:act7:\r\nTextBox:act8:\r\nTextBox:act9:\r\n','hechDodap');
/*!40000 ALTER TABLE `documentos` ENABLE KEYS */;
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
