﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HPlusSport.API.Models;
using Microsoft.EntityFrameworkCore;
using HPlusSport.API.Classes;

namespace HPlusSport.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        [HttpGet]
        //public IEnumerable<Product> GetProducts()
        //{
        //    return _context.Products.ToArray();
        //}

        //public async Task <IActionResult> GetAllProducts()
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductQueryParameters queryParameters)
        {
            // var products = await _context.Products.ToListAsync();

            IQueryable<Product> products = _context.Products;

            if (queryParameters.MinPrice!=null &&
                queryParameters.MaxPrice!=null)
            {
                products = products.Where(
                    p => p.Price >= queryParameters.MinPrice.Value &&
                        p.Price <= queryParameters.MaxPrice.Value
                );
            }

            if(!string.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(p => p.Sku == queryParameters.Sku);
            }

            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(                   
                        p => p.Name.ToLower().Contains(
                            queryParameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
                {
                    products = products.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
                }
            }

                products = products
                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);
            // return Ok(products);
            return Ok(await products.ToArrayAsync());
        }

        [HttpGet("{id:int}")]
        public async Task< IActionResult> GetProduct(int id)
        {
            var produkt = await _context.Products.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }

            return Ok(produkt);
        }

    }
}
