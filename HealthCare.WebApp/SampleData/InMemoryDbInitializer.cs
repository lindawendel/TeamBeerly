using HealthCare.Core.Data;
using HealthCare;
using System;
using Microsoft.Extensions.DependencyInjection;
using HealthCare.Core.Models;
using HealthCare.Core.Data;
using System.Data;


public static class InMemoryDbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<HealthCareContext>();

            dbContext.Database.EnsureCreated();

            if (!dbContext.Caregivers.Any())
            {
                var Caregiver1 = new Caregiver { Id = Guid.NewGuid(), Auth0Id = "659fce7eb2b6d54c87f82290", Name = "Doctor Cosmos", Role = "Doctor", DateOfBirth = new DateTime(1975, 12, 12), PhoneNumber = "031148877", Email = "just@splanned.com" };

                dbContext.Caregivers.Add(Caregiver1);

                dbContext.SaveChanges();

            }



        }
    }
}
//public static class InMemoryDbInitializer
//{
//    public static void Initialize(IServiceProvider serviceProvider)
//    {
//        using (var scope = serviceProvider.CreateScope())
//        {
//            var dbContext = scope.ServiceProvider.GetRequiredService<HealthCareContext>();

//            dbContext.Database.EnsureCreated();

//            if (!dbContext.Patients.Any() || !dbContext.Caregivers.Any() || !dbContext.Bookings.Any() || !dbContext.Appointments.Any())
//            {
//                var patient1 = new Patient { Id = Guid.NewGuid(), Name = "Hans Hansson", Email = "hans.h@example.com" };
//                var patient2 = new Patient { Id = Guid.NewGuid(), Name = "Axel Fingersson", Email = "axel.finger@example.com" };
//                var patient3 = new Patient { Id = Guid.NewGuid(), Name = "Astrid Lindgren", Email = "a.lindgren@example.com" };
//                var patient4 = new Patient { Id = Guid.NewGuid(), Name = "Eva Svensson", Email = "eva.svensson@example.com" };
//                var patient5 = new Patient { Id = Guid.NewGuid(), Name = "Oscar Nilsson", Email = "oscar.nilsson@example.com" };
//                var patient6 = new Patient { Id = Guid.NewGuid(), Name = "Sara Andersson", Email = "sara.andersson@example.com" };
//                var patient7 = new Patient { Id = Guid.NewGuid(), Name = "Emma Gustafsson", Email = "emma.gustafsson@example.com" };
//                var patient8 = new Patient { Id = Guid.NewGuid(), Name = "Victor Berg", Email = "victor.berg@example.com" };
//                var patient9 = new Patient { Id = Guid.NewGuid(), Name = "Ida Pettersson", Email = "ida.pettersson@example.com" };
//                var patient10 = new Patient { Id = Guid.NewGuid(), Name = "Niklas Johansson", Email = "niklas.j@example.com" };
//                var patient11 = new Patient { Id = Guid.NewGuid(), Name = "Maja Persdotter", Email = "maja.p@example.com" };
//                var patient12 = new Patient { Id = Guid.NewGuid(), Name = "Erik Sjöström", Email = "erik.s@example.com" };
//                var patient13 = new Patient { Id = Guid.NewGuid(), Name = "Lina Nilsson", Email = "lina.nilsson@example.com" };
//                var patient14 = new Patient { Id = Guid.NewGuid(), Name = "Simon Andersson", Email = "simon.a@example.com" };
//                var patient15 = new Patient { Id = Guid.NewGuid(), Name = "Caroline Larsson", Email = "caroline.l@example.com" };
//                var patient16 = new Patient { Id = Guid.NewGuid(), Name = "Andreas Karlsson", Email = "andreas.k@example.com" };
//                var patient17 = new Patient { Id = Guid.NewGuid(), Name = "Frida Eriksson", Email = "frida.e@example.com" };
//                var patient18 = new Patient { Id = Guid.NewGuid(), Name = "Jesper Lundqvist", Email = "jesper.l@example.com" };
//                var patient19 = new Patient { Id = Guid.NewGuid(), Name = "Hanna Lind", Email = "hanna.lind@example.com" };
//                var patient20 = new Patient { Id = Guid.NewGuid(), Name = "Olof Persson", Email = "olof.p@example.com" };

