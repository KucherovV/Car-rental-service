using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace DataBase
{
    public class DB
    {
        private static ApplicationDbContext context = new ApplicationDbContext();

        /// <summary>
        /// Returns entity of selected type by ID
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public  object GetEntityById<TEntity>(int id)
            where TEntity : class
        {

            return context.Set<TEntity>().Find(id);
        }

        public  object GetEntityById<TEntity>(string id)
           where TEntity : class
        {
            return context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Returns list of selected type 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public  IEnumerable<TEntity> GetList<TEntity>()
            where TEntity : class
        {
            return context.Set<TEntity>() as IEnumerable<TEntity>;
        }

        /// <summary>
        /// Saves entity if selected type
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public  void Save<TEntity>(TEntity entity)
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
        public  void Delete<TEntity>(TEntity entity)
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
        public  void Update<TEntity>(int id)
            where TEntity : class
        {
            var entity = context.Set<TEntity>().Find(id);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public  void Update<TEntity>(string id)
            where TEntity : class
        {
            var entity = context.Set<TEntity>().Find(id);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public  ApplicationDbContext GetContext()
        {
            return context;
        }

        public  object GetUsers()
        {
            return context.Users;
        }

        public  bool Contains<TEntity>(TEntity entity)
            where TEntity : class
        {
            //var entity = context.Set<TEntity>().Find(id);
            //if (entity == null)
            //    return false;
            //else
            //    return true;

            if (context.Set<TEntity>().Contains(entity))
                return true;
            else
                return false;
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


