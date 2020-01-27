using WebScrapping.Services.Setopati;

namespace WebScrapping.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ISetopatiService _thysShopScrappingService;

        public ApplicationService(
            ISetopatiService thysShopScrappingService
            )
        {
            _thysShopScrappingService = thysShopScrappingService;
        }

        public void Start()
        {
            //factory design pattern?
            _thysShopScrappingService.StartScrapping();
        }
    }
}
