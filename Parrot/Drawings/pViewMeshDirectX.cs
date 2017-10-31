using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows;
using Parrot.Controls;

namespace Parrot.Drawings
{
    public class pViewMeshDirectX : pControl
    {
        public Canvas Element;
        public D3DImage IMG = new D3DImage(96, 96);
        
        public pViewMeshDirectX()
        {
            IMG.Lock();

            //...
            //your render code
            ///...

            //var surface = renderTexture.GetSurfaceLevel(0);



            //IMG.SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface.ComPointer);

            var rect = new Int32Rect(0, 0, 600, 600);

            IMG.AddDirtyRect(rect);

            IMG.Unlock();


        }

        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
        }

    }
}
