using WebScrapping.Models;

namespace WebScrapping.Services.Setopati
{
    public class SetopatiElement : ScrappingElement
    {
        public new static string IgnoredCategories => "https://thysshop.be/123-verhuur," +
                                                      "https://thysshop.be/110-glazen";
        public new static string WebSiteUrl => "https://thysshop.be/";
        public new static string MenuOrCategoriesSelector => "#owl-menu-ver-left .category-left li.level-1 > a";
        public new static string ProductSelector => "a.product_img_link";
        public new static string ProductNameSelector => ".pb-center-column.col-xs-12.col-md-6 h1";
        public new static string ProductPriceSelector => "span#our_price_display";
        public new static string ProductPictureSelector => "img#bigpic";
        public new static string ProductDescriptionSelector => "div#short_description_content";
        public new static string PaginationSelector => "li#pagination_next_bottom a";
    }
}
