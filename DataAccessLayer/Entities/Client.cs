using DataAccessLayer.Entities.Base;

namespace DataAccessLayer.Entities
{
    public class Client : NamedEntity
    {
        public string Surname { get; set; }
        public string Patronymics { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
