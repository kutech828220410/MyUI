// DragDropListBox.cs - Evan 專用：支援拖曳、預覽、編號、自動滾動、統一輔助線設定
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class DragDropListBox : ListBox
{

    private int dragIndex = -1;
    private int insertIndex = -1;
    private object dragItem = null;
    private DragPreviewForm previewForm = null;
    private Timer dragFollowTimer = null;
    private Point lastCursorPosition = Point.Empty;
    private bool isDragging = false;
    private Point dragStartPoint = Point.Empty;

    private IndexedDictionary<object> boundDictionary = null;

    public void Bind<T>(IndexedDictionary<T> dictionary)
    {
        boundDictionary = new IndexedDictionary<object>();
        foreach (var entry in dictionary.Items)
            boundDictionary.Add(entry.Index, entry.Name, entry.Content);
        RefreshItemsFromDictionary();
    }
    public void RemoveSelectedItems<T>()
    {
        if (this.SelectedItems.Count == 0 || boundDictionary == null) return;

        this.BeginUpdate();

        foreach (var selected in this.SelectedItems.Cast<object>().ToList())
        {
            if (selected is IndexedEntry<object> entry)
            {
                Items.Remove(entry);
                boundDictionary.Remove(entry.Index);
            }
        }

        boundDictionary.ReIndex(); // 重新編號以確保順序連續
        RefreshItemsFromDictionary();

        this.EndUpdate();
    }
    public void RemoveItem<T>(T contentToRemove)
    {
        if (boundDictionary == null) return;

        var entry = boundDictionary.Items.FirstOrDefault(e => EqualityComparer<T>.Default.Equals((T)e.Content, contentToRemove));
        if (entry != null)
        {
            Items.Remove(entry);
            boundDictionary.Remove(entry.Index);
            boundDictionary.ReIndex();
            RefreshItemsFromDictionary();
        }
    }
    public List<T> GetSelectedContent<T>()
    {
        List<T> selectedContents = new List<T>();

        foreach (var item in this.SelectedItems)
        {
            if (item is IndexedEntry<object> entry && entry.Content is T content)
            {
                selectedContents.Add(content);
            }
        }

        return selectedContents;
    }
    public IndexedDictionary<T> GetBoundDictionary<T>()
    {
        var dict = new IndexedDictionary<T>();
        foreach (var item in boundDictionary.Items)
            dict.Add(item.Index, item.Name, (T)item.Content);
        return dict;
    }

    private void RefreshItemsFromDictionary()
    {
        Items.Clear();
        if (boundDictionary != null)
        {
            foreach (var entry in boundDictionary.Items)
                Items.Add(entry);
        }
    }


    // 拖曳預覽文字設定
    [Category("拖曳預覽")]
    [Description("拖曳時顯示的文字顏色")]
    public Color DragTextColor { get; set; } = Color.White;

    [Category("拖曳預覽")]
    [Description("拖曳時顯示的背景顏色")]
    public Color DragBackgroundColor { get; set; } = Color.Green;

    [Category("拖曳預覽")]
    [Description("拖曳背景的透明度，範圍為 0.0（完全透明）到 1.0（不透明）")]
    public float DragBackgroundOpacity { get; set; } = 0.7f;

    [Category("拖曳預覽")]
    [Description("拖曳文字的透明度，範圍為 0.0（完全透明）到 1.0（不透明）")]
    public float DragTextOpacity { get; set; } = 1.0f;

    [Category("拖曳預覽")]
    [Description("拖曳預覽邊角的圓角半徑")]
    public int DragCornerRadius { get; set; } = 2;

    // 插入提示線設定
    [Category("插入提示線")]
    [Description("拖曳時插入提示線的顏色")]
    public Color InsertLineColor { get; set; } = Color.DarkRed;

    [Category("插入提示線")]
    [Description("拖曳時插入提示線的寬度（像素）")]
    public float InsertLineWidth { get; set; } = 2f;

    // 項目顯示設定
    [Category("顯示樣式")]
    [Description("是否在每個項目前顯示編號")]
    public bool ShowItemIndex { get; set; } = true;

    // 輔助線設定
    [Category("輔助線")]
    [Description("是否顯示每個項目的底部輔助線")]
    public bool ShowHelperLine { get; set; } = true;

    [Category("輔助線")]
    [Description("輔助線是否為虛線（Dash）")]
    public bool HelperLineDash { get; set; } = true;

    [Category("輔助線")]
    [Description("輔助線的顏色（可含透明度）")]
    public Color HelperLineColor { get; set; } = Color.FromArgb(160, Color.Gray);

    [Category("輔助線")]
    [Description("輔助線的寬度（像素）")]
    public float HelperLineWidth { get; set; } = 1f;

    // 自動捲動啟動區域的邊距（上下距離，像素），當滑鼠靠近時觸發
    private const int AutoScrollMargin = 10;

    // 自動捲動速度，每次滾動幾筆項目
    private const int AutoScrollSpeed = 1;

    public DragDropListBox()
    {
        this.AllowDrop = true;
        this.DrawMode = DrawMode.OwnerDrawFixed;
        this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
        EnableDoubleBuffering();

        this.MouseDown += DragDropListBox_MouseDown;
        this.MouseMove += DragDropListBox_MouseMove;
        this.MouseUp += DragDropListBox_MouseUp;
        this.DragOver += DragDropListBox_DragOver;
        this.DragDrop += DragDropListBox_DragDrop;
        this.DrawItem += DragDropListBox_DrawItem;
        this.FontChanged += (s, e) => UpdateItemHeight();
        UpdateItemHeight();
    }




    private void EnableDoubleBuffering()
    {
        typeof(Control).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, this, new object[] { true });
    }

    private void UpdateItemHeight()
    {
        using (Graphics g = CreateGraphics())
        {
            SizeF size = g.MeasureString("測試文字", this.Font);
            this.ItemHeight = (int)Math.Ceiling(size.Height) + 6;
        }
    }

    private void DragDropListBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            dragIndex = IndexFromPoint(e.Location);
            if (dragIndex >= 0 && dragIndex < Items.Count)
            {
                dragItem = Items[dragIndex];
                dragStartPoint = e.Location;
                isDragging = true;
            }
        }
    }
    private void DragDropListBox_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging && dragItem != null)
        {
            // 當滑鼠移動距離大於 4px（避免誤觸拖曳）
            if (Math.Abs(e.X - dragStartPoint.X) > SystemInformation.DragSize.Width / 2 ||
                Math.Abs(e.Y - dragStartPoint.Y) > SystemInformation.DragSize.Height / 2)
            {
                string text = GetDisplayText(dragIndex, dragItem);

                previewForm = new DragPreviewForm(DragCornerRadius);
                previewForm.SetText(text, this.Font);
                //previewForm.TextAlpha = (int)(DragTextOpacity * 255);
                previewForm.TextColor = DragTextColor;
                //previewForm.BackAlpha = (int)(DragBackgroundOpacity * 255);
                previewForm.BackColorReal = DragBackgroundColor;

                Point cursor = Cursor.Position;
                previewForm.Location = new Point(cursor.X + 5, cursor.Y + 5);
                previewForm.Show();
                StartPreviewFollowMouse();

                // ✅ 拖曳開始
                DoDragDrop(dragItem, DragDropEffects.Move);

                // 🧹 拖曳結束後清理
                StopPreviewFollowMouse();
                previewForm?.Close();
                previewForm = null;
                isDragging = false;
                dragItem = null;
            }
        }
    }
    private void DragDropListBox_MouseUp(object sender, MouseEventArgs e)
    {
        isDragging = false;
        dragItem = null;
    }
    // 加入時間限制或快取區塊避免頻繁 Invalidate
    private DateTime lastInvalidateTime = DateTime.MinValue;

    private void DragDropListBox_DragOver(object sender, DragEventArgs e)
    {
        Point pt = PointToClient(new Point(e.X, e.Y));
        int newInsertIndex = IndexFromPoint(pt);

        if (newInsertIndex == ListBox.NoMatches)
            newInsertIndex = Items.Count;
        else
        {
            Rectangle itemRect = GetItemRectangle(newInsertIndex);
            newInsertIndex = (pt.Y < itemRect.Top + itemRect.Height / 2) ? newInsertIndex : newInsertIndex + 1;
        }

        if (newInsertIndex != insertIndex)
        {
            // 只重繪舊線與新線
            Rectangle oldRect = GetInsertLineBounds(insertIndex);
            Rectangle newRect = GetInsertLineBounds(newInsertIndex);

            insertIndex = newInsertIndex;

            Invalidate(oldRect);
            Invalidate(newRect);
        }

        e.Effect = DragDropEffects.Move;
    }
    private void DragDropListBox_DragDrop(object sender, DragEventArgs e)
    {
        if (dragItem != null)
        {
            this.BeginUpdate(); // 🟢 加入防閃爍
            Items.Remove(dragItem);
            Items.Insert(Math.Min(insertIndex, Items.Count), dragItem);
            this.EndUpdate();

            if (boundDictionary != null)
            {
                var newMap = new Dictionary<int, IndexedEntry<object>>();
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i] is IndexedEntry<object> entry)
                    {
                        entry.Index = i;
                        newMap[i] = entry;
                    }
                }
                boundDictionary.LoadFrom(newMap);
            }
        }
        dragIndex = insertIndex = -1;
        dragItem = null;
        Invalidate();
    }
    private void DragDropListBox_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= Items.Count) return;

        Graphics g = e.Graphics;
        Rectangle bounds = e.Bounds;

        // 背景
        bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
        Color backColor = isSelected ? SystemColors.Highlight : this.BackColor;
        using (SolidBrush bgBrush = new SolidBrush(backColor))
        {
            g.FillRectangle(bgBrush, bounds);
        }

        // 插入線（上）
        if (insertIndex == e.Index && dragItem != null)
        {
            using (Pen pen = new Pen(InsertLineColor, InsertLineWidth))
                g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right, bounds.Top);
        }

        // 插入線（最後一筆下方）
        if (insertIndex == Items.Count && e.Index == Items.Count - 1 && dragItem != null)
        {
            using (Pen pen = new Pen(InsertLineColor, InsertLineWidth))
                g.DrawLine(pen, bounds.Left, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1);
        }

        // 輔助線
        if (ShowHelperLine)
        {
            using (Pen pen = new Pen(HelperLineColor, HelperLineWidth))
            {
                if (HelperLineDash) pen.DashStyle = DashStyle.Dash;
                int y = bounds.Bottom - 2;
                g.DrawLine(pen, bounds.Left + 4, y, bounds.Right - 4, y);
            }
        }

        // 文字
        string text = GetDisplayText(e.Index, Items[e.Index]);
        Color textColor = isSelected ? SystemColors.HighlightText : this.ForeColor;
        Rectangle textRect = new Rectangle(bounds.X + 4, bounds.Y, bounds.Width - 4, bounds.Height);
        TextRenderer.DrawText(g, text, this.Font, textRect, textColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
    }

    private Rectangle GetInsertLineBounds(int index)
    {
        if (index < 0 || index > Items.Count) return Rectangle.Empty;

        int y;
        if (index == Items.Count && Items.Count > 0)
        {
            Rectangle lastItem = GetItemRectangle(Items.Count - 1);
            y = lastItem.Bottom - 1;
        }
        else if (index < Items.Count)
        {
            Rectangle item = GetItemRectangle(index);
            y = item.Top;
        }
        else
        {
            return Rectangle.Empty;
        }

        return new Rectangle(0, y - 2, this.Width, 4); // 包含線條上下2px
    }

    private string GetDisplayText(int index, object item)
    {
        if (item is IndexedEntry<object> entry)
        {
            string idxStr = ShowItemIndex ? (index + 1).ToString("D2") + ". " : "";
            return idxStr + entry.Name;
        }
        return item?.ToString() ?? "";
    }

    private void StartPreviewFollowMouse()
    {
        dragFollowTimer = new Timer();
        dragFollowTimer.Interval = 10;
        dragFollowTimer.Tick += (s, e) => UpdatePreviewFormLocation();
        dragFollowTimer.Start();

    }

    private void StopPreviewFollowMouse()
    {
        dragFollowTimer?.Stop();
        dragFollowTimer?.Dispose();
        dragFollowTimer = null;
    }

    private void UpdatePreviewFormLocation()
    {
        Point cursor = Cursor.Position;
        if (cursor != lastCursorPosition)
        {
            lastCursorPosition = cursor;
            previewForm.Location = new Point(cursor.X + 5, cursor.Y + 5);

            HandleAutoScroll(PointToClient(cursor));
        }

    }

    private void HandleAutoScroll(Point localMousePos)
    {
        if (localMousePos.Y < AutoScrollMargin)
        {
            int newTop = Math.Max(this.TopIndex - AutoScrollSpeed, 0);
            if (newTop != this.TopIndex) this.TopIndex = newTop;
        }
        else if (localMousePos.Y > this.Height - AutoScrollMargin)
        {
            int newTop = Math.Min(this.TopIndex + AutoScrollSpeed, this.Items.Count - 1);
            if (newTop != this.TopIndex) this.TopIndex = newTop;
        }
    }

    public class DragPreviewForm : Form
    {
        private string displayText = "";
        private Font font = SystemFonts.DefaultFont;
        private int cornerRadius;
        private Size contentSize = Size.Empty;

        public Color TextColor { get; set; } = Color.White;
        public Color BackColorReal { get; set; } = Color.Black;

        public DragPreviewForm(int cornerRadius = 0)
        {
            this.cornerRadius = cornerRadius;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.TopMost = true;

            // 模擬透明背景（避免 layered window）
            this.BackColor = Color.Fuchsia;
            this.Opacity = 0.8;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        public void SetText(string text, Font font = null)
        {
            displayText = text;
            if (font != null) this.font = font;

            using (Graphics g = CreateGraphics())
            {
                SizeF textSize = g.MeasureString(displayText, this.font);
                contentSize = new Size((int)Math.Ceiling(textSize.Width) + 20, (int)Math.Ceiling(textSize.Height) + 20);
            }

            this.Size = contentSize;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(this.TransparencyKey);

            using (GraphicsPath path = GetRoundedRectPath(new Rectangle(0, 0, contentSize.Width, contentSize.Height), cornerRadius))
            using (SolidBrush backBrush = new SolidBrush(BackColorReal))
            {
                g.FillPath(backBrush, path);
            }

            using (SolidBrush textBrush = new SolidBrush(TextColor))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.DrawString(displayText, font, textBrush, new PointF(10, 10));
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(rect.Left, rect.Top, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Top, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

}
public class IndexedEntry<T>
{
    public int Index { get; set; }
    public string Name { get; set; }

    [Browsable(false)]
    public T Content { get; set; }
}
public class IndexedDictionary<T>
{
    private Dictionary<int, IndexedEntry<T>> _entries = new Dictionary<int, IndexedEntry<T>>();

    private int _nextIndex = 0;

    public int Add(string name, T content)
    {
        return Add(_nextIndex++, name, content);
    }

    public int Add(int index, string name, T content)
    {
        _entries[index] = new IndexedEntry<T> { Index = index, Name = name, Content = content };
        _nextIndex = Math.Max(_nextIndex, index + 1);
        return index;
    }

    public void Set(int index, string name, T content)
    {
        _entries[index] = new IndexedEntry<T> { Index = index, Name = name, Content = content };
        _nextIndex = Math.Max(_nextIndex, index + 1);
    }

    public bool TryGet(int index, out IndexedEntry<T> entry)
    {
        return _entries.TryGetValue(index, out entry);
    }

    public T GetContent(int index)
    {
        return _entries.TryGetValue(index, out var entry) ? entry.Content : default;
    }

    public IEnumerable<IndexedEntry<T>> Items => _entries.Values.OrderBy(e => e.Index);

    public Dictionary<int, IndexedEntry<T>> ToDictionary()
    {
        return new Dictionary<int, IndexedEntry<T>>(_entries);
    }

    public void LoadFrom(Dictionary<int, IndexedEntry<T>> input)
    {
        _entries = new Dictionary<int, IndexedEntry<T>>(input);
        _nextIndex = _entries.Keys.Count > 0 ? _entries.Keys.Max() + 1 : 0;
    }

    public void Remove(int index)
    {
        if (_entries.ContainsKey(index))
            _entries.Remove(index);
    }

    public void Clear()
    {
        _entries.Clear();
        _nextIndex = 0;
    }

    public void Rename(int index, string newName)
    {
        if (_entries.TryGetValue(index, out var entry))
        {
            entry.Name = newName;
        }
    }

    public void MoveUp(int index)
    {
        var ordered = Items.ToList();
        int pos = ordered.FindIndex(e => e.Index == index);
        if (pos > 0)
        {
            SwapIndex(ordered[pos], ordered[pos - 1]);
        }
    }

    public void MoveDown(int index)
    {
        var ordered = Items.ToList();
        int pos = ordered.FindIndex(e => e.Index == index);
        if (pos >= 0 && pos < ordered.Count - 1)
        {
            SwapIndex(ordered[pos], ordered[pos + 1]);
        }
    }

    private void SwapIndex(IndexedEntry<T> a, IndexedEntry<T> b)
    {
        int temp = a.Index;
        a.Index = b.Index;
        b.Index = temp;
        _entries[a.Index] = a;
        _entries[b.Index] = b;
    }

    public void ReIndex()
    {
        var newEntries = new Dictionary<int, IndexedEntry<T>>();
        int idx = 0;
        foreach (var entry in Items)
        {
            entry.Index = idx;
            newEntries[idx] = entry;
            idx++;
        }
        _entries = newEntries;
        _nextIndex = idx;
    }

    public IndexedEntry<T> FindByName(string name)
    {
        return _entries.Values.FirstOrDefault(e => e.Name == name);
    }

    public int? IndexOfName(string name)
    {
        var entry = _entries.Values.FirstOrDefault(e => e.Name == name);
        return entry != null ? entry.Index : (int?)null;
    }
}