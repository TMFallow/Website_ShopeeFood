using NuGet.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;

namespace Website_ShopeeFood.Services
{
    public interface IAPIServices
    {
        public string getIPAddress();

        Task<List<AreasModel>> getArea();

        Task<AreasModel> getNameOfArea(int AreaId);

        Task<List<DetailAreasModel>> getDetailAreas(int AreaId);

        Task<List<FoodModel>> getAllFoodByIdRestaurant(int restaurantId);

        Task<List<UsersModel>> GetListUsers();

        Task<UsersModel> GetUsersByEmail(string email);

        Task<UsersModel> getUsersByUsername(string username);

        Task<List<PromotionModels>> getPromotion();

        Task<List<RestaurantsModel>> GetAllRestaurant();

        Task<RestaurantsModel> getRestaurantsByIdRestaurant(int restaurantId);

        Task<List<RestaurantsModel>> searchListRestaurantByName(string name, List<RestaurantsModel> restaurantsModels);

    }
}
