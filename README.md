# SportsHub
Sports Hub is an application to make more easier to do sport. All you need it is sing up or log in and take pleasure of using it.
# Requirements
**UML Diagram**:
![Sports Hub UseCases - 2023](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/26cf3c0e-d1c9-4c51-8b60-06de33557e93)
**User**:
- Choose kind of sport.
- See news about sport.
- See places to do sport.
- See recomended training programs.
- See profile information.
- See recomemded nutrition.
- Choose active training program.
- See sport shops.
- Log out.

**Admin**:
- Extends User functuonality.
- Add/Edit sport shops.
- Add/Edit nutritions.
- Add/Edit sport news.
- Add/Edit training programs.

**Guest**:
Can see only the home, login and sign up pages.

## Other Requirements:
- Web-site shall be easy to use for all users and all ages.
- Web-site should be responsive and should work correctly on all devices (mobile, tablet, desktop).
- Web-site should be fast and load pages with minimal delay and use lazy loading where it is possible.
- App should be secure and use HTTPS protocol.
- App should be scalable.

# Architecture
![SportsHub_RecommendationSysteM_Architecture_2023v drawio](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/9c86da45-6dd4-407f-adb8-ea28032bbcd1)
App is based on 3tier architecture. It consists of 3 layers: Presentation, Business and Data Access.
- Presentation tier is developed using React framework.
- Business tier is developed using ASP.NET Core framework. It uses Entity Framework Core to work with database tier.
- Data Access tier is running on Microsoft SQL Server.

