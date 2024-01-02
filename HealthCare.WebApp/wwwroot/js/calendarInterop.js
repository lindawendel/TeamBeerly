window.initializeCalendar = () => {
    console.log("Initializing Calendar");
    var calendarEl = document.getElementById('calendar');
    if (calendarEl) {
        var calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'timeGridWeek',
            firstDay: 1, // Start the week on Monday
            slotLabelFormat: {
                hour: '2-digit',
                minute: '2-digit',
                hour12: false // Use 24-hour format
            },
            eventTimeFormat: {
                hour: '2-digit',
                minute: '2-digit',
                hour12: false // Use 24-hour format
            },
            businessHours: {
                daysOfWeek: [1, 2, 3, 4, 5], // Monday to Friday
                startTime: '09:00', // Start time for business hours
                endTime: '15:00',   // End time for business hours
            },
            slotMinTime: '09:00', // Calendar starts at 9am
            slotMaxTime: '15:00', // Calendar ends at 3pm
            slotDuration: '01:00:00', // 1 hour slots
            selectable: true,
            selectAllow: function (selectInfo) {
                // Disallow selection on weekends
                var dayOfWeek = selectInfo.start.getDay();
                return dayOfWeek !== 0 && dayOfWeek !== 6;
            },
            select: function (info) {
                // Ensure the end time is exactly one hour after the start time
                info.end = new Date(info.start.getTime() + 60 * 60 * 1000);

                var title = prompt('Event Title:');
                if (title) {
                    calendar.addEvent({
                        title: title,
                        start: info.start,
                        end: info.end
                    });
                }
                calendar.unselect();
            },
            allDaySlot: false,
            height: "auto",
            expandRows: true,
            nowIndicator: true,
            dayHeaders: true,
            navLinks: true,
            editable: true,
            dayMaxEvents: true,
            locale: 'en',
            events: [
                // If you have existing events, they can be added here.
            ],
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'timeGridWeek,timeGridDay'
            },

            height: 650
            //aspectRatio: 2
        });
        calendar.render();
        console.log("Calendar rendered");
    } else {
        console.error("Calendar element not found");
    }
};
