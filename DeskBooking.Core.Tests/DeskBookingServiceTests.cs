using DeskBooking.Core.Contracts;
using DeskBooking.Core.Models;
using DeskBooking.Core.Services;
using FakeItEasy;
using FluentAssertions;

namespace DeskBooking.Core;

public class DeskBookingServiceTests
{
    private readonly IDeskBookingRepository _repo;
    private readonly IDeskBookingService _deskBookingService;
    private readonly DeskBookingRequest _deskBookingRequest;

    public DeskBookingServiceTests()
    {
        _repo = A.Fake<IDeskBookingRepository>();
        _deskBookingService = new DeskBookingService(_repo);
        _deskBookingRequest = new DeskBookingRequest
        {
            FirstName = "Nick",
            LastName = "Allport",
            Date = new DateTime(2022, 1, 1)
        };
    }

    [Fact]
    public void ShouldReturnABookingResultMatchingRequestValues()
    {
        // Arrange


        // Act
        var deskBookingResult = _deskBookingService.Book(_deskBookingRequest);

        // Assert
        deskBookingResult.FirstName.Should().Be(_deskBookingRequest.FirstName);
        deskBookingResult.LastName.Should().Be(_deskBookingRequest.LastName);
        deskBookingResult.Date.Should().Be(_deskBookingRequest.Date);
    }

    [Fact]
    public void ShouldThrowArgumentExceptionIfRequestIsNull()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => _deskBookingService.Book(null));

        // Assert
        exception.ParamName.Should().Be("request");
    }

    [Fact]
    public void ShouldSaveABooking()
    {
        // Arrange
        DeskBookingBase? savedDeskBooking = null;

        A.CallTo(() => _repo.Save(A<DeskBookingBase>.Ignored))
            .Invokes((DeskBookingBase deskBooking) => savedDeskBooking = deskBooking);

        // Act
        var deskBookingResult = _deskBookingService.Book(_deskBookingRequest);

        // Assert
        savedDeskBooking.Should().NotBe(null);
        A.CallTo(() => _repo.Save(A<DeskBookingBase>._)).MustHaveHappenedOnceExactly();
        deskBookingResult.FirstName.Should().Be(_deskBookingRequest.FirstName);
        deskBookingResult.LastName.Should().Be(_deskBookingRequest.LastName);
        deskBookingResult.Date.Should().Be(_deskBookingRequest.Date);
    }
}