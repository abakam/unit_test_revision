using dotnetcore_web_api.Controllers;
using dotnetcore_web_api.Data.Models;
using dotnetcore_web_api.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace dotnetcore_web_api.Test
{
    public class BooksControllerTest
    {
        BooksController _booksController;
        IBookService _bookService;

        public BooksControllerTest()
        {
            _bookService = new BookService();
            _booksController = new BooksController(_bookService);
        }

        [Fact]
        public void GetAllTest()
        {
            //Arrange

            //Act
            var result = _booksController.Get();
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;

            Assert.IsType<List<Book>>(list.Value);

            var listBooks = list.Value as List<Book>;

            Assert.Equal(5, listBooks.Count);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c211")]
        public void GetBookByIdTest(string guid1, string guid2)
        {
            //Arrange 
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);

            //Act
            var notFoundResult = _booksController.Get(invalidGuid);
            var okResult = _booksController.Get(validGuid);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
            Assert.IsType<OkObjectResult>(okResult.Result);

            //Check the value of the result for the ok object result.
            var item = okResult.Result as OkObjectResult;

            //We expect that it returns a single book
            Assert.IsType<Book>(item.Value);

            //Now, let use the check the value of the book itself.
            var bookItem = item.Value as Book;
            Assert.Equal(validGuid, bookItem.Id);
            Assert.Equal("Managing Oneself", bookItem.Title);
        }

        [Fact]
        public void BooksController_AddBook_OkTest()
        {
            //Arrange
            var book = new Book
            {
                Author = "Author",
                Title = "Title",
                Description = "Description"
            };

            //Act
            var createdResponse = _booksController.Post(book);

            //Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);

            //Value of the result
            var item = createdResponse as CreatedAtActionResult;
            Assert.IsType<Book>(item.Value);

            //Check the value of the book
            var bookItem = item.Value as Book;
            Assert.Equal(book.Author, bookItem.Author);
            Assert.Equal(book.Title, bookItem.Title);
            Assert.Equal(book.Description, bookItem.Description);
        }

        [Fact]
        public void BooksController_AddBook_BadRequest()
        {
            //Arrange
            var book = new Book
            {
                Author = "Author",
                Description = "Description"
            };

            //Act
            _booksController.ModelState.AddModelError("Title", "Title is a required field");
            var badResponse = _booksController.Post(book);

            //Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c211")]
        public void RemoveBookByIdTest(string guid1, string guid2)
        {
            //Arrange
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);

            //Act
            var notFoundResult = _booksController.Remove(invalidGuid);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            Assert.Equal(5, _bookService.GetAll().Count());

            //Act
            var okResult = _booksController.Remove(validGuid);

            //Assert
            Assert.IsType<OkResult>(okResult);
            Assert.Equal(4, _bookService.GetAll().Count());
        }
    }
}
