using BlazorSvgHelper;
using BlazorSvgHelper.Classes.SubClasses;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPaintComponent
{
    public class CompUsedColors_Logic : BlazorComponent, IDisposable
    {

        public Action<string> ActionColorClicked { get; set; }

        protected override void OnAfterRender()
        {

            LocalData.Curr_CompUsedColors = this;

            foreach (var item in LocalData.Curr_CompChildUsedColor_List)
            {
                item.ActionClicked = ColorSelected;
            }

            base.OnAfterRender();
        }

        private void ColorSelected(string a)
        {
            ActionColorClicked?.Invoke(a);
        }


        public void Refresh()
        {
            foreach (var item in LocalData.Curr_CompChildUsedColor_List)
            {
                item.Refresh();
            }

            StateHasChanged();
        }


        public void Dispose()
        {

        }
    }
}
