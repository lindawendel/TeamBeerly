namespace HealthCare.WebApp.Pages.SingeltonTest
{

        public class GridService
        {
            private List<List<string>> grid;

            public GridService()
            {
                InitializeGrid();
            }

            public List<List<string>> GetGrid()
            {
                return grid;
            }

            private void InitializeGrid()
            {
                // Initialize the grid with some default values
                grid = new List<List<string>>
        {
            new List<string> {"A1", "A2", "A3"},
            new List<string> {"B1", "B2", "B3"},
            new List<string> {"C1", "C2", "C3"}
        };
            }
        }
    
}
