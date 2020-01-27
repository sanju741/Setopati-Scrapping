using System;
using System.Collections.Generic;
using System.Text;

namespace WebScrapping.Models
{
    public abstract class ScrappingElement
    {
        public string AgeRequirement { get; }
        public string CookieRequirement { get; }
        public string IgnoredCategories { get; }
        public string WebSiteUrl { get; }
        public string MenuOrCategoriesSelector { get; }
        public string ProductSelector { get; }
        public string ProductNameSelector { get; }
        public string ProductPriceSelector { get; }
        public string ProductPictureSelector { get; }
        public string ProductDescriptionSelector { get; }
        public string PaginationSelector { get; }
    }

}
