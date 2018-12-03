using BlazorPaintComponent.classes;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPaintComponent
{
    public static class LocalData
    {
        public static CompMySVG Curr_CompMySVG = new CompMySVG();
        public static CompUsedColors_Logic Curr_CompUsedColors = new CompUsedColors_Logic();
        public static List<CompChildUsedColor> Curr_CompChildUsedColor_List = new List<CompChildUsedColor>();


        public static List<BPaintObject> ObjectsList = new List<BPaintObject>();

        public static List<string> UsedColors_List = new List<string>();

        public static MyPoint SVGPosition = new MyPoint() { x = 0, y = 0 };



        [JSInvokable]
        public static void invokeFromjs_UpdateSVGPosition(double par_x, double par_y)
        {
            SVGPosition = new MyPoint() { x = par_x, y = par_y };

        }

      
    }
}
