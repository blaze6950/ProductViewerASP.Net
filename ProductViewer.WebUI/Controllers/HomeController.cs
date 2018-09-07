using System.Configuration;
using System.Web.Mvc;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public HomeController()
        {
            var productInfoContext = new AdoNetContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _unitOfWork = new UnitOfWork(productInfoContext);
        }

        public ViewResult Index()
        {
            return View();
        }
    }
}