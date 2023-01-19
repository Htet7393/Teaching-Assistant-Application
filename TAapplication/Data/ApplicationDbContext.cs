

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TAapplication.Areas.Data;
using TAapplication.Models;
using System;
using System.Linq;

namespace TAapplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<TAUser>
    {
        public IHttpContextAccessor _httpContextAccessor;
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor http)
            : base(options)
        {
            _httpContextAccessor = http;
            
        }
  
        public async Task InitializeUsers(UserManager<TAUser> u, RoleManager<IdentityRole> r, ApplicationDbContext db_context)
        {
            

            Database.Migrate();
            if(await r.FindByNameAsync("Administrator") == null)
                await r.CreateAsync(new IdentityRole("Administrator"));
            if (await r.FindByNameAsync("Professor") == null)
                await r.CreateAsync(new IdentityRole("Professor"));
            if (await r.FindByNameAsync("Applicant") == null)
                await r.CreateAsync(new IdentityRole("Applicant"));

            string[] csv = System.IO.File.ReadAllLines(@"..\\TAapplication\temp.csv");
            for (int i = 1; i < csv.Length; i++)
            {

                var enr = new Enrollment();
                enr.course_date = csv[i];
                int enr_size = db_context.Enrollment.ToArray().Length;
                if (enr_size < 14)
                {
                    await db_context.Enrollment.AddAsync(enr);
                    await db_context.SaveChangesAsync();
                }
            }


            TAUser user1 = new()
            {
                Unid = "u1234567",
                UserName = "admin@utah.edu",
                Email = "admin@utah.edu",
                EmailConfirmed = true,
                ReferredTo = "Staff",
                Name = "John"
            };           
            await u.CreateAsync(user1, "123ABC!@#def");
            await u.AddToRoleAsync(user1, "Administrator");

            var avail = new Availability();
            avail.User = user1;
            avail.Monday = "000000000000000000000000000000000000000000000000";
            avail.Tuesday = "111111111111111111111111111111111111111111111111";
            avail.Wednesday = "000000000000000000000000000000000000000000000000";
            avail.Thursday = "000000000000000000000000000000000000000000000000";
            avail.Friday = "000000000000000000000000000000000000000000000000";
            int avail_size = db_context.Availability.ToArray().Length;
            if (avail_size == 0)
            {

                await db_context.Availability.AddAsync(avail);
                await db_context.SaveChangesAsync();
            }


            TAUser user2 = new()
            {
                Unid = "u7654321",
                UserName = "professor@utah.edu",
                Email = "professor@utah.edu",
                EmailConfirmed = true,
                ReferredTo = "Prof",
                Name = "Luke"
            };          
            await u.CreateAsync(user2, "123ABC!@#def");
            await u.AddToRoleAsync(user2, "Professor");

            var avail2 = new Availability();
            avail2.User = user2;
            avail2.Monday = "111111111111111111111111111111111111111111111111";
            avail2.Tuesday = "000000000000000000000000000000000000000000000000";
            avail2.Wednesday = "000000000000000000000000000000000000000000000000";
            avail2.Thursday = "000000000000000000000000000000000000000000000000";
            avail2.Friday = "000000000000000000000000000000000000000000000000";
            int avail_size2 = db_context.Availability.ToArray().Length;
            if (avail_size2 == 1)
            {

                await db_context.Availability.AddAsync(avail2);
                await db_context.SaveChangesAsync();
            }

            TAUser user3 = new()
            {
                Unid = "u0000000",
                UserName = "u0000000@utah.edu",
                Email = "u0000000@utah.edu",
                EmailConfirmed = true,
                ReferredTo = "Prof",
                Name = "Nate"
            };          
            await u.CreateAsync(user3, "123ABC!@#def");
            await u.AddToRoleAsync(user3, "Applicant");

            var avail3 = new Availability();
            avail3.User = user3;
            avail3.Monday = "111111111111111100000000000000000000000000000000";
            avail3.Tuesday = "000000000000000011111111111111111111000000000000";
            avail3.Wednesday = "000000000000000000000000000000000000000000000000";
            avail3.Thursday = "000000000000000011111111111111111111000000000000";
            avail3.Friday = "111111111111111100000000000000000000000000000000";
            int avail_size3 = db_context.Availability.ToArray().Length;
            if (avail_size3 == 2)
            {

                await db_context.Availability.AddAsync(avail3);
                await db_context.SaveChangesAsync();
            }


            var app = new Application();
            app.User = user3;
            app.Pursuing = Pursuing.BS;
            app.Department = "CS";
            app.GPA = (float)3.6;
            app.Hours = 8;
            app.Availability = true;
            app.CompletedSemester = 4;
            app.PersonalStatement = "I am a good student";
            app.TransferSchool = "University of Northern South";
            app.LinkedIn = "https://www.goog.com";
            app.Resume = "https://www.resume.com";
            int app_size = db_context.Application.ToArray().Length;
            if (app_size == 0) 
            {
                await db_context.Application.AddAsync(app);
                await db_context.SaveChangesAsync();
            }

            TAUser user4 = new()
            {
                Unid = "u0000001",
                UserName = "u0000001@utah.edu",
                Email = "u0000001@utah.edu",
                EmailConfirmed = true,
                ReferredTo = "Applicant",
                Name = "Ethan"
            };
            if (await u.FindByEmailAsync("u0000001@utah.edu") == null)
            {
                await u.CreateAsync(user4, "123ABC!@#def");
                await u.AddToRoleAsync(user4, "Applicant");
            }
           
            var app2 = new Application();          
            app2.User = user4;
            app2.Pursuing = Pursuing.BS;
            app2.Department = "EAE";
            app2.GPA = (float)3.65;
            app2.Hours = 7;
            app2.Availability = false;
            app2.CompletedSemester = 4;
            app2.PersonalStatement = "I am a great student";
            app2.TransferSchool = "University of Eastern West";
            int app_size2 = db_context.Application.ToArray().Length;
            if (app_size2 == 1) 
            {
                await db_context.Application.AddAsync(app2);
                await db_context.SaveChangesAsync();
            }

            var avail4 = new Availability();
            avail4.User = user4;
            avail4.Monday = "000000000000000000000000000000000000000000000000";
            avail4.Tuesday = "111111111111111111111111111111111111111111111111";
            avail4.Wednesday = "000000000000000000000000000000000000000000000000";
            avail4.Thursday = "000000000000000000000000000000000000000000000000";
            avail4.Friday = "000000000000000000000000000000000000000000000000";
            int avail_size4 = db_context.Availability.ToArray().Length;
            if (avail_size4 == 3)
            {
                await db_context.Availability.AddAsync(avail4);
                await db_context.SaveChangesAsync();
            }



            TAUser user5 = new()
            {
                Unid = "u0000002",
                UserName = "u0000002@utah.edu",
                Email = "u0000002@utah.edu",
                EmailConfirmed = true,
                ReferredTo = "Apl",
                Name = "Mike"
            };      
            await u.CreateAsync(user5, "123ABC!@#def");
            await u.AddToRoleAsync(user5, "Applicant");

            var avail5 = new Availability();
            avail5.User = user4;
            avail5.Monday = "000000000000000000000000000000000000000000000000";
            avail5.Tuesday = "111111111111111111111111111111111111111111111111";
            avail5.Wednesday = "000000000000000000000000000000000000000000000000";
            avail5.Thursday = "000000000000000000000000000000000000000000000000";
            avail5.Friday = "000000000000000000000000000000000000000000000000";
            int avail_size5 = db_context.Availability.ToArray().Length;
            if (avail_size5 == 4)
            {
                await db_context.Availability.AddAsync(avail5);
                await db_context.SaveChangesAsync();
            }



            var course = new Course();
            course.Semester = Semester.Spring;
            course.Year = Year.twothree;
            course.Title = "CS";
            course.CourseNumber = "1400";
            course.Section = "002";
            course.Description = "Introduction to Programming";
            course.ProfessorUnid = "u0213657";
            course.ProfessorName = "Perez";
            course.TimeAndDay = TimeAndDay.A;
            course.Location = "WEB210";
            course.Credit = 3;
            course.Enrollment = 100;
            course.Note = "Inro class";          
            int course_size = db_context.Course.ToArray().Length;
            if(course_size == 0)
            {
                await db_context.Course.AddAsync(course);
                await db_context.SaveChangesAsync();
            }

            var course2 = new Course();
            course2.Semester = Semester.Spring;
            course2.Year = Year.twothree;
            course2.Title = "CS";
            course2.CourseNumber = "2400";
            course2.Section = "001";
            course2.Description = "Algorithm";
            course2.ProfessorUnid = "u0363657";
            course2.ProfessorName = "Mary";
            course2.TimeAndDay = TimeAndDay.E;
            course2.Location = "MEB111";
            course2.Credit = 4;
            course2.Enrollment = 120;
            course2.Note = "Need more TAs";
            int course_size2 = db_context.Course.ToArray().Length;
            if (course_size2 == 1)
            {
                await db_context.Course.AddAsync(course2);
                await db_context.SaveChangesAsync();
            }

            var course3 = new Course();
            course3.Semester = Semester.Spring;
            course3.Year = Year.twothree;
            course3.Title = "CS";
            course3.CourseNumber = "3400";
            course3.Section = "004";
            course3.Description = "Database";
            course3.ProfessorUnid = "u1373667";
            course3.ProfessorName = "Elton";
            course3.TimeAndDay = TimeAndDay.B;
            course3.Location = "WEB102";
            course3.Credit = 3;
            course3.Enrollment = 115;
            course3.Note = "Hard class";
            int course_size3 = db_context.Course.ToArray().Length;
            if (course_size3 == 2)
            {
                await db_context.Course.AddAsync(course3);
                await db_context.SaveChangesAsync();
            }

            var course4 = new Course();
            course4.Semester = Semester.Spring;
            course4.Year = Year.twothree;
            course4.Title = "CS";
            course4.CourseNumber = "4400";
            course4.Section = "004";
            course4.Description = "Database";
            course4.ProfessorUnid = "u6364657";
            course4.ProfessorName = "Parker";
            course4.TimeAndDay = TimeAndDay.B;
            course4.Location = "MEB301";
            course4.Credit = 4;
            course4.Enrollment = 115;
            course4.Note = "Elective";
            int course_size4 = db_context.Course.ToArray().Length;
            if (course_size4 == 3)
            {
                await db_context.Course.AddAsync(course4);
                await db_context.SaveChangesAsync();
            }

            var course5 = new Course();
            course5.Semester = Semester.Spring;
            course5.Year = Year.twothree;
            course5.Title = "CS";
            course5.CourseNumber = "4400";
            course5.Section = "004";
            course5.Description = "Database";
            course5.ProfessorUnid = "u6364657";
            course5.ProfessorName = "Parker";
            course5.TimeAndDay = TimeAndDay.C;
            course5.Location = "WEB151";
            course5.Credit = 4;
            course5.Enrollment = 115;
            int course_size5 = db_context.Course.ToArray().Length;
            if (course_size5 == 4)
            {
                await db_context.Course.AddAsync(course5);
                await db_context.SaveChangesAsync();
            }          
        }



        /// <summary>
        /// Every time Save Changes is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()  // JIM: Override save changes to add timestamps
        {
            AddTimestamps();
            return base.SaveChanges();
        }
        /// <summary>
        /// Every time Save Changes (Async) is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            AddTimestamps();   // JIM: Override save changes async to add timestamps
            return await base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is ModificationTracking
                        && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = "";

            if (_httpContextAccessor.HttpContext == null) // happens during startup/initialization code
            {
                currentUsername = "DBSeeder";
            }
            else
            {
                currentUsername = _httpContextAccessor.HttpContext.User.Identity?.Name;
            }

            currentUsername ??= "Sadness"; // JIM: compound assignment magic... test for null, and if so, assign value

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((ModificationTracking)entity.Entity).CreationDate = DateTime.UtcNow;
                    ((ModificationTracking)entity.Entity).CreatedBy = currentUsername;
                }
                ((ModificationTracking)entity.Entity).ModificationDate = DateTime.UtcNow;
                ((ModificationTracking)entity.Entity).ModifiedBy = currentUsername;
            }
        }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        public DbSet<TAapplication.Models.Application> Application { get; set; }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        public DbSet<TAapplication.Models.Course> Course { get; set; }
        public DbSet<TAapplication.Models.Availability> Availability { get; set; }
        public DbSet<TAapplication.Models.Enrollment> Enrollment { get; set; }


    }   
}