using System.Text;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using ProductViewer.WebUI.Models;

namespace ProductViewer.WebUI.HtmlHelpers
{
    public static class SortColumnHelpers
    {
        public static MvcHtmlString SortColumn(this HtmlHelper html, SortConfig sortConfig,
            Func<Column, string> sortUrl)
        {
            StringBuilder result = new StringBuilder();

            #region NameColumn
            var iTag = new TagBuilder("i");
            if (sortConfig.CurrentColumn != Column.Name)
            {
                iTag.Attributes["class"] = "material-icons right hide";
            }
            else if (sortConfig.IsAsc == true)
            {
                iTag.Attributes["class"] = "material-icons right";
                iTag.SetInnerText("expand_less");
            }
            else
            {
                iTag.Attributes["class"] = "material-icons right";
                iTag.SetInnerText("expand_more");
            }
            var aTag = new TagBuilder("a");
            aTag.Attributes["class"] = "waves-effect waves-teal btn-flat no-padding";
            aTag.Attributes["href"] = $"{sortUrl(Column.Name)}";
            aTag.InnerHtml = iTag.ToString() + "Name";
            var thTag = new TagBuilder("th");
            thTag.Attributes["style"] = "min-width: 6em";
            thTag.InnerHtml = aTag.ToString();
            result.AppendLine(thTag.ToString());
            #endregion

            #region DescriptionColumn
            thTag = new TagBuilder("th");
            thTag.InnerHtml = "DESCRIPTION";
            result.AppendLine(thTag.ToString());
            #endregion

            #region UnitPriceColumn
            iTag = new TagBuilder("i");
            if (sortConfig.CurrentColumn != Column.UnitPrice)
            {
                iTag.Attributes["class"] = "material-icons right hide";
            }
            else if (sortConfig.IsAsc == true)
            {
                iTag.Attributes["class"] = "material-icons right";
                iTag.SetInnerText("expand_less");
            }
            else
            {
                iTag.Attributes["class"] = "material-icons right";
                iTag.SetInnerText("expand_more");
            }
            aTag = new TagBuilder("a");
            aTag.Attributes["class"] = "waves-effect waves-teal btn-flat no-padding";
            aTag.Attributes["href"] = $"{sortUrl(Column.UnitPrice)}";
            aTag.InnerHtml = iTag.ToString() + "Unit price";
            thTag = new TagBuilder("th");
            thTag.Attributes["style"] = "min-width: 9em";
            thTag.InnerHtml = aTag.ToString();
            result.AppendLine(thTag.ToString());
            #endregion

            #region QuantityColumn
            iTag = new TagBuilder("i");
            if (sortConfig.CurrentColumn != Column.Quantity)
            {
                iTag.Attributes["class"] = "material-icons right hide";
            }
            else if (sortConfig.IsAsc == true)
            {
                iTag.Attributes["class"] = "material-icons right";
                iTag.SetInnerText("expand_less");
            }
            else
            {
                iTag.Attributes["class"] = "material-icons right";
                iTag.SetInnerText("expand_more");
            }
            aTag = new TagBuilder("a");
            aTag.Attributes["class"] = "waves-effect waves-teal btn-flat no-padding";
            aTag.Attributes["href"] = $"{sortUrl(Column.Quantity)}";
            aTag.InnerHtml = iTag.ToString() + "Quantity";
            thTag = new TagBuilder("th");
            thTag.Attributes["style"] = "min-width: 8em";
            thTag.InnerHtml = aTag.ToString();
            result.AppendLine(thTag.ToString());
            #endregion

            #region PriceForAllColumn
            iTag = new TagBuilder("i");
            if (sortConfig.CurrentColumn != Column.PriceForAll)
            {
                iTag.Attributes["class"] = "material-icons right hide";
            }
            else if (sortConfig.IsAsc == true)
            {
                iTag.Attributes["class"] = "material-icons right";
                iTag.SetInnerText("expand_less");
            }
            else
            {
                iTag.Attributes["class"] = "material-icons right";
                iTag.SetInnerText("expand_more");
            }
            aTag = new TagBuilder("a");
            aTag.Attributes["class"] = "waves-effect waves-teal btn-flat no-padding";
            aTag.Attributes["href"] = $"{sortUrl(Column.PriceForAll)}";
            aTag.InnerHtml = iTag.ToString() + "Price for all";
            thTag = new TagBuilder("th");
            thTag.Attributes["style"] = "min-width: 10em";
            thTag.InnerHtml = aTag.ToString();
            result.AppendLine(thTag.ToString());
            #endregion

            return MvcHtmlString.Create(result.ToString());
        }
    }
}