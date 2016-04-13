using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using RodrigoDeveloper.Dominio.Entidades;
using RodrigoDeveloper.Dominio.Facades;

namespace RodrigoDeveloper.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ExportKml()
        {
            Polygon[] polygon = new Polygon[]
            {
                new Polygon()
                {
                    codPolygon = 1,
                    coordenadas = "MULTIPOLYGON (((-44.346506956887787 -18.256141755975907, -46.494384765625391 -18.318873867444921, -45.176025390625682 -18.881145783576041, -44.346506956887787 -18.256141755975907)), ((-44.2041926178474 -18.25113637710821, -44.209228515625 -18.151920615268239, -44.346506956887787 -18.256141755975907, -44.2041926178474 -18.25113637710821)), ((-43.242431640625412 -18.214546861538114, -44.2041926178474 -18.25113637710821, -44.143310546875682 -19.441536872293238, -43.242431640625412 -18.214546861538114)))",
                    descricao = "descrição de teste",
                    nome = "Polygono de Teste"
                },
                new Polygon()
                {
                    codPolygon = 2,
                    coordenadas = "POLYGON ((-52.800537109376052 -22.35598248063382, -55.371337890625476 -20.309407167222755, -55.349365234375348 -22.619906297708749, -52.800537109376052 -22.35598248063382))",
                    descricao = "descrição de teste 2",
                    nome = "Polygono de Teste 2"
                }
            };

            ExportKmlPolygonDocument exportKmlPolygonDocuments = new ExportKmlPolygonDocument()
            {
                name = "Cercas"
            };

            exportKmlPolygonDocuments.ExportKmlPolygonPlacemarks = exportKmlPolygonDocuments.SetPlacemarks(polygon);

            ExportKmlPolygon exportKmlPolygon = new ExportKmlPolygon()
            {
                ExportKmlPolygonDocuments = exportKmlPolygonDocuments
            };

            StringBuilder xml = exportKmlPolygon.GerarXml();
            if (xml == null) throw new ArgumentNullException("xml");

            ExportKmlFile exportKmlFile = new ExportKmlFile()
            {
                name = exportKmlPolygon.ExportKmlPolygonDocuments.name,
                xml = exportKmlPolygon.xml
            };

            return View("ExportKmlFile", exportKmlFile);
        }
    }
}