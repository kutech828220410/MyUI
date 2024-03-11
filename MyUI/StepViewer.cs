using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUI
{
    public enum eumStepState
    {
        Completed,
        Waiting,
        OutTime,
    }
    public class StepEntity
    {
        public string Id { get; set; }
        public string StepName { get; set; }
        public int StepOrder { get; set; }
        public eumStepState StepState { get; set; }
        public string StepDesc { get; set; }
        public object StepTag { get; set; }
        //public Image StepCompletedImage { get; set; }
        //public Image StepDoingImage { get; set; }
        public StepEntity(string id, string stepname, int steporder, string stepdesc, eumStepState stepstate, object tag)
        {
            this.Id = id;
            this.StepName = stepname;
            this.StepOrder = steporder;
            this.StepDesc = stepdesc;
            this.StepTag = tag;
            this.StepState = stepstate;
        }
    }
    public partial class StepViewer : UserControl
    {
        private int currentStep = 0;
        public int CurrentStep
        {
            get
            {
                return currentStep;
            }
            set
            {
                currentStep = value;
                Invalidate();
            }
        }
       


        public StepViewer()
        {
            InitializeComponent();
            this.Paint += StepViewer_Paint;
            Basic.Reflection.MakeDoubleBuffered(this, true);
        }

        public void Next()
        {
            CurrentStep++;
            if (CurrentStep > _dataSourceList.Count) CurrentStep = _dataSourceList.Count;
            this.Invoke(new Action(delegate 
            {
                this.Refresh();
            }));     
        }
        public void Pre()
        {
            CurrentStep--;
            if (CurrentStep <= 0) CurrentStep = 0;
            this.Invoke(new Action(delegate
            {
                this.Refresh();
            }));
        }
        private List<StepEntity> _dataSourceList = null;
        [Browsable(true), Category("StepViewer")]
        public List<StepEntity> ListDataSource
        {
            get
            {
                return _dataSourceList;
            }
            set
            {
                if (_dataSourceList != value)
                {
                    _dataSourceList = value;
                    Invalidate();
                }
            }
        }
        private int lineWidth = 120;
        [Browsable(true), Category("StepViewer")]
        public int LineWidth
        {
            get
            {
                return lineWidth;
            }
            set
            {
                lineWidth = value;
            }
        }


        public void DrawCircleWithCheck(Graphics graphic, RectangleF rectangleF, Color circleColor, Color checkColor)
        {
            // 設置外圓圈的筆刷
            using (Pen pen = new Pen(circleColor, 2))
            {
                // 繪製外圓圈
                graphic.DrawEllipse(pen, rectangleF);
            }

            // 設置勾勾的筆刷
            using (Pen pen = new Pen(checkColor, 2))
            {
                // 設置勾勾的點
                PointF[] points =
                {
            new PointF(rectangleF.Left + rectangleF.Width * 0.25f, rectangleF.Top + rectangleF.Height * 0.55f),
            new PointF(rectangleF.Left + rectangleF.Width * 0.4f, rectangleF.Top + rectangleF.Height * 0.7f),
            new PointF(rectangleF.Left + rectangleF.Width * 0.6f, rectangleF.Top + rectangleF.Height * 0.4f)
        };

                // 繪製勾勾
                graphic.DrawLines(pen, points);
            }
        }

        private void StepViewer_Paint(object sender, PaintEventArgs e)
        {
            if (this.ListDataSource != null)
            {
                int CenterY = this.Height / 2;
                int index = 1;
                int count = ListDataSource.Count;
                int StepNodeWH = 32;
                //this.Width = 32 * count + lineWidth * (count - 1) + 6+300;
                //defalut pen & brush
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Brush brush = new SolidBrush(Color.Gray);
                Pen p = new Pen(brush, 1f);
                Brush brushNode = new SolidBrush(Color.DarkGray);
                Pen penNode = new Pen(brushNode, 1f);
                Brush brushNodeCompleted = new SolidBrush(Color.Blue);
                Pen penNodeCompleted = new Pen(brushNodeCompleted, 1f);
                int initX = 6;
                //string
                Font nFont = new Font("微软雅黑", 12);
                Font stepFont = new Font("微软雅黑", 11, FontStyle.Bold);
                int NodeNameWidth = 0;
                foreach (var item in ListDataSource)
                {
                    //round
                    Rectangle rec = new Rectangle(initX, CenterY - StepNodeWH / 2, StepNodeWH, StepNodeWH);
                    if (CurrentStep == item.StepOrder)
                    {
                        if (item.StepState == eumStepState.OutTime)
                        {
                            e.Graphics.DrawEllipse(new Pen(Color.Red, 1f), rec);
                            e.Graphics.FillEllipse(new SolidBrush(Color.Red), rec);
                        }
                        else
                        {
                            e.Graphics.DrawEllipse(penNodeCompleted, rec);
                            e.Graphics.FillEllipse(brushNodeCompleted, rec);
                        }
                        //白色字体
                        SizeF fTitle = e.Graphics.MeasureString(index.ToString(), stepFont);
                        Point pTitle = new Point(initX + StepNodeWH / 2 - (int)Math.Round(fTitle.Width) / 2, CenterY - (int)Math.Round(fTitle.Height / 2));
                        e.Graphics.DrawString(index.ToString(), stepFont, Brushes.White, pTitle);
                        //nodeName
                        SizeF sNode = e.Graphics.MeasureString(item.StepName, nFont);
                        Point pNode = new Point(initX + StepNodeWH, CenterY - (int)Math.Round(sNode.Height / 2) + 2);
                        e.Graphics.DrawString(item.StepName, new Font(nFont, FontStyle.Bold), brushNode, pNode);
                        NodeNameWidth = (int)Math.Round(sNode.Width);
                        if (index < count)
                        {
                            e.Graphics.DrawLine(p, initX + StepNodeWH + NodeNameWidth, CenterY, initX + StepNodeWH + NodeNameWidth + lineWidth, CenterY);
                        }
                    }
                    else if (item.StepOrder < CurrentStep)
                    {
                        //completed
                        e.Graphics.DrawEllipse(penNodeCompleted, rec);
                        //image
                        RectangleF recRF = new RectangleF(rec.X + 6, rec.Y + 6, rec.Width - 12, rec.Height - 12);
                        DrawCircleWithCheck(e.Graphics, recRF, Color.Green, Color.Green);
                        //e.Graphics.DrawImage(ControlsResource.check_lightblue, recRF);
                        //nodeName
                        SizeF sNode = e.Graphics.MeasureString(item.StepName, nFont);
                        Point pNode = new Point(initX + StepNodeWH, CenterY - (int)Math.Round(sNode.Height / 2) + 2);
                        e.Graphics.DrawString(item.StepName, nFont, brushNode, pNode);
                        NodeNameWidth = (int)Math.Round(sNode.Width);
                        if (index < count)
                        {
                            e.Graphics.DrawLine(penNodeCompleted, initX + StepNodeWH + NodeNameWidth, CenterY, initX + StepNodeWH + NodeNameWidth + lineWidth, CenterY);
                        }
                    }
                    else
                    {
                        e.Graphics.DrawEllipse(p, rec);
                        //
                        SizeF fTitle = e.Graphics.MeasureString(index.ToString(), stepFont);
                        Point pTitle = new Point(initX + StepNodeWH / 2 - (int)Math.Round(fTitle.Width) / 2, CenterY - (int)Math.Round(fTitle.Height / 2));
                        e.Graphics.DrawString(index.ToString(), stepFont, brush, pTitle);
                        //nodeName
                        SizeF sNode = e.Graphics.MeasureString(item.StepName, nFont);
                        Point pNode = new Point(initX + StepNodeWH, CenterY - (int)Math.Round(sNode.Height / 2) + 2);
                        e.Graphics.DrawString(item.StepName, nFont, brushNode, pNode);
                        NodeNameWidth = (int)Math.Round(sNode.Width);
                        if (index < count)
                        {
                            //line
                            e.Graphics.DrawLine(p, initX + StepNodeWH + NodeNameWidth, CenterY, initX + StepNodeWH + NodeNameWidth + lineWidth, CenterY);
                        }
                    }
                    //描述信息
                    if (item.StepDesc != "")
                    {
                        Point pNode = new Point(initX + StepNodeWH, CenterY + 10);
                        e.Graphics.DrawString(item.StepDesc, new Font(nFont.FontFamily, 10), brush, pNode);
                    }
                    index++;
                    //8 is space width
                    initX = initX + lineWidth + StepNodeWH + NodeNameWidth + 8;
                }
            }
        }


    }
}
