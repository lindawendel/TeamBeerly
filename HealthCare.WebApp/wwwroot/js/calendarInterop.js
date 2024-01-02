window.initializeCalendar = () => {
    console.log("Initializing Calendar");
    var calendarEl = document.getElementById('calendar');
    if (calendarEl) {
        var calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth'
        });
        calendar.render();
        console.log("Calendar rendered");
    } else {
        console.error("Calendar element not found");
    }
};
