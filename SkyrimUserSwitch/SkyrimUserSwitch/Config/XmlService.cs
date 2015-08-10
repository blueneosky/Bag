using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using SkyrimUserSwitch.Common;

namespace SkyrimUserSwitch.Config
{
    internal static class XmlService
    {
        #region Load

        public static TData Load<TData>(string filePath)
        {
            TData result = default(TData);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                result = Read<TData>(fileStream);
            }

            return result;
        }

        public static TData Read<TData>(Stream stream)
        {
            TData result = default(TData);

            Type type = typeof(TData);

            XmlSerializer reader = new XmlSerializer(type);
            object data = reader.Deserialize(stream);
            try
            {
                result = (TData)data;
            }
            catch (Exception e)
            {
                new SkusException("Unexpected type during deserializing", e);
            }

            return result;
        }

        public static bool TryLoad<TData>(string filePath, out TData data)
        {
            bool success = false;
            try
            {
                data = Load<TData>(filePath);
                success = true;
            }
            catch (Exception)
            {
                data = default(TData);
            }

            return success;
        }

        #endregion Load

        #region Save

        public static void Save<TData>(string filePath, TData data)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    Write(fileStream, data);
                }
            }
            catch (SkusException) { throw; }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                throw new SkusException("Saving failed", e);
            }
        }

        public static bool TrySave<TData>(string filePath, TData data)
        {
            bool success = false;
            try
            {
                Save<TData>(filePath, data);
                success = true;
            }
            catch (Exception) { }

            return success;
        }

        public static void Write<TData>(Stream stream, TData data)
        {
            Type type = typeof(TData);
            using (XmlTextWriter writer = new XmlTextWriter(stream, null))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;

                XmlSerializer serializer = new XmlSerializer(type);
                serializer.Serialize(writer, data);
            }
        }

        #endregion Save
    }
}