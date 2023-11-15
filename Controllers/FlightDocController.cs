using FlightDocV1._1.CRUD;
using FlightDocV1._1.Data;
using FlightDocV1._1.FileHandler;
using FlightDocV1._1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;

namespace FlightDocV1._1.Controllers
{
    [Authorize(Policy = "Admin and Office")]
    public class FlightDocController : Controller
    {
        Query _query;
        Crud _crud;
        FileHandler.FileHandler _fileHandler;


        public FlightDocController(FlightDocContext flightDocContext)
        {
            _query = new Query(flightDocContext);
            _crud = new Crud(flightDocContext);
            _fileHandler = new FileHandler.FileHandler();
        }

        [HttpGet]
        [Route("Dashboard")]
        public ActionResult Dashboard()
        {
            ViewData["Get docList order by last modified "] = _query.GetDocList();
            ViewData["Get current flight"] = _query.GetCurrentFlight();
            return Ok();
        }

        [HttpGet]
        [Route("AllFlight")]
        public ActionResult AllFlight()
        {
            ViewData["Get all flight"] = _crud.GetFlight();
            return Ok();
        }

        [HttpGet]
        [Route("DocOfFlight")]
        public ActionResult DocOfFlight(int FlightID)
        {
            ViewData["Get document of flight"] = _query.GetFlightDocList(FlightID);
            return Ok();
        }

        [HttpPost]
        [Route("AddNewDocument")]
        public async Task<ActionResult> AddNewDocument(string docName, int userSectionId, int DoctypeId, IFormFile file)
        {
            var document = new Document();
            document.Name = docName;
            document.UserSectionID = userSectionId;
            document.DocTypeID = DoctypeId;
            var Filepath = await FileHandler.FileHandler.SaveFile(file);
            if (file != null)
                return Content("File found");
            _crud.AddDocument(document,Filepath);
            return Ok();
        }

        //[HttpPost]
        //[Route("AddFile")]
        //public async Task<string> addFile(IFormFile file)
        //{
        //    return await FileHandler.FileHandler.SaveFile(file);
        //}

        [HttpPatch]
        [Route("UpdateDocument")]
        public ActionResult UpdateDocument(int docID, IFormFile file)
        {
            String Filepath = FileHandler.FileHandler.SaveFile(file).Result;
            _crud.UpdateDocument(docID,Filepath);
            return Ok();
        }

        [HttpGet]
        [Route("DocInformation")]
        public ActionResult DocInformation(int DocID)
        {
            ViewData["Get flight, doctype and document information"] = _query.GetDocInfo(DocID);
            ViewData["Get document versions"] = _query.GetVersionsOfDoc(DocID);
            ViewData["Get permision of document"] = _query.GetDocPermitsOfDoc(DocID);
            return Ok();
        }

        [HttpGet]
        [Route("UserDocList")]
        public ActionResult UserDocList(int UserSectionID)
        {
            ViewData["Get user document list"] = _query.GetUserDocList(UserSectionID);
            return Ok();
        }

        [HttpGet]
        [Route("Configuration")]
        public ActionResult Configuration()
        {
            ViewData["Get doctype list"] = _crud.GetDocTypes();
            return Ok();
        }

        [HttpPost]
        [Route("AddNewDoctype")]
        public ActionResult AddNewDoctype(string name, string note)
        {
            var docType = new DocType();
            docType.Name = name;
            docType.Note = note;
            _crud.AddDocType(docType);
            return Ok();
        }

        [HttpGet]
        [Route("DocTypeInformation")]
        public ActionResult DocTypeInformation(int doctypeId)
        {
            ViewData["Get doctype"] = _query.GetDocType(doctypeId);
            ViewData["Get group list"] = _crud.GetGroups();
            ViewData["Get level permision list"] = _query.GetLevelPermissionListOfDoctype(doctypeId);
            return Ok();
        }

        [HttpPatch]
        [Route("DocTypeEdit")]
        public ActionResult DocTypeEdit(DocType docType)
        {
            _crud.UpdateDocType(docType);
            return Ok();
        }

        [HttpPatch]
        [Route("ChangeGroupPermisionLevel")]
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
            return Ok();
        }

        [HttpGet]
        [Route("GetGroupList")]
        public ActionResult GetGroupList()
        {
            ViewData["Get group list"] = _crud.GetGroups();
            return Ok();
        }

        [HttpPost]
        [Route("AddGroup")]
        public ActionResult AddGroup(Group Group)
        {
            _crud.AddGroup(Group);
            return Ok();
        }

        [HttpGet]
        [Route("GroupInfo")]
        public ActionResult GroupInfo(int GroupId)
        {
            ViewData["Get group information"] = _query.GetGroupById(GroupId);
            ViewData["Get member list"] = _query.GetListOfMember(GroupId);
            return Ok();
        }

        [HttpPatch]
        [Route("EditGroup")]
        public ActionResult EditGroup(Group group)
        {
            _crud.UpdateGroup(group);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteGroup")]
        public ActionResult DeleteGroup(int GroupId)
        {
            _crud.DeleteGroup(GroupId);
            return Ok();
        }

        [HttpPost]
        [Route("AddMember")]
        public ActionResult AddMember(int userID, int groupId)
        {
            _query.AddMemberToGroup(userID, groupId);
            return Ok();
        }

        [HttpGet]
        [Route("GetListOfUser")]
        public ActionResult GetListOfUser()
        {
            ViewData["Get list of user"] = _crud.GetUsers();
            return Ok();
        }

        [HttpPatch]
        [Route("TerminateUser")]
        public ActionResult TerminateUser (int userId)
        {
            _query.TerminateUserSection(userId);
            return Ok();
        }

        [HttpPatch]
        [Route("ActivateUser")]
        public ActionResult ActivateUser(int userId)
        {
            _query.ActivateUserSection(userId);
            return Ok();
        }

        [HttpGet]
        [Route("AccountSetting")]
        public ActionResult AccountSetting (int userId)
        {
            ViewData["Get user information"] = _query.GetUser(userId);
            ViewData["Get user list"] = _crud.GetUsers();
            return Ok();
        }

        [HttpPatch]
        [Route("ChangeUser")]
        public ActionResult ChangeUser(int newUserId, int userSectionID)
        {
            _query.ChangeUserSection(newUserId, userSectionID);
            return Ok();
        }
    }
}
