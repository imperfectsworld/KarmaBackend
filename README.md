# **[WeeGive](https://green-sand-0fc2ba30f.5.azurestaticapps.net/)**
<p align="center">
  <img src="https://github.com/user-attachments/assets/22a511a2-5f9b-4ff3-a7eb-0c88dd350f41" alt="weegive_720" width="400"/>
  <img src="https://github.com/user-attachments/assets/27376fa9-7984-4849-abbb-4f2d751b43ba" alt="screenshot_2024-10-24_163812_720" width="400"/>
</p>
<p align="center">
 <img src="https://github.com/user-attachments/assets/f63e2327-1a1d-46c5-bfe7-9ecb02b16ebc" alt="screenshot_2024-10-24_163827_720" width="400"/>
 <img src="https://github.com/user-attachments/assets/514ef92e-a020-4e15-9a49-f79b9f95e5d0" alt="screenshot_2024-10-24_163944_720" width="400"/>
</p>

WeeGive is a platform that allows users to share gently used baby goods, fostering community engagement and reducing waste. Users can post items, inquire about available goods, and communicate through a chat feature to coordinate pickups and deliveries.

## Table of Contents

- [Features](#features)
- [Frontend](#frontend)
- [Backend](#backend)
- [Technologies Used](#technologies-used)
- [Deployment](#deployment)

## Features

- Post baby goods with images, descriptions, and location data.
- Filter items by category and condition.
- Google authentication for user login.
- Real-time chat for users to communicate about items.
- Location services to view item pickup locations via Google Maps.

## Frontend

The frontend is built using Angular and Bootstrap, with the following core features:

- **Google Sign-In**: Integrates Google login for user authentication.
- **Item Management**: Allows users to add, view, and delete baby goods, with filtering and sorting functionality.
- **Image Upload**: Supports image uploads via Imgur API.
- **Chat Functionality**: Real-time chat using SignalR for communication between users.

### Frontend File Structure

- **Main Entry Point**: `app.component.ts`
- **Routing**: Configured using Routes, including paths for Home, Chat, Item, User, etc.
- **Service Integration**: The frontend communicates with the backend API to handle items, locations, and chat through services like `BackendService` and `GetLocationService`.

## Backend

The backend is built with C# and .NET, utilizing Entity Framework for database management. It serves as the API for handling users, items, and chat features.

### Backend Features

- **Database Integration**: Uses SQL Server for persistent storage of items, locations, and users.
- **Image Handling**: Uploads and retrieves images via the Imgur API.
- **Geolocation**: Retrieves geolocation data using Google Maps API.
- **Real-time Chat**: Implements SignalR for real-time chat between users.

### Key Services

- **ImgurService**: Handles image uploads and retrievals from the Imgur API.
- **LocationService**: Manages item location data using Google Maps.
- **ChatService**: Facilitates real-time chat between users.

## Technologies Used

- **Frontend**: Angular, Bootstrap, SignalR, Google Maps API, Imgur API
- **Backend**: .NET, C#, Entity Framework, SignalR, SQL Server
- **Database**: SQL Server
- **Authentication**: Google OAuth

## Deployment

WeeGive is deployed using Azure services:

- **Frontend**: Azure Static Web App
- **Backend**: Azure App Service
- **Database**: Azure SQL Server
