using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPaintComponent.classes
{
    public class BPaintObject : IBPaintObject
    {
        public int ObjectID { get; set; }
        public bool Selected { get; set; }
        public int SequenceNumber { get; set; }
        public MyPoint PositionChange { get; set; } = new MyPoint() { x = 0, y = 0 };
        public BPaintOpbjectType ObjectType { get; set; }

    }
}
