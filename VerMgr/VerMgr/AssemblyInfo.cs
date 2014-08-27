using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VerMgr
{
    [Serializable]
    [XmlRoot("AssemblyInfo")]
    public class AssemblyInfo
    {
        public AssemblyInfo()
        {
        }

        #region Propriétées AssemblyInfo

        public string Title { get; set; }

        public string Description { get; set; }

        public string Configuration { get; set; }

        public string Company { get; set; }

        public string Product { get; set; }

        public string Copyright { get; set; }

        public string Tradmark { get; set; }

        public string Version { get; set; }

        public string FileVersion { get; set; }

        #endregion Propriétées AssemblyInfo

        #region Propriétées de configuration

        public string TargetConfiguration { get; set; }

        #endregion Propriétées de configuration

        #region Non serialized Properties

        [XmlIgnore]
        public string[] SplittedVersion
        {
            get { return GetSplittedVersionCore(Version); }
            set { Version = GetUnsplittedVersionCore(value); }
        }

        [XmlIgnore]
        public string[] SplittedFileVersion
        {
            get { return GetSplittedVersionCore(FileVersion); }
            set { FileVersion = GetUnsplittedVersionCore(value); }
        }

        [XmlIgnore]
        public int?[] SplittedNumericVersion
        {
            get { return GetSplittedNumericVersionCore(Version); }
            set { Version = GetUnsplittedNumericVersionCore(value); }
        }

        [XmlIgnore]
        public int?[] SplittedNumericFileVersion
        {
            get { return GetSplittedNumericVersionCore(FileVersion); }
            set { FileVersion = GetUnsplittedNumericVersionCore(value); }
        }

        #endregion Non serialized Properties

        #region Functions

        private static string[] GetSplittedVersionCore(string version)
        {
            if (version == null)
                version = String.Empty;
            string[] data = version.Split(new[] { '.' }, 4);
            string[] result = new string[] { null, null, null, null };
            data.CopyTo(result, 0);

            return result;
        }

        private static string GetUnsplittedVersionCore(IEnumerable<string> versions)
        {
            string[] values = versions
                .Take(4)        // max 4
                .TakeWhile(v => false == String.IsNullOrEmpty(v))   // stop when no more valid data
                .ToArray();
            string result = String.Empty;
            if (values.Any())
                result = values.Aggregate((acc, v) => acc + "." + v);

            return result;
        }

        private static int?[] GetSplittedNumericVersionCore(string version)
        {
            string[] splittedVersion = GetSplittedVersionCore(version);
            int?[] result = splittedVersion
                .Select(v =>
                {
                    int? r = null;
                    int i;
                    bool success = Int32.TryParse(v, out i);
                    if (success)
                        r = i;

                    return r;
                })
                .ToArray();

            return result;
        }

        private static string GetUnsplittedNumericVersionCore(IEnumerable<int?> versions)
        {
            IEnumerable<string> values = versions
                .Select(i => i.HasValue ? "" + i : (string)null);
            string result = GetUnsplittedVersionCore(values);

            return result;
        }

        #endregion Functions

        #region Default

        private static AssemblyInfo _default;

        public static AssemblyInfo Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new AssemblyInfo()
                    {
                        Title = "Foo",
                        Product = "Bar",
                        Version = "0.1.*.9999",

                        TargetConfiguration = "*", // for all configuration by défault
                    };
                }
                return _default;
            }
        }

        #endregion Default
    }
}