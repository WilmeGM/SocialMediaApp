# 🌐 Social Media App – ASP.NET Core C#

A full-featured social media web application built using **ASP.NET Core C#**, designed to demonstrate clean architecture, secure authentication using ASP.NET Core Identity, and a set of modern social features. This project is ideal for developers exploring scalable, user-centric backend systems.

## 🚀 Features

- 🔐 **Authentication & Authorization:**  
  Handles login, registration, password reset, and email confirmation using **ASP.NET Core Identity**.

- 🏡 **News Feed:**  
  A personalized homepage where users can view and interact with content.

- 🤝 **Friendship System:**  
  Send, accept, and manage friend requests with clean ViewModel mappings.

- 👤 **Profile Management:**  
  Update and display personal user details including profile pictures.

- 📧 **Email Notifications:**  
  Account activation and password recovery via integrated email service.

- 🛡️ **Security First:**  
  Enforces best practices via Identity's robust password policies and account validation flows.

## 🎯 Purpose

This project aims to:
- Showcase how to integrate **ASP.NET Core Identity** for real-world authentication flows.
- Provide a modular, extensible template for social platforms.
- Promote clean code practices and testable service abstractions.

## 🧱 Tech Stack

| Layer          | Tech Stack                          |
|----------------|-------------------------------------|
| Backend        | ASP.NET Core C#                     |
| Authentication | ASP.NET Core Identity               |
| Database       | Entity Framework Core + SQL Server  |
| Mail Service   | SMTP integration via EmailService   |
| Architecture   | Onion Architecture                  |

## 📂 Getting Started

1. Clone the repository  
   `git clone https://github.com/WilmeGM/SocialMediaApp.git`

2. Update `appsettings.json` with your DB and SMTP config.

3. Apply database migrations  
   `dotnet ef database update`

4. Run the application  
   `dotnet run`

## 📄 License

This project is licensed under the MIT License – use it, fork it, and feel free to contribute! 🛠️
