using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MrSimonAcademy2.Models
{
    // Модель пользователя
    public class User : IdentityUser
    {
        public string UserFName { get; set; }
        public string UserLName { get; set; }
        public DateTime Birthday { get; set; }

    }

    // Модель студента. Студент принадлежит какой-то ОДНОЙ группе 
    public class Student 
    {
        public int Id { get; set; }


        public int? GroupId { get; set; }
        public Group StudentGroup { get; set; }
    }

    // Модель преподавателей. У преподавателя несколько групп
    public class Teacher 
    {
        public int Id { get; set; }

        public ICollection<Group> TeacherGroups { get; set; }
        public Teacher()
        {
            TeacherGroups = new List<Group>();
        }

    }

    // Модель групп. У группы 1 преподаватель и несколько студентов
    public class Group
    {
        public int Id { get; set; }

        public string GroupName { get; set; }
        public int GroupLevel { get; set; }

        public int? TeacherId { get; set; }
        public Teacher GroupTeacher { get; set; }

        public ICollection<Student> GroupStudents { get; set; }
        public Group()
        {
            GroupStudents = new List<Student>();
            GroupTeacher = new Teacher();
        }

    }

    // Контекст даных для работы с пользователями   
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() :
            base("IdentityDb")
        { }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Student { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



    }
    // Менеджер для более удобной работы с пользователями
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
                                    IOwinContext context)
        {
            ApplicationDbContext db = context.Get<ApplicationDbContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<User>(db));
            return manager;
        }

    }


    // TODO: Доделать https://www.youtube.com/watch?v=EFX7GzZ8G1E&list=PLL-k0Ff5RfqXnwdDG61WqZ2j3KXUPnfmq&index=62

    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<User>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var roleAdmin = new IdentityRole { Name = "admin" };
            var roleTeacher = new IdentityRole { Name = "teacher" };
            var roleStudent = new IdentityRole { Name = "student" };

            // добавляем роли в бд
            roleManager.Create(roleAdmin);
            roleManager.Create(roleTeacher);
            roleManager.Create(roleStudent);

            // создаем пользователей
            var admin = new User { Email = "Mr.SimonAcademy@mail.ru", UserLName = "Simon", UserFName = "Simon", Birthday = new DateTime(2020,9,1)  };
            string password = "simon123";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, roleAdmin.Name);
                userManager.AddToRole(admin.Id, roleTeacher.Name);
                userManager.AddToRole(admin.Id, roleStudent.Name);
            }

            base.Seed(context);
        }
    }
}