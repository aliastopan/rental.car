using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Common;
namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        { }

        public DbSet<User>? Users { get; set; }
        public DbSet<Car>? Cars { get; set; }
        public DbSet<Order>? Orders { get; set; }

        public (Car, bool) GetCar(int carId)
        {
            var result = this.Cars!
                .FirstOrDefault(car =>
                    carId == car.Id);

            return (result, result is not null)!;
        }

        public async Task UpdateCarAsync(Car car)
        {
            this.Cars!.Update(car);
            await this.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            this.Orders!.Update(order);
            await this.SaveChangesAsync();
        }

        public async Task InserOrderAsync(Order order)
        {
            this.Orders!.Add(order);
            await this.SaveChangesAsync();
        }

        public async Task InsertUserAsync(User user)
        {
            user.Password = Cryptograph.GetHash(user.Password!);

            this.Users!.Add(user);
            await this.SaveChangesAsync();
        }

        public (User, bool) LookUpUser(string username, string password)
        {
            string hash = Cryptograph.GetHash(password);

            var result = this.Users!
                .FirstOrDefault(user =>
                    username == user.Username &&
                    hash == user.Password);

            return (result, result is not null)!;
        }
    }
}