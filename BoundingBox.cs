﻿namespace WinFormsApp3
{
    internal class BoundingBox
    {
        public string ClassName { get; set; }
        public float Confidence { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
    }
}