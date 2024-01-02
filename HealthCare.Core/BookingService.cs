using HealthCare.Core.Models;
using System;
    using System.Collections.Generic;

    namespace HealthCare.Core
    {
        public class BookingService
        {
            private List<Booking> bookings = new List<Booking>();

            public BookingService()
            {
                // mock data
                var patient1 = new Patient { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com" };
                var patient2 = new Patient { Id = Guid.NewGuid(), Name = "Jane Smith", Email = "jane.smith@example.com" };

                bookings.Add(new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(2), Patient = patient1, Service = "General Checkup" });
                bookings.Add(new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(4), Patient = patient2, Service = "Vaccination" });
            }

            public IEnumerable<Booking> GetBookings()
            {
                return bookings;
            }

            public void AddBooking(Booking booking)
            {
                bookings.Add(booking);
            }
        }
    }

    /*    public class BookingService
        {
            private List<Booking> bookings = new List<Booking>();

            public BookingService()
            {
                // mock data
                bookings.Add(new Booking { Id = new Guid(), Time = DateTime.Now.AddHours(2), Patient = "John Doe", Service = "General Checkup" });
                bookings.Add(new Booking { Id = new Guid(), Time = DateTime.Now.AddHours(4), Patient = "Jane Smith", Service = "Vaccination" });

            }

            public IEnumerable<Booking> GetBookings()
            {
                return bookings;
            }

            public void AddBooking(Booking booking)
            {
                bookings.Add(booking);
            }
        }*/


