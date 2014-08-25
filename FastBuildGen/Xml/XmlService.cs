using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using BatchGen.Common;
using FastBuildGen.BusinessModel.Old;
using FastBuildGen.Common;
using FastBuildGen.Xml.Entity;
using FastBuildGen.BusinessModel;

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
                throw new FastBuildGenException("XmlFastBuild loading failed from file", e);
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
                throw new FastBuildGenException("XmlFastBuild reading failed from Stream", e);
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
                throw new FastBuildGenException("FBModel saving into File failed", e);
            }
        }

        public static void Write(Stream stream, FBModel fbModel)
        {
            try
            {
                XmlFastBuild xmlFastBuild = new XmlFastBuild(fbModel);
                Write<XmlFastBuild>(stream, xmlFastBuild);
            }
            catch (FastBuildGenException) { throw; }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                throw new FastBuildGenException("FBModel serialization into Stream failed", e);
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

                XmlSerializer serializer = new XmlSerializer(type);
                serializer.Serialize(writer, data);

                writer.WriteEndDocument();
            }
        }

        #endregion Save
    }
}