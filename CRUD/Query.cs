using FlightDocV1._1.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace FlightDocV1._1.Models;

public class Query
{
    private readonly FlightDocContext _context;
    public Query(FlightDocContext docContext)
    {
        this._context = docContext;
    }

    //// CMS
    //Dashboard
    public List<Document> GetDocList()
    {
        return _context.Documents
            .Include(i => i.UserSection)
            .Include(i => i.Flight)
            .Include(i => i.DocType)
            .OrderByDescending(i => i.LastModified).ToList();
    }

    public List<Flight> GetCurrentFlight()
    {
        return _context.Flights
            .Where(i => i.From_Time > DateTime.Now).ToList();
    }


    //All Flight
    // Use GetFLight to get Flight list
    public List<Document> GetFlightDocList(int FlightID)
    {
        return _context.Documents
            .Include(i => i.Flight)
            .Where(i => i.FlightID == FlightID).ToList();
    }
    // Use GetDocNewestVersion to get the newest version of each document


    //Add new Document
    // Use AddDocument to add new document

    //Document Information
    public Document GetDocInfo (int docID)
    {
        return _context.Documents
            .Include(i => i.Flight)
            .Include(i => i.DocType)
            .FirstOrDefault(i => i.Id == docID);
    }
    public List<Version> GetVersionsOfDoc(int docID)
    {
        return _context.Versions
            .Include(i => i.Document)
            .Where(i => i.DocumentID == docID).ToList();
    }
    public Version GetNewestVersionOfDoc(int NewestVersionId)
    {
        return _context.Versions.Find(NewestVersionId);
    }
    public List<DocumentPermission> GetDocPermitsOfDoc(int docID)
    {
        return _context.DocumentPermissions
            .Include(i => i.Document)
            .Where(i => i.DocumentID == docID).ToList();
    }
    public Permission GetPermissionOfDoc(int docTypeID)
    {
        return _context.Permissions.Find(docTypeID);
    }

    //My Document List
    public List<Document> GetUserDocList(int userSectionID)
    {
        return _context.Documents
            .Include(i => i.UserSection)
            .Include(i => i.DocType)
            .Include(i => i.Flight)
            .Where(i => i.UserSectionID == userSectionID).ToList();
    }

    //public DocType GetDocTypeOfDoc(Document document)
    //{
    //    return _context.DocTypes.Find(document.DocTypeID);
    //}
    //public Flight GetFlightOfDoc(Document document)
    //{
    //    return _context.Flights.Find(document.FlightID);
    //}


    //Document configuration
    public DocType GetDocType (int docTypeID)
    {
        return _context.DocTypes.Find(docTypeID);
    }
    public List<Permission> GetDocTypePermit(int DocTypeID)
    {
        return _context.Permissions
            .Include(i => i.DocType)
            .Where(i => i.DocTypeID ==  DocTypeID).ToList();
    }

    public Boolean IsRecord(int docTypeId, int groupId)
    {
        Permission permission = _context.Permissions.FirstOrDefault(i => i.DocTypeID == docTypeId && i.GroupID ==  groupId);
        if (permission == null)
        {
            return false;
        }
        return true;
    }
    public int GetLevelPermission(int DoctypeId, int GroupId)
    {
        List<Permission> permission = _context.Permissions.ToList();
        if (IsRecord(DoctypeId, GroupId) == false)
        {
            return 0;
        }
        else
        {
            return _context.Permissions
                .Where(i => i.DocTypeID == DoctypeId && i.GroupID == GroupId)
                .FirstOrDefault().Level;
        }
    }

    public List<Permission> GetLevelPermissionListOfDoctype(int DoctypeId)
    {
        return _context.Permissions
            .Include(i => i.DocType)
            .Where(i => i.DocTypeID == DoctypeId).ToList();
    }

    //Group List
    //  Use GetGroups to get list of existing group
    public Group GetGroupById(int GroupId)
    {
        return _context.Groups.Find(GroupId);
    }
    //Group Permission
    //public User GetGroupCreator(int groupID)
    //{
    //    return _context.Users
    //        .Where(i => i.Email == _context.Groups.Find(groupID).CreatorEmail)
    //        .FirstOrDefault();
    //}
    public List<UserSection> GetListOfMember (int groupID)
    {
        return _context.UserSections
            .Include(i => i.Group)
            .Include(i => i.User)
            .Where(i => i.GroupID == groupID).ToList();
    }
    public string AddMemberToGroup(int UserSectionId, int NewGroupId)
    {
        UserSection userSection = _context.UserSections.Find(UserSectionId);
        userSection.GroupID = NewGroupId;
        _context.UserSections.Update(userSection);
        _context.SaveChanges();
        return "User has been add to group";
    }
    public String TerminateUserSection (int userId)
    {
        _context.Users.Find(userId).Deactivated = true;
        return "User deactivated";
    }
    public String ActivateUserSection (int userId)
    {
        _context.Users.Find(userId).Deactivated = false;
        return "User activated";
    }


    //Account Setting
    public User GetUser(int userId)
    {
        return _context.Users.Find(userId);
    }
    // Use GetUsers to get user list
    public string ChangeUserSection(int userId, int userSectionId)
    {
        UserSection userSection = _context.UserSections.Find (userSectionId);
        userSection.UserID = userId;
        _context.UserSections.Update (userSection);
        _context.SaveChanges ();
        return "User changed";
    }


    //// Mobile
    //Today Flight
    public List<Flight> GetTodayFlight()
    {
        return _context.Flights
            .Where(i => i.From_Time.Date == DateTime.Now.Date).ToList();
    }

    //Flight Document
    public Flight GetFlight(int flightID)
    {
        return _context.Flights.Find(flightID);
    }
    // use GetFlightDocList to get document list of flight


    //Flight confirmation
    // Use GetFlight to get specific flight
    // Use GetFlightDocList to get document list of flight
    // Use GetDocNewsestVersion to get newest version of document
    public String FlightConfirmation(string signaturePath, int flightID)
    {
        _context.Flights.Find(flightID).Signature = signaturePath;
        _context.SaveChanges();
        return "Flight Confirmed";
    }


    //Login and register
    // Use AddUser to register
    public string AssignUserSection(int userId, int userSectionId)
    {
        UserSection userSection = _context.UserSections.Find(userSectionId);
        if (userSection == null) { return "No user section found"; }
        userSection.UserID = userId;
        _context.UserSections.Update(userSection);
        _context.SaveChanges();
        return ("Assigned");
    }
    public string AssignRole(int roleId, int userSectionId)
    {
        UserSection userSection = _context.UserSections.Find(userSectionId);
        if (userSection == null) { return "No user section found"; }
        userSection.RoleID = roleId;
        _context.UserSections.Update(userSection);
        _context.SaveChanges();
        return "Role added";
    }


}
