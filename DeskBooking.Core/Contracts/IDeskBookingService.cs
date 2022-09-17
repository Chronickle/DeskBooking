using DeskBooking.Core.Models;

namespace DeskBooking.Core.Contracts
{
    public interface IDeskBookingService
    {
        DeskBookingResult Book(DeskBookingRequest deskBookingRequest);
    }
}