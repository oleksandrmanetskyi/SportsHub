using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Services.Interfaces
{
    public interface IPlacesService
    {
        Task<JObject> GetPlacesByUserId(string userId);
    }
}
