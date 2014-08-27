using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using ImputationH31per.Data.Entite;

namespace ImputationH31per.Data.Xml.V1_0b
{
    [Serializable]
    [XmlType("bFP")]
    public class DatIHFormParametreXml : IDatIHFormParametre
    {
        public DatIHFormParametreXml(IDatIHFormParametre ihFormParametre)
        {
            this.Nom = ihFormParametre.Nom;
            this.EstAgrandi = ihFormParametre.EstAgrandi;
            this.Localisation = ihFormParametre.Localisation;
            this.Taille = ihFormParametre.Taille;
        }

        public DatIHFormParametreXml(V1_0a.DatIHFormParametreXml oldIhFormParametre)
        {
            this.Nom = oldIhFormParametre.Nom;
            this.EstAgrandi = oldIhFormParametre.EstAgrandi;
            this.Localisation = oldIhFormParametre.Localisation;
            this.Taille = oldIhFormParametre.Taille;
        }

        /// <summary>
        /// Pour serialization
        /// </summary>
        private DatIHFormParametreXml()
        {
        }

        #region Xml

        [XmlAttribute("EAg")]
        public int XmlEstAgrandi { get; set; }

        [XmlAttribute("X")]
        public int XmlLocalisationX { get; set; }

        [XmlAttribute("Y")]
        public int XmlLocalisationY { get; set; }

        [XmlAttribute("Nom")]
        public string XmlNom { get; set; }

        [XmlAttribute("H")]
        public int XmlTailleHauteur { get; set; }

        [XmlAttribute("W")]
        public int XmlTailleLargeur { get; set; }

        #endregion Xml

        #region Properties

        #region Propriétés scalaire

        [XmlIgnore]
        public bool EstAgrandi
        {
            get { return XmlEstAgrandi == 1; }
            set { XmlEstAgrandi = value ? 1 : 0; }
        }

        [XmlIgnore]
        public Point Localisation
        {
            get { return new Point(XmlLocalisationX, XmlLocalisationY); }
            set
            {
                XmlLocalisationX = value.X;
                XmlLocalisationY = value.Y;
            }
        }

        [XmlIgnore]
        public string Nom
        {
            get { return XmlNom; }
            set { XmlNom = value; }
        }

        [XmlIgnore]
        public Size Taille
        {
            get { return new Size(XmlTailleLargeur, XmlTailleHauteur); }
            set
            {
                XmlTailleLargeur = value.Width;
                XmlTailleHauteur = value.Height;
            }
        }

        #endregion Propriétés scalaire

        #endregion Properties
    }
}