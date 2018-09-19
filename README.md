# C# HairSalon Application

#### By Devin Mounts, 07.13.2018

## Description

## A web application that allows employees at a hair salon to create and view stylist to client relationships.

## Splash
![Welcome Page](/cover.png)

## Setup

1. Download and install .NET 1.1.4
1. Clone the repo
1. Navigate into the clone directory
1. Run `dotnet build` from project directory and fix any build errors
1. Run `dotnet test` from the test directory to run the testing suite
1. Alter `devin_mounts` in Startup.cs to connect to the database of your choice.
1. Navigate to the project folder in the terminal and run `dotnet add package MySqlConnector` add `using MySql.Data.MySqlClient;` to namespace references. 
1. Follow instructions for Downloading, Configuring ports and Runing MAMP :https://www.learnhowtoprogram.com/c/database-basics-ee7c9fd3-fcd9-4fff-8b1d-5ff7bfcbf8f0/introducing-and-installing-mamp
1. Enter following command to open MySql connection in terminal : Applications/MAMP/Library/bin/mysql --host=localhost -uroot -proot
1. In MySql:
  *CREATE DATABASE IF NOT EXISTS `db_name` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
  *USE `db_name`;
  *CREATE TABLE `clients` (`id` int(11) NOT NULL,`name` varchar(255) NOT NULL,`stylist_id` int(11) NOT NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  *CREATE TABLE `specialties` (`id` int(11) NOT NULL,`description` varchar(255) NOT NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  *CREATE TABLE `stylists` (`id` int(11) NOT NULL,`name` varchar(255) NOT NULL,`details` varchar(255) NOT NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  *CREATE TABLE `stylists_specialties` (`id` int(11) NOT NULL,`stylist_id` int(11) NOT NULL,`specialty_id` int(11) NOT NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  *ALTER TABLE `clients`ADD PRIMARY KEY (`id`);
  *ALTER TABLE `specialties`ADD PRIMARY KEY (`id`);
  *ALTER TABLE `stylists`ADD PRIMARY KEY (`id`);
  *ALTER TABLE `stylists_specialties`ADD PRIMARY KEY (`id`);
  *ALTER TABLE `clients`MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;
  *ALTER TABLE `specialties`MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
  *ALTER TABLE `stylists`MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
  *ALTER TABLE `stylists_specialties`MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
1.Navigate to the phpMyAdmin link under tools tab.
1.Copy database under Operations tab, and name `db_name_test`.

## Usage

* Option available to set up your git pairs file at the root directory
* Option available to create a base C# ASP.NET project that includes:
  * Controllers directory with single HomeController file
  * Views directory with basic index view and Layout partial
  * Models directory with single class file
  * Database.cs file in Models directory
  * ModelTests directory with single test class file
  * ControllerTests directory with single test class file
  * csproj files with ASP.NET, testing packages, MySQL Connector, and 
  * Startup.cs for ASP.NET
  * Program.cs for ASP.NET
  * wwwroot directory for static assets
  * README.md outline
* Initializes git and git pair in project directory
* Installs packages with `dotnet restore` in test directory and project directory

##Specs

* Program allows user to see a list of all our stylists.
* Program allows user to select a stylist, see their details, and see a list of all clients that belong to that stylist.
* Program allows user to add new stylists to database.
* Program allows user to add new clients to a specific stylist.
* Program allows user to delete stylists (all and single).
* Program allows user to delete clients (all and single).
* Program allows user to view clients (all and single).
* Program allows user to edit a stylist.
* Program allows user to edit ALL of the information for a client.
* Program allows user to add a specialty and view all specialties that have been added.
* Program allows user to add a specialty to a stylist.
* Program allows user to click on a specialty and see all of the stylists that have that specialty.
* Program allows user to see the stylist's specialties on the stylist's details page.
* Program allows user to be able to add a stylist to a specialty.

## Contribution Requirements

1. Clone the repo
1. Make a new branch
1. Commit and push your changes
1. Create a PR

## Technologies Used

* .NET 1.1.4
* Bash
* Git
* MAMP
* PHPMyAdmin

## Links

* [Github Repo] (https://github.com/devinmounts/Categories.Solution.git)

## License

This software is licensed under the MIT license.

Copyright (c) 2018 **Devin Mounts**