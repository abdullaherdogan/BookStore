using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context=context;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            Book book = _context.Books.Where(x=>x.Id==id).SingleOrDefault();
            return book;
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
        {
            Book book = _context.Books.Where(x=>x.Id==updatedBook.Id).SingleOrDefault();
            if(book == null)
                return BadRequest();

            book.GenreId = updatedBook.GenreId != default? updatedBook.GenreId:book.GenreId;
            book.PublishDate = updatedBook.PublishDate != default? updatedBook.PublishDate:book.PublishDate;
            book.PageCount = updatedBook.PageCount != default? updatedBook.PageCount:book.PageCount;
            book.Title = updatedBook.Title != default? updatedBook.Title:book.Title;
            _context.SaveChanges();
            return Ok();

        }
    }
}