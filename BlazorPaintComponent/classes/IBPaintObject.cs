using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPaintComponent.classes
{
    public interface IBPaintObject
    {
        int ObjectID { get; set; }
        bool Selected { get; set; }
        int SequenceNumber { get; set; }

        MyPoint PositionChange { get; set; }

        BPaintOpbjectType ObjectType { get; set; }
    }
}
