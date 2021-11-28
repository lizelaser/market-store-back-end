# Market Store
> Free and responsive Ecommerce containing organic and ancestral products, shopping cart and checkout process. Free for personal and commercial use.

## Table of Contents
* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Screenshots](#screenshots)
* [Setup](#setup)
* [Usage](#usage)
* [Project Status](#project-status)
* [Room for Improvement](#room-for-improvement)
* [Contact](#contact)
* [License](#license)

## General Information

The following project was developed based on market research to satisfy the need to obtain organic and healthy staple foods without leaving home in the population of Ayacucho, Peru.

This research was executed with the [following survey](https://docs.google.com/forms/d/e/1FAIpQLSecQ-08M_RF1_4a8P1s0EPHft-T_plLXU_ymXtlFqfemmfdbw/viewform?fbclid=IwAR0Cloj6X2HDXETP4DTBOW6HfYmfKsH5LbyA1vYFx9koB_znbqA2ntLv0y8) using Google Forms. A total of 14 participants responded, the results of which are summarized in the following images:

<img alt="Survey question" src="./images/research/1.png" width="100%" />
<img alt="Survey question" src="./images/research/2.png" width="100%" />
<img alt="Survey question" src="./images/research/3.png" width="100%" />
<img alt="Survey question" src="./images/research/4.png" width="100%" />
<img alt="Survey question" src="./images/research/5.png" width="100%" />
<img alt="Survey question" src="./images/research/6.png" width="100%" />
<img alt="Survey question" src="./images/research/7.png" width="100%" />
<img alt="Survey question" src="./images/research/8.png" width="100%" />
<img alt="Survey question" src="./images/research/9.png" width="100%" />
<img alt="Survey question" src="./images/research/10.png" width="100%" />
<img alt="Survey question" src="./images/research/11.png" width="100%" />
<img alt="Survey question" src="./images/research/12.png" width="100%" />
<img alt="Survey question" src="./images/research/13.png" width="100%" />

## Technologies Used

### Backend Stack
- Net core
- ASP.Net core
- Entity Framework
- SQL Server

### Frontend Stack
- Vue.js 2
- Vue Router
- Vuex
- Vuetify
- Eslint


## Features
- Product search
- Shopping cart
- Checkout proccess
- Product list with filters and ordering
- Credit card and Paypal payment methods
- Account creation and management
- User directions with geolocalization
- Order history
- Wish list
- Favorite products
- Category list
- Product packs by weeks

## Screenshots

### **Home**
![Screenshoot](./images/screenshots/1.jpg)
### **Cart**
![Screenshoot](./images/screenshots/10.jpg)
### **Login**
![Screenshoot](./images/screenshots/4.jpg)
### **Products**
![Screenshoot](./images/screenshots/7.jpg)


## Setup

### Requirements
* You must have [Node.js](https://nodejs.org/), wich is tipically bundled with the [npm package manager](https://www.npmjs.com/)
* You also must have [GIT](https://git-scm.com/) if you want to contribute to the project.

### Get the repository locally
First of all, clone the repository:

```bash
git clone git@github.com:lizelaser/market-store-back-end.git
cd <path_to_project>
```
### Install dependencies
Then you need to install the dependencies for the project:
```bash
dotnet restore
```

## Usage

### Build and launch for development
Start a development server and launch the project on localhost:51302 (note that the development build is not optimized):

```bash
dotnet run
```

### Build and minifies for production
Build the project in production mode:

```bash
dotnet pack
```

## Project Status

[![Project Status: Inactive](https://www.repostatus.org/badges/latest/inactive.svg)](https://www.repostatus.org/#active)

## Room for Improvement

- Update Vue.js version to 3
- Add more payment methods
- Add delivery tracking option
- Add smart product suggestions

To do:
- Publish a live demo in a website
- Improve the search experience

## Contact
Created by [@lizelaser](https://github.com/lizelaser) - feel free to contact me!

## License 
This project is open source and available under the [MIT](https://mit-license.org/) license.
