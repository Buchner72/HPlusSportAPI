using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HPlusSport.API.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task <IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
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
