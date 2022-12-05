using System.Collections.Generic;
using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Try3.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
        public List<cars> ViewCars { get; set; }
        public ICollection<orders> Orders { get; set; }
    }
    public partial class cars
    {

    /*    public cars()
        {
            this.orders = new HashSet<orders>();
        }*/
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string userId { get; set; }
        public string mark { get; set; }
        public string carNum { get; set; }
      /*  public string mark { get; set; }*/
        public virtual ApplicationUser users { get; set; }

      /*  public virtual ICollection<orders> orders { get; set; }*/
    }
    public class UserDitailView : IdentityUser
    {
        public List<orders> Orders { get; set; }
        public Nullable<int> placeId { get; set; }
        public Nullable<int> carId { get; set; }
        public string mark { get; set; }

        public int price { get; set; }
        public string Name { get; set; }
    }
    public partial class place
    {

        public place()
        {
            this.orders = new HashSet<orders>();
        }

        public int id { get; set; }
        public int price { get; set; }


        public virtual ICollection<orders> orders { get; set; }
    }
    public partial class orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string userId { get; set; }
        public Nullable<int> placeId { get; set; }
        public Nullable<int> carId { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public int quantity { get; set; }
        public string carNum { get; set; }
        public virtual cars cars { get; set; }
        public virtual place place { get; set; }
        public virtual ApplicationUser users { get; set; }

    }
/*    public partial class ViewModels
    {
        public List<orders> ViewOrders { get; set; }
        public List<cars> ViewCars { get; set; }
        public IEnumerable<place> ViewPlaces { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
       
    }*/

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       /* public DbSet<ViewModels> viewModels { get; set; }*/
        public DbSet<cars> Cars { get; set; }
        public DbSet<orders> Orders { get; set; }
        public DbSet<place> Places { get; set; }

        
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}