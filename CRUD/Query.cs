﻿using FlightDocV1._1.Data;
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
    public List<Document> GetDocList()
    {
        return _context.Documents
            .Include(i => i.UserSection)
            .Include(i => i.Flight)
            .Include(i => i.DocType)
            .OrderByDescending(i => i.LastModified).ToList();
    }

    //public Version GetDocNewsestVersion(int NewestVersionID)
    //{
    //    return _context.Versions.Find(NewestVersionID);
    //}

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
    //public Version GetNewestVersionOfDoc(Document document)
    //{
    //    return _context.Versions.Find(document.NewestVersionID);
    //}
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

    //Group List
    //  Use GetGroups to get list of existing group
    public Boolean IsRecord(int docTypeId, int groupId)
    {
        List<Permission> permission = _context.Permissions.ToList();
        foreach (Permission i in permission)
        {
            if (i.DocTypeID == docTypeId && i.GroupID == groupId)
            {
                return true;
            }
        }
        return false;
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
    // Use GetUsers to get user list
    // Use GetUser to get the specific user with userID


    //// Mobile
    //Today Flight
    public List<Flight> GetTodayFlight()
    {
        return _context.Flights
            .Where(i => i.From_Time.Date == DateTime.Now.Date).ToList();
    }

    //Flight Document
    public List<Document> GetFilghtDoc(int flightID)
    {
        return _context.Documents
            .Include(i => i.Flight)
            .Where(i => i.FlightID == flightID).ToList();
    }


    //Flight confirmation
    public Flight GetFlight(int flightID)
    {
        return _context.Flights.Find(flightID);
    }
    // Use GetFlightDocList to get doc list of flight
    // Use GetDocNewsestVersion to get newest version of document
    public String FlightConfirmation(string signature, int flightID)
    {
        _context.Flights.Find(flightID).Signature = signature;
        _context.SaveChanges();
        return "Flight Confirmed";
    }
}
