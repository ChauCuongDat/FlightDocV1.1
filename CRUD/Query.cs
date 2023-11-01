using FlightDocV1._1.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

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
    public List<Document> GetUserDocList(int userSectionID)
    {
        return _context.Documents
            .Include(i => i.UserSection)
            .Where(i =>  i.UserSectionID == userSectionID).ToList();
    }

    public Version GetDocNewsestVersion(int NewestVersionID)
    {
        return _context.Versions.Find(NewestVersionID);
    }

    public List<Flight> GetCurrentFlight()
    {
        return _context.Flights
            .Where(i => i.From_Time > DateTime.Now).ToList();
    }

    public List<Document> GetFlightDocList(int FlightID)
    {
        return _context.Documents
            .Include(i => i.Flight)
            .Where(i => i.FlightID == FlightID).ToList();
    }

    //All Flight
    // Use GetFLight() to get Flight list
    // Use GetFlightDocList(int FlightID) to get document of each Flight in FLight list
    // Use GetDocNewestVersion(int NewestVersionID) to get the newest version of each document

    //Add new Document
    // Use AddDocument(Document document, Version version) to add new document

    //Document Information
    public Document GetDocInfo (int docID)
    {
        return _context.Documents.Find(docID);
    }
    public Flight GetFlightOfDoc(Document document)
    {
        return _context.Flights.Find(document.FlightID);
    }
    public DocType GetDocTypeOfDoc(Document document)
    {
        return _context.DocTypes.Find(document.DocTypeID);
    }
    public List<Version> GetVersionsOfDoc(int docID)
    {
        return _context.Versions
            .Include(i => i.Document)
            .Where(i => i.DocumentID == docID).ToList();
    }
    public List<DocumentPermission> GetDocPermitsOfDoc(int docID)
    {
        return _context.DocumentPermissions
            .Include(i => i.Document)
            .Where(i => i.DocumentID == docID).ToList();
    }
    public List<Permission> GetPermissionsOfDoc(int docTypeID)
    {
        return _context.Permissions
            .Include(i => i.DocType)
            .Where(i => i.DocTypeID == docTypeID).ToList();
    }

    //My Document List
    // Use GetUserDocList(int userSectionID) to get user's document list
    // Use GetVersionsOfDoc(int docID) to get version of document
    // Use GetDocTypeOfDoc(Document document) to get document type
    // Use GetFlightOfDoc(Document document) to get the flight of document

    //Document configuration
    public List<DocType> GetUserDocType(int UserSectionID)
    {
        return _context.DocTypes
            .Include(i => i.UserSection)
            .Where(i => i.UserSectionID == UserSectionID).ToList();
    }
    public List<Permission> GetDocTypePermit(int DocTypeID)
    {
        return _context.Permissions
            .Include(i => i.DocType)
            .Where(i => i.DocTypeID ==  DocTypeID).ToList();
    }
    //  Use GetGroups() to get list of existing group
    public Boolean IsRecord(int GroupID, List<Permission> DoctypePermit)
    {
        foreach (Permission i in DoctypePermit)
        {
            if (i.GroupID == GroupID)
            {
                return true;
            }
        }
        return false;
    }

    //Group Permission
    // Use GetGroups() to get list of group
    public User GetGroupCreator(Group group)
    {
        return _context.Users
            .Where(i => i.Email == group.CreatorEmail)
            .FirstOrDefault();
    }

    //Edit Group
    public List<UserSection> GetListOfMember (int groupID)
    {
        return _context.UserSections
            .Include(i => i.Group)
            .Where(i => i.GroupID == groupID).ToList();
    }
    public String TerminateUserSection (int userID)
    {
        _context.Users.Find(userID).Deactivated = true;
        return "User deactivated";
    }
    public String ActivateUserSection (int userID)
    {
        _context.Users.Find(userID).Deactivated = false;
        return "User activated";
    }
    public User GetUser(int userID)
    {
        return _context.Users.Find(userID);
    }

    //Account Setting
    // Use GetUsers() to get user list
    // Use GetUser() to get the specific user with userID


    //// Mobile
    //Today Flight
    public List<Flight> GetTodayFlight()
    {
        return _context.Flights
            .Where(i => i.From_Time.Date == DateTime.Now.Date).ToList();
    }

    //Flight Document
    // Use the same query as CMS


    //Flight confirmation
    public Flight GetFlight(int flightID)
    {
        return _context.Flights.Find(flightID);
    }
    // Use GetFlightDocList(int FlightID) to get doc list of flight
    // Use GetDocNewsestVersion(int NewestVersionID) to get newest version of document
    public String FlightConfirmation(string signature, int flightID)
    {
        _context.Flights.Find(flightID).Signature = signature;
        _context.SaveChanges();
        return "Flight Confirmed";
    }
}
