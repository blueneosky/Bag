using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegexpConv
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private List<Couple> _formats;

        private List<Couple> Formats
        {
            get { return _formats; }
            set
            {
                _formats = value;
                BeginUpdate();
                SelectedFormat = null;
                RefreshFormatsListView();
                EndUpdate();
            }
        }

        private Couple _selectedFormat;

        public Couple SelectedFormat
        {
            get { return _selectedFormat; }
            set
            {
                _selectedFormat = value;
                BeginUpdate();
                RefreshFormatsListViewSelectedIndex();
                Convert();
                EndUpdate();
            }
        }

        private string _sourceText;

        private string SourceText
        {
            get { return _sourceText; }
            set
            {
                _sourceText = value;
                Convert();
            }
        }

        #region Override

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                Settings.Load();
            }
            catch (Exception)
            {
                // Note : must not interfere during form loading
            }
            Formats = Settings.Default.Formats.ToList();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Settings.Default.Formats = Formats.ToArray();
            try
            {
                Settings.Save();
            }
            catch (Exception)
            {
                // Note : must not interfere during form loading
            }

            base.OnFormClosed(e);
        }

        #endregion Override

        #region User input

        private void _sourceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            SourceText = _sourceTextBox.Text;
        }

        private void _formatsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            SelectedFormat = _formatsListView.SelectedItems
                .OfType<ListViewItemCouple>()
                .Select(lvi => lvi.Couple)
                .FirstOrDefault();
        }

        private void _addRegexButton_Click(object sender, EventArgs e)
        {
            using (NewFormatForm form = new NewFormatForm(this))
            {
                DialogResult dr = form.ShowDialog();
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                Couple format = new Couple(form.Exp1, form.Exp2);
                Formats.Add(format);
                Formats = Formats;  // refresh - fast way...
                SelectedFormat = format;
            }
        }

        private void _formatsListView_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsUpdating)
                return;

            if (e.KeyCode == Keys.Delete)
            {
                Couple format = SelectedFormat;
                if (format != null)
                {
                    SelectedFormat = null;
                    Formats.Remove(format);
                    Formats = Formats;  // refresh - fast way...
                }
            }
        }

        #endregion User input

        #region Refresh

        private int _updateCounter;

        private bool IsUpdating { get { return _updateCounter > 0; } }

        private void BeginUpdate()
        {
            _updateCounter++;
        }

        private void EndUpdate()
        {
            _updateCounter--;
            if (_updateCounter < 0)
                _updateCounter = 0;
        }

        private void RefreshFormatsListView()
        {
            BeginUpdate();
            _formatsListView.BeginUpdate();

            var items = Formats
                .Select(f => new ListViewItemCouple(f))
                .ToArray();
            _formatsListView.Items.Clear();
            _formatsListView.Items.AddRange(items);

            _formatsListView.EndUpdate();

            RefreshFormatsListViewSelectedIndex();
            EndUpdate();
        }

        private void RefreshFormatsListViewSelectedIndex()
        {
            BeginUpdate();
            _formatsListView.BeginUpdate();

            _formatsListView.SelectedIndices.Clear();
            int count = _formatsListView.Items
                .OfType<ListViewItemCouple>()
                .TakeWhile(lvi => lvi.Couple == SelectedFormat)
                .Count();
            int index = count - 1;
            if (index >= 0 && index < _formatsListView.Items.Count)
                _formatsListView.SelectedIndices.Add(index);

            _formatsListView.EndUpdate();
            EndUpdate();
        }

        #endregion Refresh

        #region Convert

        private void Convert()
        {
            _destTextBox.Text = String.Empty;

            if (SelectedFormat == null)
                return;

            string expDate1 = @"\d";
            string expDate2 = @"\d/\d\d";

            string expFormat1 = SelectedFormat.Item1;
            string expFormat2 = SelectedFormat.Item2;
            string expRegex = GetExpRegExp(expFormat1, expFormat2);
            Regex regex = new Regex(expRegex);

            string sourceText = SourceText ?? String.Empty;
            if (String.IsNullOrEmpty(sourceText))
                return;

            MatchCollection matchCollection = regex.Matches(sourceText);
            IEnumerable<Match> matches = matchCollection
                .OfType<Match>();
            if (false == matches.Any())
                return;

            string destText = matches
                .Select(m => m.Value.Trim())
                .Aggregate((acc, t) => acc + Environment.NewLine + t);
            _destTextBox.Text = destText;
        }

        public static string GetExpRegExp(string expFormat1, string expFormat2)
        {
            string expFormat = expFormat1 + expFormat2;
            string expFormat1PasSuiviDeFormat2 = expFormat1 + (@"(?!" + expFormat2 + @")");
            string expCaracterePasFormat = @"(?(" + expFormat1 + @")" + expFormat1PasSuiviDeFormat2 + @"|" + @"." + @")";
            string expFormatEtReste = expFormat + expCaracterePasFormat + "*";
            string expRegex = @"(" + expFormatEtReste + @")";

            return expRegex;
        }

        #endregion Convert

        #region ListViewItemCouple

        private class ListViewItemCouple : ListViewItem
        {
            private readonly Couple _couple;

            public ListViewItemCouple(Couple couple)
                : base(couple.Item1)
            {
                _couple = couple;
                SubItems.Add(couple.Item2);
            }

            public Couple Couple
            {
                get { return _couple; }
            }
        }

        #endregion ListViewItemCouple
    }
}