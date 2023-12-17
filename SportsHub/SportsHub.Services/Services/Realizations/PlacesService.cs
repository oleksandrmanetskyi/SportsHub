using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;

namespace SportsHub.Services.Services.Realizations
{
    public class PlacesService: IPlacesService
    {
        private UserManager<User> _userManager;
        private IOptions<GoogleMapsOptions> _options;
        private IRepositoryWrapper _repository;

        public PlacesService(UserManager<User> userManager, IOptions<GoogleMapsOptions> options, IRepositoryWrapper repository)
        {
            _userManager = userManager;
            _options = options;
            _repository = repository;
        }

        public async Task<JObject> GetPlacesByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            string sportKindName = (await _repository.SportKind.GetAll().FirstAsync(x => x.Id == user.SportKindId)).Name;
            string userLocation = user.Location;
            string url = $@"https://maps.googleapis.com/maps/api/place/textsearch/json?query={sportKindName}+{userLocation}&key={_options.Value.ApiKey}";

            WebRequest request = WebRequest.Create(url);

            WebResponse response = await request.GetResponseAsync();

            Stream data = response.GetResponseStream();

            StreamReader reader = new StreamReader(data);

            // json-formatted string from maps api
            string responseFromServer = await reader.ReadToEndAsync();

            response.Close();
            //var places = JsonSerializer.Deserialize<IEnumerable<PlaceDto>>(responseFromServer);
            return JObject.Parse(responseFromServer);
        }
    }
}
