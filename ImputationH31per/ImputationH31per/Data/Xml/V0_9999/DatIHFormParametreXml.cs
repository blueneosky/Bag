using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using ImputationH31per.Data.Entite;

namespace ImputationH31per.Data.Xml.V0_9999
{
    [Serializable]
    public class DatIHFormParametreXml
    {
        // Neutralisé
        //public DatIHFormParametreXml(IDatIHFormParametre ihFormParametre)
        //{
        //    this.Nom = ihFormParametre.Nom;
        //    this.EstAgrandi = ihFormParametre.EstAgrandi;
        //    this.Localisation = ihFormParametre.Localisation;
        //    this.Taille = ihFormParametre.Taille;
        //}

        /// <summary>
        /// Pour serialization
        /// </summary>
        private DatIHFormParametreXml()
        {
        }

        #region Propriétés scalaire

        public bool EstAgrandi { get; set; }

        public Point Localisation { get; set; }

        [XmlAttribute("Nom")]
        public string Nom { get; set; }

        public Size Taille { get; set; }

        #endregion Propriétés scalaire
    }
}