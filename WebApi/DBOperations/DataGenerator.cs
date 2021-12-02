using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                if(context.Books.Any()){
                    return;
                }
                context.Books.AddRange(
                    new Book{
              Id=1,
              Title="Lord of the Rings",
              GenreId=2,
              PageCount=400,
              PublishDate=new DateTime(2001,06,12)
          },
           new Book{
              Id=2,
              Title="Eylül",
              GenreId=4,
              PageCount=260,
              PublishDate=new DateTime(1980,06,12)
          }   
                );
                context.SaveChanges();
            }
        }
    }
}