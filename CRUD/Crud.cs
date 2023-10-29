using FlightDocV1._1.Data;
using FlightDocV1._1.Models;
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
            _context.DocTypes.Add(type);
            _context.SaveChanges();
            return "Document type added";
        }

        public List<DocType> GetDocType ()
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
        public string AddDocument(Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();
            return "Document added";
        }

        public List<Document> GetDocuments()
        {
            return _context.Documents.ToList();
        }

        public string UpdateDocument(Document document)
        {
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

        //Flight R
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
            return _context.Groups.ToList();
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
        public string AddPermission(Permission permission)
        {
            _context.Permissions.Add(permission);
            _context.SaveChanges();
            return "Permission added";
        }

        public List<Permission> GetPermissions()
        {
            return _context.Permissions.ToList();
        }

        public string UpdatePermission(Permission permission)
        {
            _context.Permissions.Update(permission);
            _context.SaveChanges();
            return "Permission updated";
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

        //User RU
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

        //User Section RU
        public List<UserSection> GetUserSections()
        {
            return _context.UsersSection.ToList();
        }

        public string UpdateUserSection(UserSection userSection)
        {
            _context.UsersSection.Update(userSection);
            _context.SaveChanges();
            return "User section updated";
        }

        //Version CR
        public string addVersion(Version version)
        {
            _context.Version.Add(version);
            _context.SaveChanges();
            return "New version added";
        }

        public List<Version> GetVersions()
        {
            return _context.Version.ToList();
        }
    }
}
