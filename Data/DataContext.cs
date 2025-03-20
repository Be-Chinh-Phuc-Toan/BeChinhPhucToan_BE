using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Identity;

namespace BeChinhPhucToan_BE.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        //Create Database & Tables
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

        }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Badge> Badges { get; set; }
        //public DbSet<GroupChat> GroupChats { get; set; }
        //public DbSet<Message> Messages { get; set; }
        public DbSet<Setting> Settings { get; set; }
        //public DbSet<StudentNotification> StudentNotifications { get; set; }
        public DbSet<Test> Tests { get; set; }
        //public DbSet<RankedTest> RankedTests { get; set; }
        //public DbSet<RateType> RateTypes { get; set; }
        //public DbSet<RankedScore> RankedScores { get; set; }
        //public DbSet<Course> Courses { get; set; }
        //public DbSet<Chapter> Chapters { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonStatus> LessonStatuses { get; set; }
        //public DbSet<StarPoint> StarPoints { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseStatus> ExerciseStatuses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<LessonTest> LessonTests { get; set; }
        //public DbSet<Comment> Comments { get; set; }
        public DbSet<NotifyUser> NotifyUsers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<GoalStatus> GoalStatus { get; set; }
        public DbSet<Operations> Operations { get; set; }
        public DbSet<StudentBadge> StudentBadges { get; set; }
        public DbSet<StudentReward> StudentRewards { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<StudentMission> StudentMissions { get; set; }


        //public DbSet<JoinGroup> JoinGroups { get; set; }
        //public DbSet<NotifyStudent> NotifyStudents { get; set; }


        //Add & Update Timestamp Automaticlly
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var baseEntities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in baseEntities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).createdAt = now;
                }
                ((BaseEntity)entity.Entity).updatedAt = now;
            }

            var userEntities = ChangeTracker.Entries()
                .Where(x => x.Entity is User && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in userEntities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((User)entity.Entity).CreatedAt = now;
                }
                ((User)entity.Entity).UpdatedAt = now;
            }
        }
    }
}
