
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp03.Contacts;
using WebApp03.Models;

namespace WebApp03.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataTransfer _dataTransfer;
        private readonly IConverter _converter;

        public HomeController(ILogger<HomeController> logger, IDataTransfer dataTransfer, IConverter converter)
        {
            _logger = logger;
            _dataTransfer = dataTransfer;
            _converter = converter;
        }

        public IActionResult Index()
        {
            string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "Data\\example.xml");
            string xsltPath = Path.Combine(Directory.GetCurrentDirectory(), "Data\\example.xslt");

            string transformedXml = _dataTransfer.TransformXml(xmlPath, xsltPath);
            ViewBag.TransformedXml = transformedXml;



            return View();
        }

        [HttpPost]
        public IActionResult GeneratePdf()
        {
            string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "Data\\example.xml");
            string xsltPath = Path.Combine(Directory.GetCurrentDirectory(), "Data\\example.xslt");

            string htmlContent = _dataTransfer.TransformXmlToHtml(xmlPath, xsltPath);
            byte[] pdfContent = ConvertHtmlToPdf(htmlContent);

            return File(pdfContent, "application/pdf", "Report.pdf");

            //return View();
        }


        public byte[] ConvertHtmlToPdf(string htmlContent)
        {
           // var converter = new SynchronizedConverter(new PdfTools());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    //OutputFormat = GlobalSettings.OutputFormatType.Pdf,
                   // Margins = new MarginSettings { Top = 10, Left = 10, Bottom = 10, Right = 10 }
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = htmlContent
                    }
                }
            };




            return _converter.Convert(doc);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
