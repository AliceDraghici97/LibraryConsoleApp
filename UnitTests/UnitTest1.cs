using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LibraryConsoleApp;
using LibraryConsoleApp.Models;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Number_of_books_is_5()
        {
            var list = Program.PopulateBookLibrary();
            Assert.AreEqual(list.Count, 5);
        }

        [TestMethod]
        public void Add_a_book()
        {
            var list = Program.PopulateBookLibrary();

            var newBook = new Book { ISBN = "978-3-16-148410-1", Name = "Alice", Price = 100, Quantity = 10 };
            Program.AddBook(list, newBook);

            Assert.AreEqual(list.Last().Name, "Alice");
        }

        [TestMethod]
        public void Book_quantity_gets_set()
        {
            var list = Program.PopulateBookLibrary();

            var newBook = new Book { ISBN = "978-3-16-148410-1", Name = "Alice", Price = 100, Quantity = 15 };
            Program.AddBook(list, newBook);
            var qty = Program.GetBookQuantity(list, "978-3-16-148410-1");

            Assert.AreEqual(qty, 15);
        }

        [TestMethod]
        public void Lend_three_books()
        {
            var list = Program.PopulateBookLibrary();
            var borrowedList = Program.InitializeBorrowedLibrary();
            Program.LendBook(list, borrowedList, "978-3-17-148570-0", 3);

            Assert.AreEqual(borrowedList.First().Quantity, 3);
        }

        [TestMethod]
        public void Lend_three_books_return_two()
        {
            double overdraft = 0;
            var list = Program.PopulateBookLibrary();
            var borrowedList = Program.InitializeBorrowedLibrary();

            Program.LendBook(list, borrowedList, "978-3-17-148570-0", 3);
            Program.ReturnBook(list, borrowedList, "978-3-17-148570-0", 2, out overdraft);

            Assert.AreEqual(borrowedList.First().Quantity, 1);
        }

        [TestMethod]
        public void Overdraft_returning_book_late()
        {
            var list = Program.PopulateBookLibrary();
            var borrowedList = Program.PopulateBorrowedLibrary();

            double overdraft = 0;

            Program.ReturnBook(list, borrowedList, "978-3-12-148751-0", 6, out overdraft);

            Assert.AreEqual(overdraft, 0.32);
        }
    }
}
