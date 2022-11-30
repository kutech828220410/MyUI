using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using DrawingClass;
using Basic;
using HsBasic;
using HsBase;


namespace HsPat
{
    public class Match
    {
        private Stopwatch stopwatch = new Stopwatch();
        public Match()
        {
            stopwatch.Start();
            this.MatchedResultInit();
        }

        [Serializable]
        public class MatchedResult
        {
            public double MatchedOrgX;
            public double MatchedOrgY;
            public double MatchedOrgWidth;
            public double MatchedOrgHeight;

            public double MatchedCenterX;
            public double MatchedCenterY;
            public double MatchedWidth;
            public double MatchedHeight;

            public double MatchedScore;
            public double MatchedAngle;

            public static void CaculateResult(ref List<MatchedResult> MatchedResule_List)
            {
                foreach(MatchedResult matchedResult in MatchedResule_List)
                {
                    matchedResult.MatchedCenterX = matchedResult.MatchedOrgX + matchedResult.MatchedOrgWidth / 2;
                    matchedResult.MatchedCenterY = matchedResult.MatchedOrgY + matchedResult.MatchedOrgHeight / 2;

                }
            }
        }
        public List<MatchedResult> MatchedResule_List = new List<MatchedResult>();


        private IntPtr IntegralImagePtr;
        private IntPtr IntegralImagePowPtr;
        private int IntegralImageLen;
        private int IntegralWidth;
        private int IntegraHeight;

        private StructVegaHandle StructSrcImageHandle;
        private StructVegaHandle StructPatImageHandle;
        private StructVegaHandle StructScoreImageHandle;
        public bool IsLearnPattern = false;
        private IntPtr _PatImageHandle;
        public IntPtr PatImageHandle
        {
            get
            {
                return _PatImageHandle;
            }
            set
            {
                this._PatImageHandle = value;
                this.StructPatImageHandle = Memory.PtrToStructure<StructVegaHandle>(value);
            }
        }
        private IntPtr _SrcImageHandle;
        public IntPtr SrcImageHandle
        {
            get
            {
                return _SrcImageHandle;
            }
            set
            {
                this._SrcImageHandle = value;
                this.StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(value);
            }
        }
        public IntPtr VegaHandle
        {
            get
            {
                return ImageClass.VegaHandle;
            }
            private set
            {

            }
        }

        private ImageClass ImageClass = new ImageClass(ImageClass.ImagePixelFormat.GRAY);
        private ImageClass ScoreImageClass = new ImageClass(ImageClass.ImagePixelFormat.GRAY);

        public int ScoreWidth
        {
            get
            {
                return StructScoreImageHandle.Width;
            }
            private set
            {
                StructScoreImageHandle.Width = value;
            }
        }
        public int ScoreHeight
        {
            get
            {
                return StructScoreImageHandle.Height;
            }
            private set
            {
                StructScoreImageHandle.Height = value;
            }
        }

        public enum TxOperationMode
        {
            FAST, NORMAL
        }
        public TxOperationMode OperationMode = TxOperationMode.FAST;
        public double MaxOverLappedRatioX = 0.9D;
        public double MaxOverLappedRatioY = 0.9D;
        public double MinScore = 0.8D;
        public double MatchAngle = 0D;
        public double MatchAngleStep = 1;
        public double MatchSADThreshHold = 100;
        private int _MinReducedArea = 64;
        public int PyramidMatchRangeX = 1;
        public int PyramidMatchRangeY = 1;
        private int _PyramidLevel = 4;
        public int PyramidLevel
        {
            get { return _PyramidLevel; }
            set
            {
                _PyramidLevel = value;
            }
        }
        private int _PyramidIndex;
        public int PyramidIndex
        {
            get
            {
                return _PyramidIndex;
            }
            set
            {
                if (value > ImageClass_pyramid_list.Count - 1) value = ImageClass_pyramid_list.Count - 1;
                if (value < 0) value = 0;
                _PyramidIndex = value;
            }
        }
        public IntPtr PyramidHandle
        {
            get
            {
                return ImageClass_pyramid_list[PyramidIndex].VegaHandle;
            }
        }
        public int PyramidWidth
        {
            get
            {
                return ImageClass_pyramid_list[PyramidIndex].Width;
            }
        }
        public int PyramidHeight
        {
            get
            {
                return ImageClass_pyramid_list[PyramidIndex].Height;
            }
        }
        private List<ImageClass> ImageClass_pyramid_list = new List<ImageClass>();
        private ImageClass ImageClass_pyramid_level_01 = new ImageClass(ImageClass.ImagePixelFormat.GRAY);
        private ImageClass ImageClass_pyramid_level_02 = new ImageClass(ImageClass.ImagePixelFormat.GRAY);
        private ImageClass ImageClass_pyramid_level_03 = new ImageClass(ImageClass.ImagePixelFormat.GRAY);
        private ImageClass ImageClass_pyramid_level_04 = new ImageClass(ImageClass.ImagePixelFormat.GRAY);
        private ImageClass ImageClass_pyramid_level_05 = new ImageClass(ImageClass.ImagePixelFormat.GRAY);
        private ImageClass ImageClass_pyramid_level_06 = new ImageClass(ImageClass.ImagePixelFormat.GRAY);
        private ImageClass ImageClass_pyramid_level_07 = new ImageClass(ImageClass.ImagePixelFormat.GRAY);
        private ImageClass ImageClass_pyramid_level_08 = new ImageClass(ImageClass.ImagePixelFormat.GRAY);

        private int _PatPyramidIndex;
        public int PatPyramidIndex
        {
            get
            {
                return _PatPyramidIndex;
            }
            set
            {
                if (value > ImageClass_PatPyramid_list.Count - 1) value = ImageClass_PatPyramid_list.Count - 1;
                if (value < 0) value = 0;
                _PatPyramidIndex = value;
            }
        }
        public IntPtr PatPyramidHandle
        {
            get
            {
                return ImageClass_PatPyramid_list[PatPyramidIndex].VegaHandle;
            }
        }
        public int PatPyramidWidth
        {
            get
            {
                return ImageClass_PatPyramid_list[PatPyramidIndex].Width;
            }
        }
        public int PatPyramidHeight
        {
            get
            {
                return ImageClass_PatPyramid_list[PatPyramidIndex].Height;
            }
        }
        public uint PatInegralValue
        {
            get
            {
                return PatInegralValue_List[PatPyramidIndex];
            }
        }
        public ulong PatInegralPowValue
        {
            get
            {
                return PatInegralPowValue_List[PatPyramidIndex];
            }
        }
        public double PatPyramidZoomX
        {
            get
            {
                return ImageClass_PatPyramid_list[PatPyramidIndex].ZoomX;
            }
        }
        public double PatPyramidZoomY
        {
            get
            {
                return ImageClass_PatPyramid_list[PatPyramidIndex].ZoomY;
            }
        }

        private List<ImageClass> ImageClass_PatPyramid_list = new List<ImageClass>();
        private List<uint> PatInegralValue_List = new List<uint>();
        private List<ulong> PatInegralPowValue_List = new List<ulong>();
        private ImageClass ImageClass_PatPyramid_level_01 = new ImageClass();
        private ImageClass ImageClass_PatPyramid_level_02 = new ImageClass();
        private ImageClass ImageClass_PatPyramid_level_03 = new ImageClass();
        private ImageClass ImageClass_PatPyramid_level_04 = new ImageClass();
        private ImageClass ImageClass_PatPyramid_level_05 = new ImageClass();
        private ImageClass ImageClass_PatPyramid_level_06 = new ImageClass();
        private ImageClass ImageClass_PatPyramid_level_07 = new ImageClass();
        private ImageClass ImageClass_PatPyramid_level_08 = new ImageClass();


        public int PatWidth
        {
            get
            {
                return StructPatImageHandle.Width;
            }
            private set
            {
                StructPatImageHandle.Width = value;
            }
        }
        public int PatHeight
        {
            get
            {
                return StructPatImageHandle.Height;
            }
            private set
            {
                StructPatImageHandle.Height = value;
            }
        }

