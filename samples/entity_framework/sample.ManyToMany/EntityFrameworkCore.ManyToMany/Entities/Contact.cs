using System.Collections.Generic;

namespace EntityFrameworkCore.ManyToMany.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ContactSkill> Skills { get; set; } = new List<ContactSkill>();
    }
}