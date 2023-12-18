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
