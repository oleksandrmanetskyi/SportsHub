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
It also has additional module - **Recommendation System**. This separate module is responsible for providing recommendations for users. It is based on Machine Learning algorithms. It is independent from other layers and can be easily replaced with another module.
Also application uses Azure cloud service to store data and Google API to provide maps.

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




