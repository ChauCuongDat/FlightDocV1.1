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

        public ActionResult AddNewDoctype(DocType Doctype)
        {
            _crud.AddDocType(Doctype);
            return View();
        }

        public ActionResult DocTypeInformation(int doctypeId)
        {
            ViewData["Get doctype"] = _query.GetDocType(doctypeId);
            ViewData["Get group list"] = _crud.GetGroups();
            ViewData["Get level permision list"] = _query.GetLevelPermissionListOfDoctype(doctypeId);
            return View();
        }

        public ActionResult DocTypeEdit(DocType docType)
        {
            _crud.UpdateDocType(docType);
            return View();
        }

        public ActionResult ChangeGroupPermisionLevel(int doctypeID, int groupId,int newLevel)
        {
            if (newLevel == 0)
            {
                _crud.DeletePermission(doctypeID,groupId);
            }
            else if(_query.IsRecord(doctypeID, groupId) == true)
            {
                _crud.UpdatePermission(doctypeID,groupId,newLevel);
            }
            else
            {
                _crud.AddPermission(doctypeID,groupId,newLevel);
            }
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

        public ActionResult GroupInfo(int GroupId)
        {
            ViewData["Get group information"] = _query.GetGroupById(GroupId);
            ViewData["Get member list"] = _query.GetListOfMember(GroupId);
            return View();
        }

        public ActionResult EditGroup(Group group)
        {
            _crud.UpdateGroup(group);
            return View();
        }

        public ActionResult DeleteGroup(int GroupId)
        {
            _crud.DeleteGroup(GroupId);
            return View();
        }

        public ActionResult AddMember(int userID, int groupId)
        {
            _query.AddMemberToGroup(userID, groupId);
            return View();
        }

        public ActionResult GetListOfUser()
        {
            ViewData["Get list of user"] = _crud.GetUsers();
            return View();
        }

        public ActionResult TerminateUser (int userId)
        {
            _query.TerminateUserSection(userId);
            return View();
        }

        public ActionResult ActivateUser(int userId)
        {
            _query.ActivateUserSection(userId);
            return View();
        }

        public ActionResult AccountSetting (int userId)
        {
            ViewData["Get user information"] = _query.GetUser(userId);
            ViewData["Get user list"] = _crud.GetUsers();
            return View();
        }

        public ActionResult ChangeUser(int newUserId, int userSectionID)
        {
            _query.ChangeUserSection(newUserId, userSectionID);
            return View();
        }


    }
}
