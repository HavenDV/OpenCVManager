using System;
using System.IO;
using System.Runtime.InteropServices;

namespace OpenCVManager.Utilities
{
    public static class BinaryUtilities
    {
        public enum MachineType : ushort
        {
            ImageFileMachineUnknown = 0x0,
            ImageFileMachineAm33 = 0x1d3,
            ImageFileMachineAmd64 = 0x8664,
            ImageFileMachineArm = 0x1c0,
            ImageFileMachineEbc = 0xebc,
            ImageFileMachineI386 = 0x14c,
            ImageFileMachineIa64 = 0x200,
            ImageFileMachineM32R = 0x9041,
            ImageFileMachineMips16 = 0x266,
            ImageFileMachineMipsfpu = 0x366,
            ImageFileMachineMipsfpu16 = 0x466,
            ImageFileMachinePowerpc = 0x1f0,
            ImageFileMachinePowerpcfp = 0x1f1,
            ImageFileMachineR4000 = 0x166,
            ImageFileMachineSh3 = 0x1a2,
            ImageFileMachineSh3Dsp = 0x1a3,
            ImageFileMachineSh4 = 0x1a6,
            ImageFileMachineSh5 = 0x1a8,
            ImageFileMachineThumb = 0x1c2,
            ImageFileMachineWcemipsv2 = 0x169,
        }

        public static MachineType GetDllMachineType(string dllPath)
        {
            // 
            // See http://www.microsoft.com/whdc/system/platform/firmware/PECOFF.mspx
            // Offset to PE header is always at 0x3C.
            // The PE header starts with "PE\0\0" =  0x50 0x45 0x00 0x00,
            // followed by a 2-byte machine type field (see the document above for the enum).
            //
            if (!File.Exists(dllPath))
            {
                return MachineType.ImageFileMachineUnknown;
            }

            var stream = new FileStream(dllPath, FileMode.Open, FileAccess.Read);
            var reader = new BinaryReader(stream);
            stream.Seek(0x3c, SeekOrigin.Begin);
            var peOffset = reader.ReadInt32();
            stream.Seek(peOffset, SeekOrigin.Begin);
            var peHead = reader.ReadUInt32();

            if (peHead != 0x00004550)
            { // "PE\0\0", little-endian
                throw new Exception("Can't find PE header");
            }

            var type = (MachineType)reader.ReadUInt16();
            reader.Close();
            stream.Close();
            return type;
        }

        public static bool? UnmanagedDllIs64Bit(string dllPath)
        {
            switch (GetDllMachineType(dllPath))
            {
                case MachineType.ImageFileMachineAmd64:
                case MachineType.ImageFileMachineIa64:
                    return true;
                case MachineType.ImageFileMachineI386:
                    return false;
                default:
                    return null;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int GetBinaryTypeA(string lpApplicationName, ref int lpBinaryType);

        public static bool Is64Bit(string path)
        {
            path = !string.IsNullOrWhiteSpace(path) ? path : throw new ArgumentNullException(nameof(path));
            path = File.Exists(path) ? path : throw new FileNotFoundException();

            if (!string.Equals(Path.GetExtension(path), "exe", StringComparison.OrdinalIgnoreCase))
            {
                return UnmanagedDllIs64Bit(path) ?? throw new Exception("Unsupported format");
            }
            
            var binaryType = 0;
            if (GetBinaryTypeA(path, ref binaryType) == 0)
            {
                throw new Exception("GetBinaryTypeA failed");
            }

            const int scs32BitBinary = 0;
            const int scs64BitBinary = 6;

            switch (binaryType)
            {
                case scs32BitBinary:
                    return false;

                case scs64BitBinary:
                    return true;

                default:
                    throw new Exception("GetBinaryTypeA return unknown executable format for " + path);
            }
        }
    }
}
