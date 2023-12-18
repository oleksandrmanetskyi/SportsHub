# SportsHub
Sports Hub is an application to make more easier to do sport. All you need it is sing up or log in and take pleasure of using it.
# Requirements
![Sports Hub UseCases - 2023](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/26cf3c0e-d1c9-4c51-8b60-06de33557e93)
## Authentication and Authorization:
**User**:
- Log in with created credentials.
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
- Edit sport shops.
- Add sport shops.
- Edit sport shops.
- Edit nutritions.
- Add nutritions.
- Edit sport news.
- Add sport news.
- Edit training programs.
- Add training programs.

**Guest**:
Can see only the home, login and sign up pages.

# Non-Functional Requirements:
## 

# Architecture
![SportsHub_RecommendationSysteM_Architecture_2023v drawio](https://github.com/oleksandrmanetskyi/SportsHub/assets/47561209/9c86da45-6dd4-407f-adb8-ea28032bbcd1)

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




