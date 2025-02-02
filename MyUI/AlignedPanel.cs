using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class AlignedPanel : Panel
{
    public enum Alignment
    {
        Left,
        Center,
        Right
    }

    private Control _childControl;
    private Alignment _horizontalAlignment = Alignment.Center;
    private bool _fillWidth = false;
    private bool _fillHeight = false;


    /// <summary>
    /// 控制元件的水平對齊方式
    /// </summary>
    [Category("Layout")]
    [Description("設定內部元件的對齊方式（靠左、置中、靠右）")]
    [DefaultValue(Alignment.Center)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Alignment HorizontalAlignment
    {
        get => _horizontalAlignment;
        set
        {
            if (_horizontalAlignment != value)
            {
                _horizontalAlignment = value;
                AlignControl();
            }
        }
    }

    /// <summary>
    /// 設定元件是否填滿 Panel 寬度
    /// </summary>
    [Category("Layout")]
    [Description("設定內部元件是否填滿 Panel 寬度")]
    [DefaultValue(false)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool FillWidth
    {
        get => _fillWidth;
        set
        {
            if (_fillWidth != value)
            {
                _fillWidth = value;
                AlignControl();
            }
        }
    }

    /// <summary>
    /// 設定元件是否填滿 Panel 高度
    /// </summary>
    [Category("Layout")]
    [Description("設定內部元件是否填滿 Panel 高度")]
    [DefaultValue(false)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool FillHeight
    {
        get => _fillHeight;
        set
        {
            if (_fillHeight != value)
            {
                _fillHeight = value;
                AlignControl();
            }
        }
    }

    public AlignedPanel()
    {
        this.ControlAdded += AlignedPanel_ControlAdded; 
        this.Resize += (s, e) => AlignControl(); // 當 Panel 大小變更時，重新對齊
    }

    private void AlignedPanel_ControlAdded(object sender, ControlEventArgs e)
    {
        _childControl = e.Control;

        if (_childControl != null)
        {
            _childControl.Anchor = AnchorStyles.None;
            _childControl.Dock = DockStyle.None;       
            AlignControl(); // 立即對齊
        }
    }

    /// <summary>
    /// 重新計算元件位置與大小
    /// </summary>
    private void AlignControl()
    {
        if (_childControl == null) return;

        int x = 0;
        int y = (this.Height - _childControl.Height) / 2; // 預設垂直置中
        int width = _childControl.Width;
        int height = _childControl.Height;

        // 設定填滿寬度
        if (FillWidth)
        {
            x = 0;
            width = this.Width;
        }
        else
        {
            switch (_horizontalAlignment)
            {
                case Alignment.Center:
                    x = (this.Width - _childControl.Width) / 2;
                    break;
                case Alignment.Right:
                    x = this.Width - _childControl.Width;
                    break;
                case Alignment.Left:
                    x = 0;
                    break;
            }
        }

        // 設定填滿高度
        if (FillHeight)
        {
            y = 0;
            height = this.Height;
        }

        _childControl.SetBounds(x, y, width, height);
    }

}
