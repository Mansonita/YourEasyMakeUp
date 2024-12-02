﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography.Xml;
using YourEasyRent.UserState;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YourEasyRent.Entities.ProductForSubscription
{
    public class ProductForSubscription
    {
        public string UserId { get; private set; }
        public string? ChatId { get; private set; }
        public string Brand { get; private set; }
        public string Name { get; private set; }

        public decimal Price { get; private set; }
        public string? Url { get; private set; }
        public ProductForSubscription() { }

        public static ProductForSubscription CreateProductForSubscription(UserSearchState userSearchState) // переносим одни данные в оъбект Subscriber
        { 
            ProductForSubscription subscriber = new ProductForSubscription()
            {
                UserId = userSearchState.UserId,
                ChatId = userSearchState.ChatId,
                Brand = userSearchState.Brand,
                Name = userSearchState.Name,
                Price = (decimal)userSearchState.Price
            };
            return subscriber;
        }
        public override string ToString() 
        {
            return $"{Brand}\n{Name}\n{Price}\n[Ссылка на продукт] {Url}"; // AK TODO переместить этот метод в ProductForSubscriptionService, здесь он больше не нужен
        }

        //public ProductForSubscriptionDto ToDto() // Может понадобиться потом
        //{
        //    ProductForSubscriptionDto subscribersDto = new ProductForSubscriptionDto()
        //    {
        //        UserId = UserId,
        //        ChatId = ChatId,
        //        Brand = Brand,
        //        Name = Name,
        //        Price = Price,
        //        Url = Url
        //    };
        //    return subscribersDto;
        //}

    }
}