//                var patients = new List<Patient>
//                {
//                    patient1, patient2, patient3, patient4, patient5, patient6, patient7, patient8, patient9, patient10, patient11,
//                    patient12, patient13, patient14, patient15, patient16,patient17, patient18, patient19, patient20
//                };

//                dbContext.Patients.AddRange(patients);

//                var caregiver1 = new Caregiver { Id = Guid.NewGuid(), Name = "Emma Engberg", Email = "emma.e@example.com", };
//                var caregiver2 = new Caregiver { Id = Guid.NewGuid(), Name = "Marcus Nilsson", Email = "marcus.n@example.com" };
//                var caregiver3 = new Caregiver { Id = Guid.NewGuid(), Name = "Sandra Ask", Email = "sandra.a@example.com" };

//                dbContext.Caregivers.Add(caregiver1);
//                dbContext.Caregivers.Add(caregiver2);
//                dbContext.Caregivers.Add(caregiver3);

//                var booking1 = new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(2), Patient = patient1, Service = "General Checkup" };
//                var booking2 = new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(4), Patient = patient2, Service = "Vaccination" };
//                var booking3 = new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(6), Patient = patient3, Service = "Amputation" };

//                dbContext.Bookings.Add(booking1);
//                dbContext.Bookings.Add(booking2);
//                dbContext.Bookings.Add(booking3);

//                var appointment1 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(2), Patient = patient1, Caregiver = caregiver1, Service = booking1.Service, Description = "Ahoy ahoy" };
//                var appointment2 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(4), Patient = patient2, Caregiver = caregiver2, Service = booking2.Service, Description = "Ahoy ahoy2" };
//                var appointment3 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(6), Patient = patient3, Caregiver = caregiver3, Service = booking3.Service, Description = "Ahoy ahoy3" };
//                var appointment4 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(8), Patient = patient4, Caregiver = caregiver1, Service = booking1.Service, Description = "Meeting with patient4" };
//                var appointment5 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(10), Patient = patient5, Caregiver = caregiver2, Service = booking2.Service, Description = "Checkup for patient5" };
//                var appointment6 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(12), Patient = patient6, Caregiver = caregiver3, Service = booking3.Service, Description = "Appointment with patient6" };
//                var appointment7 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(14), Patient = patient7, Caregiver = caregiver1, Service = booking1.Service, Description = "Consultation with patient7" };
//                var appointment8 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(16), Patient = patient8, Caregiver = caregiver2, Service = booking2.Service, Description = "Follow-up for patient8" };
//                var appointment9 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(18), Patient = patient9, Caregiver = caregiver3, Service = booking3.Service, Description = "Appointment for patient9" };
//                var appointment10 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(20), Patient = patient10, Caregiver = caregiver1, Service = booking1.Service, Description = "Checkup for patient10" };
//                var appointment11 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(22), Patient = patient11, Caregiver = caregiver2, Service = booking2.Service, Description = "Meeting with patient11" };
//                var appointment12 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(24), Patient = patient12, Caregiver = caregiver3, Service = booking3.Service, Description = "Appointment with patient12" };
//                var appointment13 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(26), Patient = patient13, Caregiver = caregiver1, Service = booking1.Service, Description = "Consultation with patient13" };
//                var appointment14 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(28), Patient = patient14, Caregiver = caregiver2, Service = booking2.Service, Description = "Follow-up for patient14" };
//                var appointment15 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(30), Patient = patient15, Caregiver = caregiver3, Service = booking3.Service, Description = "Appointment for patient15" };
//                var appointment16 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(32), Patient = patient1, Caregiver = caregiver2, Service = booking2.Service, Description = "Flue-shot" };

