using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Drawing;

namespace HsWin
{
    public abstract class SkinFormRenderer
    {
        private EventHandlerList _events;
        private static readonly object EventRenderSkinFormCaption = new object();
        private static readonly object EventRenderSkinFormBorder = new object();
        private static readonly object EventRenderSkinFormControlBox = new object();
        public delegate void SkinFormBorderRenderEventHandler(object sender, SkinFormBorderRenderEventArgs e);
        public delegate void SkinFormControlBoxRenderEventHandler(object sender, SkinFormControlBoxRenderEventArgs e);

        protected SkinFormRenderer()
        {
        }


        protected EventHandlerList Events
        {
            get
            {
                if (_events == null)
                {
                    _events = new EventHandlerList();
                }
                return _events;
            }
        }



        public event SkinFormCaptionRenderEventHandler RenderSkinFormCaption
        {
            add { AddHandler(EventRenderSkinFormCaption, value); }
            remove { RemoveHandler(EventRenderSkinFormCaption, value); }
        }

        public event SkinFormBorderRenderEventHandler RenderSkinFormBorder
        {
            add { AddHandler(EventRenderSkinFormBorder, value); }
            remove { RemoveHandler(EventRenderSkinFormBorder, value); }
        }

        public event SkinFormControlBoxRenderEventHandler RenderSkinFormControlBox
        {
            add { AddHandler(EventRenderSkinFormControlBox, value); }
            remove { RemoveHandler(EventRenderSkinFormControlBox, value); }
        }

        public abstract Region CreateRegion(HskinMain form);

        public abstract void InitSkinForm(HskinMain form);

        public void DrawSkinFormCaption(
            SkinFormCaptionRenderEventArgs e)
        {
            OnRenderSkinFormCaption(e);
            SkinFormCaptionRenderEventHandler handle =
                Events[EventRenderSkinFormCaption]
                as SkinFormCaptionRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }


        public void DrawSkinFormBorder(
            SkinFormBorderRenderEventArgs e)
        {
            OnRenderSkinFormBorder(e);
            SkinFormBorderRenderEventHandler handle =
                Events[EventRenderSkinFormBorder]
                as SkinFormBorderRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        public void DrawSkinFormControlBox(
            SkinFormControlBoxRenderEventArgs e)
        {
            OnRenderSkinFormControlBox(e);
            SkinFormControlBoxRenderEventHandler handle =
                Events[EventRenderSkinFormControlBox]
                as SkinFormControlBoxRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        protected abstract void OnRenderSkinFormCaption(
               SkinFormCaptionRenderEventArgs e);

        protected abstract void OnRenderSkinFormBorder(
            SkinFormBorderRenderEventArgs e);

        protected abstract void OnRenderSkinFormControlBox(
            SkinFormControlBoxRenderEventArgs e);

        [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
        protected void AddHandler(object key, Delegate value)
        {
            Events.AddHandler(key, value);
        }

        [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
        protected void RemoveHandler(object key, Delegate value)
        {
            Events.RemoveHandler(key, value);
        }
    }
    public delegate void SkinFormCaptionRenderEventHandler(
       object sender,
       SkinFormCaptionRenderEventArgs e);

    public class SkinFormCaptionRenderEventArgs : PaintEventArgs
    {
        private HskinMain _skinForm;
        private bool _active;

        public SkinFormCaptionRenderEventArgs(
            HskinMain skinForm,
            Graphics g,
            Rectangle clipRect,
            bool active)
            : base(g, clipRect)
        {
            _skinForm = skinForm;
            _active = active;
        }

        public HskinMain SkinForm
        {
            get { return _skinForm; }
        }

        public bool Active
        {
            get { return _active; }
        }
    }
    public class SkinFormBorderRenderEventArgs : PaintEventArgs
    {
        private HskinMain _skinForm;
        private bool _active;

        public SkinFormBorderRenderEventArgs(
            HskinMain skinForm,
            Graphics g,
            Rectangle clipRect,
            bool active)
            : base(g, clipRect)
        {
            _skinForm = skinForm;
            _active = active;
        }

        public HskinMain SkinForm
        {
            get { return _skinForm; }
        }

        public bool Active
        {
            get { return _active; }
        }
    }
    public enum ControlBoxStyle
    {
        None = 0,
        Minimize = 1,
        Maximize = 2,
        Close = 3,
        CmSysBottom = 4
    }
    public enum ControlBoxState
    {
        Normal = 0,
        Hover,
        Pressed,
        PressedLeave
    }
    public class SkinFormControlBoxRenderEventArgs : PaintEventArgs
    {
        private HskinMain _form;
        private bool _active;
        private ControlBoxState _controlBoxState;
        private ControlBoxStyle _controlBoxStyle;
        private CmSysButton _CmSysbutton;

        public SkinFormControlBoxRenderEventArgs(
            HskinMain form,
            Graphics graphics,
            Rectangle clipRect,
            bool active,
            ControlBoxStyle controlBoxStyle,
            ControlBoxState controlBoxState,
            CmSysButton cmSysbutton = null)
            : base(graphics, clipRect)
        {
            _form = form;
            _active = active;
            _controlBoxState = controlBoxState;
            _controlBoxStyle = controlBoxStyle;
            _CmSysbutton = cmSysbutton;
        }
        public CmSysButton CmSysButton
        {
            get { return _CmSysbutton; }
        }

        public HskinMain Form
        {
            get { return _form; }
        }

        public bool Active
        {
            get { return _active; }
        }

        public ControlBoxStyle ControlBoxStyle
        {
            get { return _controlBoxStyle; }
        }

        public ControlBoxState ControlBoxtate
        {
            get { return _controlBoxState; }
        }
    }
}
