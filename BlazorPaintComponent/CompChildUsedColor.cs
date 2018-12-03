using BlazorSvgHelper;
using BlazorSvgHelper.Classes.SubClasses;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPaintComponent
{
    public class CompChildUsedColor : BlazorComponent
    {

        [Parameter]
        protected int par_id { get; set; }

        public Action<string> ActionClicked { get; set; }

        private SvgHelper SvgHelper1 = new SvgHelper();


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            circle c = new circle()
            {
               // id = "usedcolorcircle" + Guid.NewGuid().ToString("d").Substring(1, 4),
                cx = (9-par_id) * 30 + 15,
                cy = 15,
                r = 10,
                fill = LocalData.UsedColors_List[par_id],
                stroke = "black",
                stroke_width = 1,
                onclick = "notEmpty",
            };


            SvgHelper1.Cmd_Render(c, 0, builder);

            base.BuildRenderTree(builder);

        }


        protected override void OnAfterRender()
        {
            SvgHelper1.ActionClicked = ComponentClicked;
            LocalData.Curr_CompChildUsedColor_List[par_id] = this;
        }


        public void ComponentClicked(UIMouseEventArgs e)
        {

            Console.WriteLine(LocalData.UsedColors_List[par_id]);
            ActionClicked?.Invoke(LocalData.UsedColors_List[par_id]);
        }


        public void Refresh()
        {
            StateHasChanged();
        }

        public void Dispose()
        {

        }



    }
}
