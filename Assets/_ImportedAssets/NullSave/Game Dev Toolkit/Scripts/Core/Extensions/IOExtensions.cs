//
// Game Developers Toolkit � 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDocLocation("extensions")]
    [AutoDoc("This class contains methods that extends Stream.")]
    public static class IOExtensions
    {

        #region Public Methods

        [AutoDoc("Read a bool from a stream")]
        [AutoDocParameter("Stream to use")]
        public static bool ReadBool(this Stream fs)
        {
            if (fs.ReadByte() > 0) return true;
            return false;
        }

        [AutoDoc("Read a color from a stream")]
        [AutoDocParameter("Stream to use")]
        public static Color ReadColor(this Stream fs)
        {
            return new Color(fs.ReadFloat(), fs.ReadFloat(), fs.ReadFloat(), fs.ReadFloat());
        }

        [AutoDoc("Read a double from a stream")]
        [AutoDocParameter("Stream to use")]
        public static double ReadDouble(this Stream fs)
        {
            byte[] b = new byte[8];
            fs.Read(b, 0, b.Length);
            return BitConverter.ToDouble(b, 0);
        }

        [AutoDoc("Read an integer from a stream")]
        [AutoDocParameter("Stream to use")]
        public static int ReadInt(this Stream fs)
        {
            byte[] b = new byte[4];
            fs.Read(b, 0, b.Length);
            return BitConverter.ToInt32(b, 0);
        }

        [AutoDoc("Read a float from a stream")]
        [AutoDocParameter("Stream to use")]
        public static float ReadFloat(this Stream fs)
        {
            byte[] b = new byte[4];
            fs.Read(b, 0, b.Length);
            return BitConverter.ToSingle(b, 0);
        }

        [AutoDoc("Read a long from a stream")]
        [AutoDocParameter("Stream to use")]
        public static long ReadLong(this Stream fs)
        {
            byte[] b = new byte[8];
            fs.Read(b, 0, b.Length);
            return BitConverter.ToInt64(b, 0);
        }

        [AutoDoc("Read a quaternion from a stream")]
        [AutoDocParameter("Stream to use")]
        public static Quaternion ReadQuaternion(this Stream fs)
        {
            return new Quaternion(fs.ReadFloat(), fs.ReadFloat(), fs.ReadFloat(), fs.ReadFloat());
        }

        [AutoDoc("Read a rect from a stream")]
        [AutoDocParameter("Stream to use")]
        public static Rect ReadRect(this Stream fs)
        {
            return new Rect(fs.ReadFloat(), fs.ReadFloat(), fs.ReadFloat(), fs.ReadFloat());
        }

        [AutoDoc("Read a string packet from a stream")]
        [AutoDocParameter("Stream to use")]
        public static string ReadStringPacket(this Stream fs)
        {
            byte[] b = new byte[fs.ReadLong()];
            fs.Read(b, 0, b.Length);
            return new string(Encoding.UTF8.GetChars(b));
        }

        [AutoDoc("Read a string packet from a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Encoding to use")]
        public static string ReadStringPacket(this Stream fs, Encoding encoding)
        {
            byte[] b = new byte[fs.ReadLong()];
            fs.Read(b, 0, b.Length);
            return new string(encoding.GetChars(b));
        }

        [AutoDoc("Read a vector2 from a stream")]
        [AutoDocParameter("Stream to use")]
        public static Vector2 ReadVector2(this Stream fs)
        {
            return new Vector2(fs.ReadFloat(), fs.ReadFloat());
        }

        [AutoDoc("Read a vector3 from a stream")]
        [AutoDocParameter("Stream to use")]
        public static Vector3 ReadVector3(this Stream fs)
        {
            return new Vector3(fs.ReadFloat(), fs.ReadFloat(), fs.ReadFloat());
        }

        [AutoDoc("Write a bool to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteBool(this Stream fs, bool value)
        {
            if (value)
            {
                fs.WriteByte(1);
            }
            else
            {
                fs.WriteByte(0);
            }
        }

        [AutoDoc("Write a color to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteColor(this Stream fs, Color value)
        {
            fs.WriteFloat(value.r);
            fs.WriteFloat(value.g);
            fs.WriteFloat(value.b);
            fs.WriteFloat(value.a);
        }

        [AutoDoc("Write a double to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteDouble(this Stream fs, double value)
        {
            byte[] b = BitConverter.GetBytes(value);
            fs.Write(b, 0, b.Length);
        }

        [AutoDoc("Write a int to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteInt(this Stream fs, int value)
        {
            byte[] b = BitConverter.GetBytes(value);
            fs.Write(b, 0, b.Length);
        }

        [AutoDoc("Write a float to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteFloat(this Stream fs, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            fs.Write(b, 0, b.Length);
        }

        [AutoDoc("Write a long to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteLong(this Stream fs, long value)
        {
            byte[] b = BitConverter.GetBytes(value);
            fs.Write(b, 0, b.Length);
        }

        [AutoDoc("Write a quaternion to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteQuaternion(this Stream fs, Quaternion value)
        {
            fs.WriteFloat(value.x);
            fs.WriteFloat(value.y);
            fs.WriteFloat(value.z);
            fs.WriteFloat(value.w);
        }

        [AutoDoc("Write a rect to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteRect(this Stream fs, Rect rect)
        {
            fs.WriteFloat(rect.x);
            fs.WriteFloat(rect.y);
            fs.WriteFloat(rect.width);
            fs.WriteFloat(rect.height);
        }

        [AutoDoc("Write a string to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteString(this Stream fs, string value)
        {
            if (value == null) value = string.Empty;
            byte[] b = Encoding.UTF8.GetBytes(value);
            fs.Write(b, 0, b.Length);
        }

        [AutoDoc("Write a string to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        [AutoDocParameter("Encoding to use")]
        public static void WriteString(this Stream fs, string value, Encoding encoding)
        {
            if (value == null) value = string.Empty;
            byte[] b = encoding.GetBytes(value);
            fs.Write(b, 0, b.Length);
        }

        [AutoDoc("Write a string packet to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteStringPacket(this Stream fs, string value)
        {
            if (value == null) value = string.Empty;
            byte[] b = Encoding.UTF8.GetBytes(value);
            fs.WriteLong(b.Length);
            fs.Write(b, 0, b.Length);
        }

        [AutoDoc("Write a string packet to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        [AutoDocParameter("Encoding to use")]
        public static void WriteStringPacket(this Stream fs, string value, Encoding encoding)
        {
            if (value == null) value = string.Empty;
            byte[] b = encoding.GetBytes(value);
            fs.WriteLong(b.Length);
            fs.Write(b, 0, b.Length);
        }

        [AutoDoc("Write a vector2 to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteVector2(this Stream fs, Vector2 value)
        {
            fs.WriteFloat(value.x);
            fs.WriteFloat(value.y);
        }

        [AutoDoc("Write a vector3 to a stream")]
        [AutoDocParameter("Stream to use")]
        [AutoDocParameter("Value to write")]
        public static void WriteVector3(this Stream fs, Vector3 value)
        {
            fs.WriteFloat(value.x);
            fs.WriteFloat(value.y);
            fs.WriteFloat(value.z);
        }

        #endregion

    }
}