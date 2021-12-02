using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        public void Handle()
        {
            Book book = _dbContext.Books.Where(x=>x.Title==Model.Title).SingleOrDefault();
            if(book is not null)
                throw new Exception("Kitap zaten mevcut");
            
            book = new Book();
            book.Title = Model.Title;
            book.GenreId= Model.GenreId;
            book.PageCount = Model.PageCount;
            book.PublishDate=Model.PublishDate;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

    }
    public class CreateModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}