        public int MinReducedArea
        {
            get
            {
                return _MinReducedArea;
            }
            set
            {
                _MinReducedArea = value;
            }
        }
        private List<List<MatchedResult>> MatchedResult_MainList = new List<List<MatchedResult>>();
        private List<MatchedResult> MatchedResult_Dot09 = new List<MatchedResult>();
        private List<MatchedResult> MatchedResult_Dot08 = new List<MatchedResult>();
        private List<MatchedResult> MatchedResult_Dot07 = new List<MatchedResult>();
        private List<MatchedResult> MatchedResult_Dot06 = new List<MatchedResult>();
        private List<MatchedResult> MatchedResult_Dot05 = new List<MatchedResult>();
        private List<MatchedResult> MatchedResult_Dot04 = new List<MatchedResult>();
        private void MatchedResultClear()
        {
            foreach (List<MatchedResult> list in MatchedResult_MainList)
            {
                list.Clear();
            }
        }
        private void MatchedResultInit()
        {
            MatchedResult_MainList.Add(MatchedResult_Dot09);
            MatchedResult_MainList.Add(MatchedResult_Dot08);
            MatchedResult_MainList.Add(MatchedResult_Dot07);
            MatchedResult_MainList.Add(MatchedResult_Dot06);
            MatchedResult_MainList.Add(MatchedResult_Dot05);
            MatchedResult_MainList.Add(MatchedResult_Dot04);
        }
        private void MatchedResultAdd(MatchedResult MatchedResult)
        {
            if (MatchedResult.MatchedScore >= 0.9) MatchedResult_MainList[0].Add(MatchedResult);
            else if (MatchedResult.MatchedScore >= 0.8) MatchedResult_MainList[1].Add(MatchedResult);
            else if (MatchedResult.MatchedScore >= 0.7) MatchedResult_MainList[2].Add(MatchedResult);
            else if (MatchedResult.MatchedScore >= 0.6) MatchedResult_MainList[3].Add(MatchedResult);
            else if (MatchedResult.MatchedScore >= 0.5) MatchedResult_MainList[4].Add(MatchedResult);
            else if (MatchedResult.MatchedScore >= 0.4) MatchedResult_MainList[5].Add(MatchedResult);
        }
        private void MatchedResultRemoveAt(int Lenth0, int Lenth1)
        {
            MatchedResult_MainList[Lenth0].RemoveAt(Lenth1);
        }
        private void MatchedResultCheck(double Score, double OrgX, double OrgY, double Min_Width, double Min_Height, ref bool flag_OverLapped, ref int Remove_Lenth0, ref int Remove_Lenth1)
        {
            bool flag_OverLapped_IsRemove = false;
            flag_OverLapped = false;
            Remove_Lenth0 = 0;
            Remove_Lenth1 = 0;
            foreach (List<MatchedResult> MatchedResultlist in MatchedResult_MainList)
            {
                Remove_Lenth1 = 0;
                foreach (MatchedResult matchedResult in MatchedResultlist)
                {
                    flag_OverLapped = false;
                    if (Math.Abs(OrgX - matchedResult.MatchedOrgX) <= Min_Width && Math.Abs(OrgY - matchedResult.MatchedOrgY) <= Min_Height)
                    {
                        if (Score > matchedResult.MatchedScore)
                        {
                            flag_OverLapped_IsRemove = true;
                        }
                        else
                        {
                            flag_OverLapped_IsRemove = false;
                        }
                        flag_OverLapped = true;
                    }
                    if (flag_OverLapped)
                    {
                        break;
                    }
                    Remove_Lenth1++;
                }
                if (flag_OverLapped)
                {
                    break;
                }
                Remove_Lenth0++;
            }
            if (!flag_OverLapped_IsRemove)
            {
                Remove_Lenth0 = -1;
                Remove_Lenth1 = -1;
            }
            else
            {
                flag_OverLapped = false;
            }
        }
        private void MatchedResultCheck(double Score, double OrgX, double OrgY, double Min_Width, double Min_Height, ref bool flag_OverLapped)
        {
            flag_OverLapped = false;
            foreach (List<MatchedResult> MatchedResultlist in MatchedResult_MainList)
            {
                foreach (MatchedResult matchedResult in MatchedResultlist)
                {
                    flag_OverLapped = false;
                    if (Math.Abs(OrgX - matchedResult.MatchedOrgX) <= Min_Width && Math.Abs(OrgY - matchedResult.MatchedOrgY) <= Min_Height)
                    {
                        if (Score < matchedResult.MatchedScore)
                        {
                            flag_OverLapped = true;
                        }
                    }
                    if (flag_OverLapped)
                    {
                        break;
                    }
                }
                if (flag_OverLapped)
                {
                    break;
                }
            }
        }
        private List<MatchedResult> GetMatchedResult()
        {
            List<MatchedResult> MatchedResultList = new List<MatchedResult>();
            foreach (List<MatchedResult> MatchedResultlist in MatchedResult_MainList)
            {
                foreach (MatchedResult matchedResult in MatchedResultlist)
                {
                    MatchedResultList.Add(matchedResult);
                }

            }
            return MatchedResultList;
        }

        unsafe public void CreatIntegralImage(IntPtr SrcImageHandle)
        {
            StructVegaHandle StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(SrcImageHandle);
            byte* SrcPtr = (byte*)StructSrcImageHandle.ImageDataPtr;

            int X_SIZE = StructSrcImageHandle.Width;
            int Y_SIZE = StructSrcImageHandle.Height;
            int SrcWidth = StructSrcImageHandle.ByteOfWidth;
            int DstWidth = X_SIZE;
            int Lenth = X_SIZE * Y_SIZE;
            uint sum = 0;
            ulong sumPow = 0;
            int SrcIndex;
            int DstIndex;
            int SrcWidthxY;
            int DstWidthxY;
            int DstOffset = 0;

            this.IntegralWidth = X_SIZE;
            this.IntegraHeight = Y_SIZE;

            if (IntegralImageLen != Lenth)
            {
                if (IntegralImagePtr != IntPtr.Zero) Memory.Free(IntegralImagePtr);
                IntegralImagePtr = Memory.Malloc(Lenth * 4);
                if (IntegralImagePowPtr != IntPtr.Zero) Memory.Free(IntegralImagePowPtr);
                IntegralImagePowPtr = Memory.Malloc(Lenth * 8);

            }
            uint* DstPtr = (uint*)IntegralImagePtr;
            ulong* DstPowPtr = (ulong*)IntegralImagePowPtr;
            this.IntegralImageLen = Lenth;

            uint SrcValue = 0;
            for (int y = 0; y < Y_SIZE; y++)
            {
                sum = 0;
                SrcWidthxY = y * SrcWidth;
                DstWidthxY = y * DstWidth;
                for (int x = 0; x < X_SIZE; x++)
                {
                    SrcIndex = SrcWidthxY + x;
                    DstIndex = DstWidthxY + x;
                    SrcValue = SrcPtr[SrcIndex];
                    DstOffset = DstIndex - DstWidth;
                    sum += SrcValue;
                    sumPow += SrcValue * SrcValue;
                    if (DstOffset >= 0)
                    {
                        DstPtr[DstIndex] = DstPtr[DstOffset] + sum;
                        DstPowPtr[DstIndex] = DstPowPtr[DstOffset] + sumPow;
                    }
                    else
                    {
                        DstPtr[DstIndex] = sum;
                        DstPowPtr[DstIndex] = sumPow;
                    }

                }
            }
        }
        unsafe public ulong GetInegralPowValue(int x, int y, int width, int height)
        {
            x--;
            y--;
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            ulong sum = 0;
            ulong* IntegralPowPtr = (ulong*)IntegralImagePowPtr;
            sum += IntegralPowPtr[x + width + IntegralWidth * (y + height)];
            sum += IntegralPowPtr[x + IntegralWidth * (y)];
            sum -= IntegralPowPtr[x + width + IntegralWidth * (y)];
            sum -= IntegralPowPtr[x + IntegralWidth * (y + height)];
            return sum;
        }
        unsafe public uint GetInegralValue(int x, int y, int width, int height)
        {
            x--;
            y--;
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            uint sum = 0;
            uint* IntegralPtr = (uint*)IntegralImagePtr;
            sum += IntegralPtr[x + width + IntegralWidth * (y + height)];
            sum += IntegralPtr[x + IntegralWidth * (y)];
            sum -= IntegralPtr[x + width + IntegralWidth * (y)];
            sum -= IntegralPtr[x + IntegralWidth * (y + height)];
            return sum;
        }

