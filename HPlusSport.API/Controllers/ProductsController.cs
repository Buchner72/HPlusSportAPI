using System;
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
        public async Task<IActionResult> GetAllProducts([FromQuery]QueryParameters queryParameters)
        {
            // var products = await _context.Products.ToListAsync();

            IQueryable<Product> products = _context.Products;
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
