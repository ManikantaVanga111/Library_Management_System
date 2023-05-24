using LibraryManagementSystem;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
namespace LibraryTesting
{
    public class BookTesting
    {
        [Fact]
        public void Add_Book_Details_WhenCalled_Return_Count()
        {
            var book = new Mock<IBooks>();
            book.Setup(x => x.Add_Book_Details()).Returns(1);
            var result = book.Object.Add_Book_Details();
            result.Should().Be(1);
        }

        [Fact]
        public void Edit_Book_Details_WhenCalled_Return_Count()
        {
            var book = new Mock<IBooks>();
            book.Setup(x => x.Edit_Book_Details()).Returns(1);
            var result = book.Object.Edit_Book_Details();
            result.Should().Be(1);
        }
        [Fact]
        public void Delete_Book_Details_WhenCalled_Return()
        {
            var book = new Mock<IBooks>();
            book.Setup(x => x.Delete_Book_Details()).Returns(1);
            var result = book.Object.Delete_Book_Details();
            result.Should().Be(1);
        }
    }
}
