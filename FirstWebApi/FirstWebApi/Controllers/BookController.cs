using FirstWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        static List<Book> booklist =new List<Book>();
        [HttpGet]
        public List<Book> GetBooks()
        {
         
            for(int i = 0; i < 5; i++)
            {
                Book book = new Book();
                book.BookID = i;
                book.Title = "Wimpy kid" + i;
                book.Cost = i*100;
                book.AuthorName = "author " + i;
                booklist.Add(book);
            }
            return booklist;
        }
        [HttpPost]
        public int AddBook(Book book)
        {
            booklist.Add(book);
            return 1;
        }

    }


}
