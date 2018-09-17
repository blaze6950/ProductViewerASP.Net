namespace ProductViewer.WebUI.Models
{
    /// <summary>
    /// This enumeration of columns by which the list can be sorted
    /// </summary>
    public enum Column
    {
        Name,
        UnitPrice,
        Quantity,
        PriceForAll
    }

    /// <summary>
    /// Class SortConfig for set configuration of sorting. That class help view curresnt sort config at Index view and help send config from client to server
    /// </summary>
    public class SortConfig
    {
        public SortConfig()
        {
            CurrentColumn = Column.Name;
            IsAsc = true;
        }

        /// <summary>
        /// CurrentColumn property is responsible on which column do sorting
        /// </summary>
        public Column CurrentColumn { get; set; }
        /// <summary>
        /// IsAsc property is responsible for sorting direction
        /// </summary>
        public bool IsAsc { get; set; }
    }
}