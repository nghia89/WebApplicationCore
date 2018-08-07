﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAppCore.Application.Interfaces;
using WebAppCore.Models.ProductViewModels;

namespace WebAppCore.Controllers
{
    public class ProductController : Controller
    {

        IProductService _productService;
        IBillService _billService;
        IProductCategoryService _productCategoryService;
        IConfiguration _configuration;
        public ProductController(IProductService productService, IConfiguration configuration,
            IBillService billService,
            IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _configuration = configuration;
            _billService = billService;
        }
        [Route("san-pham.html")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{alias}-c.{id}.html")]
        public IActionResult Catalog(int id, int? pageSize, string sortBy, int page = 1)
        {
            var catalog = new CatalogViewModel();
            ViewData["BodyClass"] = "shop_grid_full_width_page";
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            catalog.Data = _productService.GetAllPaging(id, string.Empty, page, pageSize.Value, sortBy);
            catalog.Category = _productCategoryService.GetById(id);
            return View(catalog);
        }


        [Route("{alias}-p.{id}.html", Name = "ProductDetail")]
        public IActionResult Detail(int id)
        {
            ViewData["BodyClass"] = "product-page";
            var model = new DetailViewModel();
            model.Product = _productService.GetById(id);
            model.Category = _productCategoryService.GetById(model.Product.CategoryId);
            model.RelatedProducts = _productService.GetRelatedProducts(id, 9);
            model.UpsellProducts = _productService.GetUpsellProducts(6);
            model.ProductImages = _productService.GetImages(id);
            model.Tags = _productService.GetProductTags(id);
            return View(model);

        }
    }
}