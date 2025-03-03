# HikerTrader
A simple command line app that allows hikers to trade consumable items.

## Introduction
This project was an opportunity to learn the repository and service patterns used in .NET Core. Although the base program utilises an in-memory repository implementation, the use of these layers and their respective interfaces makes dependency injections simple for unit testing and the possible integration of an actual database.

## Installation
Git clone the repository, move into it and use `dotnet run` to launch the application. Requires that .NET Core SDK is installed and set up correctly.

## Usage
The application asks the user to input the data for 2 hikers, including the number of medicine, water and food in their inventories. If neither hiker is injured and both inventories are worth the same total amount, the inventories of the two hikers exchanged.

## Author
[Jaakko Junttila](https://github.com/kaulin)
