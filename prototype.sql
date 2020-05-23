-- phpMyAdmin SQL Dump
-- version 4.9.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: May 23, 2020 at 01:37 PM
-- Server version: 10.4.10-MariaDB
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
  `id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `administrators`
--

INSERT INTO `administrators` (`id`) VALUES
(1),
(2),
(3),
(4);

-- --------------------------------------------------------

--
-- Table structure for table `csgomatch`
--

DROP TABLE IF EXISTS `csgomatch`;
CREATE TABLE IF NOT EXISTS `csgomatch` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `winner` int(11) NOT NULL DEFAULT -1,
  `startTime` datetime DEFAULT NULL,
  `endTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2682 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `csgomatch`
--

INSERT INTO `csgomatch` (`id`, `winner`, `startTime`, `endTime`) VALUES
(2681, -1, '2020-05-22 21:23:46', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `csgo_match_player`
--

DROP TABLE IF EXISTS `csgo_match_player`;
CREATE TABLE IF NOT EXISTS `csgo_match_player` (
  `teamno` int(11) NOT NULL,
  `admitted_Defeat` int(11) NOT NULL,
  `player_id` int(11) NOT NULL,
  `csgomatch_id` int(11) NOT NULL,
  PRIMARY KEY (`player_id`,`csgomatch_id`),
  KEY `plays` (`csgomatch_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `csgo_match_player`
--

INSERT INTO `csgo_match_player` (`teamno`, `admitted_Defeat`, `player_id`, `csgomatch_id`) VALUES
(0, 0, 8, 2681),
(0, 0, 100, 2681),
(0, 0, 101, 2681),
(0, 0, 102, 2681),
(0, 0, 103, 2681),
(1, 0, 104, 2681),
(1, 0, 105, 2681),
(1, 0, 106, 2681),
(1, 0, 107, 2681),
(1, 0, 108, 2681);

-- --------------------------------------------------------

--
-- Table structure for table `forum_post`
--

DROP TABLE IF EXISTS `forum_post`;
CREATE TABLE IF NOT EXISTS `forum_post` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  `text` varchar(255) NOT NULL,
  `createDate` date NOT NULL,
  `creator_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `creates_post` (`creator_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `isuser`
--

DROP TABLE IF EXISTS `isuser`;
CREATE TABLE IF NOT EXISTS `isuser` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nickname` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `createDate` date NOT NULL,
  `description` varchar(255) NOT NULL,
  `lastLogin` date NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=110 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `isuser`
--

INSERT INTO `isuser` (`id`, `nickname`, `password`, `createDate`, `description`, `lastLogin`) VALUES
(1, 'test123', 'test123', '2020-05-06', 'asdasd', '2020-05-19'),
(2, 'adminas2', 'adminas2', '2020-05-05', 'eks dee', '2020-05-05'),
(3, 'adminas2', 'adminas3', '2020-05-05', 'eks dee', '2020-05-05'),
(4, 'adminas2', 'adminas4', '2020-05-05', 'eks dee', '2020-05-05'),
(5, 'geimeris1', 'geimeris1', '2020-05-06', 'asdasd', '2020-05-28'),
(6, 'geimeris2', 'geimeris1', '2020-05-06', 'asdasd', '2020-05-28'),
(7, 'geimeris3', 'geimeris1', '2020-05-06', 'asdasd', '2020-05-28'),
(8, 'geimeris4', 'geimeris1', '2020-05-06', 'asdasd', '2020-05-28'),
(100, 'valdas100', 'valdas100', '2020-05-05', 'adasd', '2020-05-13'),
(101, 'valdas101', 'valdas100', '2020-05-05', 'adasd', '2020-05-13'),
(102, 'valdas102', 'valdas100', '2020-05-05', 'adasd', '2020-05-13'),
(103, 'valdas103', 'valdas100', '2020-05-05', 'adasd', '2020-05-13'),
(104, 'valdas104', 'valdas100', '2020-05-19', 'dasfsdf', '2020-05-12'),
(105, 'valdas105', 'valdas100', '2020-05-05', 'adasd', '2020-05-13'),
(106, 'valdas106', 'valdas100', '2020-05-05', 'adasd', '2020-05-13'),
(107, 'valdas107', 'valdas100', '2020-05-05', 'adasd', '2020-05-13'),
(108, 'valdas108', 'valdas100', '2020-05-05', 'adasd', '2020-05-13'),
(109, 'valdas109', 'valdas100', '2020-05-05', 'adasd', '2020-05-13');

-- --------------------------------------------------------

--
-- Table structure for table `isuser_isfriend`
--

DROP TABLE IF EXISTS `isuser_isfriend`;
CREATE TABLE IF NOT EXISTS `isuser_isfriend` (
  `user_id` int(11) NOT NULL,
  `friend_id` int(11) NOT NULL,
  PRIMARY KEY (`user_id`,`friend_id`),
  KEY `user_friend` (`friend_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `match_message`
--

DROP TABLE IF EXISTS `match_message`;
CREATE TABLE IF NOT EXISTS `match_message` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `text` varchar(255) NOT NULL,
  `csgomatch_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `belongs_to3` (`csgomatch_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `news_posts`
--

INSERT INTO `news_posts` (`id`, `title`, `text`, `createDate`, `fk_writtenBy`) VALUES
(1, 'Naujiena 1', '1506', '2020-04-23', 1),
(3, 'sdfasd', 'FalseTrueTrueTrue', '1991-10-06', 1),
(5, 'dsfsdf', 'sdfsdf', '2000-01-01', 1);

-- --------------------------------------------------------

--
-- Table structure for table `personalmessage`
--

DROP TABLE IF EXISTS `personalmessage`;
CREATE TABLE IF NOT EXISTS `personalmessage` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `text` varchar(255) NOT NULL,
  `sendDate` date NOT NULL,
  `senderID` int(11) NOT NULL,
  `recipientID` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `receives` (`recipientID`),
  KEY `sends` (`senderID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `player`
--

DROP TABLE IF EXISTS `player`;
CREATE TABLE IF NOT EXISTS `player` (
  `id` int(11) NOT NULL,
  `matchCount` int(11) NOT NULL,
  `winCount` int(11) NOT NULL,
  `rating` double NOT NULL,
  `inQueue` int(11) NOT NULL,
  `region` varchar(255) NOT NULL,
  `inQueuesince` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `player`
--

INSERT INTO `player` (`id`, `matchCount`, `winCount`, `rating`, `inQueue`, `region`, `inQueuesince`) VALUES
(5, 0, 0, 10, 0, 'Europe', '2020-05-06 00:00:00'),
(6, 1, 1, 50, 0, 'Europe', '2020-05-13 00:00:00'),
(7, 1, 1, 50, 0, 'Europe', '2020-05-13 00:00:00'),
(8, 1, 1, 50, 0, 'Europe', '2020-05-13 00:00:00'),
(100, 0, 0, 10, 0, 'Europe', '2020-05-14 00:00:00'),
(101, 0, 0, 10, 0, 'Europe', '2020-05-14 00:00:00'),
(102, 0, 0, 10, 0, 'Europe', '2020-05-15 00:00:00'),
(103, 0, 0, 10, 0, 'Europe', '2020-05-14 00:00:00'),
(104, 0, 0, 10, 0, 'Europe', '2020-05-14 00:00:00'),
(105, 0, 0, 10, 0, 'Europe', '2020-05-14 00:00:00'),
(106, 0, 0, 10, 0, 'Europe', '2020-05-14 00:00:00'),
(107, 0, 0, 10, 0, 'Europe', '2020-05-14 00:00:00'),
(108, 0, 0, 10, 0, 'Europe', '2020-05-14 00:00:00'),
(109, 0, 0, 10, 1, 'Europe', '2020-05-14 00:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `reply`
--

DROP TABLE IF EXISTS `reply`;
CREATE TABLE IF NOT EXISTS `reply` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `text` varchar(255) NOT NULL,
  `writeDate` date NOT NULL,
  `creator_id` int(11) NOT NULL,
  `forum_post_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `is_a_part_of2` (`forum_post_id`),
  KEY `writes` (`creator_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `request`
--

DROP TABLE IF EXISTS `request`;
CREATE TABLE IF NOT EXISTS `request` (
  `creationDate` date NOT NULL,
  `team_id` int(11) NOT NULL,
  `player_id` int(11) NOT NULL,
  PRIMARY KEY (`team_id`,`player_id`),
  KEY `creates2` (`player_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `team`
--

DROP TABLE IF EXISTS `team`;
CREATE TABLE IF NOT EXISTS `team` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `matchCount` int(11) NOT NULL,
  `winCount` int(11) NOT NULL,
  `isRemoved` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `team`
--

INSERT INTO `team` (`id`, `name`, `matchCount`, `winCount`, `isRemoved`) VALUES
(1, 'kjbjn', 0, 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `team_member`
--

DROP TABLE IF EXISTS `team_member`;
CREATE TABLE IF NOT EXISTS `team_member` (
  `isCaptain` int(11) NOT NULL,
  `player_id` int(11) NOT NULL,
  `team_id` int(11) NOT NULL,
  PRIMARY KEY (`player_id`,`team_id`),
  KEY `belongs_to_team` (`team_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `team_member`
--

INSERT INTO `team_member` (`isCaptain`, `player_id`, `team_id`) VALUES
(1, 5, 1);

-- --------------------------------------------------------

--
-- Table structure for table `team_tournament_participation`
--

DROP TABLE IF EXISTS `team_tournament_participation`;
CREATE TABLE IF NOT EXISTS `team_tournament_participation` (
  `tournament_id` int(11) NOT NULL,
  `team_id` int(11) NOT NULL,
  PRIMARY KEY (`tournament_id`,`team_id`),
  KEY `is_team` (`team_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `team_tournament_participation`
--

INSERT INTO `team_tournament_participation` (`tournament_id`, `team_id`) VALUES
(1, 1),
(2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `tournament`
--

DROP TABLE IF EXISTS `tournament`;
CREATE TABLE IF NOT EXISTS `tournament` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tournamentCreator` int(11) NOT NULL,
  `playerCount` int(11) NOT NULL,
  `startDate` datetime NOT NULL,
  `maxPlayerCount` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `creates` (`tournamentCreator`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tournament`
--

INSERT INTO `tournament` (`id`, `tournamentCreator`, `playerCount`, `startDate`, `maxPlayerCount`, `name`) VALUES
(1, 2, 156, '2020-05-29 00:00:00', 16, 'lknlnk'),
(2, 1, 10, '2020-05-23 10:00:00', 1, '0');

-- --------------------------------------------------------

--
-- Table structure for table `tournament_match`
--

DROP TABLE IF EXISTS `tournament_match`;
CREATE TABLE IF NOT EXISTS `tournament_match` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `startDate` date NOT NULL,
  `endDate` date NOT NULL,
  `matchNo` int(11) NOT NULL,
  `tournamentID` int(11) NOT NULL,
  `tournamentMatchTeamID` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `is_a_part_of` (`tournamentID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `tournament_match_team`
--

DROP TABLE IF EXISTS `tournament_match_team`;
CREATE TABLE IF NOT EXISTS `tournament_match_team` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `won` int(11) DEFAULT NULL,
  `tournamentmatch_id` int(11) NOT NULL,
  `team_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `is` (`team_id`),
  KEY `belongs to` (`tournamentmatch_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `administrators`
--
ALTER TABLE `administrators`
  ADD CONSTRAINT `admin_isuser` FOREIGN KEY (`id`) REFERENCES `isuser` (`id`);

--
-- Constraints for table `csgo_match_player`
--
ALTER TABLE `csgo_match_player`
  ADD CONSTRAINT `is_player` FOREIGN KEY (`player_id`) REFERENCES `player` (`id`),
  ADD CONSTRAINT `plays` FOREIGN KEY (`csgomatch_id`) REFERENCES `csgomatch` (`id`);

--
-- Constraints for table `forum_post`
--
ALTER TABLE `forum_post`
  ADD CONSTRAINT `creates_post` FOREIGN KEY (`creator_id`) REFERENCES `isuser` (`id`);

--
-- Constraints for table `isuser_isfriend`
--
ALTER TABLE `isuser_isfriend`
  ADD CONSTRAINT `user` FOREIGN KEY (`user_id`) REFERENCES `isuser` (`id`),
  ADD CONSTRAINT `user_friend` FOREIGN KEY (`friend_id`) REFERENCES `isuser` (`id`);

--
-- Constraints for table `match_message`
--
ALTER TABLE `match_message`
  ADD CONSTRAINT `belongs_to3` FOREIGN KEY (`csgomatch_id`) REFERENCES `csgomatch` (`id`);

--
-- Constraints for table `news_posts`
--
ALTER TABLE `news_posts`
  ADD CONSTRAINT `fkc_writtenBy` FOREIGN KEY (`fk_writtenBy`) REFERENCES `administrators` (`id`);

--
-- Constraints for table `personalmessage`
--
ALTER TABLE `personalmessage`
  ADD CONSTRAINT `receives` FOREIGN KEY (`recipientID`) REFERENCES `isuser` (`id`),
  ADD CONSTRAINT `sends` FOREIGN KEY (`senderID`) REFERENCES `isuser` (`id`);

--
-- Constraints for table `player`
--
ALTER TABLE `player`
  ADD CONSTRAINT `player_isuser` FOREIGN KEY (`id`) REFERENCES `isuser` (`id`);

--
-- Constraints for table `reply`
--
ALTER TABLE `reply`
  ADD CONSTRAINT `is_a_part_of2` FOREIGN KEY (`forum_post_id`) REFERENCES `forum_post` (`id`),
  ADD CONSTRAINT `writes` FOREIGN KEY (`creator_id`) REFERENCES `isuser` (`id`);

--
-- Constraints for table `request`
--
ALTER TABLE `request`
  ADD CONSTRAINT `belongs_to2` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`),
  ADD CONSTRAINT `creates2` FOREIGN KEY (`player_id`) REFERENCES `player` (`id`);

--
-- Constraints for table `team_member`
--
ALTER TABLE `team_member`
  ADD CONSTRAINT `belongs_to_team` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`),
  ADD CONSTRAINT `is_teammember` FOREIGN KEY (`player_id`) REFERENCES `player` (`id`);

--
-- Constraints for table `team_tournament_participation`
--
ALTER TABLE `team_tournament_participation`
  ADD CONSTRAINT `is_team` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`),
  ADD CONSTRAINT `plays_in` FOREIGN KEY (`tournament_id`) REFERENCES `tournament` (`id`);

--
-- Constraints for table `tournament`
--
ALTER TABLE `tournament`
  ADD CONSTRAINT `creates` FOREIGN KEY (`tournamentCreator`) REFERENCES `administrators` (`id`);

--
-- Constraints for table `tournament_match`
--
ALTER TABLE `tournament_match`
  ADD CONSTRAINT `is_a_part_of` FOREIGN KEY (`tournamentID`) REFERENCES `tournament` (`id`);

--
-- Constraints for table `tournament_match_team`
--
ALTER TABLE `tournament_match_team`
  ADD CONSTRAINT `belongs to` FOREIGN KEY (`tournamentmatch_id`) REFERENCES `tournament_match` (`id`),
  ADD CONSTRAINT `is` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
