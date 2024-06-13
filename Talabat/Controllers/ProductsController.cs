﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.core.Entities;
using Talabat.core.Repositorires;
using Talabat.core.Specifications;
using Talabat.DTOs;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _ProductRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        public readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo
           , IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductType> typesRepo, IMapper mapper)

        {
            this._ProductRepo = ProductRepo;
            this._brandsRepo = brandsRepo;
            this._typesRepo = typesRepo;
            this._mapper = mapper;
        }

        //Api/Controller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(string sort)
        {
            var spec = new ProductBrandandTypeSpecification(sort);

            var Products = await _ProductRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var spec = new ProductBrandandTypeSpecification(id);
            var product = await _ProductRepo.GetByIdWithSpecAsync(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product, ProductDTO>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetTypes()
        {
            var types = await _typesRepo.GetAllAsync();
            return Ok(types);
        }

    }
}
