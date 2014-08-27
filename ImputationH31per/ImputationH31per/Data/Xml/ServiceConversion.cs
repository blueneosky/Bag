using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ImputationH31per.Data.Xml
{
    public static class ServiceConversion
    {
        public static DataRootXml Convertir(DataRootXml dataRoot)
        {
            if (dataRoot == null)
                return dataRoot;

            bool estAConvertir = true;

            while (estAConvertir)
            {
                switch (dataRoot.Version)
                {
                    case DataRootXml.ConstanteVersion0_9999:
                        dataRoot = Convertir0_999Vers1_0a(dataRoot);
                        break;

                    case DataRootXml.ConstanteVersion1_0a:
                        dataRoot = Convertir1_0aVers1_1a(dataRoot);
                        break;

                    case DataRootXml.ConstanteVersion1_0b:
                        // dernière version : pas de conversion
                        estAConvertir = false;
                        break;

                    default:
                        break;
                }
            }

            return dataRoot;
        }

        public static DataRootXml Convertir0_999Vers1_0a(DataRootXml dataRoot)
        {
            V0_9999.DatDataXml oldData = dataRoot.Data0_9999;

            DataRootXml resultat;

            try
            {
                // pas de changement fondamental
                V1_0a.DatDataXml newData = new V1_0a.DatDataXml(oldData);

                resultat = new DataRootXml(DataRootXml.ConstanteVersion1_0a, newData);

                Trace.WriteLine("Conversion de 0_9999 vers 1_0a réussie");
            }
            catch (Exception e)
            {
                resultat = null;
                Trace.WriteLine(e);
            }

            return resultat;
        }

        private static DataRootXml Convertir1_0aVers1_1a(DataRootXml dataRoot)
        {
            V1_0a.DatDataXml oldData = dataRoot.Data1_0a;

            DataRootXml resultat;

            try
            {
                // pas de changement fondamental
                V1_0b.DatDataXml newData = new V1_0b.DatDataXml(oldData);

                resultat = new DataRootXml(DataRootXml.ConstanteVersion1_0b, newData);

                Trace.WriteLine("Conversion de 1_0a vers 1_0b réussie");
            }
            catch (Exception e)
            {
                resultat = null;
                Trace.WriteLine(e);
            }

            return resultat;
        }
    }
}
