using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaggeringIsometricMapCsharp
{
    public partial class Main : Form
    {

        public bool Drag = false; public int Xaxys = 0; public int Yaxys = 0;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            draw_map();
            enableDragControl();
            enableDragForm();
        }
        private void enableDragForm()
        {
            this.MouseUp += (e_sender, eventargs) =>
            {
                disableDrag();
            };
            this.MouseDown += (e_sender, eventargs) =>
            {
                enableDrag();
            };
            this.MouseMove += (e_sender, eventargs) =>
            {
                moveControl();
            };
        }

        private void enableDragControl()
        {
            foreach (Control c in this.Controls)
            {
                c.MouseUp += (e_sender, eventargs) =>
                {
                    disableDrag();
                };
                c.MouseDown += (e_sender, eventargs) =>
                {
                    enableDrag();
                };
                c.MouseMove += (e_sender, eventargs) =>
                {
                    moveControl();
                };
                if ((c) is Panel)
                {
                    foreach (Control panelctl in c.Controls)
                    {
                        panelctl.MouseUp += (e_sender, eventargs) =>
                        {
                            disableDrag();
                        };
                        panelctl.MouseDown += (e_sender, eventargs) =>
                        {
                            enableDrag();
                        };
                        panelctl.MouseMove += (e_sender, eventargs) =>
                        {
                            moveControl();
                        };
                    }
                }
            }
        }

        public void draw_map()
        {
            Graphics g = IsometricGrid.CreateGraphics();

            int MapWidth = 15;
            int MapHeight = 17;

            double cellWidth = 40;
            double cellHeight = Math.Ceiling(cellWidth / 2);

            int offsetX = Convert.ToInt32((Width - ((MapWidth + 0.5) * cellWidth)) / (double)2);
            var offsetY = Convert.ToInt32((Height - ((MapHeight + 0.5) * cellHeight)) / (double)2);

            double medianCellH = cellHeight / 2;
            double medianCellW = cellWidth / 2;

            int count = 0;

            for (int y = 0; y <= 2 * MapHeight - 1; y++)
            {
                if ((y % 2) == 0)
                {
                    for (int x = 0; x <= MapWidth - 1; x++)
                    {
                        Point left = new Point(System.Convert.ToInt32((offsetX + x * cellWidth)), System.Convert.ToInt32((offsetY + y * medianCellH + medianCellH)));
                        Point top = new Point(System.Convert.ToInt32((offsetX + (x * cellWidth) + medianCellW)), System.Convert.ToInt32((offsetY + (y * medianCellH))));
                        Point right = new Point(System.Convert.ToInt32((offsetX + x * cellWidth + cellWidth)), System.Convert.ToInt32((offsetY + y * medianCellH + medianCellH)));
                        Point down = new Point(System.Convert.ToInt32((offsetX + (x * cellWidth) + medianCellW)), System.Convert.ToInt32((offsetY + (y * medianCellH) + cellHeight)));
                        renderCell(left, top, right, down, count, Color.DimGray);
                        count += 1;
                    }
                }
                else
                    for (int x = 0; x <= MapWidth - 2; x++)
                    {
                        Point left = new Point(System.Convert.ToInt32((offsetX + x * cellWidth + medianCellW)), System.Convert.ToInt32((offsetY + y * medianCellH + medianCellH)));
                        Point top = new Point(System.Convert.ToInt32((offsetX + x * cellWidth + cellWidth)), System.Convert.ToInt32((offsetY + y * medianCellH)));
                        Point right = new Point(System.Convert.ToInt32((offsetX + x * cellWidth + cellWidth + medianCellW)), System.Convert.ToInt32((offsetY + y * medianCellH + medianCellH)));
                        Point down = new Point(System.Convert.ToInt32((offsetX + x * cellWidth + cellWidth)), System.Convert.ToInt32((offsetY + y * medianCellH + cellHeight)));
                        renderCell(left, top, right, down, count, Color.Teal);
                        count += 1;
                    }
            }
        }

        private void renderCell(Point left, Point top, Point right, Point down, int count, Color color)
        {
            DiamondPictureBox cell = new DiamondPictureBox();
            cell.BackColor = color;
            cell.Size = new Size(36, 18);
            cell.Location = left;
            cell.Name = count.ToString();
            cell.Click += (e_sender, eventargs) =>
            {
                DiamondPictureBox diamondPictureBox = (DiamondPictureBox)e_sender;
                MessageBox.Show(diamondPictureBox.Name);
            };
            cell.MouseMove += (e_sender, eventargs) =>
            {
                cell.BackColor = Color.Teal;
            };
            cell.MouseLeave += (e_sender, eventargs) =>
            {
                cell.BackColor = Color.DarkGray;
            };
            IsometricGrid.Controls.Add(cell);
        }

        private void disableDrag()
        {
            Drag = false;
        }

        private void enableDrag()
        {
            Drag = true;
            Xaxys = System.Windows.Forms.Cursor.Position.X - this.Left;
            Yaxys = System.Windows.Forms.Cursor.Position.Y - this.Top;
        }

        private void moveControl()
        {
            if (Drag)
            {
                this.Top = System.Windows.Forms.Cursor.Position.Y - Yaxys;
                this.Left = System.Windows.Forms.Cursor.Position.X - Xaxys;
            }
        }
    }
}
