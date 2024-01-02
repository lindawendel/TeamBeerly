window.initializeCalendar = () => {
    console.log("Initializing Calendar");
    var calendarEl = document.getElementById('calendar');
    if (calendarEl) {
        var calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'timeGridWeek',
            firstDay: 1,
            slotLabelFormat: {
                hour: '2-digit',
                minute: '2-digit',
                hour12: false
            },
            eventTimeFormat: {
                hour: '2-digit',
                minute: '2-digit',
                hour12: false
            },
            businessHours: {
                daysOfWeek: [1, 2, 3, 4, 5],
                startTime: '09:00',
                endTime: '15:00',
            },
            slotMinTime: '09:00',
            slotMaxTime: '15:00',
            slotDuration: '01:00:00',
            selectable: true,
            selectAllow: function (selectInfo) {
                
                var dayOfWeek = selectInfo.start.getDay();
                return dayOfWeek !== 0 && dayOfWeek !== 6;
            },
            select: function (info) {
                
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
                
            ],
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'timeGridWeek,timeGridDay'
            },

            height: 650
            
        });
        calendar.render();
        console.log("Calendar rendered");
    } else {
        console.error("Calendar element not found");
    }
};
