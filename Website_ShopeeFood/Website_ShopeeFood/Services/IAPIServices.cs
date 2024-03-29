﻿using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;

namespace Website_ShopeeFood.Services
{
    public interface IAPIServices
    {
        public string getIPAddress();

        public string getIPAddressMVC();

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

        Task<List<FoodModel>> searchListRestaurantByTypeID(int IdTypes);

        Task<FoodModel> getFoodById(int id);

        Task<List<FoodModel>> getAllFood();

        Task<List<FoodModel>> getListRestaurantBasedOnTypeID(int Id);

        Task<List<RestaurantsModel>> getListRestauranrByIdDistricts(int IdDistricts);

        Task<List<InvoicesModel>> getListInvoicesByUserID(int userId, string token);

        Task<List<InvoiceDetailsModel>> getListDetailsInvoiceByInvoices(int invoicesId);

        void insertInvoices(InvoicesModel invoicesModel);

        void insertInvoiceDetails(InvoiceDetailsModel invoiceDetailsModel);

        Task<List<InvoicesModel>> getAllInvoices();

        Task<UsersModel> getUserById(int id);

        void updateUsersInfo(UsersModel usersModel);

        List<FoodModel> searchListFoodByEachRestaurant(string name, List<FoodModel> foodModels);

        Task<List<AddressUserModel>> getListAddressUserByUserId(int userId);

        Task<AddressUserModel> getAddressToDelivery(int addressId);

        List<AddressUserModel> searchListAddressUserModel(string name, List<AddressUserModel> addressUserModels);

        Task<string> loginUser(String username, String password);

    }
}
