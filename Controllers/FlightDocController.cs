using FlightDocV1._1.CRUD;
using FlightDocV1._1.Data;
using FlightDocV1._1.FileHandler;
using FlightDocV1._1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;

namespace FlightDocV1._1.Controllers
{
    public class FlightDocController : Controller
    {
        Query _query;
        Crud _crud;
        FileHandler.FileHandler _fileHandler;

        //CMS

        public FlightDocController(FlightDocContext flightDocContext)
        {
            _query = new Query(flightDocContext);
            _crud = new Crud(flightDocContext);
            _fileHandler = new FileHandler.FileHandler();
        }
        
        public ActionResult Dashboard()
        {
            ViewData["Get docList order by last modified "] = _query.GetDocList();
            ViewData["Get current flight"] = _query.GetCurrentFlight();
            return View();
        }

        public ActionResult AllFlight()
        {
            ViewData["Get all flight"] = _crud.GetFlight();
            return View();
        }
        public ActionResult DocOfFlight(int FlightID)
        {
            ViewData["Get document of flight"] = _query.GetFlightDocList(FlightID);
            return View();
        }

        public ActionResult AddNewDocument(Document document, IFormFile file, List<DocumentPermission> DocPermissions)
        {
            string Filepath = FileHandler.FileHandler.SaveFile(file).Result;
            _crud.AddDocument(document,Filepath);
            foreach (DocumentPermission p in DocPermissions)
            {
                _crud.AddDocPermission(p);
            }
            return View();
        }

        public ActionResult UpdateDocument(int docID, IFormFile file)
        {
            String Filepath = FileHandler.FileHandler.SaveFile(file).Result;
            _crud.UpdateDocument(docID,Filepath);
            return View();
        }

        public ActionResult DocInformation(int DocID)
        {
            ViewData["Get flight, doctype and document information"] = _query.GetDocInfo(DocID);
            ViewData["Get document versions"] = _query.GetVersionsOfDoc(DocID);
            ViewData["Get permision of document"] = _query.GetDocPermitsOfDoc(DocID);
            return View();
        }

        public ActionResult UserDocList(int UserSectionID)
        {
            ViewData["Get user document list"] = _query.GetUserDocList(UserSectionID);
            return View();
        }

        public ActionResult Configuration()
        {
            ViewData["Get doctype list"] = _crud.GetDocTypes();
            return View();
        }

        public ActionResult DocTypeEdit(DocType docType)
        {
            ViewData["Get doctype"] = _query.GetDocType(docType.Id);
            _crud.UpdateDocType(docType);
            return View();
        }
        public ActionResult GroupPermision(int doctypeID, int level)
        {
            ViewData["Get group list"] = _crud.GetGroups();
            foreach (Group g in _crud.GetGroups())
            {
                ViewData["Get level permision"] = _query.GetLevelPermission(doctypeID, g.Id);
                if (_query.IsRecord(doctypeID,g.Id) == false)
                {
                    _crud.AddPermission(doctypeID, g.Id, level);
                }
                _crud.UpdatePermission(doctypeID, g.Id, level);
            }
            return View();
        }

        public ActionResult AddDocType(DocType type)
        {
            _crud.AddDocType(type);
            return View();
        }

        public ActionResult GetGroupList()
        {
            ViewData["Get group list"] = _crud.GetGroups();
            return View();
        }

        public ActionResult AddGroup(Group Group)
        {
            _crud.AddGroup(Group);
            return View();
        }

        public ActionResult EditGroup(int GroupID, Group group)
        {
            _crud.UpdateGroup(group);
            ViewData["Get User List"] = _query.GetListOfMember(GroupID);
            return View();
        }


    }
}
