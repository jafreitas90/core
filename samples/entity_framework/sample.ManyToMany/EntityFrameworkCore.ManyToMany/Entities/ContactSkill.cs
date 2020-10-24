using EntityFrameworkCore.ManyToMany.Entities.Enums;

namespace EntityFrameworkCore.ManyToMany.Entities
{
    public class ContactSkill
    {
        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        public ExpertiseLevel Level { get; set; }

        private ContactSkill()
        {
        }

        public ContactSkill(ExpertiseLevel expertiseLevel, int skillId)
        {
            Level = expertiseLevel;
            SkillId = skillId;
        }

        public ContactSkill(ExpertiseLevel expertiseLevel, int skillId, int contactId)
        {
            Level = expertiseLevel;
            SkillId = skillId;
            ContactId = contactId;
        }

        public ContactSkill(ExpertiseLevel expertiseLevel, Skill skill, Contact contact)
        {
            Level = expertiseLevel;
            Skill = skill;
            Contact = contact;
        }
    }
}