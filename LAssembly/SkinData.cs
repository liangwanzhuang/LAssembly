using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAssembly
{
    [System.Serializable]
    internal struct SkinData
    {
        public int FrameWidth;

        public int CaptionHeight;

        public System.Drawing.Color FrameColor;

        public System.Drawing.Color CaptionColor;

        public System.Drawing.Bitmap logo;

        public System.Drawing.Bitmap Min;

        public System.Drawing.Bitmap MinF;

        public System.Drawing.Bitmap Max;

        public System.Drawing.Bitmap MaxF;

        public System.Drawing.Bitmap Close;

        public System.Drawing.Bitmap CloseF;

        public System.Drawing.Bitmap Normal;

        public System.Drawing.Bitmap NormalF;
    }
}
