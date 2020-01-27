using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WebScrapping.Data;
using WebScrapping.Models;

namespace WebScrapping.Services.Setopati
{
    public class SetopatiService : ISetopatiService
    {
        private readonly IChromeSeleniumService _seleniumService;
        private readonly ILogger<SetopatiService> _logger;
        private readonly IProductRepository _productRepository;

        public SetopatiService(
            IChromeSeleniumService seleniumService,
                ILogger<SetopatiService> logger,
            IProductRepository productRepository)
        {
            _seleniumService = seleniumService;
            _logger = logger;
            _productRepository = productRepository;
        }

        public void StartScrapping()
        {
            try
            {
                //Go to the site url
                _seleniumService.GoToUrl(SetopatiElement.WebSiteUrl);

                //Get all categories url to be scraped
                ReadOnlyCollection<IWebElement> allCategories = _seleniumService
                    .GetElementsByCssSelector(SetopatiElement.MenuOrCategoriesSelector);

                string[] ignoredCategories = SetopatiElement.IgnoredCategories.Split(",");
                //scrapping all categories
                for (var i = 0; i < allCategories.Count; i++)
                {
                    //Get category info
                    IWebElement scrappedCategory = allCategories[i];
                    string categoryLink = _seleniumService.GetElementByAttribute(scrappedCategory, "href");

                    if (ignoredCategories.Contains(categoryLink))
                        continue;

                    //Go to category page
                    _seleniumService.GoToUrl(categoryLink);

                    //The while statement is for pagination. A category page can have different number of pages.
                    //We get out from category page only after scrapping product from all the pages
                    int pageNumber = 1;
                    while (true)
                    {
                        //Get all products elements
                        ReadOnlyCollection<IWebElement> products = _seleniumService
                            .GetElementsByCssSelector(SetopatiElement.ProductSelector);

                        //scrapping all products for the page for the selected category
                        for (var j = 0; j < products.Count; j++)
                        {

                            IWebElement scrappedProduct = products[j];
                            string productUrl = _seleniumService.GetElementByAttribute(scrappedProduct, "href");
                            //goto product detail page
                            _seleniumService.GoToUrl(productUrl);

                            //get product information
                            var product = new WebData()
                            {
                                Url = productUrl,
                                Name = _seleniumService
                                    .GetAttributeByByCssSelector(SetopatiElement.ProductNameSelector,
                                        AttributeTypeEnum.Text),
                                PriceStr = _seleniumService
                                    .GetAttributeByByCssSelector(SetopatiElement.ProductPriceSelector, AttributeTypeEnum.Text),
                                SourcePictureUrl =
                                    _seleniumService.GetAttributeByByCssSelector(SetopatiElement.ProductPictureSelector,
                                        AttributeTypeEnum.ImagePath),
                                Description = _seleniumService
                                    .GetAttributeByByCssSelector(SetopatiElement.ProductDescriptionSelector,
                                        AttributeTypeEnum.Text) ?? "",
                            };

                            product.DescriptionExtra = "";

                            //Save product to the database
                            _productRepository.AddProduct(product);

                            Console.WriteLine($"[{j}] Product {product.Name} in url {productUrl} ");
                            //go back to category listing page once information about the product is scrapped
                            _seleniumService.ReturnToPreviousPage();

                            //selenium treats the information as stale once the page has been left
                            //so refresh the information
                            products = _seleniumService.GetElementsByCssSelector(SetopatiElement.ProductSelector);
                        }

                        //Check for pagination
                        IWebElement pagination = _seleniumService.
                            GetElementByCssSelector(SetopatiElement.PaginationSelector);

                        if (pagination == null)
                            break; //all pages have been scrapped

                        pageNumber += 1;
                        string nextPageUrl = _seleniumService.GetElementByAttribute(pagination, "href");
                        nextPageUrl = nextPageUrl.Substring(0, (nextPageUrl.Length - 1)) + pageNumber;

                        //Go to the another page and scrap
                        _seleniumService.ReturnToPreviousPage();
                        _seleniumService.GoToUrl(nextPageUrl);
                        _seleniumService.WaitForAjax();
                    }

                    // go back to home page once all the products from the category are scrapped
                    _seleniumService.ReturnToPreviousPage();

                    //selenium treats the information as stale once the page has been left
                    //so refresh the information
                    allCategories =
                        _seleniumService.GetElementsByCssSelector(SetopatiElement.MenuOrCategoriesSelector);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message} --------------------{e.InnerException} ------ {e.StackTrace}");
                Console.WriteLine($"{e.Message} --------------------{e.InnerException} ------ {e.StackTrace}");
            }
        }
    }
}
