namespace ProductViewer.WebUI.Models
{
    /// <summary>
    /// Class SortConfig for set configuration of sorting. That class help view curresnt sort config at Index view and help send config from client to server
    /// </summary>
    public class SortConfig
    {
        /// <summary>
        /// Name sort if true asc, else desc. Default value = true
        /// </summary>
        public bool? Name { get; set; } = true;

        /// <summary>
        /// UnitPrice sort if true asc, else desc. Default value = null
        /// </summary>
        public bool? UnitPrice { get; set; } = null;
        /// <summary>
        /// Quantity sort if true asc, else desc. Default value = null
        /// </summary>
        public bool? Quantity { get; set; } = null;
        /// <summary>
        /// PriceForAll sort if true asc, else desc. Default value = null
        /// </summary>
        public bool? PriceForAll { get; set; } = null;
    }
}