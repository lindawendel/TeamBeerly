if (window.FullCalendar) {
    var calendar;

    window.initializeCalendar = (viewType) => {
        console.log("Initializing Calendar");

        var calendarEl = document.getElementById('calendar');
        /*var calendar;*/

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
                if (calendar && viewType === 'patients') {

                    var title = prompt('Event Title:');
                    if (title) {
                        calendar.addEvent({
                            title: title,
                            start: info.start,
                            end: info.end
                        });
                    }
                } else if (calendar && viewType === 'caregivers') {

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
                    }
                }
                if (calendar) {
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
}   else {
    console.error("FullCalendar not found. Make sure it's properly loaded.");
}


//window.addAppointmentSlotToCalendar = (slot) => {
//    if (calendar) {
//        calendar.addEvent({
//            title: 'Available',
//            start: slot.startTime,
//            end: slot.endTime,
//            backgroundColor: 'green',
//            borderColor: 'green',
//            editable: true,
//        });
//    }
//};
//}
//else {
//    console.error("FullCalendar not found. Make sure it's properly loaded.");
//}


//window.initializeCalendar = (viewType) => {
//    console.log("Initializing Calendar");

//    var calendarEl = document.getElementById('calendar');
//    var calendar;

//    if (calendarEl) {
//        var initialView = 'timeGridWeek';
//        var slotDuration = '00:30:00';
//        var slotInterval = '00:15:00';

//        if (viewType === 'patients') {
//            initialView = 'timeGridWeek';
//        } else if (viewType === 'caregivers') {
//            initialView = 'timeGridWeek';

//            var businessHours = {
//                daysOfWeek: [1, 2, 3, 4, 5],
//                startTime: '09:00',
//                endTime: '15:00',
//            };
//            var selectAllow = function (selectInfo) {
//                var start = selectInfo.start;
//                var end = selectInfo.end;

//                return (
//                    start.getDay() >= 1 &&
//                    start.getDay() <= 5 &&
//                    start.getHours() >= 9 &&
//                    end.getHours() <= 15 &&
//                    (start.getMinutes() % 15 === 0) &&
//                    (end.getMinutes() % 15 === 0)
//                );
//            };
//            Object.assign(calendarOptions, { selectAllow: selectAllow });
//        }

//        var calendarOptions = {
//            initialView: initialView,
//            slotDuration: slotDuration,
//            slotLabelFormat: {
//                hour: '2-digit',
//                minute: '2-digit',
//                hour12: false
//            },
//            businessHours: {
//                daysOfWeek: [1, 2, 3, 4, 5],
//                startTime: '09:00',
//                endTime: '15:00',
//            },
//            selectable: true,
//            select: function (info) {
//                if (calendar && viewType === 'patients') {
//                    var title = prompt('Event Title:');
//                    if (title) {
//                        calendar.addEvent({
//                            title: title,
//                            start: info.start,
//                            end: info.end
//                        });
//                    }
//                } else if (calendar && viewType === 'caregivers') {
//                    var confirmAdd = confirm('Do you want to add this time slot?');
//                    if (confirmAdd) {
//                        calendar.addEvent({
//                            title: 'Available',
//                            start: info.start,
//                            end: info.end,
//                            backgroundColor: 'green',
//                            borderColor: 'green',
//                            editable: true,
//                        });
//                    }
//                }
//                if (calendar) {
//                    calendar.unselect();
//                }
//            },
//            // ... other common options
//        };

//        calendar = new FullCalendar.Calendar(calendarEl, calendarOptions);

//        calendar.render();
//        console.log("Calendar rendered");
//    } else {
//        console.error("Calendar element not found");
//    }
//};


//window.initializeCalendar = (viewType) => {
//    console.log("Initializing Calendar");

//    var calendarEl = document.getElementById('calendar');
//    var calendar; // Declare the calendar variable at a higher scope

//    if (calendarEl) {
//        // Customize calendar options based on the viewType
//        var initialView = 'timeGridWeek'; // Default initial view
//        var slotDuration = '00:30:00'; // 30 minutes
//        var slotInterval = '00:15:00'; // 15 minutes

//        if (viewType === 'patients') {
//            initialView = 'timeGridWeek';
//        } else if (viewType === 'caregivers') {
//            initialView = 'timeGridWeek';
//            // Allow caregivers to select any time slot within the specified business hours
//            var businessHours = {
//                daysOfWeek: [1, 2, 3, 4, 5], // Monday to Friday
//                startTime: '09:00',
//                endTime: '15:00',
//            };
//            var selectAllow = function (selectInfo) {
//                var start = selectInfo.start;
//                var end = selectInfo.end;

//                // Check if the selected time slot is within business hours and adheres to slot intervals
//                return (
//                    start.getDay() >= 1 && // Monday
//                    start.getDay() <= 5 && // Friday
//                    start.getHours() >= 9 &&
//                    end.getHours() <= 15 &&
//                    (start.getMinutes() % 15 === 0) && // Slot interval
//                    (end.getMinutes() % 15 === 0)
//                );
//            };
//            // Update the selectAllow function
//            Object.assign(calendarOptions, { selectAllow: selectAllow });
//        }

//        var calendarOptions = {
//            initialView: initialView,
//            slotDuration: slotDuration,
//            slotLabelFormat: {
//                hour: '2-digit',
//                minute: '2-digit',
//                hour12: false
//            },
//            businessHours: {
//                daysOfWeek: [1, 2, 3, 4, 5], // Monday to Friday
//                startTime: '09:00',
//                endTime: '15:00',
//            },
//            selectable: true,
//            select: function (info) {
//                if (viewType === 'patients') {
//                    // Patients can only book available time slots provided by the caregiver
//                    // Implement your logic to handle patient booking here
//                    var title = prompt('Event Title:');
//                    if (title) {
//                        calendar.addEvent({
//                            title: title,
//                            start: info.start,
//                            end: info.end
//                        });
//                    }
//                } else if (viewType === 'caregivers') {
//                    // Caregivers can add time slots
//                    // Implement your logic to handle caregiver adding time slots here
//                    // For example, prompt for confirmation and add the slot if confirmed
//                    var confirmAdd = confirm('Do you want to add this time slot?');
//                    if (confirmAdd) {
//                        calendar.addEvent({
//                            title: 'Available',
//                            start: info.start,
//                            end: info.end,
//                            backgroundColor: 'green',
//                            borderColor: 'green',
//                            editable: true, // Allow caregivers to edit the time slot
//                        });
//                    }
//                }
//                calendar.unselect();
//            },
//            // ... other common options
//        };

//        calendar = new FullCalendar.Calendar(calendarEl, calendarOptions); // Assign the calendar object

//        // Render the calendar
//        calendar.render();
//        console.log("Calendar rendered");
//    } else {
//        console.error("Calendar element not found");
//    }
//};


//window.initializeCalendar = (viewType) => {
//    console.log("Initializing Calendar");

//    var calendarEl = document.getElementById('calendar');

//    if (calendarEl) {
//        // Customize calendar options based on the viewType
//        var initialView = 'timeGridWeek'; // Default initial view
//        var slotDuration = '00:30:00'; // 30 minutes
//        var slotInterval = '00:15:00'; // 15 minutes

//        if (viewType === 'patients') {
//            initialView = 'timeGridWeek';
//        } else if (viewType === 'caregivers') {
//            initialView = 'timeGridWeek';
//            // Allow caregivers to select any time slot within the specified business hours
//            var businessHours = {
//                daysOfWeek: [1, 2, 3, 4, 5], // Monday to Friday
//                startTime: '09:00',
//                endTime: '15:00',
//            };
//            var selectAllow = function (selectInfo) {
//                var start = selectInfo.start;
//                var end = selectInfo.end;

//                // Check if the selected time slot is within business hours and adheres to slot intervals
//                return (
//                    start.getDay() >= 1 && // Monday
//                    start.getDay() <= 5 && // Friday
//                    start.getHours() >= 9 &&
//                    end.getHours() <= 15 &&
//                    (start.getMinutes() % 15 === 0) && // Slot interval
//                    (end.getMinutes() % 15 === 0)
//                );
//            };
//            // Update the selectAllow function
//            Object.assign(calendarOptions, { selectAllow: selectAllow });
//        }

//        var calendarOptions = {
//            initialView: initialView,
//            slotDuration: slotDuration,
//            slotLabelFormat: {
//                hour: '2-digit',
//                minute: '2-digit',
//                hour12: false
//            },
//            businessHours: {
//                daysOfWeek: [1, 2, 3, 4, 5], // Monday to Friday
//                startTime: '09:00',
//                endTime: '15:00',
//            },
//            selectable: true,
//            select: function (info) {
//                if (viewType === 'patients') {
//                    // Patients can only book available time slots provided by the caregiver
//                    // Implement your logic to handle patient booking here
//                    var title = prompt('Event Title:');
//                    if (title) {
//                        calendar.addEvent({
//                            title: title,
//                            start: info.start,
//                            end: info.end
//                        });
//                    }
//                } else if (viewType === 'caregivers') {
//                    // Caregivers can add time slots
//                    // Implement your logic to handle caregiver adding time slots here
//                    // For example, prompt for confirmation and add the slot if confirmed
//                    var confirmAdd = confirm('Do you want to add this time slot?');
//                    if (confirmAdd) {
//                        calendar.addEvent({
//                            title: 'Available',
//                            start: info.start,
//                            end: info.end,
//                            backgroundColor: 'green',
//                            borderColor: 'green',
//                            editable: true, // Allow caregivers to edit the time slot
//                        });
//                    }
//                }
//                calendar.unselect();
//            },
//            // ... other common options
//        };

//        var calendar = new FullCalendar.Calendar(calendarEl, calendarOptions);

//        // Render the calendar
//        calendar.render();
//        console.log("Calendar rendered");
//    } else {
//        console.error("Calendar element not found");
//    }
//};


//window.initializeCalendar = (viewType) => {
//    console.log("Initializing Calendar");

//    var calendarEl = document.getElementById('calendar');

//    if (calendarEl) {
//        // Customize calendar options based on the viewType
//        var initialView = 'timeGridWeek'; // Default initial view

//        if (viewType === 'patients') {
//            initialView = 'timeGridWeek';
//        } else if (viewType === 'caregivers') {
//            initialView = 'timeGridWeek';
//        } else if (viewType === 'all') {
//            initialView = 'listWeek'; // Set the initial view for 'all'
//            // Add other options based on the 'all' viewType
//            // ...
//        }

//        var calendarOptions = {
//            initialView: initialView,
//            // Add other options based on the viewType
//            // ...

//            // Common options
//            selectable: true,
//            selectAllow: function (selectInfo) {
//                var dayOfWeek = selectInfo.start.getDay();
//                return dayOfWeek !== 0 && dayOfWeek !== 6;
//            },
//            select: function (info) {
//                info.end = new Date(info.start.getTime() + 60 * 60 * 1000);
//                var title = prompt('Event Title:');
//                if (title) {
//                    calendar.addEvent({
//                        title: title,
//                        start: info.start,
//                        end: info.end
//                    });
//                }
//                calendar.unselect();
//            },
//            // ... other common options
//        };

//        var calendar = new FullCalendar.Calendar(calendarEl, calendarOptions);

//        // Render the calendar
//        calendar.render();
//        console.log("Calendar rendered");
//    } else {
//        console.error("Calendar element not found");
//    }
//};

//window.initializeCalendar = (viewType) => {
//    console.log("Initializing Calendar");

//    var calendarEl = document.getElementById('calendar');

//    if (calendarEl) {
//        // Customize calendar options based on the viewType
//        var calendarOptions = {
//            initialView: viewType === 'patients' ? 'timeGridWeek' : 'dayGridMonth',
//            // Add other options based on the viewType
//            // ...

//            // Common options
//            selectable: true,
//            selectAllow: function (selectInfo) {
//                var dayOfWeek = selectInfo.start.getDay();
//                return dayOfWeek !== 0 && dayOfWeek !== 6;
//            },
//            select: function (info) {
//                info.end = new Date(info.start.getTime() + 60 * 60 * 1000);
//                var title = prompt('Event Title:');
//                if (title) {
//                    calendar.addEvent({
//                        title: title,
//                        start: info.start,
//                        end: info.end
//                    });
//                }
//                calendar.unselect();
//            },
//            // ... other common options
//        };

//        var calendar = new FullCalendar.Calendar(calendarEl, calendarOptions);

//        // Render the calendar
//        calendar.render();
//        console.log("Calendar rendered");
//    } else {
//        console.error("Calendar element not found");
//    }
//};


//window.initializeCalendar = () => {
//    console.log("Initializing Calendar");
//    var calendarEl = document.getElementById('calendar');
//    if (calendarEl) {
//        var calendar = new FullCalendar.Calendar(calendarEl, {
//            initialView: 'timeGridWeek',
//            firstDay: 1,
//            slotLabelFormat: {
//                hour: '2-digit',
//                minute: '2-digit',
//                hour12: false
//            },
//            eventTimeFormat: {
//                hour: '2-digit',
//                minute: '2-digit',
//                hour12: false
//            },
//            businessHours: {
//                daysOfWeek: [1, 2, 3, 4, 5],
//                startTime: '09:00',
//                endTime: '15:00',
//            },
//            slotMinTime: '09:00',
//            slotMaxTime: '15:00',
//            slotDuration: '01:00:00',
//            selectable: true,
//            selectAllow: function (selectInfo) {

//                var dayOfWeek = selectInfo.start.getDay();
//                return dayOfWeek !== 0 && dayOfWeek !== 6;
//            },
//            select: function (info) {

//                info.end = new Date(info.start.getTime() + 60 * 60 * 1000);

//                var title = prompt('Event Title:');
//                if (title) {
//                    calendar.addEvent({
//                        title: title,
//                        start: info.start,
//                        end: info.end
//                    });
//                }
//                calendar.unselect();
//            },
//            allDaySlot: false,
//            height: "auto",
//            expandRows: true,
//            nowIndicator: true,
//            dayHeaders: true,
//            navLinks: true,
//            editable: true,
//            dayMaxEvents: true,
//            locale: 'en',
//            events: [

//            ],
//            headerToolbar: {
//                left: 'prev,next today',
//                center: 'title',
//                right: 'timeGridWeek,timeGridDay'
//            },

//            height: 650

//        });
//        calendar.render();
//        console.log("Calendar rendered");
//    } else {
//        console.error("Calendar element not found");
//    }
//};
