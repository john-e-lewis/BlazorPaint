using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorPaintComponent
{
    public class BPaintJsInterop
    {
        public static Task<string> alert(string message)
        {

            return JSRuntime.Current.InvokeAsync<string>(
                "JsInteropBPaintComp.alert",
                message);
        }


        public static Task<bool> GetElementBoundingClientRect(string id, DotNetObjectRef dotnethelper)
        {

            return JSRuntime.Current.InvokeAsync<bool>(
                "JsInteropBPaintComp.GetElementBoundingClientRect",
                new { id, dotnethelper });
        }


        public static Task<bool> SetCursor(string cursorStyle = "default")
        {

            return JSRuntime.Current.InvokeAsync<bool>(
                "JsInteropBPaintComp.SetCursor",
                cursorStyle);
        }
    }
}
