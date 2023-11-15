using FlightDocV1._1.CRUD;
using FlightDocV1._1.Data;
using FlightDocV1._1.FileHandler;
using FlightDocV1._1.Models;
using FlightDocV1._1.SignatureHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocV1._1.Controllers
{
    [Authorize(Policy = "Admin, Pilot and Crew")]
    public class FlightDocContractorController : Controller
    {
        Query _query;
        Crud _crud;
        SignatureHandler.SignatureHandler _signatureHandler;

        public FlightDocContractorController(FlightDocContext flightDocContext)
        {
            _query = new Query(flightDocContext);
            _crud = new Crud(flightDocContext);
            _signatureHandler = new SignatureHandler.SignatureHandler();
        }

        [HttpGet]
        [Route("TodayFlight")]
        public IActionResult TodayFlight()
        {
            ViewData["Today flight"] = _query.GetTodayFlight();
            return Ok();
        }

        [HttpGet]
        [Route("FlightDocument")]
        public IActionResult FlightDocument(int flightId)
        {
            ViewData["Get flight"] = _query.GetFlight(flightId);
            ViewData["Get document of flight"] = _query.GetFlightDocList(flightId);
            return Ok();
        }

        [HttpGet]
        [Route("DocumentDetail")]
        public IActionResult DocumentDetail(int NewestVersionId)
        {
            ViewData["Document information"] = _query.GetNewestVersionOfDoc(NewestVersionId);
            return Ok();
        }

        [HttpPatch]
        [Route("UpdateDocumentFormContractor")]
        public ActionResult UpdateDocument(int docID, IFormFile file)
        {
            String Filepath = FileHandler.FileHandler.SaveFile(file).Result;
            _crud.UpdateDocument(docID, Filepath);
            return Ok();
        }

        [HttpGet]
        [Route("Confirmation")]
        public ActionResult Confirmation(int flightId)
        {
            ViewData["Get flight"] = _query.GetFlight(flightId);
            ViewData["Get document of flight"] = _query.GetFlightDocList(flightId);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveFile")]
        public ActionResult RemoveFile(int docId)
        {
            _crud.DeleteDocument(docId);
            return Ok();
        }

        [HttpPost]
        [Route("SaveSignature")]
        public ActionResult SaveSignature(int flightId, IFormFile signature)
        {
            String Filepath = SignatureHandler.SignatureHandler.SaveFile(signature).Result;
            _query.FlightConfirmation(Filepath, flightId);
            return Ok();
        }

    }
}
