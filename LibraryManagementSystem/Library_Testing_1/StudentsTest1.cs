using LibraryManagementSystem;
using Moq;
using FluentAssertions;

namespace LibraryTesting
{
    public class StudentsTest1
    {
        [Fact]
        public void Add_Student_Details_WhenCalled_Return_count()
        {
            var student = new Mock<IStudents>();
            student.Setup(x => x.Add_Student_Details()).Returns(1);
            var service=new StudentService(student.Object);
            var result = service.Data_inserted();
            result.Should().Be(1);
        }
          [Fact]
        public void Delete_Student_Details_WhenCalled_Return_Count()
        {
            var student = new Mock<IStudents>();
           student.Setup(x => x.Delete_Student_Details()).Returns(1);
            var service = new StudentService(student.Object);
            var result = service.Data_Deleted();
            result.Should().Be(1);
        }
        [Fact]
        public void Edit_Student_Details_WhenCalled_Return_Count()
        {
            var student = new Mock<IStudents>();
            student.Setup(x => x.Edit_Student_Details()).Returns(1);
            var service = new StudentService(student.Object);
            var result = service.Data_Updated();
            result.Should().Be(1);
        }

    }
}