using FlightDocV1._1.CRUD;
using FlightDocV1._1.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocV1._1.Controllers
{
    public class FlightDocContractorController : Controller
    {
        Query _query;
        Crud _crud;
        public IActionResult TodayFlight()
        {
            ViewData["Today flight"] = _query.GetTodayFlight();
            return View();
        }
        public IActionResult FlightDocument(int flightId)
        {
            ViewData["Get flight"] = _query.GetFlight(flightId);
            ViewData["Get document of flight"] = _query.GetFlightDocList(flightId);
            return View();
        }
        public IActionResult DocumentDetail(int NewestVersionId)
        {
            ViewData["Document information"] = _query.GetNewestVersionOfDoc(NewestVersionId);
            return View();
        }
        public ActionResult UpdateDocument(int docID, IFormFile file)
        {
            String Filepath = FileHandler.FileHandler.SaveFile(file).Result;
            _crud.UpdateDocument(docID, Filepath);
            return View();
        }
        public ActionResult Confirmation(int flightId)
        {
            ViewData["Get flight"] = _query.GetFlight(flightId);
            ViewData["Get document of flight"] = _query.GetFlightDocList(flightId);
            return View();
        }
        public ActionResult RemoveFile(int docId)
        {
            _crud.DeleteDocument(docId);
            return View();
        }
        public ActionResult SaveSignature(int flightId, IFormFile signature)
        {
            String Filepath = FileHandler.FileHandler.SaveFile(signature).Result;
            _query.FlightConfirmation(Filepath, flightId);
            return View();
        }

    }
}
