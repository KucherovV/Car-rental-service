using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Entities;

namespace DataBase
{
    public static class DB
    {
        private static ApplicationDbContext context = new ApplicationDbContext();

        /// <summary>
        /// Returns entity of selected type by ID
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object GetEntityById<TEntity>(int id)
            where TEntity : class
        {

            return context.Set<TEntity>().Find(id);
        }

        public static object GetEntityById<TEntity>(string id)
           where TEntity : class
        {
            return context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Returns list of selected type 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TEntity> GetList<TEntity>()
            where TEntity : class
        {
            return context.Set<TEntity>() as IEnumerable<TEntity>;
        }

        /// <summary>
        /// Saves entity if selected type
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public static void Save<TEntity>(TEntity entity)
            where TEntity : class
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes entity of selected type
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public static void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Updates the entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public static void Update<TEntity>(int id)
            where TEntity : class
        {
            var entity = context.Set<TEntity>().Find(id);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static void Update<TEntity>(string id)
            where TEntity : class
        {
            var entity = context.Set<TEntity>().Find(id);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static ApplicationDbContext GetContext()
        {
            return context;
        }

        public static IEnumerable<ApplicationUser> GetUsers()
        {
            return context.Users;
        }
    }


    //public static void AsignRoleWithUser(string roleName, ApplicationUser user)
    //{
    //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
    //    //var manager = new ApplicationUserManager(new UserStore<Entities.ApplicationUser>(context.Get<ApplicationDbContext>()));
    //    var userRoles = new List<IdentityUserRole>();
    //    //var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));




    //    string roleID = context.Roles.SingleOrDefault(r => r.Name == roleName).Id;

    //    IdentityUserRole userRole = new IdentityUserRole
    //    {
    //        RoleId = roleID,
    //        UserId = user.Id
    //    };

    //    context.R


    //}
}