        unsafe public void GetInegralValue(StructVegaHandle StructSrcImageHandle, StructVegaHandle StructPatImageHandle, ref uint SumValue, ref ulong SumPowValue, int StartX, int StartY)
        {
            byte* SrcPtr = (byte*)StructSrcImageHandle.ImageDataPtr;
            byte* PatPtr = (byte*)StructPatImageHandle.ImageDataPtr;
            int SrcWidthOfByte = StructSrcImageHandle.ByteOfWidth;
            int PatWidthOfByte = StructPatImageHandle.ByteOfWidth;
            int X_SIZE = StructPatImageHandle.Width;
            int Y_SIZE = StructPatImageHandle.Height;
            int Value = 0;
            int SrcIndex;
            int SrcWidthxY;
            int PatIndex;
            int PatWidthxY;
            SrcPtr += StartY * SrcWidthOfByte;
            SrcPtr += StartX;
            SumValue = 0;
            SumPowValue = 0;
            for (int y = 0; y < Y_SIZE; y++)
            {
                SrcWidthxY = y * SrcWidthOfByte;
                PatWidthxY = y * PatWidthOfByte;
                for (int x = 0; x < X_SIZE; x++)
                {
                    PatIndex = PatWidthxY + x;
                    if (PatPtr[PatIndex] != 0)
                    {
                        SrcIndex = SrcWidthxY + x;
                        Value = SrcPtr[SrcIndex];
                        SumValue += (uint)Value;
                        SumPowValue += (ulong)(Value * Value);
                    }

                }
            }
        }
        #region GetScoreByNCC_Var
        double ScoreByNCC;
        IntPtr GetScoreByNCC_floatPtr;
        IntPtr GetScoreByNCC_IntPtr;
        #endregion
        unsafe public double GetScoreByNCC(IntPtr SrcImageHandle, StructVegaHandle StructPatImageHandle, int Src_x, int Src_y)
        {
            StructVegaHandle StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(SrcImageHandle);
            return this.GetScoreByNCC(StructSrcImageHandle, StructPatImageHandle, Src_x, Src_y);
        }
        unsafe public double GetScoreByNCC(StructVegaHandle StructSrcImageHandle, StructVegaHandle StructPatImageHandle, int Src_x, int Src_y)
        {

            ScoreByNCC = 0;
            uint Pat_X_Size = (uint)StructPatImageHandle.Width;
            uint Pat_Y_Size = (uint)StructPatImageHandle.Height;
            int Pat_WidthByte = StructPatImageHandle.ByteOfWidth;

            int Src_WidthByte = StructSrcImageHandle.ByteOfWidth;

            int SrcWidthxY;
            int PatWidthxY;
            int SrcIndex;
            int PatIndex;

            byte* PatPtr = (byte*)StructPatImageHandle.ImageDataPtr;
            byte* SrcPtr = (byte*)StructSrcImageHandle.ImageDataPtr;
            double SAD_Score;
            long value00_long = 0;
            long value01_long = 0;
            long value02_long = 0;
            float value10_float = 0;
            float value11_float = 0;
            float value12_float = 0;
            SrcPtr = SrcPtr + (Src_y * Src_WidthByte) + Src_x;
            long value_long = 0;
            uint PatRegion = StructPatImageHandle.ValidPixcelsNum;
            ulong Pat_InegralValue = StructPatImageHandle.IntegraValue;
            ulong Pat_InegralValue_AVG = Pat_InegralValue / (PatRegion);
            ulong PatInegralPowValue = StructPatImageHandle.IntegraPowValue;

            //  ulong Src_InegralValue00 = this.GetInegralValue(Src_x, Src_y, (int)Pat_X_Size, (int)Pat_Y_Size);
            //  ulong Src_InegralPowValue00 = this.GetInegralPowValue(Src_x, Src_y, (int)Pat_X_Size, (int)Pat_Y_Size);

            uint Src_InegralValue = 0;
            ulong Src_InegralPowValue = 0;
            this.GetInegralValue(StructSrcImageHandle, StructPatImageHandle, ref Src_InegralValue, ref Src_InegralPowValue, Src_x, Src_y);

            ulong Src_InegralValue_AVG = Src_InegralValue / (PatRegion);



            if (Src_InegralValue > Pat_InegralValue) SAD_Score = (Src_InegralValue - Pat_InegralValue) / (double)PatRegion;
            else SAD_Score = (Pat_InegralValue - Src_InegralValue) / (double)PatRegion;
            if (SAD_Score >= this.MatchSADThreshHold)
            {
                //return 0;
            }

            value00_long = (long)(Src_InegralValue_AVG * Pat_InegralValue + Pat_InegralValue_AVG * Src_InegralValue - PatRegion * Src_InegralValue_AVG * Pat_InegralValue_AVG);
            value01_long = (long)(Src_InegralPowValue - 2 * Src_InegralValue_AVG * Src_InegralValue + PatRegion * Src_InegralValue_AVG * Src_InegralValue_AVG);
            value02_long = (long)(PatInegralPowValue - 2 * Pat_InegralValue_AVG * Pat_InegralValue + PatRegion * Pat_InegralValue_AVG * Pat_InegralValue_AVG);

            for (int y = 0; y < Pat_Y_Size; y++)
            {
                PatWidthxY = y * Pat_WidthByte;
                SrcWidthxY = y * Src_WidthByte;
                for (int x = 0; x < Pat_X_Size; x++)
                {
                    PatIndex = PatWidthxY + x;
                    if (PatPtr[PatIndex] != 0)
                    {
                        SrcIndex = SrcWidthxY + x;
                        value_long += SrcPtr[SrcIndex] * PatPtr[PatIndex];
                    }

                }
            }

            value10_float = (float)((value_long - value00_long) / PatRegion);
            value11_float = (float)Math.Sqrt(value01_long / PatRegion);
            value12_float = (float)Math.Sqrt(value02_long / PatRegion);

            ScoreByNCC = value10_float / (value11_float * value12_float);
            ScoreByNCC = Math.Abs(ScoreByNCC);

            return ScoreByNCC;
        }

        public void MatchByNCC()
        {
            this.CreatPyramidImage(PyramidLevel);
           /* for (int i = PyramidLevel - 1; i >= 0; i--)
            {
                this.PyramidIndex = i;
                this.PatPyramidIndex = i;

                if (i == PyramidLevel - 1)
                {
                    StructVegaHandle StructPatImageHandle = Memory.PtrToStructure<StructVegaHandle>(this.PatImage_Rotate[i][0].VegaHandle);
                    this.MatchByNCC(this.PyramidHandle, StructPatImageHandle, 0.1, 1, 1, 0, MatchAngle);
                }
                else if (i == PyramidLevel - 2)
                {
                    this.PyramidMatchRangeX = 2;
                    this.PyramidMatchRangeY = 2;
                    StructVegaHandle StructPatImageHandle = Memory.PtrToStructure<StructVegaHandle>(this.PatImage_Rotate[i][0].VegaHandle);
                    this.MatchByNCC(this.PyramidHandle, StructPatImageHandle, 0.1, this.MatchedResule_List, i + 1, i, 1, 1, true, true);
                }
                else if (i == 0)
                {
                    StructVegaHandle StructPatImageHandle = Memory.PtrToStructure<StructVegaHandle>(this.PatImage_Rotate[i][0].VegaHandle);
                    if (OperationMode == TxOperationMode.NORMAL) this.MatchByNCC(this.PyramidHandle, StructPatImageHandle, this.MinScore, this.MatchedResule_List, i + 1, i, 1, 1, false, true);
                }
                else
                {
                    if (OperationMode == TxOperationMode.FAST)
                    {
                        this.PyramidMatchRangeX = 2;
                        this.PyramidMatchRangeY = 2;
                    }
                    else if (OperationMode == TxOperationMode.NORMAL)
                    {
                        this.PyramidMatchRangeX = 5;
                        this.PyramidMatchRangeY = 5;
                    }
                    StructVegaHandle StructPatImageHandle = Memory.PtrToStructure<StructVegaHandle>(this.PatImage_Rotate[i][0].VegaHandle);
                    this.MatchByNCC(this.PyramidHandle, StructPatImageHandle, this.MinScore, this.MatchedResule_List, i + 1, i, 1, 1, true, true);
                }
            }
       
            if (true)
            {

                for (int i = 0; i < MatchedResule_List.Count; i++)
                {
                    if (OperationMode == TxOperationMode.NORMAL)
                    {
                        this.PatPyramidIndex = 0;
                        MatchedResule_List[i].MatchedOrgX /= this.PatPyramidZoomX;
                        MatchedResule_List[i].MatchedOrgY /= this.PatPyramidZoomY;
                        MatchedResule_List[i].MatchedOrgWidth /= this.PatPyramidZoomX;
                        MatchedResule_List[i].MatchedOrgHeight /= this.PatPyramidZoomY;
                        MatchedResule_List[i].MatchedWidth /= this.PatPyramidZoomX;
                        MatchedResule_List[i].MatchedHeight /= this.PatPyramidZoomY;
                    }
                    else if (OperationMode == TxOperationMode.FAST)
                    {
                        this.PatPyramidIndex = 1;
                        MatchedResule_List[i].MatchedOrgX /= this.PatPyramidZoomX;
                        MatchedResule_List[i].MatchedOrgY /= this.PatPyramidZoomY;
                        MatchedResule_List[i].MatchedOrgWidth /= this.PatPyramidZoomX;
                        MatchedResule_List[i].MatchedOrgHeight /= this.PatPyramidZoomY;
                        MatchedResule_List[i].MatchedWidth /= this.PatPyramidZoomX;
                        MatchedResule_List[i].MatchedHeight /= this.PatPyramidZoomY;
                    }

                }
            }*/
            this.PyramidIndex = 3;
            MatchByNCC_New(this.PyramidHandle, 3, this.MinScore, 0, 0 , 1);
            this.PatPyramidIndex = 3;
            for (int i = 0; i < MatchedResule_List.Count; i++)
            {
                MatchedResule_List[i].MatchedOrgX /= this.PatPyramidZoomX;
                MatchedResule_List[i].MatchedOrgY /= this.PatPyramidZoomY;
                MatchedResule_List[i].MatchedOrgWidth /= this.PatPyramidZoomX;
                MatchedResule_List[i].MatchedOrgHeight /= this.PatPyramidZoomY;
                MatchedResule_List[i].MatchedWidth /= this.PatPyramidZoomX;
                MatchedResule_List[i].MatchedHeight /= this.PatPyramidZoomY;
            }

            MatchedResult.CaculateResult(ref this.MatchedResule_List);
        }
        unsafe public void MatchByNCC(IntPtr SrcImageHandle, StructVegaHandle StructPatImageHandle, double ScoreThreshHold, List<MatchedResult> SrcMatchedResule, int SrcResuleLevel, int DstLevel, int X_space, int Y_space, bool IsRangeEnable, bool CheckOverLapped)
        {

            int Level = SrcResuleLevel - DstLevel;
            if (Level > 0)
            {
                List<MatchedResult> SrcMatchedResuleClone = SrcMatchedResule.DeepClone();
                List<MatchedResult> MatchedResule = new List<MatchedResult>();
                StructVegaHandle StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(SrcImageHandle);
                int X_Start;
                int Y_Start;
                int X_Range = PyramidMatchRangeX;
                if (!IsRangeEnable) X_Range = 0;
                int Y_Range = PyramidMatchRangeY;
                if (!IsRangeEnable) Y_Range = 0;
                int LevelVal = (int)Math.Pow(2, Level);
                this.CreatIntegralImage(SrcImageHandle);
                bool flag_OverLapped = false;
                int Lenth0 = 0;
                int Lenth1 = 0;
                double start_Angle;
                double end_Angle;
                foreach (MatchedResult temp in SrcMatchedResuleClone)
                {
                    X_Start = (int)(temp.MatchedOrgX * LevelVal);
                    Y_Start = (int)(temp.MatchedOrgY * LevelVal);

                    start_Angle = temp.MatchedAngle - (LevelVal * MatchAngleStep);
                    end_Angle = temp.MatchedAngle + (LevelVal * MatchAngleStep);


                    this.MatchByNCC(StructSrcImageHandle, StructPatImageHandle, ScoreThreshHold, X_Start, Y_Start, X_Range, Y_Range, X_space, Y_space, start_Angle, end_Angle, true);

                    foreach (MatchedResult MatchedResultClass in this.MatchedResule_List)
                    {
                        MatchedResule.Add(MatchedResultClass);
                    }
                }
                if (CheckOverLapped)
                {
                    this.MatchedResultClear();
                    double Min_Width = StructPatImageHandle.Width * this.MaxOverLappedRatioX;
                    double Min_Height = StructPatImageHandle.Height * this.MaxOverLappedRatioY;
                    foreach (MatchedResult MatchedResultClass in MatchedResule)
                    {
                        this.MatchedResultCheck(Score, MatchedResultClass.MatchedOrgX, MatchedResultClass.MatchedOrgY, Min_Width, Min_Height, ref flag_OverLapped, ref Lenth0, ref Lenth1);
                        if (Lenth0 != -1 && Lenth1 != -1)
                        {
                            MatchedResultRemoveAt(Lenth0, Lenth1);
                        }
                        if (!flag_OverLapped)
                        {
                            this.MatchedResultAdd(MatchedResultClass);
                        }
                    }
                    this.MatchedResule_List = GetMatchedResult();
                }
                else
                {
                    this.MatchedResule_List = MatchedResule.DeepClone();
                }
            }

        }
        public void MatchByNCC_New(IntPtr SrcImageHandle, int PatLevel, double ScoreThreshHold, double start_Angle, double end_Angle, double AngleStep)
        {
            StructVegaHandle StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(SrcImageHandle);
            int SrcWidth = StructSrcImageHandle.Width;
            int SrcHeight = StructSrcImageHandle.Height;

            bool flag_OverLapped = false;
            int Lenth0 = 0;
            int Lenth1 = 1;
            this.PyramidIndex = PatLevel;
            double Min_Width = this.Pat_Width * this.MaxOverLappedRatioX;
            double Min_Height = this.Pat_Height * this.MaxOverLappedRatioY;
            MatchedResule_List.Clear();
            Task_MatchByNCC_New(1, SrcHeight, StructSrcImageHandle, PatLevel, ScoreThreshHold, start_Angle, end_Angle, AngleStep);
        }

