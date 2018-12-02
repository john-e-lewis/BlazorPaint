using BlazorPaintComponent.classes;
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
        

    }
}
