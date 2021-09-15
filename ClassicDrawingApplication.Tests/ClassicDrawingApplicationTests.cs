using ClassicDrawingApplication.Interfaces;
using Moq;
using Xunit;

namespace ClassicDrawingApplication.Tests
{
    public class ClassicDrawingApplicationTests
    {
        [Fact]
        public void Run_Should_Call_Add()
        {
            var toolsServiceMock = new Mock<IToolsService>();
            toolsServiceMock.Setup(ts => ts.ReadInput()).Returns("Add");

            var sut = new ClassicDrawingApplication(toolsServiceMock.Object);

            sut.EnterCommand();
            toolsServiceMock.Verify(ts => ts.Add("Add"));
        }

        [Fact]
        public void Run_Should_Call_Select()
        {
            var toolsServiceMock = new Mock<IToolsService>();
            toolsServiceMock.Setup(ts => ts.ReadInput()).Returns("Select");

            var sut = new ClassicDrawingApplication(toolsServiceMock.Object);

            sut.EnterCommand();
            toolsServiceMock.Verify(ts => ts.Select("Select"));
        }

        [Fact]
        public void Run_Should_Call_Move()
        {
            var toolsServiceMock = new Mock<IToolsService>();
            toolsServiceMock.Setup(ts => ts.ReadInput()).Returns("Move");

            var sut = new ClassicDrawingApplication(toolsServiceMock.Object);

            sut.EnterCommand();
            toolsServiceMock.Verify(ts => ts.Move("Move"));
        }

        [Fact]
        public void Run_Should_Call_Resize()
        {
            var toolsServiceMock = new Mock<IToolsService>();
            toolsServiceMock.Setup(ts => ts.ReadInput()).Returns("Resize");

            var sut = new ClassicDrawingApplication(toolsServiceMock.Object);

            sut.EnterCommand();
            toolsServiceMock.Verify(ts => ts.Resize("Resize"));
        }

        [Fact]
        public void Run_Should_Call_Delete()
        {
            var toolsServiceMock = new Mock<IToolsService>();
            toolsServiceMock.Setup(ts => ts.ReadInput()).Returns("Delete");

            var sut = new ClassicDrawingApplication(toolsServiceMock.Object);

            sut.EnterCommand();
            toolsServiceMock.Verify(ts => ts.Delete());
        }

        [Fact]
        public void Run_Should_Call_SetColour()
        {
            var toolsServiceMock = new Mock<IToolsService>();
            toolsServiceMock.Setup(ts => ts.ReadInput()).Returns("SetColour");

            var sut = new ClassicDrawingApplication(toolsServiceMock.Object);

            sut.EnterCommand();
            toolsServiceMock.Verify(ts => ts.SetColour("SetColour"));
        }

        [Fact]
        public void Run_Should_Call_Redraw()
        {
            var toolsServiceMock = new Mock<IToolsService>();
            toolsServiceMock.Setup(ts => ts.ReadInput()).Returns("Redraw");

            var sut = new ClassicDrawingApplication(toolsServiceMock.Object);

            sut.EnterCommand();
            toolsServiceMock.Verify(ts => ts.Redraw());
        }

        [Fact]
        public void Run_Should_Not_Call()
        {
            var toolsServiceMock = new Mock<IToolsService>(MockBehavior.Strict);
            toolsServiceMock.Setup(ts => ts.ReadInput()).Returns("Test");

            var sut = new ClassicDrawingApplication(toolsServiceMock.Object);

            sut.EnterCommand();

            toolsServiceMock.Verify(ts => ts.SetColour("SetColour"), Times.Never);
            toolsServiceMock.Verify(ts => ts.Delete(), Times.Never);
            toolsServiceMock.Verify(ts => ts.Resize("Resize"), Times.Never);
            toolsServiceMock.Verify(ts => ts.Move("Move"), Times.Never);
            toolsServiceMock.Verify(ts => ts.Select("Select"), Times.Never);
            toolsServiceMock.Verify(ts => ts.Add("Add"), Times.Never);
            toolsServiceMock.Verify(ts => ts.Redraw(), Times.Never);
        }
    }
}
