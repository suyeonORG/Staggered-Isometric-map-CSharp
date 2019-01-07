using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;

namespace StaggeringIsometricMapCsharp
{
    public class DiamondPictureBox : PictureBox
    {
        private GraphicsPath p;

        protected override void OnPaint(PaintEventArgs pe)
        {
            p = new GraphicsPath();
            p.AddPolygon(new Point[] { new Point(Width / 2, 0), new Point(Width, Height / 2), new Point(Width / 2, Height), new Point(0, Height / 2) });
            this.Region = new Region(p);
            base.OnPaint(pe);
        }
    }

}
