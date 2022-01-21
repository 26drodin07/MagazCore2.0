using CodeFirst;
using CodeFirst.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazCore2._0.Repos
{
    public class CartRepo : ICartRepo
    {
        private readonly DataContext db;

        public CartRepo(DataContext db)
        {
            this.db = db;
        }

        public async Task AddprodAsync(string user_id, int prod_id)
        {
            Cart cart = (await db.Users.FindAsync(user_id)).Cart;
            Product product = await db.Products.FindAsync(prod_id);
            cart.Products.Add(product);
            db.Update(cart);
            await db.SaveChangesAsync();
        }
        public async Task ClearCartAsync(string user_id)
        {
            Cart cart = (await db.Users.FindAsync(user_id)).Cart;
            cart.Products.Clear();
            db.Carts.Update(cart);
            await db.SaveChangesAsync();
        }



    }
}
