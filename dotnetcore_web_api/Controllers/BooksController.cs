using dotnetcore_web_api.Data.Models;
using dotnetcore_web_api.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcore_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService; 
        }

        // GET: api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var items = _bookService.GetAll();
            return Ok(items);
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id)
        {
            var item = _bookService.GetById(id);

            if(item == null) return NotFound();
           
            return Ok(item);
        }

        // POST: api/books
        [HttpPost]
        public ActionResult Post([FromBody] Book value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var item = _bookService.Add(value);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var existingItem = _bookService.GetById(id);

            if (existingItem == null) return NotFound();

            _bookService.Remove(id);
            return Ok();
        }

    }
}
