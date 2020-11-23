﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Data.Context;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.IOC
{
    public class DepndencyContainer
    {
        public static void RegisterServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShoppingCartDbContext>(options =>
               options.UseSqlServer(connectionString));



            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductsService, ProductsService>();
        }
    }
}
