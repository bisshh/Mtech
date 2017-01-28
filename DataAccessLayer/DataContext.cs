using CoreLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataContext : DbContext
    {
        public DataContext() : base("College") { }

        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<EmployeePersonalInfo> employeePersonalInfo { get; set; }
        public DbSet<Student> student { get; set; }
        public DbSet<StudentPersonalInfo> studentPersonalInfo { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<Level> level { get; set; }
        public DbSet<Enrollment> enrollment { get; set; }
        public DbSet<User> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            CreateTableUser(modelBuilder);
            CreateTableDepartment(modelBuilder);
            CreateTableLevel(modelBuilder);
            CreateTableCourse(modelBuilder);
            CreateTableEmployee(modelBuilder);
            CreateTableEmployeePersonalInfo(modelBuilder);
            CreateTableStudent(modelBuilder);
            CreateTableStudentPersonalInfo(modelBuilder);
            CreateTableEnrollment(modelBuilder);
        }
        private static void CreateTableUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasKey(x => x.UserID);
            modelBuilder.Entity<User>().Property(x => x.UserID).HasColumnName("UserID");
            modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("Username");
            modelBuilder.Entity<User>().Property(x => x.Role).HasColumnName("Role");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("Password");
            modelBuilder.Entity<User>().Property(x => x.InsertedDate).HasColumnName("CreatedDate");
            modelBuilder.Entity<User>().Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");
        }

        private static void CreateTableDepartment(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Department>().HasKey(x => x.DepartmentID);
            modelBuilder.Entity<Department>().Property(x => x.DepartmentID).HasColumnName("DepartmentID");
            modelBuilder.Entity<Department>().Property(x => x.DepartmentName).HasColumnName("DepartmentName");
            modelBuilder.Entity<Department>().Property(x => x.Description).HasColumnName("Description");
            modelBuilder.Entity<Department>().Property(x => x.EstdDate).HasColumnName("EstdDate");
            modelBuilder.Entity<Department>().Property(x => x.DepartmentHead).HasColumnName("DepartmentHead");
            modelBuilder.Entity<Department>().Property(x => x.InsertedDate).HasColumnName("CreatedDate");
            modelBuilder.Entity<Department>().Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");
        }

        private static void CreateTableEmployee(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("employee");
            modelBuilder.Entity<Employee>().HasKey(x => x.EmployeeID);
            modelBuilder.Entity<Employee>().Property(x => x.EmployeeID).HasColumnName("EmployeeID");
            modelBuilder.Entity<Employee>().Property(x => x.UserID).HasColumnName("UserID");
            modelBuilder.Entity<Employee>().Property(x => x.DepartmentID).HasColumnName("DepartmentID");
            modelBuilder.Entity<Employee>().Property(x => x.FirstName).HasColumnName("FirstName");
            modelBuilder.Entity<Employee>().Property(x => x.MiddleName).HasColumnName("MiddleName");
            modelBuilder.Entity<Employee>().Property(x => x.LastName).HasColumnName("LastName");
            modelBuilder.Entity<Employee>().Property(x => x.Email).HasColumnName("Email");
            modelBuilder.Entity<Employee>().Property(x => x.InsertedDate).HasColumnName("CreatedDate");
            modelBuilder.Entity<Employee>().Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");
            // Creating relation with Employee and Department table
            modelBuilder.Entity<Employee>()
                    .HasRequired<Department>(x => x.Department)
                    .WithMany(y => y.Employee)
                    .HasForeignKey(x => x.DepartmentID);
        }

        private static void CreateTableEmployeePersonalInfo(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeePersonalInfo>().ToTable("employeePersonalInfo");
            modelBuilder.Entity<EmployeePersonalInfo>().HasKey(x => x.EmployeeID);
            modelBuilder.Entity<EmployeePersonalInfo>().Property(x => x.EmployeeID).HasColumnName("Employee ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<EmployeePersonalInfo>().Property(x => x.Phone).HasColumnName("Phone");
            modelBuilder.Entity<EmployeePersonalInfo>().Property(x => x.Address).HasColumnName("Address");
            modelBuilder.Entity<EmployeePersonalInfo>().Property(x => x.Position).HasColumnName("Position");
            modelBuilder.Entity<EmployeePersonalInfo>().Property(x => x.Gender).HasColumnName("Gender");
            modelBuilder.Entity<EmployeePersonalInfo>().Property(x => x.HiredDate).HasColumnName("HiredDate");
           // modelBuilder.Entity<EmployeePersonalInfo>().Property(x => x.ResignationDate).HasColumnName("ResignationDate");
            modelBuilder.Entity<EmployeePersonalInfo>().Property(x => x.InsertedDate).HasColumnName("InsertedDate");
            modelBuilder.Entity<EmployeePersonalInfo>().Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");
            // Creating one to one relation with employee and employee personal info
            modelBuilder.Entity<Employee>()
                .HasRequired(x => x.EmployeePersonalInfo)
                .WithRequiredPrincipal(y => y.Employee);
        }

        private static void CreateTableStudent(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("student");
            modelBuilder.Entity<Student>().HasKey(x => x.StudentID);
            modelBuilder.Entity<Student>().Property(x => x.StudentID).HasColumnName("StudentID");
            modelBuilder.Entity<Student>().Property(x => x.DepartmentID).HasColumnName("DepartmentID");
            modelBuilder.Entity<Student>().Property(x => x.LevelID).HasColumnName("LevelID");
            modelBuilder.Entity<Student>().Property(x => x.FirstName).HasColumnName("FirstName");
            modelBuilder.Entity<Student>().Property(x => x.MiddleName).HasColumnName("MiddleName");
            modelBuilder.Entity<Student>().Property(x => x.LastName).HasColumnName("LastName");
            // Creating relation with Student and Department table
            modelBuilder.Entity<Student>()
                .HasRequired<Department>(x => x.Department)
                .WithMany(y => y.Student)
                .HasForeignKey(x => x.DepartmentID);

            // Creating relation with Student and Level table
            modelBuilder.Entity<Student>()
                .HasRequired<Level>(x => x.Level)
                .WithMany(y => y.Student)
                .HasForeignKey(x => x.LevelID);
        }

        private static void CreateTableStudentPersonalInfo(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentPersonalInfo>().ToTable("studentPersonalInfo");
            modelBuilder.Entity<StudentPersonalInfo>().HasKey(x => x.StudentID);
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.StudentID).HasColumnName("Student ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.Address).HasColumnName("Address");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.Contact).HasColumnName("Contact");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.GuardianName).HasColumnName("GuardianName");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.GuardianContact).HasColumnName("GuardianContact");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.GuardianRelation).HasColumnName("GuardianRelation");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.DateOfBirth).HasColumnName("DateOfBirth");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.Email).HasColumnName("Email");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.Gender).HasColumnName("Gender");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.Semester).HasColumnName("Semester");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.AdmissionDate).HasColumnName("AdmissionDate");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.CitizenshipNumber).HasColumnName("CitizenshipNumber");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.InsertedDate).HasColumnName("InsertedDate");
            modelBuilder.Entity<StudentPersonalInfo>().Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");
            // One to one relation with student and student personal info
            modelBuilder.Entity<Student>()
                .HasRequired(x => x.StudentPersonalInfo)
                .WithRequiredPrincipal(y => y.Student);
        }

        private static void CreateTableCourse(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("course");
            modelBuilder.Entity<Course>().HasKey(x => x.CourseID);
            modelBuilder.Entity<Course>().Property(x => x.CourseID).HasColumnName("CourseID");
            modelBuilder.Entity<Course>().Property(x => x.DepartmentID).HasColumnName("DepartmentID");
            modelBuilder.Entity<Course>().Property(x => x.LevelID).HasColumnName("LevelID");
            modelBuilder.Entity<Course>().Property(x => x.Name).HasColumnName("CourseName");
            modelBuilder.Entity<Course>().Property(x => x.Duration).HasColumnName("CourseDuration");
            modelBuilder.Entity<Course>().Property(x => x.Credit).HasColumnName("Credit");
            modelBuilder.Entity<Course>().Property(x => x.Description).HasColumnName("Description");
            modelBuilder.Entity<Course>().Property(x => x.InsertedDate).HasColumnName("CreatedDate");
            modelBuilder.Entity<Course>().Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");

            // Creating relation with Course and Department table
            modelBuilder.Entity<Course>()
                .HasRequired<Department>(x => x.Department)
                .WithMany(y => y.Course)
                .HasForeignKey(x => x.DepartmentID);

            // Creating relation with Course and Level table
            modelBuilder.Entity<Course>()
                .HasRequired<Level>(x => x.Level)
                .WithMany(y => y.Course)
                .HasForeignKey(x => x.LevelID);
        }

        private static void CreateTableLevel(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>().ToTable("level");
            modelBuilder.Entity<Level>().HasKey(x => x.LevelID);
            modelBuilder.Entity<Level>().Property(x => x.LevelID).HasColumnName("LevelID");
            modelBuilder.Entity<Level>().Property(x => x.Year).HasColumnName("Year");
            modelBuilder.Entity<Level>().Property(x => x.Semester).HasColumnName("Semester");
            modelBuilder.Entity<Level>().Property(x => x.Description).HasColumnName("CourseDescription");
            modelBuilder.Entity<Level>().Property(x => x.InsertedDate).HasColumnName("CreatedDate");
            modelBuilder.Entity<Level>().Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");
        }

        private static void CreateTableEnrollment(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>().ToTable("enrollment");
            modelBuilder.Entity<Enrollment>().HasKey(x => x.EnID);
            modelBuilder.Entity<Enrollment>().Property(x => x.EnID).HasColumnName("EnID");
            modelBuilder.Entity<Enrollment>().Property(x => x.StudentID).HasColumnName("StudentID");
            modelBuilder.Entity<Enrollment>().Property(x => x.CourseID).HasColumnName("CourseID");
            modelBuilder.Entity<Enrollment>().Property(x => x.EnrolledDate).HasColumnName("EnrolledDate");
            modelBuilder.Entity<Enrollment>().Property(x => x.InsertedDate).HasColumnName("CreatedDate");
            modelBuilder.Entity<Enrollment>().Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
