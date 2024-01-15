// GridService.cs
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace HealthCare.WebApp.Pages.SingeltonTest
{
    public class GridService
    {
        private readonly IHubContext<GridHub> hubContext;
        private List<List<GridCell>> grid;
        private List<ClickEvent> clickEvents;

        public GridService(IHubContext<GridHub> hubContext)
        {
            this.hubContext = hubContext;
            InitializeGrid();
            InitializeClickEvents();
        }

        public List<List<GridCell>> GetGrid()
        {
            return grid;
        }

        public void UpdateGrid()
        {
            foreach (var row in grid)
            {
                foreach (var cell in row)
                {
                    var clickEvent = clickEvents.FirstOrDefault(c => c.Cell == cell.Value);
                    cell.IsClickable = clickEvent != null && clickEvent.IsClickable;
                }
            }
        }

        public void ResetGrid()
        {
            foreach (var clickEvent in clickEvents)
            {
                clickEvent.IsClickable = true;
            }

            // Reset the grid with default values
            InitializeGrid();
            // Update clickable cells after resetting the grid
            UpdateGrid();
        }

        public List<ClickEvent> ClickEvents => clickEvents;

        private void InitializeGrid()
        {
            // Initialize the grid with some default values
            grid = new List<List<GridCell>>
            {
                new List<GridCell> {new GridCell("A1"), new GridCell("A2"), new GridCell("A3")},
                new List<GridCell> {new GridCell("B1"), new GridCell("B2"), new GridCell("B3")},
                new List<GridCell> {new GridCell("C1"), new GridCell("C2"), new GridCell("C3")}
            };
        }

        private void InitializeClickEvents()
        {
            // Initialize the clickEvents list with some initial clickable cells
            clickEvents = new List<ClickEvent>
            {
                new ClickEvent { Id = 1, Cell = "A1", Time = DateTime.Now, IsClickable = true },
                new ClickEvent { Id = 2, Cell = "B2", Time = DateTime.Now, IsClickable = true },
                // Add more seeded clickable cells as needed
            };
        }

        public class GridCell
        {
            public GridCell(string value)
            {
                Value = value;
            }

            public string Value { get; }
            public bool IsClickable { get; set; } = false;
        }

        public class ClickEvent
        {
            public DateTime Time { get; set; }
            public int Id { get; set; }
            public string Cell { get; set; }
            public bool IsClickable { get; set; } = false;
        }
    }
}
