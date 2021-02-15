#pragma checksum "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c63c58038f86048d205bbbb5369719c389f26a51"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\_ViewImports.cshtml"
using ParkyWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\_ViewImports.cshtml"
using ParkyWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c63c58038f86048d205bbbb5369719c389f26a51", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9bcb7748360e1430599a0955dc17d19f99604859", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ParkyWeb.Models.ViewModel.IndexVM>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"container\">\r\n    <div class=\"row pb-4 backgroundWhite\">\r\n\r\n");
#nullable restore
#line 6 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
         foreach (var nationalPark in Model.NationalParkList)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""container backgroundWhite pb-4"">
                <div class=""card border"">
                    <div class=""card-header bg-dark text-light ml-0 row container"">
                        <div class=""col-12 col-md-6"">
                            <h1 class=""text-warning"">");
#nullable restore
#line 12 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                                Write(nationalPark.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n                        </div>\r\n                        <div class=\"col-12 col-md-6 text-md-right\">\r\n                            <h1 class=\"text-warning\">State : ");
#nullable restore
#line 15 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                                        Write(nationalPark.State);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
                        </div>
                    </div>
                    <div class=""card-body"">
                        <div class=""container rounded p-2"">
                            <div class=""row"">
                                <div class=""col-12 col-lg-8"">
                                    <div class=""row"">
                                        <div class=""col-12"">
                                            <h3 style=""color:#bbb9b9"">Established: ");
#nullable restore
#line 24 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                                                              Write(nationalPark.Established.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                                        </div>\r\n                                        <div class=\"col-12\">\r\n");
#nullable restore
#line 27 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                             if (Model.TrailList.Where(u => u.NationalParkId == nationalPark.Id).Count() > 0)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                <table class=""table table-striped"" style=""border:1px solid #808080 "">
                                                    <tr class=""table-secondary"">
                                                        <th>
                                                            Trail
                                                        </th>
                                                        <th>Distance</th>
                                                        <th>Elevation Gain</th>
                                                        <th>Difficulty</th>
                                                    </tr>


");
#nullable restore
#line 40 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                                     foreach (var trails in Model.TrailList.Where(u => u.NationalParkId == nationalPark.Id))
                                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                        <tr>\r\n                                                            <td>");
#nullable restore
#line 43 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                                           Write(trails.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                            <td>");
#nullable restore
#line 44 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                                           Write(trails.Distance);

#line default
#line hidden
#nullable disable
            WriteLiteral(" miles</td>\r\n                                                            <td>");
#nullable restore
#line 45 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                                           Write(trails.Elevation);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ft</td>\r\n                                                            <td>");
#nullable restore
#line 46 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                                           Write(trails.Difficulty);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                        </tr>\r\n");
#nullable restore
#line 48 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                </table>\r\n");
#nullable restore
#line 50 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                            }
                                            else
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <p>No trail exists...</p>\r\n");
#nullable restore
#line 54 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        </div>\r\n                                    </div>\r\n                                </div>\r\n                                <div class=\"col-12 col-lg-4 text-center\">\r\n");
#nullable restore
#line 59 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                     if (nationalPark.Picture != null)
                                    {
                                        var base64 = Convert.ToBase64String(nationalPark.Picture);
                                        var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <img");
            BeginWriteAttribute("src", " src=\"", 3807, "\"", 3820, 1);
#nullable restore
#line 63 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
WriteAttributeValue("", 3813, imgsrc, 3813, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top p-2 rounded\" width=\"100%\" />\r\n");
#nullable restore
#line 64 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 71 "C:\Users\rhasq\OneDrive\Documents\Courses\Web API .net core 3\Parky\ParkyWeb\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ParkyWeb.Models.ViewModel.IndexVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
