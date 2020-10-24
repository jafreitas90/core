using System;
using EntityFrameworkCore.ManyToMany.Entities;
using EntityFrameworkCore.ManyToMany.Entities.Enums;
using EntityFrameworkCore.ManyToMany.EntityConfigurations;

namespace EntityFrameworkCore.ManyToMany
{
    internal class Program
    {
        private static void CreateAllEntitiesAtOnce()
        {
            using (var context = new ApplicationDbContext())
            {
                var skill1 = new Skill() { Name = "Leadership" };
                var skill2 = new Skill() { Name = "Management skills" };

                // Create two new contacts
                var contact1 = new Contact() { FirstName = "Jorge", LastName = "Freitas" };
                var contact2 = new Contact() { FirstName = "Filipe", LastName = "Marques" };

                contact1.Skills.Add(new ContactSkill(ExpertiseLevel.Expert, skill1, contact1));
                contact1.Skills.Add(new ContactSkill(ExpertiseLevel.Intermediate, skill2, contact1));

                contact2.Skills.Add(new ContactSkill(ExpertiseLevel.Novice, skill2, contact2));

                // Add contacts to DB
                context.Contacts.Add(contact1);
                context.Contacts.Add(contact2);

                // Save to db
                context.SaveChanges();
            }
        }

        private static void CreateEntitiesSeparated()
        {
            using (var context = new ApplicationDbContext())
            {
                #region Create skills and add to DB

                var skill1 = new Skill() { Name = "Active listening" };
                var skill2 = new Skill() { Name = "Communication" };
                var skill3 = new Skill() { Name = "Customer service" };

                context.Skills.Add(skill1);
                context.Skills.Add(skill2);
                context.Skills.Add(skill3);
                context.SaveChanges();

                #endregion Create skills and add to DB

                #region Create contacts and add to DB

                var contact1 = new Contact() { FirstName = "Cristiano", LastName = "Ronaldo" };
                var contact2 = new Contact() { FirstName = "Lionel ", LastName = "Messi" };

                context.Contacts.Add(contact1);
                context.Contacts.Add(contact2);
                context.SaveChanges();

                #endregion Create contacts and add to DB

                #region Create Link Between Contact and Skills

                contact1.Skills.Add(new ContactSkill(ExpertiseLevel.Expert, skill2.Id, contact1.Id));
                contact1.Skills.Add(new ContactSkill(ExpertiseLevel.Intermediate, skill3.Id));
                context.SaveChanges();

                #endregion Create Link Between Contact and Skills
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine($"########## Many to Many relationships with entity framework ##########");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. Create all entities before save on Database");
            Console.WriteLine("2. Create entity and save immediately");
            Console.Write("Please select an option: ");

            var result = Console.ReadKey();
            Console.WriteLine($"{Environment.NewLine}");

            switch (result.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    // create all entities before save on Database
                    CreateEntitiesSeparated();
                    Console.WriteLine("Please Check DB");
                    Console.ReadKey();
                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    // create entity and save immediately, just to show that we can create the link between the entities whenever we want
                    CreateAllEntitiesAtOnce();
                    Console.WriteLine("Please Check DB");
                    Console.ReadKey();
                    break;
            }
        }
    }
}