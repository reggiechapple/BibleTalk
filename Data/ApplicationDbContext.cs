using System;
using System.Threading;
using System.Threading.Tasks;
using BibleTalk.Data.Entities;
using BibleTalk.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibleTalk.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public string CurrentUserId { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Channel> BibleTalk { get; set; }
        public DbSet<BibleTalkubscriber> BibleTalkubscribers { get; set; }
        public DbSet<ChannelMessage> ChannelMessages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follow> Follows { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<BibleTalkubscriber>(chanMem =>
            {
                chanMem.HasKey(cm => new { cm.SubscriberId, cm.ChannelId });

                chanMem.HasOne(cm => cm.Subscriber)
                    .WithMany(m => m.Subscriptions)
                    .HasForeignKey(cm => cm.SubscriberId)
                    .IsRequired();

                chanMem.HasOne(cm => cm.Channel)
                    .WithMany(c => c.Subscribers)
                    .HasForeignKey(cm => cm.ChannelId)
                    .IsRequired();
            });

            builder.Entity<UserNotification>(notif =>
            {
                notif.HasKey(un => new { un.MemberId, un.NotificationId });

                notif.HasOne(un => un.Member)
                    .WithMany(m => m.Notifications)
                    .HasForeignKey(un => un.MemberId)
                    .IsRequired();

                notif.HasOne(un => un.Notification)
                    .WithMany(n => n.Users)
                    .HasForeignKey(un => un.NotificationId)
                    .IsRequired();
            });

            builder.Entity<Follow>(follow =>
            {
                follow.HasKey(f => new { f.FollowedId, f.FollowerId });

                follow.HasOne(f => f.Followed)
                    .WithMany(f => f.Follows)
                    .HasForeignKey(f => f.FollowedId)
                    .IsRequired();

                follow.HasOne(f => f.Follower)
                    .WithMany(f => f.Following)
                    .HasForeignKey(f => f.FollowerId)
                    .IsRequired();
            });

            builder.SeedAdmin();
        }

        public override int SaveChanges()
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = changedEntity.Entity as Entity;
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.Created = DateTime.Now;
                        entity.Updated = DateTime.Now;
                        
                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.Updated = DateTime.Now;
                    }
                }

            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = changedEntity.Entity as Entity;
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.Created = DateTime.Now;
                        entity.Updated = DateTime.Now;
                        
                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.Updated = DateTime.Now;
                    }
                }
            }
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
        
    }
}