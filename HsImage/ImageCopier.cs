using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using DrawingClass;
using Basic;
using HsBasic;
namespace HsImage
{
    public class ImageCopier
    {
        private StructVegaHandle _StructSrcImageHandle;
        private StructVegaHandle _StructDstImageHandle;
        public IntPtr SrcImageHandle;
        public IntPtr DstImageHandle;
        unsafe public void Copy()
        {
            _StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(SrcImageHandle);
            _StructDstImageHandle = Memory.PtrToStructure<StructVegaHandle>(DstImageHandle);
            _StructDstImageHandle.CopyStruct(_StructSrcImageHandle);
            Memory.StructureToPtr<StructVegaHandle>(_StructDstImageHandle,DstImageHandle);
            Memory.CopyPtr(_StructSrcImageHandle.ImageDataPtr, _StructDstImageHandle.ImageDataPtr, _StructSrcImageHandle.LenthOfPtr);
        }
    }
}
