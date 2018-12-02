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
        public static CompMySVG Curr_CompMySVG;

        public static List<BPaintObject> ObjectsList = new List<BPaintObject>();


        public static MyPoint SVGPosition = new MyPoint() { x = 0, y = 0 };



        [JSInvokable]
        public static void invokeFromjs_UpdateSVGPosition(double par_x, double par_y)
        {
            SVGPosition = new MyPoint() { x = par_x, y = par_y };

        }

      
    }
}
