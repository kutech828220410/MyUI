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
    /// 設定 Panel 內的唯一元件
    /// </summary>
    [Browsable(false)] // 不顯示在設計器中
    public Control ChildControl
    {
        get => _childControl;
        set
        {
            if (_childControl != null)
            {
                this.Controls.Remove(_childControl); // 移除舊的元件
            }

            _childControl = value;

            if (_childControl != null)
            {
                this.Controls.Add(_childControl);
                _childControl.Anchor = AnchorStyles.None; // 禁止 Anchor
                _childControl.Dock = DockStyle.None; // 禁止 Dock
                _childControl.LocationChanged += (s, e) => AlignControl();
                _childControl.SizeChanged += (s, e) => AlignControl();
                AlignControl(); // 立即對齊
            }
        }
    }

    /// <summary>
    /// 控制元件的水平對齊方式
    /// </summary>
    [Category("Layout")]
    [Description("設定內部元件的對齊方式（靠左、置中、靠右）")]
    [DefaultValue(Alignment.Center)] // 設定預設值為置中
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Alignment HorizontalAlignment
    {
        get => _horizontalAlignment;
        set
        {
            _horizontalAlignment = value;
            AlignControl();
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
            _fillWidth = value;
            AlignControl();
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
            _fillHeight = value;
            AlignControl();
        }
    }

    public AlignedPanel()
    {
        this.Resize += (s, e) => AlignControl(); // 當 Panel 大小變更時，重新對齊
    }

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
                    x = (this.Width - _childControl.Width) / 2; // 置中
                    break;
                case Alignment.Right:
                    x = this.Width - _childControl.Width; // 靠右
                    break;
                case Alignment.Left:
                    x = 0; // 靠左
                    break;
            }
        }

        // 設定填滿高度
        if (FillHeight)
        {
            y = 0;
            height = this.Height;
        }

        _childControl.Location = new Point(x, y);
        _childControl.Width = width;
        _childControl.Height = height;
        _childControl.Invalidate();
        this.Invalidate();
     
    }
}
