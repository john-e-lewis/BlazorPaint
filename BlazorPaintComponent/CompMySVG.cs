using BlazorPaintComponent.classes;
using BlazorSvgHelper;
using BlazorSvgHelper.Classes.SubClasses;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPaintComponent
{
    public class CompMySVG: BlazorComponent, IDisposable
    {


        [Parameter]
        public double par_width { get; set; }

        [Parameter]
        public double par_height { get; set; }

       
        svg _Svg = null;


        SvgHelper SvgHelper1 = new SvgHelper();



        protected override void OnAfterRender()
        {

            LocalData.Curr_CompMySVG = this;


            base.OnAfterRender();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            Generate_SVG();

            SvgHelper1.Cmd_Render(_Svg, 0, builder);

            base.BuildRenderTree(builder);
        }

        public void Generate_SVG()
        {
            _Svg = new svg
            {
                id = "svgPaint",
                width = par_width,
                height = par_height,
                xmlns = "http://www.w3.org/2000/svg",
            };

            _Svg.Children.Add(new rect
            {
                width = par_width,
                height = par_height,
                fill = "wheat",
                stroke = "red",
                stroke_width = 1,
            });



            foreach (var item in LocalData.ObjectsList.OrderBy(x=>x.SequenceNumber))
            {
                switch (item.ObjectType)
                {
                    case BPaintOpbjectType.HandDraw:
                        _Svg.Children.Add(drawPath(item as BPaintHandDraw));
                        break;
                    case BPaintOpbjectType.Line:
                        BPaintLine currLine = item as BPaintLine;
                        _Svg.Children.Add(drawLine(currLine));
                        if (item.Selected)
                        {
                            _Svg.Children.Add(new circle()
                            {
                                cx = currLine.start.x + currLine.PositionChange.x,
                                cy = currLine.start.y + currLine.PositionChange.y,
                                r = currLine.width * 1.5,
                                fill = "wheat",
                                stroke = currLine.Color,
                                stroke_width = 2,
                            });

                            _Svg.Children.Add(new circle()
                            {
                                cx = currLine.end.x + currLine.PositionChange.x,
                                cy = currLine.end.y + currLine.PositionChange.y,
                                r = currLine.width * 1.5,
                                fill = "wheat",
                                stroke = currLine.Color,
                                stroke_width = 2,
                            });
                        }

                        
                        break;
                    default:
                        break;
                }
            }

        }



        private line drawLine(BPaintLine Par_Object)
        {
            return new line()
            {
                x1 = Par_Object.start.x + Par_Object.PositionChange.x,
                y1 = Par_Object.start.y + Par_Object.PositionChange.y,
                x2 = Par_Object.end.x + Par_Object.PositionChange.x,
                y2 = Par_Object.end.y + Par_Object.PositionChange.y,
               // opacity = 1,
                stroke = Par_Object.Color,
                stroke_width = Par_Object.width,
               // stroke_linecap = strokeLinecap.round,
            };
        }


        private path drawPath(BPaintHandDraw Par_Object)
        {

            StringBuilder sb = new StringBuilder();

            bool IsFirst = true;


            sb.Append("M");
            sb.Append(Par_Object.data[0].x + Par_Object.PositionChange.x);
            sb.Append(" ");
            sb.Append(Par_Object.data[0].y + Par_Object.PositionChange.y);
            sb.Append(" ");

            for (int i = 1; i < Par_Object.data.Count; i++)
            {
                sb.Append("L");
                sb.Append(Par_Object.data[i].x + Par_Object.PositionChange.x);
                sb.Append(" ");
                sb.Append(Par_Object.data[i].y + Par_Object.PositionChange.y);
                sb.Append(" ");

               
            }


            return new path()
            {
                id = "path" + Par_Object.ObjectID,
                fill = "none",
                stroke = Par_Object.Color,
                d = sb.ToString(),
                // opacity = _opacity,
                stroke_width = Par_Object.width,
            };

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
