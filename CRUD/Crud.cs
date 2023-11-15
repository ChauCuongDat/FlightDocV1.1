using FlightDocV1._1.Data;
using FlightDocV1._1.Models;
using Microsoft.EntityFrameworkCore;
using System.Security;
using System.Text.RegularExpressions;
using Group = FlightDocV1._1.Models.Group;
using Version = FlightDocV1._1.Models.Version;

namespace FlightDocV1._1.CRUD
{
    public class Crud
    {
        private readonly FlightDocContext _context;
        public Crud(FlightDocContext docContext)
        {
            this._context = docContext;
        }

        //Document Type CRUD
        public string AddDocType (DocType type)
        {
            type.CreatedDate = DateTime.Now;
            _context.DocTypes.Add(type);
            _context.SaveChanges();
            return "Document type added";
        }

        public List<DocType> GetDocTypes ()
        {
            return _context.DocTypes.ToList();
        }

        public string UpdateDocType(DocType type)
        {
            _context.DocTypes.Update(type);
            _context.SaveChanges();
            return "Document type updated";
        }

        public string DeleteDocType (int ID)
        {
            DocType type = _context.DocTypes.Find(ID);
            if (type != null)
            {
                _context.DocTypes.Remove(type);
                _context.SaveChanges();
                return "Document type removed";
            }
            return "Document type no found";
        }

        //Document CRUD
        public string AddDocument(Document document, string fileAddress)
        {
            Version version = new Version();
            document.NewestVersion = 1.0F;
            document.LastModified = DateTime.Now;
            _context.Documents.Add(document);
            _context.SaveChanges();
            version.VersionNum = 1.0F;
            version.UpdatedDate = DateTime.Now;
            version.FileAddress = fileAddress;
            document.NewestVersionID = addVersion(version, document.Id, document.NewestVersion);
            _context.Documents.Update(document);
            _context.SaveChanges();
            return "Document added";
        }

        public List<Document> GetDocuments()
        {
            return _context.Documents.ToList();
        }

        public string UpdateDocument(int docID, string fileAddress)
        {
            Document document = _context.Documents.Find(docID);
            Version version = new Version();
            document.NewestVersion = document.NewestVersion + 0.1F;
            document.LastModified = DateTime.Now;
            version.VersionNum = document.NewestVersion;
            version.UpdatedDate = DateTime.Now;
            version.FileAddress = fileAddress;
            document.NewestVersionID = addVersion(version, document.Id, document.NewestVersion);
            _context.Documents.Update(document);
            _context.SaveChanges();
            return "Document updated";
        }

        public string DeleteDocument(int ID)
        {
            Document document = _context.Documents.Find(ID);
            if (document != null)
            {
                _context.Documents.Remove(document);
                _context.SaveChanges();
                return "Document removed";
            }
            return "No Document Found";
        }

        //Document Permission CRUD
        public string AddDocPermission (DocumentPermission DocPermission)
        {
            _context.DocumentPermissions.Add(DocPermission);
            _context.SaveChanges();
            return "Document permission added";
        }

        public List<DocumentPermission> GetDocPermissions()
        {
            return _context.DocumentPermissions.ToList();
        }

        public string UpdateDocPermission(DocumentPermission DocPermission)
        {
            _context.DocumentPermissions.Update(DocPermission);
            _context.SaveChanges();
            return "Document Permission updated";
        }

        public string DeleteDocPermission(int ID)
        {
            DocumentPermission DocPermit = _context.DocumentPermissions.Find(ID);
            if (DocPermit != null)
            {
                _context.DocumentPermissions.Remove(DocPermit);
                _context.SaveChanges();
                return "Document Permission removed";
            }
            return "Document Permision not found";
        }

        //Flight CR
        public string AddFlight(Flight Flight)
        {
            _context.Flights.Add(Flight);
            _context.SaveChanges();
            return "Flight added";
        }
        public List<Flight> GetFlight()
        {
            return _context.Flights.ToList();
        }

        //Group CRUD
        public string AddGroup(Group group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
            return "Group added";
        }

        public List<Group> GetGroups()
        {
            return _context.Groups
                .Include(i => i.UserSections)
                .Include(i => i.Permissions)
                .ToList();
        }

        public string UpdateGroup(Group group)
        {
            _context.Groups.Update(group);
            _context.SaveChanges();
            return "Group updated";
        }

        public string DeleteGroup(int ID)
        {
            Group group = _context.Groups.Find(ID);
            if (group != null)
            {
                _context.Groups.Remove(group);
                _context.SaveChanges();
                return "Group removed";
            }
            return "Group not found";
        }

        //Permission CRU
        public string AddPermission(int docTypeId, int GroupId, int level)
        {
            Permission permission = new Permission();
            permission.DocTypeID = docTypeId;
            permission.GroupID = GroupId;
            permission.Level = level;
            _context.Permissions.Add(permission);
            _context.SaveChanges();
            return "Permission added";
        }

        public List<Permission> GetPermissions()
        {
            return _context.Permissions.ToList();
        }

        public string UpdatePermission(int docTypeId, int groupId, int level)
        {
            Permission permission = _context.Permissions.FirstOrDefault(i => i.DocTypeID == docTypeId & i.GroupID == groupId);
            permission.Level = level;
            _context.Permissions.Update(permission);
            _context.SaveChanges();
            return "Permission updated";
        }

        public string DeletePermission(int docTypeId, int groupId)
        {
            Permission permission = _context.Permissions.FirstOrDefault(i => i.DocTypeID == docTypeId & i.GroupID == groupId);
            if (permission == null)
            {
                return "No permission found";
            }
            _context.Permissions.Remove(permission);
            _context.SaveChanges();
            return "Permission deleted";
        }

        //Role CRUD
        public string AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return "Role added";
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public string UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return "Role updated";
        }
        public string DeleteRole(int ID)
        {
            Role role = _context.Roles.Find(ID);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
                return "Role removed";
            }
            return "Role notfound";
        }

        //User CRU
        public string AddUser(User user)
        {
            user.Deactivated = false;
            _context.Users.Add(user);
            _context.SaveChanges();
            return "User added";
        }
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public string UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return "UserUpdated";
        }

        //User Section CRU
        public string AddUserSection(UserSection userSection, string name)
        {
            userSection.Name = name;
            _context.UserSections.Add(userSection);
            _context.SaveChanges();
            return "UserSection added";
        }
        public List<UserSection> GetUserSections()
        {
            return _context.UserSections.ToList();
        }

        public string UpdateUserSection(UserSection userSection)
        {
            _context.UserSections.Update(userSection);
            _context.SaveChanges();
            return "User section updated";
        }

        //Version CR
        public int addVersion(Version version, int docID, float versionNum)
        {
            version.DocumentID = docID;
            version.VersionNum = versionNum;
            _context.Versions.Add(version);
            _context.SaveChanges();
            return version.Id;
        }

        public List<Version> GetVersions()
        {
            return _context.Versions.ToList();
        }
    }
}
