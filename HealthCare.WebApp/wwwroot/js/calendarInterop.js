
if (window.FullCalendar) {
    var calendar;

    window.initializeCalendar = (viewType, dotnetReference) => {
        console.log("Initializing Calendar");

        var calendarEl = document.getElementById('calendar');

        // Define calendarOptions with default values
        var calendarOptions = {
            initialView: 'timeGridWeek',
            slotDuration: '00:30:00',
            slotLabelFormat: {
                hour: '2-digit',
                minute: '2-digit',
                hour12: false
            },
            /*slotLabelInterval: '00:45:00',*/
            businessHours: {
                daysOfWeek: [1, 2, 3, 4, 5],
                startTime: '09:00',
                endTime: '15:00',
            },
            selectable: true,
            select: function (info) {
                if (calendar) {
                    var slot = {
                        startTime: info.start,
                        endTime: info.end
                    };

                    if (viewType === 'patients') {

                        //var title = prompt('Event Title:');
                        //if (title) {
                        //    calendar.addEvent({
                        //        title: title,
                        //        start: info.start,
                        //        end: info.end
                        //    });
                        //}
                    } else if (viewType === 'caregivers') {

                        var confirmAdd = confirm('Do you want to add this time slot?');
                        if (confirmAdd) {
                            calendar.addEvent({
                                title: 'Available',
                                start: info.start,
                                end: info.end,
                                backgroundColor: 'green',
                                borderColor: 'green',
                                editable: true,
                            });
                            dotnetReference.invokeMethodAsync('AddAppointmentFromCalendar', slot.startTime, slot.endTime);
                        }
                    }

                    calendar.unselect();
                }
            },
            // ... other common options
        };

        // Customize calendarOptions for 'caregivers' view
        if (viewType === 'caregivers') {

            var businessHours = {
                daysOfWeek: [1, 2, 3, 4, 5],
                startTime: '09:00',
                endTime: '15:00',
            };
            var selectAllow = function (selectInfo) {
                var start = selectInfo.start;
                var end = selectInfo.end;

                return (
                    start.getDay() >= 1 &&
                    start.getDay() <= 5 &&
                    start.getHours() >= 9 &&
                    end.getHours() <= 15 &&
                    (start.getMinutes() % 45 != 15) && // 30 minutes available
                    (end.getMinutes() % 45 != 15)
                );
            };
            Object.assign(calendarOptions, { businessHours: businessHours, slotMinTime: '09:00', slotMaxTime: '15:00', selectAllow: selectAllow });
        }

        if (calendarEl) {
            // Create and render FullCalendar instance
            calendar = new FullCalendar.Calendar(calendarEl, calendarOptions);
            calendar.render();
            console.log("Calendar rendered");
        } else {
            console.error("Calendar element not found");
        }
    };

    window.addAppointmentSlotToCalendar = (slot) => {
        console.log("Adding appointment slot to calendar", slot);
        console.log("Received slot data:", slot);
        if (calendar) {
            calendar.addEvent({
                title: 'Available',
                start: slot.startTime,
                end: slot.endTime,
                backgroundColor: 'green',
                borderColor: 'green',
                editable: true,
            });
        }
    };

    window.updateCalendarAppointments = (appointments) => {
        console.log("Updating calendar with appointments", appointments);
        if (calendar) {
            // Clear existing events
            calendar.removeAllEvents();

            // Add new events
            for (const appointment of appointments) {
                calendar.addEvent({
                    title: 'Available',
                    start: appointment.startTime,
                    end: appointment.endTime,
                    backgroundColor: 'green',
                    borderColor: 'green',
                    editable: true,
                });
            }
        }
    };

} else {
    console.error("FullCalendar not found. Make sure it's properly loaded.");
}