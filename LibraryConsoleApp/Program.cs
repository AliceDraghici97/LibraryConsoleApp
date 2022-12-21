using LibraryConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryConsoleApp
{
    public class Program
    {
        public static void AddBook(List<Book> books, Book book)
        {
            var bookItem = books.FirstOrDefault(b => b == book);

            if (bookItem == null)
            {
                books.Add(book);
            }
            else
            {
                bookItem.Quantity++;
            }
        }


        public static void ShowBooks(List<Book> books)
        {
            foreach (Book book in books)
            {
                Console.WriteLine(String.Format("ISBN: {0}, Name: {1}, Price: {2}, Quantity: {3}"
                    , book.ISBN
                    , book.Name
                    , book.Price
                    , book.Quantity));
            }
        }

        public static int GetBookQuantity(List<Book> books, string ISBN)
        {
            return books.FirstOrDefault(b => b.ISBN == ISBN).Quantity;
        }

        public static void LendBook(List<Book> books, List<BorrowedBook> borrowedBooks, string ISBN, int quantity)
        {
            var bookItem = books.FirstOrDefault(b => b.ISBN == ISBN);

            if (bookItem == null)
            {
                Console.WriteLine(String.Format("This book ({0}) does not exist in our library.", bookItem.Name));
                return;
            }

            if (bookItem.Quantity <= 0)
            {
                Console.WriteLine(String.Format("This book ({0}) does not have enough quantity in our library.", bookItem.Name));
                return;
            }

            bookItem.Quantity -= quantity;

            BorrowedBook newBorrowedBook = new BorrowedBook { ISBN = ISBN, LendingDateTime = DateTime.Now, Quantity = quantity };

            borrowedBooks.Add(newBorrowedBook);
        }

        public static void ReturnBook(List<Book> books, List<BorrowedBook> borrowedBooks, string ISBN, int quantity, out double overdraft)
        {
            overdraft = 0;

            var bookItem = books.FirstOrDefault(b => b.ISBN == ISBN);
            var borrowedBookItem = borrowedBooks.FirstOrDefault(b => b.ISBN == ISBN);
            var borrowedDays = (int)(DateTime.Now - borrowedBookItem.LendingDateTime).TotalDays;

            if (borrowedDays > 14)
            {
                overdraft = bookItem.Price * 0.01 * borrowedDays;
                Console.WriteLine(String.Format("Client need to pay: {0}.", overdraft));
            }

            bookItem.Quantity += quantity;

            borrowedBookItem.Quantity -= quantity;
            if (borrowedBookItem.Quantity == 0)
            {
                borrowedBooks.Remove(borrowedBookItem);
            }
        }

        public static void Main(string[] args)
        {

        }

        public static List<Book> PopulateBookLibrary()
        {
            List<Book> books = new List<Book> {

                    new Book { ISBN = "978-3-16-148410-0", Name = "Book1", Price = 12.5 ,  Quantity=1}
                    , new Book { ISBN = "978-3-17-148570-0", Name = "Book2", Price = 10 ,  Quantity=3}
                    , new Book { ISBN = "978-6-10-148360-0", Name = "Book3", Price = 8 ,  Quantity=1}
                    , new Book { ISBN = "978-4-16-148410-0", Name = "Book4", Price = 6.08 ,  Quantity=1}
                    , new Book { ISBN = "978-3-12-148751-0", Name = "Book5", Price = 2 ,  Quantity=5}
            };
            return books;
        }

        public static List<BorrowedBook> InitializeBorrowedLibrary()
        {
            List<BorrowedBook> borrowedBooks = new List<BorrowedBook>();
            return borrowedBooks;
        }

        public static List<BorrowedBook> PopulateBorrowedLibrary()
        {
            List<BorrowedBook> borrowedBooks = InitializeBorrowedLibrary();

            borrowedBooks.Add(new BorrowedBook { ISBN = "978-3-12-148751-0", LendingDateTime = DateTime.Now.AddDays(-16), Quantity = 7 });

            return borrowedBooks;
        }
    }
}