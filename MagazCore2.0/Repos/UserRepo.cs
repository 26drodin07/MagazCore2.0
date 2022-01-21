using CodeFirst;
using CodeFirst.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazCore2._0.Repos
{
    public class UserRepo
    {
        private readonly DataContext db;

        public UserRepo(DataContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(User user)
        {
            //user.Cart = new Cart();
            await db.AddAsync(user);
            await db.SaveChangesAsync(); 

        }

        public async Task UpdateAsync(User user)
        {           
                db.Update(user);
                await db.SaveChangesAsync();          
        }

        public async Task DeleteAsync(User user)
        {                       
                db.Users.Remove(user);               
                await db.SaveChangesAsync();            
        }
        
        public async Task<User> FindByIdAsync(string userId)
        {
            return await db.Users.FindAsync(userId);
        }
       
        public  User FindByName(string userName)
        {
            return db.Users.FirstOrDefault(u => u.username == userName);
        }


    }
}