        private void Task_MatchByNCC_New(int NumOfTask, int Y_Size, StructVegaHandle StructSrcImageHandle, int PatLevel, double ScoreThreshHold, double start_Angle, double end_Angle, double AngleStep)
        {
            int[] StartY = new int[NumOfTask];
            int[] EndY = new int[NumOfTask];
            int thread_Y_size = Y_Size / NumOfTask;
            Action[] Act = new Action[NumOfTask];
            for (int i = 0; i < NumOfTask; i++)
            {
                StartY[i] = i * thread_Y_size;
                if (i == NumOfTask - 1) EndY[i] = Y_Size;
                else EndY[i] = (i + 1) * thread_Y_size;
                if (i == 0)
                {
                    Act[0] = new Action(delegate
                    {

                        Sub_MatchByNCC_New(StartY[0], EndY[0], StructSrcImageHandle, PatLevel, ScoreThreshHold, start_Angle, end_Angle, AngleStep);
                    });
                }
                else if (i == 1)
                {
                    Act[1] = new Action(delegate
                    {
                        Sub_MatchByNCC_New(StartY[1], EndY[1], StructSrcImageHandle, PatLevel, ScoreThreshHold, start_Angle, end_Angle, AngleStep);
                    });
                }
                else if (i == 2)
                {
                    Act[2] = new Action(delegate
                    {
                        Sub_MatchByNCC_New(StartY[2], EndY[2], StructSrcImageHandle, PatLevel, ScoreThreshHold, start_Angle, end_Angle, AngleStep);
                    });
                }
                else if (i == 3)
                {
                    Act[3] = new Action(delegate
                    {
                        Sub_MatchByNCC_New(StartY[3], EndY[3], StructSrcImageHandle, PatLevel, ScoreThreshHold, start_Angle, end_Angle, AngleStep);
                    });
                }
                else if (i == 4)
                {
                    Act[4] = new Action(delegate
                    {
                        Sub_MatchByNCC_New(StartY[4], EndY[4], StructSrcImageHandle, PatLevel, ScoreThreshHold, start_Angle, end_Angle, AngleStep);
                    });
                }
                else if (i == 5)
                {
                    Act[5] = new Action(delegate
                    {
                        Sub_MatchByNCC_New(StartY[5], EndY[5], StructSrcImageHandle, PatLevel, ScoreThreshHold, start_Angle, end_Angle, AngleStep);
                    });
                }
            }
            Parallel.Invoke(Act);
        }
        private void Sub_MatchByNCC_New(int StartY, int EndY, StructVegaHandle StructSrcImageHandle, int PatLevel, double ScoreThreshHold, double start_Angle, double end_Angle, double AngleStep)
        {
            MatchedResult MatchedResult;
            int SrcWidth = StructSrcImageHandle.Width;
            for (int y = StartY; y < EndY; y++)
            {
                for (int x = 0; x < SrcWidth; x++)
                {
                    MatchedResult = this.MatchByNCC_SerchBestAngle(StructSrcImageHandle, PatLevel, ScoreThreshHold, x, y, start_Angle, end_Angle, AngleStep);
                    if (MatchedResult != null) this.MatchedResule_List.Add(MatchedResult);

                }
            }
        }
        unsafe public MatchedResult MatchByNCC_SerchBestAngle(StructVegaHandle StructSrcImageHandle, int PatLevel, double ScoreThreshHold ,int CenterX, int CenterY, double start_Angle, double end_Angle, double AngleStep)
        {
            MatchedResult MatchedResult = new Match.MatchedResult();
            StructVegaHandle StructPatImageHandle;
            double BestScore = 0;
            double BestAngle = 0;
            int BestX_Start = 0;
            int BestY_Start = 0;
            int BestPatWidth = 0;
            int BestPatHeight = 0;
            double Score = 0;
            int X_Start;
            int Y_Start;
            int X_End;
            int Y_End;
            start_Angle *= 10;
            end_Angle *= 10;
            int Start_Angle = (int)start_Angle;
            int End_Angle = (int)end_Angle;
            int IndexStep = (int)(AngleStep * 10);
            int Angle;
            for (int i = Start_Angle; i <= End_Angle; i = IndexStep + i)
            {
                Angle = i;
                if (Angle == 3600) break;
                if (Angle < 0) Angle += 3600;
                Score = 0;
                StructPatImageHandle = Memory.PtrToStructure<StructVegaHandle>(this.PatImage_Rotate[PatLevel][Angle].VegaHandle);
                X_Start = CenterX - StructPatImageHandle.Width / 2;
                Y_Start = CenterY - StructPatImageHandle.Height / 2;
                X_End = CenterX + StructPatImageHandle.Width / 2;
                Y_End = CenterY + StructPatImageHandle.Height / 2;

                if (X_Start >= 0 && Y_Start >= 0 && X_End < StructSrcImageHandle.Width - 1 && Y_End < StructSrcImageHandle.Height - 1)
                {
                    Score = this.GetScoreByNCC(StructSrcImageHandle, StructPatImageHandle, X_Start, Y_Start);
                }
                else
                {
                    continue;
                }
                if (Score > BestScore)
                {
                    BestScore = Score;
                    BestAngle = i / 10D;
                    BestX_Start = X_Start;
                    BestY_Start = Y_Start;
                    BestPatWidth = StructPatImageHandle.Width;
                    BestPatHeight = StructPatImageHandle.Height;
                }
                if (BestScore >= ScoreThreshHold)
                {
                    if (BestScore != 0)
                    {
                        this.PatPyramidIndex = PatLevel;
                        MatchedResult.MatchedOrgX = BestX_Start;
                        MatchedResult.MatchedOrgY = BestY_Start;
                        MatchedResult.MatchedOrgWidth = BestPatWidth;
                        MatchedResult.MatchedOrgHeight = BestPatHeight;
                        MatchedResult.MatchedWidth = this.PatPyramidWidth;
                        MatchedResult.MatchedHeight = this.PatPyramidHeight;
                        MatchedResult.MatchedScore = BestScore;
                        MatchedResult.MatchedAngle = BestAngle;
                        return MatchedResult;
                    }
                    break;
                }
            }
    
            return null;

           
        }
        #region MatchByNcc_Var
        bool WhileIsRun;
        List<MatchedResult> MatchedResule_List_MatchByNCC = new List<MatchedResult>();
        MatchedResult MatchedResult_MatchByNCC;
        int Dst_Start_X;
        int Dst_Start_Y;
        int Dst_End_X;
        int Dst_End_Y;
        int Pat_Width;
        int Pat_Height;
        int Dst_X_SIZE;
        int Dst_Y_SIZE;
        double Score;
        #endregion
        unsafe public void MatchByNCC(StructVegaHandle StructSrcImageHandle, StructVegaHandle StructPatImageHandle, double ScoreThreshHold, int X_Start, int Y_Start, int X_Range, int Y_Range, int X_space, int Y_space, double start_Angle, double end_Angle, bool IsSmallRange)
        {
            WhileIsRun = true;
            MatchedResule_List_MatchByNCC.Clear();
            Dst_Start_X = X_Start - X_Range;
            Dst_Start_Y = Y_Start - Y_Range;
            Dst_End_X = 0;
            Dst_End_Y = 0;
            Pat_Width = StructPatImageHandle.Width;
            Pat_Height = StructPatImageHandle.Height;
            Dst_X_SIZE = (X_Start + X_Range + Pat_Width);
            Dst_Y_SIZE = (Y_Start + Y_Range + Pat_Height);
            Score = 0;
            if (Dst_Start_X < 1) Dst_Start_X = 1;
            if (Dst_Start_Y < 1) Dst_Start_Y = 1;
            if (Dst_X_SIZE > StructSrcImageHandle.Width) Dst_X_SIZE = StructSrcImageHandle.Width;
            if (Dst_Y_SIZE > StructSrcImageHandle.Height) Dst_Y_SIZE = StructSrcImageHandle.Height;

            if (Dst_Start_X + Pat_Width > Dst_X_SIZE || Dst_Start_Y + Pat_Height > Dst_Y_SIZE) WhileIsRun = false;
            this.MatchedResultClear();
            bool flag_OverLapped = false;
            int Lenth0 = 0;
            int Lenth1 = 1;
            double Min_Width = Pat_Width * this.MaxOverLappedRatioX;
            double Min_Height = Pat_Height * this.MaxOverLappedRatioY;
            if (!IsSmallRange)
            {
                StructScoreImageHandle = Memory.PtrToStructure<StructVegaHandle>(ScoreImageClass.VegaHandle);
                StructScoreImageHandle.Width = Dst_X_SIZE;
                StructScoreImageHandle.Height = Dst_Y_SIZE;
                StructScoreImageHandle.ZoomX = 1.0;
                StructScoreImageHandle.ZoomY = 1.0;
                StructScoreImageHandle.ByteOfSkip = StructScoreImageHandle.GetBitmapSkip();
                StructScoreImageHandle.ByteOfWidth = StructScoreImageHandle.Width + StructScoreImageHandle.ByteOfSkip;
                StructScoreImageHandle.ImageDataLenth = StructScoreImageHandle.ByteOfWidth * StructScoreImageHandle.Height;
                Memory.StructureToPtr<StructVegaHandle>(StructScoreImageHandle, ScoreImageClass.VegaHandle);
                StructScoreImageHandle.StructRefresh();
                StructScoreImageHandle.ClearCanvas();


                Min_Width = Pat_Width * 0.3;
                Min_Height = Pat_Height * 0.3;
            }
         

            double Angle = 0;
            if (start_Angle > -MatchAngle) start_Angle = -MatchAngle;
            if (end_Angle > MatchAngle) end_Angle = MatchAngle;
            if (start_Angle < 0) start_Angle += 360;

            while (WhileIsRun)
            {
                Dst_End_X = Dst_Start_X + Pat_Width;
                if (Dst_End_X > Dst_X_SIZE)
                {
                    Dst_Start_X = X_Start - X_Range;
                    if (Dst_Start_X < 1) Dst_Start_X = 1;
                    Dst_Start_Y += Y_space;
                    continue;
                }
                Dst_End_Y = Dst_Start_Y + Pat_Height;
                if (Dst_End_Y > Dst_Y_SIZE) break;

                Score = this.GetScoreByNCC(StructSrcImageHandle, StructPatImageHandle, Dst_Start_X, Dst_Start_Y);

                if (Score >= ScoreThreshHold)
                {
                    flag_OverLapped = false;
                    if (!(IsSmallRange))
                    {
                        byte* ScoreImagePtr = (byte*)StructScoreImageHandle.ImageDataPtr;
                        ScoreImagePtr[Dst_Start_Y * StructScoreImageHandle.ByteOfWidth + Dst_Start_X] += (byte)(255 * Score);
                    }
                    if (!(IsSmallRange))
                    {
                        this.MatchedResultCheck(Score, Dst_Start_X, Dst_Start_Y, Min_Width, Min_Height, ref flag_OverLapped);

                    }
                    else
                    {
                        this.MatchedResultCheck(Score, Dst_Start_X, Dst_Start_Y, Min_Width, Min_Height, ref flag_OverLapped, ref Lenth0, ref Lenth1);
                        if (Lenth0 != -1 & Lenth1 != -1)
                        {
                            this.MatchedResultRemoveAt(Lenth0, Lenth1);
                        }
                    }
                    if (!flag_OverLapped)
                    {

                        MatchedResult_MatchByNCC = new Match.MatchedResult();
                        MatchedResult_MatchByNCC.MatchedOrgX = Dst_Start_X;
                        MatchedResult_MatchByNCC.MatchedOrgY = Dst_Start_Y;
                        MatchedResult_MatchByNCC.MatchedOrgWidth = Pat_Width;
                        MatchedResult_MatchByNCC.MatchedOrgHeight = Pat_Height;
                        MatchedResult_MatchByNCC.MatchedWidth = this.PatPyramidWidth;
                        MatchedResult_MatchByNCC.MatchedHeight = this.PatPyramidHeight;
                        MatchedResult_MatchByNCC.MatchedScore = Score;
                        MatchedResult_MatchByNCC.MatchedAngle = Angle;
                        this.MatchedResultAdd(MatchedResult_MatchByNCC);
                    }
                }

                Dst_Start_X += X_space;
            }
            MatchedResule_List = GetMatchedResult();
        }
        unsafe public void MatchByNCC(StructVegaHandle StructSrcImageHandle, StructVegaHandle StructPatImageHandle, double ScoreThreshHold, int X_Start, int Y_Start, int X_Range, int Y_Range, int X_space, int Y_space, double start_Angle, double end_Angle)
        {
            this.MatchByNCC(StructSrcImageHandle, StructPatImageHandle, ScoreThreshHold, X_Start, Y_Start, X_Range, Y_Range, X_space, Y_space, start_Angle, end_Angle, false);

        }
        unsafe public void MatchByNCC(IntPtr SrcImageHandle, StructVegaHandle StructPatImageHandle, double ScoreThreshHold, int X_space, int Y_space, double start_Angle, double end_Angle)
        {
            this.CreatIntegralImage(SrcImageHandle);
            StructVegaHandle StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(SrcImageHandle);
            this.MatchByNCC(StructSrcImageHandle, StructPatImageHandle, ScoreThreshHold, 1, 1, StructSrcImageHandle.Width, StructSrcImageHandle.Height, X_space, Y_space, start_Angle, end_Angle);
        }
   
