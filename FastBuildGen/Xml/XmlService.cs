using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Xml.Entity;
using BatchGen.Common;

namespace FastBuildGen.Xml
{
    internal static class XmlService
    {
        #region Load

        public static XmlFastBuild LoadXmlFastBuild(string filePath)
        {
            XmlFastBuild result = null;
            try
            {
                result = Load<XmlFastBuild>(filePath);
            }
            catch (FastBuildGenException) { throw; }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                throw new FastBuildGenException("XmlFastBuild loading from file failed", e);
            }

            return result;
        }

        public static XmlFastBuild ReadXmlFastBuild(Stream stream)
        {
            XmlFastBuild result = null;
            try
            {
                result = Read<XmlFastBuild>(stream);
            }
            catch (FastBuildGenException) { throw; }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                throw new FastBuildGenException("XmlFastBuild reading from Stream failed", e);
            }

            return result;
        }

        private static TData Load<TData>(string filePath)
        {
            TData result = default(TData);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                result = Read<TData>(fileStream);
            }

            return result;
        }

        private static TData Read<TData>(Stream stream)
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
                new FastBuildGenException("Unexpected type during deserializing", e);
            }

            return result;
        }

        #endregion Load

        #region Save

        public static void Save(string filePath, IFastBuildModel model)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    Write(fileStream, model);
                }
            }
            catch (FastBuildGenException) { throw; }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                throw new FastBuildGenException("IFastBuildModel saving into File failed", e);
            }
        }

        public static void Write(Stream stream, IFastBuildModel model)
        {
            try
            {
                using (XmlSession session = new XmlSession())
                {
                    XmlFastBuild xmlFastBuild = session.GetOrCreateXmlFastBuild(model);
                    Write<XmlFastBuild>(stream, xmlFastBuild);
                }
            }
            catch (FastBuildGenException ) { throw; }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                throw new FastBuildGenException("IFastBuildModel serialization into Stream failed", e);
            }
        }

        private static void Write<TData>(Stream stream, TData data)
            where TData : class
        {
            Type type = typeof(TData);
            using (XmlTextWriter writer = new XmlTextWriter(new ProxyStream(stream), null))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;

                writer.WriteStartDocument();
                writer.WriteComment("Keep values of XmlId valid and uniq for each node!");

                XmlSerializer serializer = new XmlSerializer(type);
                serializer.Serialize(writer, data);

                writer.WriteEndDocument();
            }
        }

        #endregion Save
    }
}