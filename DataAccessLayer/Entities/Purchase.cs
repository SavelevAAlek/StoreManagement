using DataAccessLayer.Entities.Base;

namespace DataAccessLayer.Entities
{
    public class Purchase : Entity
    {
        public Client Client { get; set; }
        public Product Product { get; set; }
    }
}