        public void CreatPyramidImage(int Level)
        {
            this.CreatPyramidImage(SrcImageHandle, Level);
        }
        public void CreatPyramidImage(IntPtr SrcImageHandle, int Level)
        {
            StructVegaHandle StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(SrcImageHandle);
            int Src_X_Size = StructSrcImageHandle.Width;
            int Src_Y_Size = StructSrcImageHandle.Height;
            int Src_WidthByte = StructSrcImageHandle.ByteOfWidth;
            this.ImageClass_pyramid_list.Clear();
            for (int i = 0; i < Level; i++)
            {

                if (i == 0) this.ImageClass_pyramid_list.Add(this.ImageClass_pyramid_level_01);
                else if (i == 1) this.ImageClass_pyramid_list.Add(this.ImageClass_pyramid_level_02);
                else if (i == 2) this.ImageClass_pyramid_list.Add(this.ImageClass_pyramid_level_03);
                else if (i == 3) this.ImageClass_pyramid_list.Add(this.ImageClass_pyramid_level_04);
                else if (i == 4) this.ImageClass_pyramid_list.Add(this.ImageClass_pyramid_level_05);
                else if (i == 5) this.ImageClass_pyramid_list.Add(this.ImageClass_pyramid_level_06);
                else if (i == 6) this.ImageClass_pyramid_list.Add(this.ImageClass_pyramid_level_07);
                else if (i == 7) this.ImageClass_pyramid_list.Add(this.ImageClass_pyramid_level_08);

            }
            for (int i = 0; i < this.ImageClass_pyramid_list.Count; i++)
            {
                this.PatPyramidIndex = i;
                ImageClass_pyramid_list[i].SetSurfacePtr(StructSrcImageHandle.ImageDataPtr, Src_X_Size, Src_Y_Size, this.PatPyramidZoomX, this.PatPyramidZoomY, HsBasic.ImageClass.ImagePixelFormat.GRAY);
            }
        }
        public void CreatPatPyramidImage(int Level)
        {
            this.CreatPatPyramidImage(PatImageHandle, Level);
        }
        public void CreatPatPyramidImage(IntPtr SrcImageHandle, int Level)
        {
            StructVegaHandle StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(SrcImageHandle);
            int Src_X_Size = StructSrcImageHandle.Width;
            int Src_Y_Size = StructSrcImageHandle.Height;
            int Src_WidthByte = StructSrcImageHandle.ByteOfWidth;
            double ZoomX = (double)(this.MinReducedArea) / (double)Src_X_Size;
            double ZoomY = (double)(this.MinReducedArea) / (double)Src_Y_Size;
            this.ImageClass_PatPyramid_list.Clear();
            this.PyramidLevel = Level;
            for (int i = 0; i < Level; i++)
            {
                if (i == 0)
                {
                    ImageClass_PatPyramid_level_01.InitImage(Src_X_Size, Src_Y_Size, 1 / Math.Pow(2, i) * ZoomX, 1 / Math.Pow(2, i) * ZoomY, StructSrcImageHandle.ImagePixelFormat, true);
                    this.ImageClass_PatPyramid_list.Add(this.ImageClass_PatPyramid_level_01);
                }
                else if (i == 1)
                {
                    ImageClass_PatPyramid_level_02.InitImage(Src_X_Size, Src_Y_Size, 1 / Math.Pow(2, i) * ZoomX, 1 / Math.Pow(2, i) * ZoomY, StructSrcImageHandle.ImagePixelFormat, true);
                    this.ImageClass_PatPyramid_list.Add(this.ImageClass_PatPyramid_level_02);
                }
                else if (i == 2)
                {
                    ImageClass_PatPyramid_level_03.InitImage(Src_X_Size, Src_Y_Size, 1 / Math.Pow(2, i) * ZoomX, 1 / Math.Pow(2, i) * ZoomY, StructSrcImageHandle.ImagePixelFormat, true);
                    this.ImageClass_PatPyramid_list.Add(this.ImageClass_PatPyramid_level_03);
                }
                else if (i == 3)
                {
                    ImageClass_PatPyramid_level_04.InitImage(Src_X_Size, Src_Y_Size, 1 / Math.Pow(2, i) * ZoomX, 1 / Math.Pow(2, i) * ZoomY, StructSrcImageHandle.ImagePixelFormat, true);
                    this.ImageClass_PatPyramid_list.Add(this.ImageClass_PatPyramid_level_04);
                }
                else if (i == 4)
                {
                    ImageClass_PatPyramid_level_05.InitImage(Src_X_Size, Src_Y_Size, 1 / Math.Pow(2, i) * ZoomX, 1 / Math.Pow(2, i) * ZoomY, StructSrcImageHandle.ImagePixelFormat, true);
                    this.ImageClass_PatPyramid_list.Add(this.ImageClass_PatPyramid_level_05);
                }
                else if (i == 5)
                {
                    ImageClass_PatPyramid_level_06.InitImage(Src_X_Size, Src_Y_Size, 1 / Math.Pow(2, i) * ZoomX, 1 / Math.Pow(2, i) * ZoomY, StructSrcImageHandle.ImagePixelFormat, true);
                    this.ImageClass_PatPyramid_list.Add(this.ImageClass_PatPyramid_level_06);
                }
                else if (i == 6)
                {
                    ImageClass_PatPyramid_level_07.InitImage(Src_X_Size, Src_Y_Size, 1 / Math.Pow(2, i) * ZoomX, 1 / Math.Pow(2, i) * ZoomY, StructSrcImageHandle.ImagePixelFormat, true);
                    this.ImageClass_PatPyramid_list.Add(this.ImageClass_PatPyramid_level_07);
                }
                else if (i == 7)
                {
                    ImageClass_PatPyramid_level_08.InitImage(Src_X_Size, Src_Y_Size, 1 / Math.Pow(2, i) * ZoomX, 1 / Math.Pow(2, i) * ZoomY, StructSrcImageHandle.ImagePixelFormat, true);
                    this.ImageClass_PatPyramid_list.Add(this.ImageClass_PatPyramid_level_08);
                }
            }
            this.PatInegralValue_List.Clear();
            this.PatInegralPowValue_List.Clear();
            uint SumValue = 0;
            ulong SumPowValue = 0;
            StructVegaHandle StructPatImageHandle;
            ImageClass_PatPyramid_list[0].SetSurfacePtr(StructSrcImageHandle.ImageDataPtr, Src_X_Size, Src_Y_Size, ImageClass_PatPyramid_list[0].ZoomX, ImageClass_PatPyramid_list[0].ZoomY, HsBasic.ImageClass.ImagePixelFormat.GRAY);
            StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(ImageClass_PatPyramid_list[0].VegaHandle);
            ImageClass_PatPyramid_list[0].GetInegralValue();
            this.PatInegralValue_List.Add(SumValue);
            this.PatInegralPowValue_List.Add(SumPowValue);
            for (int i = 1; i < this.ImageClass_PatPyramid_list.Count; i++)
            {
                StructPatImageHandle = Memory.PtrToStructure<StructVegaHandle>(ImageClass_PatPyramid_list[i].VegaHandle);
                this.SetPatPyramidImage(StructSrcImageHandle, StructPatImageHandle, i);
                ImageClass_PatPyramid_list[i].GetInegralValue();
                this.PatInegralValue_List.Add(ImageClass_PatPyramid_list[i].IntegraValue);
                this.PatInegralPowValue_List.Add(ImageClass_PatPyramid_list[i].IntegraPowValue);
            }
        }
        unsafe void SetPatPyramidImage(StructVegaHandle StructSrcImageHandle, StructVegaHandle StructDstImageHandle, int Level)
        {
            int LevelValue = (int)Math.Pow(2, Level);
            byte* SrcPtr = (byte*)StructSrcImageHandle.ImageDataPtr;
            byte* DstPtr = (byte*)StructDstImageHandle.ImageDataPtr;
            int SrcWidth = StructSrcImageHandle.ByteOfWidth;
            int DstWidth = StructDstImageHandle.ByteOfWidth;
            int SrcIndex = 0;
            int DstIndex = 0;
            int SrcWidthxY;
            int DstWidthxY;
            double value = 0;
            for (int y = 0; y < StructDstImageHandle.Height; y++)
            {
                DstWidthxY = y * DstWidth;
                SrcWidthxY = y * SrcWidth * LevelValue;
                for (int x = 0; x < StructDstImageHandle.Width; x++)
                {
                    DstIndex = DstWidthxY + x;
                    SrcIndex = SrcWidthxY + x * LevelValue;
                    if (Level == 0)
                    {
                        value = SrcPtr[SrcIndex];
                    }
                    else if (Level == 1)
                    {
                        value = 0;
                        for (int i = 0; i < 2; i++)
                        {
                            value += SrcPtr[SrcIndex + SrcWidth * i] + SrcPtr[SrcIndex + SrcWidth * i + 1];
                        }

                        value /= 4;
                    }
                    else if (Level == 2)
                    {
                        value = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            value += SrcPtr[SrcIndex + SrcWidth * i] + SrcPtr[SrcIndex + SrcWidth * i + 1] + SrcPtr[SrcIndex + SrcWidth * i + 2] + SrcPtr[SrcIndex + SrcWidth * i + 3];
                        }

                        value /= 16;
                    }
                    else if (Level == 3)
                    {
                        value = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            value += SrcPtr[SrcIndex + SrcWidth * i] + SrcPtr[SrcIndex + SrcWidth * i + 1] + SrcPtr[SrcIndex + SrcWidth * i + 2] + SrcPtr[SrcIndex + SrcWidth * i + 3];
                            value += SrcPtr[SrcIndex + SrcWidth * i + 4] + SrcPtr[SrcIndex + SrcWidth * i + 5] + SrcPtr[SrcIndex + SrcWidth * i + 6] + SrcPtr[SrcIndex + SrcWidth * i + 7];
                        }

                        value /= 64;
                    }
                    DstPtr[DstIndex] = (byte)(value);
                }
            }
        }

        PatternRotator ImageRotator = new PatternRotator();
        List<List<ImageClass>> PatImage_Rotate = new List<List<ImageClass>>();
        List<ImageClass> Pat_Rotate_Leve00 = new List<ImageClass>();
        List<ImageClass> Pat_Rotate_Leve01 = new List<ImageClass>();
        List<ImageClass> Pat_Rotate_Leve02 = new List<ImageClass>();
        List<ImageClass> Pat_Rotate_Leve03 = new List<ImageClass>();
        public void LearnPattern()
        {
            this.IsLearnPattern = false;
            this.CreatPatPyramidImage(PyramidLevel);
            PatImage_Rotate.Clear();
            Pat_Rotate_Leve00.Clear();
            Pat_Rotate_Leve01.Clear();
            Pat_Rotate_Leve02.Clear();
            Pat_Rotate_Leve03.Clear();
            StructVegaHandle StructPatImageHandle;
            for (int i = 0; i < this.PyramidLevel; i++)
            {
                this.PatPyramidIndex = i;

                for (int k = 0; k < 3600; k++)
                {
                    ImageClass ImageClass_Pattern = new HsBasic.ImageClass();
                    StructPatImageHandle = Memory.PtrToStructure<StructVegaHandle>(ImageClass_Pattern.VegaHandle);
                    ImageRotator.SrcImageHandle = this.PatPyramidHandle;
                    ImageRotator.RotateDegree = k;
                    ImageRotator.DstImageHandle = ImageClass_Pattern.VegaHandle;
                    ImageRotator.Rotate();
                    ImageClass_Pattern.GetInegralValue();
                    if (i == 0) Pat_Rotate_Leve00.Add(ImageClass_Pattern);
                    else if (i == 1) Pat_Rotate_Leve01.Add(ImageClass_Pattern);
                    else if (i == 2) Pat_Rotate_Leve02.Add(ImageClass_Pattern);
                    else if (i == 3) Pat_Rotate_Leve03.Add(ImageClass_Pattern);
                }
                if (i == 0) PatImage_Rotate.Add(Pat_Rotate_Leve00);
                else if (i == 1) PatImage_Rotate.Add(Pat_Rotate_Leve01);
                else if (i == 2) PatImage_Rotate.Add(Pat_Rotate_Leve02);
                else if (i == 3) PatImage_Rotate.Add(Pat_Rotate_Leve03);
            }
            this.IsLearnPattern = true;
        }
        public void DrawPyramidImage(Canvas Canvas, double ZoomX, double ZoomY)
        {
            this.DrawPyramidImage(Canvas, ZoomX, ZoomY, this.PyramidIndex);
        }
        public void DrawPyramidImage(Canvas Canvas, double ZoomX, double ZoomY, int index)
        {
            this.ImageClass_pyramid_list[index].DrawImage(Canvas, ZoomX, ZoomY);
        }
        public void DrawPatPyramidImage(Canvas Canvas, double ZoomX, double ZoomY)
        {
            this.DrawPatPyramidImage(Canvas, ZoomX, ZoomY, this.PatPyramidIndex);
        }
        public void DrawPatPyramidImage(Canvas Canvas, double ZoomX, double ZoomY, int index)
        {
            this.ImageClass_PatPyramid_list[index].DrawImage(Canvas, ZoomX, ZoomY);
        }
        public void DrawPatPyramidImage(Canvas Canvas, int PyramidLevel, double Degree)
        {
            Degree = Degree * 10;
            Canvas.SetScale(this.PatImage_Rotate[PyramidLevel][(int)Degree].Width, this.PatImage_Rotate[PyramidLevel][(int)Degree].Height);

            this.PatImage_Rotate[PyramidLevel][(int)Degree].DrawImage(Canvas, Canvas.ZoomX, Canvas.ZoomY);
        }
        public void DrawMatchPattern(Canvas Canvas, int OrgX, int OrgY, double index, double ZoomX, double ZoomY)
        {
            IntPtr hdcDest = Canvas.CanvasHDC;
            Graphics g = Graphics.FromHdc(hdcDest);
            PointF CENTER;

            float[] DashValue = new float[] { 2, 2, 2, 2 };
            foreach (MatchedResult matchedResult in MatchedResule_List)
            {
  
                CENTER = new PointF((float)(matchedResult.MatchedCenterX ), (float)(matchedResult.MatchedCenterY));
                CENTER.X += OrgX;
                CENTER.Y += OrgY;

                PointF[] pointF = new PointF[4];
                double RotateAngle =  - matchedResult.MatchedAngle;
                double r = Math.Sqrt(Math.Pow((double)matchedResult.MatchedWidth / 2d, 2d) + Math.Pow((double)matchedResult.MatchedHeight / 2d, 2d)); //半徑L
                double OriginalAngle = Math.Acos((matchedResult.MatchedWidth / 2d) / r) / Math.PI * 180d;  //對角線和X軸的角度θ
                double[] drawPoint = new double[4];

                drawPoint[0] = (-OriginalAngle + RotateAngle) * Math.PI / 180d;
                drawPoint[1] = (OriginalAngle + RotateAngle) * Math.PI / 180d;
                drawPoint[2] = (180f - OriginalAngle + RotateAngle) * Math.PI / 180d;
                drawPoint[3] = (180f + OriginalAngle + RotateAngle) * Math.PI / 180d;
                int indexpoint = 0;
                foreach (double point in drawPoint) //由四個角的點算出X、Y的最大值及最小值
                {
                    double x = r * Math.Cos(point);
                    double y = r * Math.Sin(point);
                    x += matchedResult.MatchedCenterX + OrgX;
                    y += matchedResult.MatchedCenterY + OrgY;
                    pointF[indexpoint] = new PointF((float)x, (float)y);
                    indexpoint++;
                }
                Draw.線段繪製(pointF[0], pointF[1], Color.White, 1.0F, g, (float)ZoomX, (float)ZoomY);
                Draw.線段繪製(pointF[1], pointF[2], Color.White, 1.0F, g, (float)ZoomX, (float)ZoomY);
                Draw.線段繪製(pointF[2], pointF[3], Color.White, 1.0F, g, (float)ZoomX, (float)ZoomY);
                Draw.線段繪製(pointF[3], pointF[0], Color.White, 1.0F, g, (float)ZoomX, (float)ZoomY);
                Draw.線段繪製_Dash(pointF[0], pointF[1], Color.Red, 1.0F, DashValue, g, (float)ZoomX, (float)ZoomY);
                Draw.線段繪製_Dash(pointF[1], pointF[2], Color.Red, 1.0F, DashValue, g, (float)ZoomX, (float)ZoomY);
                Draw.線段繪製_Dash(pointF[2], pointF[3], Color.Red, 1.0F, DashValue, g, (float)ZoomX, (float)ZoomY);
                Draw.線段繪製_Dash(pointF[3], pointF[0], Color.Red, 1.0F, DashValue, g, (float)ZoomX, (float)ZoomY);

                Draw.十字中心(CENTER, 30F, Color.Lime, 1, g, (float)ZoomX, (float)ZoomY);
            }
            Canvas.ReleaseHDC();

        }
        private PointF TaransFormAxis(PointF RotateCenter , PointF Po , double Degree)
        {
            double Radiu = Degree * Math.PI / 180;
            double SinValue = Math.Sin(Radiu);
            double CosValue = Math.Cos(Radiu);
            double Dst_Y = ((Po.X - RotateCenter.X) * SinValue + (Po.Y - RotateCenter.Y) * CosValue) + RotateCenter.Y;
            double Dst_X = ((Po.X - RotateCenter.X) * CosValue - (Po.Y - RotateCenter.Y) * SinValue) + RotateCenter.X;
            return new PointF((float)Dst_X, (float)Dst_Y);
        }
        public void DrawScoreImage(Canvas Canvas, double ZoomX, double ZoomY)
        {
            this.ScoreImageClass.DrawImage(Canvas, ZoomX, ZoomY);
        }


        private class PatternRotator
        {
            private StructVegaHandle StructSrcImageHandle;
            private StructVegaHandle StructDstImageHandle;
            private IntPtr _SrcImageHandle;
            public IntPtr SrcImageHandle
            {
                get
                {
                    return _SrcImageHandle;
                }
                set
                {

                    StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(value);
                    _SrcImageHandle = value;
                }
            }
            private IntPtr _DstImageHandle;
            public IntPtr DstImageHandle
            {
                get
                {
                    return _DstImageHandle;
                }
                set
                {
                    _DstImageHandle = value;
                }
            }
            public uint BlankColorBW8 = 0;
            public double RotateCenterX = 0;
            public double RotateCenterY = 0;
            public double RotateDegree = 0;
            public int Width
            {
                get
                {
                    return StructDstImageHandle.Width;
                }
                private set
                {
                }
            }
            public int Height
            {
                get
                {
                    return StructDstImageHandle.Height;
                }
                private set
                {
                }
            }
            unsafe public void Rotate()
            {
                double Radiu = RotateDegree * Math.PI / 180;
                double SinValue = Math.Sin(Radiu);
                double CosValue = Math.Cos(Radiu);
                int Src_X_SIZE = StructSrcImageHandle.Width;
                int Src_Y_SIZE = StructSrcImageHandle.Height;
                Size Dst_Size = this.CalculateNewSize(Src_X_SIZE, Src_Y_SIZE, RotateDegree);
                int Dst_X_SIZE = Dst_Size.Width;
                int Dst_Y_SIZE = Dst_Size.Height;

                StructDstImageHandle = Memory.PtrToStructure<StructVegaHandle>(DstImageHandle);
                StructDstImageHandle.Width = Dst_X_SIZE;
                StructDstImageHandle.Height = Dst_Y_SIZE;
                StructDstImageHandle.ColorDepth = StructSrcImageHandle.ColorDepth;
                StructDstImageHandle.ByteOfSkip = StructDstImageHandle.GetBitmapSkip();
                StructDstImageHandle.ImagePixelFormat = StructSrcImageHandle.ImagePixelFormat;
                StructDstImageHandle.ByteOfWidth = (StructDstImageHandle.Width * StructDstImageHandle.ColorDepth) + StructDstImageHandle.ByteOfSkip;
                StructDstImageHandle.MaskImage = true;
                StructDstImageHandle.LenthOfPtr = (StructDstImageHandle.ByteOfWidth * StructDstImageHandle.Height);
                Memory.StructureToPtr<StructVegaHandle>(StructDstImageHandle, DstImageHandle);
                StructDstImageHandle.StructRefresh();
                StructDstImageHandle.ClearCanvas();

                // this.ShiftToCenter(Src_X_SIZE, Src_Y_SIZE, Dst_X_SIZE, Dst_Y_SIZE);
                Task_Rotate(6, Dst_Y_SIZE);

            }
            private void Task_Rotate(int NumOfTask, int Y_Size)
            {
                int[] StartY = new int[NumOfTask];
                int[] EndY = new int[NumOfTask];
                int thread_Y_size = Y_Size / NumOfTask;
                Action[] Act = new Action[NumOfTask];
                for (int i = 0; i < NumOfTask; i++)
                {
                    StartY[i] = i * thread_Y_size;
                    if (i == NumOfTask - 1) EndY[i] = Y_Size;
                    else EndY[i] = (i + 1) * thread_Y_size;
                    if (i == 0)
                    {
                        Act[0] = new Action(delegate
                        {

                            sub_Rotate(StartY[0], EndY[0]);
                        });
                    }
                    else if (i == 1)
                    {
                        Act[1] = new Action(delegate
                        {
                            sub_Rotate(StartY[1], EndY[1]);
                        });
                    }
                    else if (i == 2)
                    {
                        Act[2] = new Action(delegate
                        {
                            sub_Rotate(StartY[2], EndY[2]);
                        });
                    }
                    else if (i == 3)
                    {
                        Act[3] = new Action(delegate
                        {
                            sub_Rotate(StartY[3], EndY[3]);
                        });
                    }
                    else if (i == 4)
                    {
                        Act[4] = new Action(delegate
                        {
                            sub_Rotate(StartY[4], EndY[4]);
                        });
                    }
                    else if (i == 5)
                    {
                        Act[5] = new Action(delegate
                        {
                            sub_Rotate(StartY[5], EndY[5]);
                        });
                    }
                }
                Parallel.Invoke(Act);
            }
            unsafe private void sub_Rotate(int start_Y, int end_Y)
            {
                int Src_X_SIZE = StructSrcImageHandle.Width;
                int Src_Y_SIZE = StructSrcImageHandle.Height;
                int Dst_X_SIZE = StructDstImageHandle.Width;
                int Dst_Y_SIZE = StructDstImageHandle.Height;
                int Distance_Src_Dst_X = Dst_X_SIZE - Src_X_SIZE;
                int Distance_Src_Dst_Y = Dst_Y_SIZE - Src_Y_SIZE;


                Distance_Src_Dst_X /= 2;
                Distance_Src_Dst_Y /= 2;
                start_Y -= Distance_Src_Dst_Y;
                end_Y -= Distance_Src_Dst_Y;
                double Radiu = RotateDegree * Math.PI / 180;
                double SinValue = Math.Sin(Radiu);
                double CosValue = Math.Cos(Radiu);
                int Src_WidthByte = StructSrcImageHandle.ByteOfWidth;
                int Dst_WidthByte = StructDstImageHandle.ByteOfWidth;
                byte* SrcPtr = (byte*)StructSrcImageHandle.ImageDataPtr;
                byte* DstPtr = (byte*)StructDstImageHandle.ImageDataPtr;
                RotateCenterX = Src_X_SIZE / 2.0;
                RotateCenterY = Src_Y_SIZE / 2.0;
                Src_X_SIZE = StructSrcImageHandle.Width;
                Src_Y_SIZE = StructSrcImageHandle.Height;
                int DstWidthxY;
                int DstIndex;
                int SrcIndex;
                int m;
                int n;
                double p;
                double q;
                double Dst_Y;
                double Dst_X;
                double value = 0;

                for (int y = start_Y; y < end_Y; y++)
                {
                    DstWidthxY = y * Dst_WidthByte;
                    for (int x = -Distance_Src_Dst_X; x < Dst_X_SIZE - Distance_Src_Dst_X; x++)
                    {
                        DstIndex = DstWidthxY + x;
                        Dst_Y = ((x - RotateCenterX) * SinValue + (y - RotateCenterY) * CosValue) + RotateCenterY;
                        Dst_X = ((x - RotateCenterX) * CosValue - (y - RotateCenterY) * SinValue) + RotateCenterX;
                        if (Dst_Y > 0) m = (int)Dst_Y;
                        else m = (int)(Dst_Y - 1);
                        if (Dst_X > 0) n = (int)Dst_X;
                        else n = (int)(Dst_X - 1);
                        m = (int)Dst_Y;
                        n = (int)Dst_X;
                        q = Dst_Y - m;
                        p = Dst_X - n;
                        SrcIndex = m * Src_WidthByte + n;
                        if ((m >= 0) && (m <= Src_Y_SIZE - 1) && (n >= 0) && (n <= Src_X_SIZE - 1))
                        {
                            if (n == Src_X_SIZE - 1)
                            {
                                value = SrcPtr[SrcIndex];
                            }
                            else if (m == Src_Y_SIZE - 1)
                            {
                                value = SrcPtr[SrcIndex];
                            }
                            else value = (1.0 - q) * ((1.0 - p) * SrcPtr[SrcIndex] + p * SrcPtr[SrcIndex + 1]) + q * ((1.0 - p) * SrcPtr[SrcIndex + Src_WidthByte] + p * SrcPtr[SrcIndex + Src_WidthByte + 1]);

                        }
                        else
                        {
                            value = 0;
                        }
                        DstPtr[DstIndex + Distance_Src_Dst_X + Distance_Src_Dst_Y * Dst_WidthByte] = (byte)value;
                    }
                }
            }

            public Size CalculateNewSize(int width, int height, double RotateAngle)
            {
                double r = Math.Sqrt(Math.Pow((double)width / 2d, 2d) + Math.Pow((double)height / 2d, 2d)); //半徑L
                double OriginalAngle = Math.Acos((width / 2d) / r) / Math.PI * 180d;  //對角線和X軸的角度θ
                double minW = 0d, maxW = 0d, minH = 0d, maxH = 0d; //最大和最小的 X、Y座標
                double[] drawPoint = new double[4];

                drawPoint[0] = (-OriginalAngle + RotateAngle) * Math.PI / 180d;
                drawPoint[1] = (OriginalAngle + RotateAngle) * Math.PI / 180d;
                drawPoint[2] = (180f - OriginalAngle + RotateAngle) * Math.PI / 180d;
                drawPoint[3] = (180f + OriginalAngle + RotateAngle) * Math.PI / 180d;

                foreach (double point in drawPoint) //由四個角的點算出X、Y的最大值及最小值
                {
                    double x = r * Math.Cos(point);
                    double y = r * Math.Sin(point);

                    if (x < minW)
                        minW = x;
                    if (x > maxW)
                        maxW = x;
                    if (y < minH)
                        minH = y;
                    if (y > maxH)
                        maxH = y;
                }
                int SizeX = (int)Math.Ceiling((maxW - minW));
                int SizeY = (int)Math.Ceiling((maxH - minH));
                return new Size(SizeX, SizeY);
            }
        }
    }

}
