using BlazorPaintComponent.classes;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPaintComponent
{
    public class CompBlazorPaint_Logic: BlazorComponent
    {
        bool IsCompLoaded = false;

        int CurrObjectID = 0;


        protected string Color1 = "#fc3807";

        protected int LineWidth1 = 3;

        protected int StepSize = 5;


        protected int modeCode = 1;
      
        protected override void OnAfterRender()
        {

            if (IsCompLoaded == false)
            {
                GetBoundingClientRect("PaintArea1");


                IsCompLoaded = true;
            }

            base.OnAfterRender();

        }


        public void cmd_clear()
        {
            if (LocalData.ObjectsList.Any())
            {
                LocalData.ObjectsList = new List<BPaintObject>();
                cmd_RefreshSVG();
            }
        }


        public void cmd_undo()
        {
            if (LocalData.ObjectsList.Any())
            {
                LocalData.ObjectsList.Remove(LocalData.ObjectsList.Last());
                cmd_RefreshSVG();
            }
        }


        public void cmd_onmousedown(UIMouseEventArgs e)
        {
            if (modeCode == 1)
            {
                cmd_prepareDraw(e);
            }

            if (modeCode == 2)
            {
                cmd_prepareLine(e);
            }

        }



        public void cmd_prepareDraw(UIMouseEventArgs e)
        {
            MyPoint CurrPosition = new MyPoint() { x = e.ClientX - LocalData.SVGPosition.x, y = e.ClientY - LocalData.SVGPosition.y };

            BPaintHandDraw new_BPaintHandDraw = new BPaintHandDraw();


            if (LocalData.ObjectsList.Any())
            {
                new_BPaintHandDraw.ObjectID = LocalData.ObjectsList.Max(x => x.ObjectID) + 1;
            }
            else
            {
                new_BPaintHandDraw.ObjectID = 1;
            }

            new_BPaintHandDraw.ObjectType = BPaintOpbjectType.HandDraw;
            new_BPaintHandDraw.Color = Color1;
            new_BPaintHandDraw.width = LineWidth1;
            new_BPaintHandDraw.data = new List<MyPoint>();
            new_BPaintHandDraw.data.Add(new MyPoint() { x = CurrPosition.x, y = CurrPosition.y });

            LocalData.ObjectsList.Add(new_BPaintHandDraw);


            CurrObjectID = new_BPaintHandDraw.ObjectID;

        }


        public void cmd_prepareLine(UIMouseEventArgs e)
        {
            MyPoint CurrPosition = new MyPoint() { x = e.ClientX - LocalData.SVGPosition.x, y = e.ClientY - LocalData.SVGPosition.y };

            BPaintLine new_BPaintLine = new BPaintLine();


            if (LocalData.ObjectsList.Any())
            {
                new_BPaintLine.ObjectID = LocalData.ObjectsList.Max(x => x.ObjectID) + 1;


                foreach (var item in LocalData.ObjectsList.Where(x => x.Selected))
                {
                    item.Selected = false;
                }  
            }
            else
            {
                new_BPaintLine.ObjectID = 1;
            }


            
           


            new_BPaintLine.ObjectType = BPaintOpbjectType.Line;
            new_BPaintLine.Selected = true;
            new_BPaintLine.Color = Color1;
            new_BPaintLine.width = LineWidth1;
            new_BPaintLine.start = new MyPoint() { x = CurrPosition.x, y = CurrPosition.y };
            new_BPaintLine.end = new_BPaintLine.start;
            LocalData.ObjectsList.Add(new_BPaintLine);


            CurrObjectID = new_BPaintLine.ObjectID;

        }


        public void cmd_onmousemove(UIMouseEventArgs e)
        {
            if (modeCode == 1)
            {
                cmd_draw(e);
            }

            if (modeCode == 2)
            {
                cmd_Line(e);
            }
        }


        public void cmd_draw(UIMouseEventArgs e)
        { 
            MyPoint CurrPosition = new MyPoint() { x = e.ClientX - LocalData.SVGPosition.x, y = e.ClientY - LocalData.SVGPosition.y };


            BPaintHandDraw Curr_Object = (BPaintHandDraw)LocalData.ObjectsList.Single(x => x.ObjectID == CurrObjectID);

            if (Curr_Object.data.Any())
            {

                MyPoint LastPoint = Curr_Object.data.Last();

                if (LastPoint.x!= CurrPosition.x || LastPoint.y!= CurrPosition.y)
                {
                    Curr_Object.data.Add(CurrPosition);
                    cmd_RefreshSVG();
                }
            }
            else
            {
                Curr_Object.data.Add(CurrPosition);
                cmd_RefreshSVG();
            }
        }

        public void cmd_Line(UIMouseEventArgs e)
        {
            MyPoint CurrPosition = new MyPoint() { x = e.ClientX - LocalData.SVGPosition.x, y = e.ClientY - LocalData.SVGPosition.y };


            BPaintLine Curr_Object = (BPaintLine)LocalData.ObjectsList.Single(x => x.ObjectID == CurrObjectID);


                if (Curr_Object.end.x != CurrPosition.x || Curr_Object.end.y != CurrPosition.y)
                {
                    Curr_Object.end = CurrPosition;
                    cmd_RefreshSVG();
                }
            
        }


        public void cmd_onmouseup(UIMouseEventArgs e)
        {

            CurrObjectID = 0;

        }


        void cmd_RefreshSVG()
        {
            LocalData.Curr_CompMySVG.Refresh();
            StateHasChanged();

        }



        public void Dispose()
        {

        }



        


        protected void cmd_Move(BPaintMoveDirection Par_Direction)
        {

            bool returnZeroID = CurrObjectID == 0;

            if (LocalData.ObjectsList.Any())
            {
                if (returnZeroID)
                {

                    CurrObjectID = LocalData.ObjectsList.Last().ObjectID;
    
                }
                

                BPaintObject Curr_Object = LocalData.ObjectsList.Single(x => x.ObjectID == CurrObjectID);


                switch (Par_Direction)
                {
                    case BPaintMoveDirection.left:
                        Curr_Object.PositionChange.x -= StepSize;
                        break;
                    case BPaintMoveDirection.right:
                        Curr_Object.PositionChange.x += StepSize;
                        break;
                    case BPaintMoveDirection.up:
                        Curr_Object.PositionChange.y -= StepSize;
                        break;
                    case BPaintMoveDirection.down:
                        Curr_Object.PositionChange.y += StepSize;
                        break;
                    default:
                        break;
                }

                

                cmd_RefreshSVG();

                if (returnZeroID)
                {
                    CurrObjectID = 0;
                }
            }
          
        }

      

        public void GetBoundingClientRect(string ElementID)
        {
            BPaintJsInterop.GetElementBoundingClientRect(ElementID, new DotNetObjectRef(this));
        }

        [JSInvokable]
        public void invokeFromjs(string id, double par_x, double par_y)
        {
          LocalData.SVGPosition = new MyPoint() { x = par_x, y = par_y };
            
        }

    }
}
