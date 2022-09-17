using System;
using DeskBooking.Core.Contracts;
using DeskBooking.Core.Models;

namespace DeskBooking.Core.Services
{
    public class DeskBookingService : IDeskBookingService
    {
        private readonly IDeskBookingRepository _repo;

        public DeskBookingService(IDeskBookingRepository repo)
        {
            _repo = repo;
        }

        public DeskBookingResult Book(DeskBookingRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _repo.Save(request);

            return new DeskBookingResult
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Date = request.Date
            };
        }
    }
}