using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PackLabelPrint
{
    public static class TsplPrinter
    {
        public static void SendBitmap(string printer, byte[] mono, int widthDots, int heightDots, string widthMm, string heightMm, string gapMm)
        {
            int rowBytes = (widthDots + 7) / 8;
            if (mono == null || mono.Length != rowBytes * heightDots)
            {
                throw new Exception("标签图像大小不对");
            }

            string header =
                "SIZE " + widthMm + " mm," + heightMm + " mm\r\n" +
                "GAP " + gapMm + " mm,0\r\n" +
                "DENSITY 8\r\n" +
                "SPEED 3\r\n" +
                "DIRECTION 1\r\n" +
                "REFERENCE 0,0\r\n" +
                "CLS\r\n" +
                "BITMAP 0,0," + rowBytes + "," + heightDots + ",0,";
            byte[] head = Encoding.ASCII.GetBytes(header);
            byte[] tail = Encoding.ASCII.GetBytes("\r\nPRINT 1\r\n");

            byte[] job = new byte[head.Length + mono.Length + tail.Length];
            Buffer.BlockCopy(head, 0, job, 0, head.Length);
            Buffer.BlockCopy(mono, 0, job, head.Length, mono.Length);
            Buffer.BlockCopy(tail, 0, job, head.Length + mono.Length, tail.Length);

            RawWrite(printer, job);
        }

        static void RawWrite(string printer, byte[] data)
        {
            IntPtr handle = IntPtr.Zero;
            bool opened = OpenPrinter(printer, out handle, IntPtr.Zero);
            if (!opened || handle == IntPtr.Zero)
            {
                throw new Exception("打不开打印机：" + printer + "（" + Marshal.GetLastWin32Error() + "）");
            }

            try
            {
                DOCINFOA info = new DOCINFOA();
                info.pDocName = "PACK客户码纸条";
                info.pDataType = "RAW";
                if (StartDocPrinter(handle, 1, info) == 0)
                {
                    throw new Exception("StartDocPrinter 失败 " + Marshal.GetLastWin32Error());
                }
                try
                {
                    if (!StartPagePrinter(handle))
                    {
                        throw new Exception("StartPagePrinter 失败 " + Marshal.GetLastWin32Error());
                    }
                    try
                    {
                        GCHandle pinned = GCHandle.Alloc(data, GCHandleType.Pinned);
                        try
                        {
                            int written;
                            if (!WritePrinter(handle, pinned.AddrOfPinnedObject(), data.Length, out written))
                            {
                                throw new Exception("WritePrinter 失败 " + Marshal.GetLastWin32Error());
                            }
                            if (written != data.Length)
                            {
                                throw new Exception("只写出 " + written + " / " + data.Length);
                            }
                        }
                        finally
                        {
                            pinned.Free();
                        }
                    }
                    finally
                    {
                        EndPagePrinter(handle);
                    }
                }
                finally
                {
                    EndDocPrinter(handle);
                }
            }
            finally
            {
                ClosePrinter(handle);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        [DllImport("winspool.drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern bool OpenPrinter(string printer, out IntPtr handle, IntPtr defaults);

        [DllImport("winspool.drv", SetLastError = true)]
        static extern bool ClosePrinter(IntPtr handle);

        [DllImport("winspool.drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern int StartDocPrinter(IntPtr handle, int level, [In] DOCINFOA info);

        [DllImport("winspool.drv", SetLastError = true)]
        static extern bool EndDocPrinter(IntPtr handle);

        [DllImport("winspool.drv", SetLastError = true)]
        static extern bool StartPagePrinter(IntPtr handle);

        [DllImport("winspool.drv", SetLastError = true)]
        static extern bool EndPagePrinter(IntPtr handle);

        [DllImport("winspool.drv", SetLastError = true)]
        static extern bool WritePrinter(IntPtr handle, IntPtr data, int count, out int written);
    }
}