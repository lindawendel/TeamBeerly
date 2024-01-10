using HealthCare.Core.Data;
using HealthCare.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthCare.WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly HealthCareContext database;
        private readonly object _lock = new object();
        public ApiController(HealthCareContext database)
        {
            this.database = database;
        }

        [HttpGet("caregivers/{caregiverId}/appointments")]
        public async Task<IActionResult> GetCaregiverAppointments(Guid caregiverId)
        {
            var caregiverAppointments = await database.Appointments
                .Where(appointment => appointment.Caregiver.Id == caregiverId)
                .ToListAsync();

            return Ok(caregiverAppointments);
        }

        
        [HttpPost("caregivers/{caregiverId}/appointments")]
        //public async Task<IActionResult> CreateAppointment(Guid caregiverId, [FromBody] Appointment newAppointment)
        public async Task<IActionResult> CreateAppointment(Guid caregiverId, Appointment newAppointment)
        {
            lock (_lock)
            {
                // Generate a new Guid for the appointment
                newAppointment.Id = Guid.NewGuid();

                // Assign caregiverId to the new appointment
                newAppointment.Caregiver.Id = caregiverId;

                // Add the new appointment to the database
                database.Appointments.Add(newAppointment);
                database.SaveChanges();

                return CreatedAtAction(nameof(GetCaregiverAppointments), new { caregiverId }, newAppointment);
            }
        }



        [HttpPut("caregivers/{caregiverId}/appointments/{appointmentId}")]
        public async Task<IActionResult> UpdateAppointment(Guid caregiverId, Guid appointmentId, [FromBody] Appointment updatedAppointment)
        {
            lock (_lock)
            {
                var existingAppointment = database.Appointments
                    .FirstOrDefault(appointment => appointment.Id == appointmentId && appointment.Caregiver.Id == caregiverId);

                if (existingAppointment == null)
                {
                    return NotFound();
                }

                // Update the existing appointment
                existingAppointment.StartTime = updatedAppointment.StartTime;
                existingAppointment.EndTime = updatedAppointment.EndTime;
                existingAppointment.Service = updatedAppointment.Service;
                existingAppointment.Description = updatedAppointment.Description;
                existingAppointment.IsBooked = updatedAppointment.IsBooked;

                database.SaveChanges();

                return Ok(existingAppointment);
            }
        }

        [HttpDelete("caregivers/{caregiverId}/appointments/{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment(Guid caregiverId, Guid appointmentId)
        {
            lock (_lock)
            {
                var existingAppointment = database.Appointments
                    .FirstOrDefault(appointment => appointment.Id == appointmentId && appointment.Caregiver.Id == caregiverId);

                if (existingAppointment == null)
                {
                    return NotFound();
                }

                // Remove the existing appointment from the database
                database.Appointments.Remove(existingAppointment);
                database.SaveChanges();

                return Ok(new { message = "Appointment deleted successfully." });
            }
        }

        ////[HttpGet("notes")]
        //[HttpGet("/notes")]
        //public ToDoNote[] Get(bool? completed)
        //{
        //    if (completed == null)
        //    {
        //        return database.ToDoNotes.ToArray();

        //    }
        //    else if (completed == true)
        //    {
        //        return database.ToDoNotes.Where(m => m.IsDone == true).ToArray();

        //    }
        //    else if (completed == false)
        //    {
        //        return database.ToDoNotes.Where(m => m.IsDone == false).ToArray();
        //    }
        //    //kanske inte en så bra lösning
        //    else
        //    {
        //        return default;
        //    }
        //}

        ////[HttpGet("remaining")]
        //[HttpGet("/remaining")]
        //public int GetCount()
        //{
        //    return database.ToDoNotes.Where(m => m.IsDone == false).Count();
        //}

        ////[HttpPost("notes")]
        //[HttpPost]
        //public ActionResult<ToDoNote> PostToDo(ToDoNote httpToDoNote)
        ////public ActionResult PostToDo(ToDoNote httpToDoNote)
        //{
        //    if (httpToDoNote is null)
        //    {
        //        throw new ArgumentNullException(nameof(httpToDoNote));
        //    }

        //    var dbToDoNote = new ToDoNote
        //    {
        //        Text = httpToDoNote.Text,
        //        IsDone = false
        //    };

        //    database.ToDoNotes.Add(dbToDoNote);
        //    database.SaveChanges();

        //    return CreatedAtAction(nameof(Get), new { id = dbToDoNote.Id }, dbToDoNote);
        //}

        ////[HttpPost("toggle-all")]
        //[HttpPost("/toggle-all")]
        //public void ToggleAll()
        //{
        //    var notes = database.ToDoNotes.ToArray();

        //    if (notes.Length == 0)
        //    {
        //        return;
        //    }

        //    else if (notes.All(notes => notes.IsDone))
        //    {
        //        foreach (var note in notes)
        //        {
        //            note.IsDone = false;
        //        }
        //    }

        //    else
        //    {
        //        foreach (var note in notes)
        //        {
        //            note.IsDone = true;
        //        }
        //    }

        //    database.SaveChanges();
        //}

        ////[HttpPost("clear-completed")]
        //[HttpPost("/clear-completed")]
        //public void ClearCompletedNotes()
        //{
        //    var completedNotes = database.ToDoNotes.Where(m => m.IsDone).ToArray();

        //    foreach (var note in completedNotes)
        //    {
        //        database.ToDoNotes.Remove(note);
        //    }
        //    database.SaveChanges();
        //}

        ////[HttpPut("notes/{id:int}")]
        //[HttpPut("{id:int}")]
        //public void ChangeNote(int id, ToDoNote updatedNote)
        //{
        //    var noteToChange = database.ToDoNotes.FirstOrDefault(m => m.Id == id);

        //    if (noteToChange == null)
        //    {
        //        return;
        //    }

        //    else if (!noteToChange.IsDone)
        //    {
        //        noteToChange.IsDone = true;
        //    }

        //    else if (noteToChange.IsDone)
        //    {
        //        noteToChange.IsDone = false;
        //    }

        //    database.ToDoNotes.Update(noteToChange);
        //    database.SaveChanges();
        //}

        ////[HttpDelete("notes/{id:int}")]
        //[HttpDelete("{id:int}")]
        //public void DeleteNote(int id)
        //{
        //    //ADDED
        //    if (database.ToDoNotes.Where(_ => _.Id == id).Count() == 0)
        //    {
        //        throw new Exception("Id not found");
        //    }
        //    else
        //    {
        //        var noteToDelete = database.ToDoNotes.FirstOrDefault(m => m.Id == id);

        //        database.ToDoNotes.Remove(noteToDelete);
        //        database.SaveChanges();
        //    }
        //}


    }
}
