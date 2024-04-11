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

# Test Cases
## User Test Cases

### Test Case 1: Successful User Registration
**Description**: Test for User Registration with valid inputs.
**Pre-Condition**: The user is on the registration page.
1. Enter a valid name in the name field.
2. Enter a valid email address in the email field.
3. Enter a valid password in the password field.
4. Enter the same password in the confirm password field.
5. Enter a valid surname in the surname field.
6. Enter a valid location in the location field.
7. Choose kind of sport in "Your kind of sport" field.
8. Click the 'Sign up' button.

**Expected Result**:
1. The user should be successfully registered.
2. A confirmation message should be displayed.
3. The user should be redirected to the "Home" page.
4. The user should be able to log in with the registered credentials.


### Test Case 2: Failed User Registration
**Description**: Test for User Registration with valid inputs.
**Pre-Condition**: The user is on the registration page.
1. Enter an invalid name in the name field.
2. Enter an invalid email address in the email field.
3. Enter an invalid password in the password field.
4. Enter a different password in the confirm password field.
5. Enter an invalid surname in the surname field.
6. Enter an invalid location in the location field.
7. Don't choose kind of sport in "Your kind of sport" field.
8. Click the 'Sign up' button.

**Expected Result**:
1. The user should not be registered.
2. An error validation messages should be displayed for each invalid field.
3. The user should not be able to log in with the entered credentials.

### Test Case 3: Viewing Sports News
**Description**: Test for viewing sports news.
**Pre-Condition**: The user is logged in and on the homepage.
1. Click on the 'News' tab in Navigation Menu.
2. View the list of sports news.
3. Click on a news article to view it.

**Expected Result**:
1. The user should be able to view the list of sports news.
2. The user should be able to click on a news article and be redirected to it's page to view it.

### Test Case 4: Viewing Sports Places
**Description**: Test for viewing sports places.
**Pre-Condition**: The user is logged in and on the homepage.
1. Click on the 'Places' tab in Navigation Menu.
2. View the google map with places to do specified in user profile kind of sport in the location specified in user profile.
3. View buttons "Next place" and "Change location".
4. Click on a sports place to view it details - name, location, rating.
5. Click "Next place" button to select the next place
6. Click the "Change location" button.

**Expected Result**:
1. The user should be able to view the google map with places to do specified in user profile kind of sport in the location specified in user profile.
2. By default, 1 place should be highlighted as recommended, user should be available to click "Next place" button to select the next place.
3. The user should be able to click the "Change location" button.
4. After clicking the "Change location" button the user should be redirected to "User profile" page.


### Test Case 5: Viewing Training Programs
**Description**: Test for viewing training programs.
**Pre-Condition**: The user is logged in and on the homepage.
1. Click on the 'Training programs' tab in Navigation Menu.
2. View the list of training programs.
3. View filter with predefined values.
4. View pagination.
5. Select "By my sport kind" filter.
6. Select "Recommended" filter.
7. Click next pages in pagination.
8. Click on a training program to view it.

**Expected Result**:
1. The user should be able to view the list of all training programs by default.
2. After selecting "By my sport kind" filter - should be displayed only training programs corresponding to specified in user profile kind of sport.
3. After selecting "Recommended" filter - should be displayed only training programs recommended to user reflecting his old ratings of existing training programs, random training programs should be recommended only in case user doesn't have any performed rating actions.
4. User should be able to select pages in pagination, after selecting training programs from that page should be displayed.
5. The user should be able to click on a training program and be redirected to it's page to view it.

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
