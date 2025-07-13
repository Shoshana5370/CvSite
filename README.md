# GitHub Portfolio API

A .NET Core Web API application that connects to a developer's GitHub account and displays their public portfolio in a structured and customizable way.

---

## ✨ Project Overview

This application fetches data from your GitHub account and displays information about your repositories. It also supports public repository search.

### Key Features:

- Connects to a personal GitHub account
- Displays personal repositories with:
  - Programming languages used
  - Date of last commit
  - Number of stars ⭐
  - Number of pull requests
  - Link to repository
- Public GitHub search:
  - By repository name
  - By language
  - By GitHub username

---

## 🧱 Project Structure

- **GitHubPortfolio.Service** – A Class Library project that handles communication with GitHub via Octokit.
- **GitHubPortfolio.Api** – The Web API project that exposes the endpoints.

---

## 🛠 Technologies Used

- ASP.NET Core 6 Web API
- [Octokit.NET](https://octokitnet.readthedocs.io/en/latest/)
- In-Memory Caching
- Dependency Injection
- Options Pattern
- [Scrutor](https://andrewlock.net/adding-decorated-classes-to-the-asp.net-core-di-container-using-scrutor/) (for decorator pattern)

---

## 🔐 Configuration & Security

### Create a GitHub Personal Access Token

To access private account data, create a personal access token on GitHub.  
Guide: [Creating a personal access token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token)

### Store token securely using `secrets.json` (during development):

```bash
dotnet user-secrets init
dotnet user-secrets set "GitHub:Username" "your-username"
dotnet user-secrets set "GitHub:Token" "your-token"
