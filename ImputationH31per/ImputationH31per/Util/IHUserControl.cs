using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ImputationH31per.Util
{
    public class IHUserControl : UserControl
    {
        private const int ConstanteValeurMaximumCompteurMiseAJour = 20; // arbitraire
        private int _compteurMiseAJour;

        public IHUserControl()
        {
            //InitializeComponent();

            _compteurMiseAJour = 0;
        }

        protected bool EstMiseAJourEnCours
        {
            get { return _compteurMiseAJour > 0; }
        }

        protected void CommencerMiseAJour()
        {
            _compteurMiseAJour++;
            Debug.Assert(_compteurMiseAJour <= ConstanteValeurMaximumCompteurMiseAJour);
        }

        protected void TerminerMiseAJour()
        {
            _compteurMiseAJour--;
            Debug.Assert(_compteurMiseAJour >= 0);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // IHUserControl
            //
            this.Name = "IHUserControl";
            this.Size = new System.Drawing.Size(180, 163);
            this.ResumeLayout(false);
        }
    }
}