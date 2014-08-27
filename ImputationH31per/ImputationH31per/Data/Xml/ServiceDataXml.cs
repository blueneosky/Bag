using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using ImputationH31per.Data.Entite;
using ImputationH31per.Util;

using VNamespace = ImputationH31per.Data.Xml.V1_0b;

namespace ImputationH31per.Data.Xml
{
    public class ServiceDataXml : IServiceData
    {
        private const string ConstanteVersionCourante = DataRootXml.ConstanteVersionCourante;

        private readonly string _cheminFichier;

        public ServiceDataXml(string cheminFichier)
        {
            _cheminFichier = cheminFichier;
        }

        public string CheminFichierData
        {
            get { return _cheminFichier; }
        }

        public string Nom { get; set; }

        public void EnregistrerData(IDatData<IDatTacheTfs, IDatIHFormParametre> data)
        {
            string chemin = CheminFichierData;

            string cheminBase = Path.GetDirectoryName(chemin);
            Directory.CreateDirectory(cheminBase);

            VNamespace.DatDataXml dataXml = data as VNamespace.DatDataXml;
            if (dataXml == null)
            {
                dataXml = new VNamespace.DatDataXml(data);
            }
            Type type = typeof(DataRootXml);
            DataRootXml dataRootXml = new DataRootXml(ConstanteVersionCourante, dataXml);

            try
            {
                using (XmlTextWriter writer = new XmlTextWriter(chemin, null))
                {
#if DEBUG
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
#endif

                    XmlSerializer serializer = new XmlSerializer(type);
                    serializer.Serialize(writer, dataRootXml);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                string message = "Erreur durant l'enregistrement dees données (" + e.Message + ")";
                throw new IHException(message, e);
            }
        }

        public IDatData<IDatTacheTfs, IDatIHFormParametre> ObtenirData()
        {
            IDatData<IDatTacheTfs, IDatIHFormParametre> resultat = null;

            string chemin = CheminFichierData;

            try
            {
                using (StreamReader file = new StreamReader(chemin))
                {
                    Type type = typeof(DataRootXml);
                    DataRootXml dataRootXml;
                    VNamespace.DatDataXml data = null;

                    XmlSerializer reader = new XmlSerializer(type);
                    dataRootXml = reader.Deserialize(file) as DataRootXml;

                    if (dataRootXml != null)
                    {
                        dataRootXml = ServiceConversion.Convertir(dataRootXml);
                        data = dataRootXml.Data1_0b;
                    }

                    if (data != null)
                    {
                        // post traitement
                        if (data.TacheTfss.Any())
                        {
                            foreach (VNamespace.DatTacheTfsXml tacheTfs in data.TacheTfss)
                            {
                                foreach (VNamespace.DatTicketTfsXml ticketTfs in tacheTfs.TicketTfss)
                                {
                                    ticketTfs.TacheTfs = tacheTfs;

                                    foreach (VNamespace.DatImputationTfsXml imputationTfs in ticketTfs.XmlImputationTfss)
                                    {
                                        imputationTfs.TicketTfs = ticketTfs;
                                    }
                                }
                            }
                        }

                        resultat = data;
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            if (resultat == null)
            {
                resultat = new DatData();
            }

            return resultat;
        }
    }
}