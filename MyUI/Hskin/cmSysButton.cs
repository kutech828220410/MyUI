using HsWin;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


public class CmSysButton
{
    public CmSysButton() { }

 
    public CmSysButton Clone()
    {
        CmSysButton SysButton = new CmSysButton();
        SysButton.Bounds = this.Bounds;
        SysButton.Location = this.Location;
        SysButton.size = this.Size;
        SysButton.ToolTip = this.ToolTip; ;
        SysButton.SysButtonNorml = this.SysButtonNorml;
        SysButton.SysButtonMouse = this.SysButtonMouse;
        SysButton.SysButtonDown = this.SysButtonDown;
        SysButton.OwnerForm = this.OwnerForm;
        SysButton.Name = this.Name;
        return SysButton;
    }

    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private ControlBoxState boxState;

    [Browsable(false)]
    public ControlBoxState BoxState
    {
        get { return boxState; }
        set
        {
            if (boxState != value)
            {
                boxState = value;
                if (OwnerForm != null)
                {
                    OwnerForm.Invalidate(Bounds);
                }
            }
        }
    }

    private Rectangle bounds;

    [Browsable(false)]
    public Rectangle Bounds
    {
        get
        {
            if (bounds == Rectangle.Empty)
            {
                bounds = new Rectangle();
            }
            bounds.Location = Location;
            bounds.Size = Size;
            return bounds;
        }
        set
        {
            bounds = value;
            Location = bounds.Location;
            Size = bounds.Size;
        }
    }

    private Point location = new Point(0, 0);

    [Browsable(false)]
    [Category("按鈕的位置")]
    public Point Location
    {
        get { return location; }
        set
        {
            if (location != value)
            {
                location = value;
            }
        }
    }

    private Size size = new Size(28, 20);
    /// <summary>
    /// 按钮的大小
    /// </summary>
    [DefaultValue(typeof(Size), "28, 20")]
    [Description("設定自定義按鈕的大小")]
    [Category("按鈕大小")]
    public Size Size
    {
        get { return size; }
        set
        {
            if (size != value)
            {
                size = value;
            }
        }
    }

    private string toolTip;

    [Category("懸浮提示")]
    [Description("自定義系統按鈕懸浮提示")]
    public string ToolTip
    {
        get { return toolTip; }
        set
        {
            if (toolTip != value)
            {
                toolTip = value;
            }
        }
    }

    private bool visibale = true;

    [Category("是否顯示")]
    [DefaultValue(typeof(bool), "true")]
    [Description("自定義系統按鈕是否顯示")]
    public bool Visibale
    {
        get { return visibale; }
        set
        {
            if (visibale != value)
            {
                visibale = value;
            }
        }
    }

    private Image sysButtonMouse;

    [Category("按鈕圖案")]
    [Description("自定義系統按鈕懸浮圖案")]
    public Image SysButtonMouse
    {
        get { return sysButtonMouse; }
        set
        {
            if (sysButtonMouse != value)
            {
                sysButtonMouse = value;
            }
        }
    }

    private Image sysButtonDown;
    /// <summary>
    /// 自定义系统按钮点击时
    /// </summary>
    [Category("按鈕圖案")]
    [Description("自定義系統按鈕點擊圖案")]
    public Image SysButtonDown
    {
        get { return sysButtonDown; }
        set
        {
            if (sysButtonDown != value)
            {
                sysButtonDown = value;
            }
        }
    }

    private Image sysButtonNorml;
    /// <summary>
    /// 自定义系统按钮初始时
    /// </summary>
    [Category("按鈕圖案")]
    [Description("自定義系統按鈕初始時圖案")]
    public Image SysButtonNorml
    {
        get { return sysButtonNorml; }
        set
        {
            if (sysButtonNorml != value)
            {
                sysButtonNorml = value;
            }
        }
    }

    private HskinMain ownerForm;

    [Browsable(false)]
    public HskinMain OwnerForm
    {
        get { return ownerForm; }
        set { ownerForm = value; }
    }
}