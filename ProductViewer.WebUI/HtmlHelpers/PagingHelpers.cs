using System;
using System.Text;
using System.Web.Mvc;
using ProductViewer.WebUI.Models;

namespace ProductViewer.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            var leftArrow = new TagBuilder("li");
            if (pagingInfo.CurrentPage == 1)
            {
                leftArrow.AddCssClass("disabled");
                leftArrow.InnerHtml = $@"<a href=""#!""><i class=""material-icons"">chevron_left</i></a>";
            }
            else
            {
                leftArrow.AddCssClass("waves-effect");
                leftArrow.InnerHtml = $@"<a href=""{ pageUrl(pagingInfo.CurrentPage - 1) }""><i class=""material-icons"">chevron_left</i></a>";
            }
            result.Append(leftArrow.ToString());
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder liTag = new TagBuilder("li");
                TagBuilder aTag = new TagBuilder("a");
                aTag.MergeAttribute("href", pageUrl(i));
                aTag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    liTag.AddCssClass("active");
                }
                else
                {
                    liTag.AddCssClass("waves-effect");
                }
                liTag.InnerHtml = aTag.ToString();
                result.Append(liTag.ToString());
            }
            var rightArrow = new TagBuilder("li");
            if (pagingInfo.TotalPages - pagingInfo.CurrentPage > 0)
            {
                rightArrow.AddCssClass("waves-effect");
                rightArrow.InnerHtml = $@"<a href=""{ pageUrl(pagingInfo.CurrentPage + 1) }""><i class=""material-icons"">chevron_right</i></a>";
            }
            else
            {
                rightArrow.AddCssClass("disabled");
                rightArrow.InnerHtml = $@"<a href=""#!""><i class=""material-icons"">chevron_right</i></a>";
            }
            result.Append(rightArrow.ToString());
            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination center-align");
            ulTag.InnerHtml = result.ToString();
            return MvcHtmlString.Create(ulTag.ToString());
        }
    }
}