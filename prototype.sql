-- phpMyAdmin SQL Dump
-- version 4.9.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3308
-- Generation Time: Apr 23, 2020 at 07:07 PM
-- Server version: 8.0.18
-- PHP Version: 7.3.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `prototype`
--

-- --------------------------------------------------------

--
-- Table structure for table `administrators`
--

DROP TABLE IF EXISTS `administrators`;
CREATE TABLE IF NOT EXISTS `administrators` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nickname` varchar(30) NOT NULL,
  `password` varchar(100) NOT NULL,
  `createDate` date NOT NULL,
  `description` varchar(4000) DEFAULT NULL,
  `lastLogin` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `administrators`
--

INSERT INTO `administrators` (`id`, `nickname`, `password`, `createDate`, `description`, `lastLogin`) VALUES
(1, 'admin', 'empty', '2020-04-23', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `news_posts`
--

DROP TABLE IF EXISTS `news_posts`;
CREATE TABLE IF NOT EXISTS `news_posts` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(200) NOT NULL,
  `text` varchar(4000) NOT NULL,
  `createDate` date NOT NULL,
  `fk_writtenBy` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fkc_writtenBy` (`fk_writtenBy`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `news_posts`
--

INSERT INTO `news_posts` (`id`, `title`, `text`, `createDate`, `fk_writtenBy`) VALUES
(1, 'Naujiena 1', 'Trumpas sakinys apie naujiena', '2020-04-23', 1),
(2, 'Naujiena 2', 'Ilgesnis sakinys apie naujiena ir ne tik kaskdaksfksdgksdgk.', '2020-04-09', 1);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `news_posts`
--
ALTER TABLE `news_posts`
  ADD CONSTRAINT `fkc_writtenBy` FOREIGN KEY (`fk_writtenBy`) REFERENCES `administrators` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