//                var upcommingAppointments = new List<Appointment>
//                {
//                    appointment1, appointment2, appointment3, appointment4, appointment5,
//                    appointment6, appointment7, appointment8, appointment9, appointment10,
//                    appointment11, appointment12, appointment13, appointment14, appointment15, appointment16
//                };




//                dbContext.Appointments.AddRange(upcommingAppointments);


//                var pastAppointment1 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-2), Patient = patient15, Caregiver = caregiver1, Service = booking1.Service, Description = "Past Ahoy ahoy" };
//                var pastAppointment2 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-4), Patient = patient14, Caregiver = caregiver2, Service = booking2.Service, Description = "Past Ahoy ahoy2" };
//                var pastAppointment3 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-6), Patient = patient13, Caregiver = caregiver3, Service = booking3.Service, Description = "Past Ahoy ahoy3" };
//                var pastAppointment4 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-8), Patient = patient12, Caregiver = caregiver1, Service = booking1.Service, Description = "Past Meeting with patient4" };
//                var pastAppointment5 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-10), Patient = patient11, Caregiver = caregiver2, Service = booking2.Service, Description = "Past Checkup for patient5" };
//                var pastAppointment6 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-12), Patient = patient10, Caregiver = caregiver3, Service = booking3.Service, Description = "Past Appointment with patient6" };
//                var pastAppointment7 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-14), Patient = patient9, Caregiver = caregiver1, Service = booking1.Service, Description = "Past Consultation with patient7" };
//                var pastAppointment8 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-16), Patient = patient8, Caregiver = caregiver2, Service = booking2.Service, Description = "Past Follow-up for patient8" };
//                var pastAppointment9 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-18), Patient = patient7, Caregiver = caregiver3, Service = booking3.Service, Description = "Past Appointment for patient9" };
//                var pastAppointment10 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-20), Patient = patient6, Caregiver = caregiver1, Service = booking1.Service, Description = "Past Checkup for patient10" };
//                var pastAppointment11 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-22), Patient = patient5, Caregiver = caregiver2, Service = booking2.Service, Description = "Past Meeting with patient11" };
//                var pastAppointment12 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-24), Patient = patient4, Caregiver = caregiver3, Service = booking3.Service, Description = "Past Appointment with patient12" };
//                var pastAppointment13 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-26), Patient = patient3, Caregiver = caregiver1, Service = booking1.Service, Description = "Past Consultation with patient13" };
//                var pastAppointment14 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-28), Patient = patient2, Caregiver = caregiver2, Service = booking2.Service, Description = "Past Follow-up for patient14" };
//                var pastAppointment15 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-30), Patient = patient1, Caregiver = caregiver3, Service = booking3.Service, Description = "Past Appointment for patient15" };
//                var pastAppointment16 = new Appointment { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(-32), Patient = patient1, Caregiver = caregiver1, Service = booking1.Service, Description = "Past Consultation with patient1" };


//                var pastAppointments = new List<Appointment>
//                {
//                    pastAppointment1, pastAppointment2, pastAppointment3, pastAppointment4, pastAppointment5,
//                    pastAppointment6, pastAppointment7, pastAppointment8, pastAppointment9, pastAppointment10,
//                    pastAppointment11, pastAppointment12, pastAppointment13, pastAppointment14, pastAppointment15, pastAppointment16
//                };

//                dbContext.Appointments.AddRange(pastAppointments);

//                dbContext.SaveChanges();
//            }


//            if (!dbContext.Feedbacks.Any())
//            {
//                dbContext.Add(new Feedback { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(6), Title = "snygge jobbe", Comment = "riktigt bra jobbat, " });
//                dbContext.Add(new Feedback { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(2), Title = "ful o dum", Comment = "riktigt... jobbat " });
//                dbContext.Add(new Feedback { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(5), Title = "hjälpte", Comment = "jösses va?" });

//                dbContext.SaveChanges();

//            }

//        }
//    }
//}
