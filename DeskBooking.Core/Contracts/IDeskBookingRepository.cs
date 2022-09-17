using System;
using DeskBooking.Core.Models;

namespace DeskBooking.Core.Contracts
{
    public interface IDeskBookingRepository
    {
        void Save(DeskBookingBase deskBookingBase);
    }
}

