using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using ToDoApp.Models;

namespace ToDoApp.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")] // Передаем в представление значение с префиксом
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>(); // Сопоставляем каждой строке объект

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";
            
            //список ul
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            //формирование 3х ссылок на current, next , prev
            TagBuilder currentItem = CreateTag(PageViewModel.PageNumber, urlHelper);

            //ссылка на пред страницу
            if(PageViewModel.HasPreviousPage)
            {
                TagBuilder prevItem = CreateTag(PageViewModel.PageNumber - 1, urlHelper);
                tag.InnerHtml.AppendHtml(prevItem);
            }
            tag.InnerHtml.AppendHtml(currentItem);
            // ссылка на next page
            if(PageViewModel.HasNextPage)
            {
                TagBuilder nextItem = CreateTag(PageViewModel.PageNumber + 1, urlHelper);
                tag.InnerHtml.AppendHtml(nextItem);
            }
            output.Content.AppendHtml(tag);
        }

        private TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if(pageNumber == this.PageViewModel.PageNumber)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = pageNumber });
            }

            item.AddCssClass("page-item");
            link.AddCssClass("page-link");

            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            
            return item;
        }
    }
}
