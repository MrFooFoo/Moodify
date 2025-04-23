# Moodify - Mood-Based Playlist Recommendation System

Moodify is a web application that allows users to select their mood or upload a selfie to receive a mood-based music playlist recommendation. It uses a mood detection API to analyze the user's mood and fetches personalized music suggestions from a playlist API. The project is built using React, .NET Core, Kafka, Docker, and Kubernetes.

## Features

- **Mood Detection**: Users can select a mood (e.g., Happy, Sad, Chill, Energetic) and receive a music playlist suited to their mood.
- **Selfie Upload**: Users can upload a selfie, and the app will automatically detect the mood and recommend a playlist based on the detected mood.
- **Dynamic Playlist**: Once a mood is detected or selected, the app fetches a personalized playlist from an API.

## Tech Stack

- **Frontend**: ReactJS (with hooks for state management)
- **Backend**: .NET Core (API for mood detection and playlist fetching)
- **Messaging**: Kafka (for communication between services)
- **Containerization**: Docker & Kubernetes (for deployment)

## Getting Started

To run the project locally, follow these steps:

### Prerequisites

- **Node.js**: Make sure Node.js is installed on your machine. You can download it from [here](https://nodejs.org/).
- **.NET SDK**: Ensure you have .NET SDK installed. You can download it from [here](https://dotnet.microsoft.com/download).
- **Docker**: Install Docker if you plan to run the services in containers. [Download Docker](https://www.docker.com/products/docker-desktop).

### 1. Clone the repository

Clone the repository to your local machine:

```bash
git clone https://github.com/your-username/moodify.git
cd moodify
