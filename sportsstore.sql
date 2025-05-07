-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 07, 2025 at 01:03 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sportsstore`
--

-- --------------------------------------------------------

--
-- Table structure for table `cartline`
--

CREATE TABLE `cartline` (
  `CartLineID` int(11) NOT NULL,
  `OrderID` int(11) DEFAULT NULL,
  `ProductID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `cartline`
--

INSERT INTO `cartline` (`CartLineID`, `OrderID`, `ProductID`, `Quantity`) VALUES
(1, 3, 6, 1),
(2, 3, 3, 1),
(3, 4, 7, 1),
(4, 5, 6, 1);

-- --------------------------------------------------------

--
-- Table structure for table `cartlines`
--

CREATE TABLE `cartlines` (
  `CartLineID` int(11) NOT NULL,
  `ProductID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `OrderID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `OrderID` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Line1` longtext NOT NULL,
  `Line2` longtext NOT NULL,
  `City` longtext NOT NULL,
  `State` longtext NOT NULL,
  `Zip` longtext NOT NULL,
  `Country` longtext NOT NULL,
  `GiftWrap` tinyint(1) NOT NULL,
  `Shipped` tinyint(1) NOT NULL,
  `Province` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`OrderID`, `Name`, `Line1`, `Line2`, `City`, `State`, `Zip`, `Country`, `GiftWrap`, `Shipped`, `Province`) VALUES
(3, 'Test Tosterone', '1st North Shepparton', '424', 'Shepparton', '', '3630', 'Australia', 1, 0, 'Victoria'),
(4, '4242', '1st North Shepparton', 'ewqerwqr', 'Shepparton', '', '3630', 'Philippines', 0, 0, 'Victoria'),
(5, '4242', 'wqewqewq', 'eqwewq', 'ewqewq', '', 'wqewqe', 'Spain', 0, 0, 'wqewq');

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `ProductID` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(500) NOT NULL,
  `Price` decimal(8,2) NOT NULL,
  `Category` varchar(50) NOT NULL,
  `ImageUrl` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`ProductID`, `Name`, `Description`, `Price`, `Category`, `ImageUrl`) VALUES
(2, 'Soccer Ball', 'FIFA-approved size and weight', 1950.50, 'Soccer', 'img/soccer.jpg'),
(3, 'Running Shoes', 'Lightweight running shoes', 4250.00, 'Running', 'img/running.avif'),
(4, 'Basketball', 'NBA-sized basketball', 2305.00, 'Basketball', 'img/basketball.png'),
(5, 'Tennis Racket', 'Professional tennis racket', 8500.00, 'Tennis', 'img/tennis.jpg'),
(6, 'Baseball Glove', 'Leather baseball glove', 1650.95, 'Baseball', 'img/glove.jpg'),
(7, 'Swimming Goggles', 'Anti-fog swimming goggles', 2888.00, 'Swimming', 'img/googles.jpg'),
(8, 'Yoga Mat', 'Non-slip yoga mat', 930.50, 'Fitness', 'img/yogamat.jpg'),
(9, 'Dumbbells', 'Set of 2 dumbbells (5kg each)', 745.50, 'Fitness', 'img/dumbbell.jpg');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cartline`
--
ALTER TABLE `cartline`
  ADD PRIMARY KEY (`CartLineID`),
  ADD KEY `FK_CartLine_Orders` (`OrderID`);

--
-- Indexes for table `cartlines`
--
ALTER TABLE `cartlines`
  ADD PRIMARY KEY (`CartLineID`),
  ADD KEY `IX_CartLines_OrderID` (`OrderID`),
  ADD KEY `IX_CartLines_ProductID` (`ProductID`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`OrderID`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`ProductID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cartline`
--
ALTER TABLE `cartline`
  MODIFY `CartLineID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `cartlines`
--
ALTER TABLE `cartlines`
  MODIFY `CartLineID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `OrderID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `ProductID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `cartline`
--
ALTER TABLE `cartline`
  ADD CONSTRAINT `FK_CartLine_Orders` FOREIGN KEY (`OrderID`) REFERENCES `orders` (`OrderID`) ON DELETE CASCADE;

--
-- Constraints for table `cartlines`
--
ALTER TABLE `cartlines`
  ADD CONSTRAINT `FK_CartLines_Orders_OrderID` FOREIGN KEY (`OrderID`) REFERENCES `orders` (`OrderID`),
  ADD CONSTRAINT `FK_CartLines_Products_ProductID` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
