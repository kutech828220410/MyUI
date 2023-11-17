using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace MyUI
{
    public partial class LoadingForm : Form
    {
        public delegate void MethodEventHandler();
        public event MethodEventHandler MethodEvent;

        private static LoadingForm pLoading = new LoadingForm();
        delegate void SetTextCallback(string title, string caption, string description);
        delegate void CloseFormCallback();

        public LoadingForm()
        {
            InitializeComponent();
            Thread t = new Thread(new ThreadStart(delegateEventMethod));
            t.IsBackground = true;
            t.Start();
            this.Load += LoadingForm_Load;
            this.FormClosing += LoadingForm_FormClosing;
        }
        public static LoadingForm getLoading()
        {
            if (pLoading.IsDisposed)
            {
                pLoading = new LoadingForm();
                return pLoading;
            }
            else
            {
                return pLoading;
            }
        }
        public void SetCaptionAndDescription(string title, string caption, string description)
        {
            if (this.InvokeRequired && pLoading.lbl_caption.InvokeRequired && pLoading.lbl_description.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetCaptionAndDescription);
                this.Invoke(d, new object[] { title, caption, description });
            }
            else
            {
                if (!title.Equals(""))
                {
                    this.Text = title;
                }
                if (!caption.Equals(""))
                {
                    pLoading.lbl_caption.Text = caption;
                }
                if (!description.Equals(""))
                {
                    pLoading.lbl_description.Text = description;
                }
            }
        }
        public void SetExecuteMethod(MethodEventHandler method)
        {
            this.MethodEvent += method;
        }
        private void delegateEventMethod()
        {
            if (MethodEvent != null) MethodEvent();
        }
        public void CloseLoadingForm()
        {
            if (this.InvokeRequired)
            {
                CloseFormCallback d = new CloseFormCallback(CloseLoadingForm);
                this.Invoke(d, new object[] { });
            }
            else
            {
                if (!this.IsDisposed)
                {
                    this.Dispose(true);
                }
            }
        }


        private void LoadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsDisposed)
            {
                this.Dispose(true);
            }
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
           
        }
    
    }
}