It also has additional module - **Recommendation System**. This separate module is responsible for providing recommendations for users. It is based on Machine Learning algorithms. It is independent from other layers and can be easily replaced with another module. Recommendation Service is developed using .NET Core framework. It uses Model-Based Recommendation System to provide recommendations.\
*More information here [Building a Recommendation Engine in C#](https://www.codeproject.com/Articles/1232150/Building-a-Recommendation-Engine-in-Csharp).*

Also application uses Azure Blob Storage to store data and Google API to provide maps.

# Concurency patterns usage
Application API tier uses **Asynchronous Request Processing** pattern. It allows to process requests in parallel and increase performance of the application. To implement this pattern we use **async/await** keywords and **Task** class.
*Example from the NewsController:*
```csharp
[HttpGet("all")]
public async Task<IActionResult> GetAll()
{
    try
    {
        var news = await _newsService.GetAllAsync();

        return Ok(news);
    }
    catch (Exception e)
    {
        _loggerService.LogError(e.Message);
    }

    return BadRequest();
}
```

# Security Model
## Threat Modeling Report
![Знімок екрана 2023-12-18 221703](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/a136316a-03b6-40af-ad52-dd6490a2e316)
### Interaction: API Request/Response
![Знімок екрана 2023-12-18 222752](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/75d23ab1-21fb-4d49-8827-b744c1e3e196)

**1. An adversary may block access to the application or API hosted on JustEatIt REST API through a denial of service attack**

| State                  | Needs more investigation    |
|------------------------|-----------------------------|
| Priority               | High                        |
| Category               | Denial of Service           |
| Description            | An adversary may block access to the application or API hosted on SportsHub API through a denial of service attack |
| Possible Mitigation(s) | Network level denial of service mitigations are automatically enabled as part of the Azure platform (Basic Azure DDoS Protection). Implement application level throttling (e.g. per-user, per-session, per-API) to maintain service availability and protect against DoS attacks. Leverage Azure API Management for managing and protecting APIs. |

**2. An adversary may gain long term persistent access to related resources through the compromise of an application identity**

| State                  | Needs more investigation    |
|------------------------|-----------------------------|
| Priority               | High                        |
| Category               | Elevation of Privileges     |
| Description            | An adversary may gain long term persistent access to related resources through the compromise of an application identity |
| Possible Mitigation(s) | Store secrets in secret storage solutions where possible, and rotate secrets on a regular cadence. Use Managed Service Identity to create a managed app identity on Azure Active Directory and use it to access AAD-protected resources. |

**3. An adversary may perform action(s) on behalf of another user due to lack of controls against cross domain requests**

| State                  | Needs more investigation    |
|------------------------|-----------------------------|
| Priority               | High                        |
| Category               | Elevation of Privileges     |
| Description            | An adversary may perform action(s) on behalf of another user due to lack of controls against cross-domain requests |
| Possible Mitigation(s) | Ensure that only trusted origins are allowed if CORS is being used. |

### Interaction: DB Query
![Знімок екрана 2023-12-18 223653](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/edd1e3be-1e40-44c9-aff5-7ec1715c17e8)

**4. An adversary may perform action(s) on behalf of another user due to lack of controls against cross domain requests**

| State                  | Needs more investigation    |
|------------------------|-----------------------------|
| Priority               | High                        |
| Category               | Elevation of Privileges     |
| Description            | An adversary can gain long-term, persistent access to a Azure SQL instance through the compromise of local user account password(s). |
| Possible Mitigation(s) | It is recommended to rotate user account passwords (e.g. those used in connection strings) regularly, in accordance with your organization's policies. Store secrets in a secret storage solution (e.g. Azure Key Vault). |

### Interaction: HTTPS Request
![Знімок екрана 2023-12-18 225159](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/044dd4ce-8b8d-4fde-b55e-297813d55b70)

**5. An adversary may spoof an Azure administrator and gain access to Azure subscription portal**

| State                  | Needs more investigation    |
|------------------------|-----------------------------|
| Priority               | High                        |
| Category               | Spoofing                    |
| Description            | An adversary may spoof an Azure administrator and gain access to the Azure subscription portal if the administrator's credentials are compromised. |
| Possible Mitigation(s) | Enable fine-grained access management to Azure Subscription using RBAC. Enable Azure Multi-Factor Authentication for Azure Administrators. |

**6. Attacker can steal user session cookies due to insecure cookie attributes**

| State                  | Needs more investigation    |
|------------------------|-----------------------------|
| Priority               | High                        |
| Category               | Information Disclosure      |
| Description            | Attacker can steal user session cookies due to insecure cookie attributes. |
| Possible Mitigation(s) | Applications available over HTTPS must use secure cookies. All HTTP-based applications should specify HTTP only for cookie definition. |



# Data model
![Sportshub_data_model](https://github.com/oleksandrmanetskyi/SportsHub/assets/56358923/a2f07554-2374-4d99-b4b7-eba8bacc1df0)

## Logical entities` models
- Users.
- Sport kinds.
- Training programs.
- Nutritions.
- Shops.
- News.
- Recommendations.

# Resiliency model
- Interaction: User - SPA Application

Interaction Description:
Represents all user interactions with the Single Page Application (SPA) on the website.
Includes activities like browsing, submitting forms, etc.
Resilience Measures:
Implement client-side error handling to gracefully handle user input errors.
Use client-side caching for improved performance during intermittent network issues.

- Interaction: C# Application - Database (Primary)

Interaction Description:
Represents the primary database connection used by the C# application for data storage and retrieval.
Resilience Measures:
Implement connection pooling to efficiently manage and reuse database connections.
Utilize transaction management to ensure data consistency during complex operations.

- Interaction: C# Application - External Services

Interaction Description:
C# application interacts with external services for additional functionalities.
Resilience Measures:
Implement timeout mechanisms for external service calls to prevent blocking the application.
Use asynchronous programming to maintain responsiveness during external service delays.

# Analytics model
**Potential functional metrics that can be collected from the application**
- Articles` views.
- Nutritions` rate.
- Training program rate/views.
- Shops` rate.
- Sport category popularity.
- Recommendations` rate
- User`s information that would not reveal their identity(age, sex, weight).
- Number of registered users.
- Training program`s active users.


# Deployment model
<img width="704" alt="Screenshot 2023-12-19 at 00 18 06" src="https://github.com/oleksandrmanetskyi/SportsHub/assets/56626861/fe1a6d9b-102a-42bd-870f-3f582efa9cb9">

- React WebApp - Azure App Service
- API - Azure App Service / Azure Functions
- MSSQL Server - Azure SQL Database
- Service for Recommendations - Azure Container Instances

Additinal services:
- Azure Blob Storage
- Google Maps API

# Monitoring
- HTTP Response Codes

Measurement Unit: Count (Number), Acceptable Range: 0, Monitoring Tool: AWS Elastic Beanstalk, Criticality Level: High
Track the occurrences of different HTTP response codes (e.g., 4xx, 5xx). A sudden increase in error responses may indicate issues with your application or server.
- Client-Side Errors

Measurement Unit: Count (Number), Acceptable Range: 0, Monitoring Tool: Real User Monitoring (RUM) tools or custom error tracking solutions (CloudWatch RUM), Criticality Level: High
Track the number of client-side errors occurring in the application. This includes JavaScript errors that may impact the user experience.
- User Interactions
 
Measurement Unit: Count (Number), Acceptable Range: Depends on the nature of the application, Monitoring Tool: Custom event tracking or analytics tools (CloudWatch RUM), Criticality Level: Medium
Monitor user interactions such as clicks, form submissions, and other meaningful events within your React application.
- Database Query Performance

Measurement Unit: Milliseconds (ms), Acceptable Range: Varies based on the complexity of queries, Monitoring Tool: SQL Server Profiler or Azure SQL Analytics, Criticality Level: High
Monitor the performance of SQL queries executed by your application. Identify slow queries and optimize their execution plans.
- Transaction Response Time
 
Measurement Unit: Milliseconds (ms), Acceptable Range: Varies based on transaction complexity, Monitoring Tool: SQL Server Profiler or Azure SQL Analytics, Criticality Level: High
Measure the time it takes to complete database transactions. This includes INSERT, UPDATE, DELETE, and SELECT operations.
- API Response Time

Measurement Unit: Milliseconds (ms), Acceptable Range: Varies based on the complexity of operations, Monitoring Tool: Application Performance Monitoring (APM) tools or custom logging, Criticality Level: High
Measure the time it takes for the API layer to respond to incoming requests. Optimize API endpoints for faster response times.
- Error Rates in Application Layer

Measurement Unit: Percentage (%), Acceptable Range: 0% - 5%, Monitoring Tool: APM tools or custom error tracking, Criticality Level: High
Track the percentage of failed requests or exceptions in the application layer. Identify and address errors promptly.
- Database Connection Pooling

Measurement Unit: Count (Number), Acceptable Range: Varies based on application traffic, Monitoring Tool: SQL Server Management Studio or custom logging, Criticality Level: Medium
Monitor the number of open and available connections in the database connection pool. Ensure efficient use of connections to prevent connection-related issues.
- App Availability

Measurement Unit: Milliseconds (ms), Acceptable Range: 0ms (Continuous Availability), Monitoring Tool: Amazon CloudWatch or Custom Uptime Monitoring System, Criticality Level: High
Measure the application's availability by tracking response times. Achieve continuous availability with minimal downtime.
- CPU Usage

Measurement Unit: Percentage (%), Acceptable Range: 0% - 100%, Monitoring Tool: Amazon CloudWatch or System-Level Monitoring Tools, Criticality Level: High
Monitor the CPU usage to ensure it remains within acceptable limits. High CPU usage can lead to performance degradation or even system instability.

# Team members
## Product Owner & Technical Lead - Maksym Lanchevych
Managing the project, fills and prioritize the product backlog, plans new features.
### Responsibilities
- Prioritize and maintain the product backlog.
- Define features, user stories, and acceptance criteria.
- Plan and prioritize releases.
- Provide technical leadership and guidance to the team.
- Conduct code reviews and provide constructive feedback.

## Full-Stack Developer - Oleksandr Manetskyi
Develop backend and frontend parts.
### Responsibilities
- Develop frontend and backend parts
- Testing and debugging code to identify and fix issues.
- Design database schemas and optimize queries.
- Conduct code reviews and provide constructive feedback.
- Update documentation.

## Quality Assurance Engineer - Volodymyr Derkach
Ensuring the quality of product.
### Responsibilities
- Develop and execute test plans, test cases.
- Report defects.
- Verify tasks based on acceptance criteria.
- Update documentation.

## DevOps Engineer - Andrii Klochovskyi
Managing deployment, automation processes.
### Responsibilities
- Automating and optimizing the deployment, configuration, and management of infrastructure and software systems.
- Implementing continuous integration and continuous deployment (CI/CD) pipelines.
- Ensuring security, compliance, and reliability of infrastructure.
- Update documentation.

# Project plan
## Gantt chart
![Знімок екрана 2024-03-16 235311](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/4201121e-cdf3-4c7d-9f24-83cdc59cd69c)

[Gantt.pdf](https://github.com/oleksandrmanetskyi/SportsHub/files/14625346/Gantt.pdf)

## Gantt chart with dependencies
![Знімок екрана 2024-03-16 235631](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/2314865b-0c14-4f0c-bc86-b0c8743d9e97)

# User stories
1. As a user, I want to be able to choose the type of sport I'm interested in, so I can view relevant information and updates.
2. As a user, I want to read news articles about my favorite sports to stay updated on current events.
3. As a user, I want to be able to comment news articles.
4. As a user, I want to discover places where I can participate in sports activities, including gyms, fields, courts, etc.
5. As a user, I want to explore recommended training programs tailored to my chosen sport to improve my skills and fitness level.
6. As a user, I want to view my profile information, including personal details, preferences, and activity history.
7. As a user, I want to have an ability to edit profile information.
8. As a user, I want to set my profile picture, update and delete it.
9. As a user, I want to confirm my email address by using a code from verification email sended by SportsHub platoform.
10. As a user, I want to access recommended nutrition plans designed to complement my sports training and performance goals.
11. As a user, I want to select an active training program from the available options to follow and track my progress.
12. As a user, I want to browse sport shops to discover and purchase equipment, apparel, and accessories.
13. As a user, I want the ability to log out of my account.
14. As a user, I want to filter news articles by date, popularity, or topic to customize my news feed.
15. As a user, I want to receive personalized recommendations for training programs and nutrition based on my profile data and preferences.
16. As a user, I want to rate and review sport shops to help other users.
17. As a user, I want to customize my profile settings, such as notification preferences, privacy settings, and account details.
18. As a user, I want to access customer support or help resources for assistance with any issues.
19. As a user, I want to delete my account and wipe all my data.
20. As a user, I want to link my account with Google Account, Apple ID, Facebook account.
21. As a user, I want to share my training progress and achievements with my social media networks directly from SportsHub.
22. As a user, I want to receive personalized coaching tips and feedback based on my performance and training history.
23. As a user, I want to earn virtual badges, trophies, or rewards for reaching milestones and completing challenges.
24. As an admin, I want to manage user accounts and permissions.
25. As an admin, I want to add, edit, or remove sport shops to keep the directory updated and accurate.
26. As an admin, I want to create, modify, or delete nutrition plans.
27. As an admin, I want to publish, update, or remove sport news articles.
28. As an admin, I want to develop and maintain training programs, including adding, editing, or removing programs as needed.
29. As an admin, I want to analyze user engagement metrics and feedback.
30. As an admin, I want to schedule and automate content publishing.
31. As an admin, I want to use analytics tools to track user behavior, trends, and preferences.
32. As an admin, I want to conduct regular backups and maintenance tasks to prevent data loss.
33. As an admin, I want to have an ability to block other users account.
34. As an admin, I want to have an ability to delete other users account.
35. As a guest, I want to register on the platform using email and password.
36. As a guest, I want to register on the platoform using Google Account, Apple ID, Facebook account.
37. As a guest, I want to login to the platform.
38. As a guest, I want to explore featured content or highlights on the home page.
39. As a guest, I want to read reviews or ratings from other users.
40. As a guest, I want to see links to SportsHub social medias.

# UI Requirements 
## Wireframes
### Login
![Login page](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/76856235-54e1-4337-977c-3df0e3ce36b5)

### Sign up
![SignUp page](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/a9c77808-fb5b-465a-8d4d-2e1eb65674cd)

### My profile
![My profile page](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/18d7e254-b3f9-477f-95b6-6cff61398f7a)

### Welcome
![Welcome page](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/d917fc4a-3bf5-4462-945a-b448c8651688)

### Nutritions
![Nutritions page](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/624e09e8-6d5a-45bd-916b-7edc978de70d)

### Nutrition
![Nutrition page(inc My)](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/0c64b1dc-7256-4b42-8229-357de8e53ea2)

### Places
![Places page](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/c20cfc9f-ac00-4772-b2e3-f42998616730)

### Training programs
![Training programs page](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/faf661a6-9729-481b-b143-9b08bec429ac)

### Training program
![Training program page(inc My)](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/def01ea0-0d07-410b-8b56-7440d9332aa8)
