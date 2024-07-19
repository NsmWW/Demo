using DemoBTL.Domain.Entity;
using DemoBTL.Domain.Entity.Address.W.D.P;
using DemoBTL.Domain.Entity.Blogs;
using DemoBTL.Domain.Entity.Cerificate.Detail;
using DemoBTL.Domain.Entity.StutyStudent;
using DemoBTL.Domain.Entity.StutyStudent.Course.Detail;
using DemoBTL.Domain.Entity.StutyStudent.DoHomework;
using DemoBTL.Domain.Entity.Users.Function;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoBTL.Infastructure.DataContexts
{
    public class ApplicationDBcontext : DbContext, IApplicationDbcontext
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> option) : base(option)
        { }
        public ApplicationDBcontext() { }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        //public virtual DbSet<Blog> Blogs { get; set; }
        //public virtual DbSet<CommentBlog> CommentBlogs { get; set; }
        //public virtual DbSet<LikeBlog> LikeBlogs { get; set; }
        public virtual DbSet<CerificateType> CerificateTypes { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<ConfirmEmail> ConfirmEmails { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseSubject> CourseSubjects { get; set; }
        public virtual DbSet<LearningProgres> LearningProgres { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }    
        public virtual DbSet<SubjectDetail> SubjectDetail { get; set; }
        public virtual DbSet<RegisterStudy> RegisterStudies { get; set; }
        public virtual DbSet<Dohomework> Dohomeworks { get; set; }

        //public virtual DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRole(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(u => u.Ward).WithMany().OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(u => u.Province).WithMany().OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(u => u.District).WithMany().OnDelete(DeleteBehavior.NoAction);
            });
            //modelBuilder.Entity<LearningProgres>(entity =>
            //{
            //    entity.HasOne(u=>u.RegisterStudy).WithMany().OnDelete(DeleteBehavior.NoAction);
            //    entity.HasOne(u => u.User).WithMany().OnDelete(DeleteBehavior.Cascade);
            //    entity.HasOne(u => u.Subject).WithMany().OnDelete(DeleteBehavior.NoAction);
            //});
            //modelBuilder.Entity<RegisterStudy>(entity =>
            //{
            //    entity.HasOne(u => u.User).WithMany().OnDelete(DeleteBehavior.Cascade);
            //    entity.HasOne(u => u.Subject).WithMany().OnDelete(DeleteBehavior.NoAction);
            //});
            
        }

        private static void SeedRole(ModelBuilder builder)
        {

            #region Data thêm của role
            builder.Entity<Role>().HasData(
               new Role { RoleCode = "Admin", RoleName = "Admin", Id = 1 },
               new Role { RoleCode = "Teacher", RoleName = "Teacher", Id = 2 },
               new Role { RoleCode = "Student", RoleName = "Student", Id = 3 }
               );
            #endregion

     
            #region Data thêm của Tỉnh Province
            builder.Entity<Province>().HasData(
                new Province { Name = "Tỉnh 1", Id = 1 },
                new Province { Name = "Tỉnh 2", Id = 2 }
                );
            #endregion

            #region Data thêm của Huyện District
            builder.Entity<District>().HasData(
                new District { Id = 1, Name = "Huyện 1 của tỉnh 1", ProvinceId = 1 },
                new District { Id = 2, Name = "Huyện 2 của tỉnh 1", ProvinceId = 1 },
                new District { Id = 3, Name = "Huyện 3 của tỉnh 2", ProvinceId = 2 },
                new District { Id = 4, Name = "Huyện 4 của tỉnh 2", ProvinceId = 2 }
                );
            #endregion

            #region Data thêm của phường Ward
            builder.Entity<Ward>().HasData(
                new Ward { Id = 1, Name = "Phường 1 của huyện 1", DistrictId = 1 },
                new Ward { Id = 2, Name = "Phường 2 của huyện 2", DistrictId = 2 },
                new Ward { Id = 3, Name = "Phường 3 của huyện 3", DistrictId = 3 },
                new Ward { Id = 4, Name = "Phường 4 của huyện 4", DistrictId = 4 }
                );
            #endregion


        }
        public async Task<int> CommitChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
