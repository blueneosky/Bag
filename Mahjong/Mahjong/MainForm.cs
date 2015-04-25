using Mahjong.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mahjong
{
    public partial class MainForm : AForm
    {
        #region Members

        private MainFormModel _model;
        private MainFormControler _controler;

        private Dictionary<RadioButton, EnumTileFamily> _tileFamilyByRadioButtons;
        private Dictionary<EnumTileFamily, RadioButton> _radioButtonByTileFamilys;

        private Dictionary<EnumTileFamily, GroupBox> _groupBoxByTileFamilys;

        private Dictionary<RadioButton, EnumTileSubNumber> _tileSubNumberByRadioButtons;
        private Dictionary<EnumTileSubNumber, List<RadioButton>> _radionButtonsByTileSubNumbers;

        #endregion Members

        #region Init

        private MainForm()
        {
            InitializeComponent();
        }

        public MainForm(MainFormModel model)
            : this(model, new MainFormControler(model))
        {
        }

        public MainForm(MainFormModel model, MainFormControler controler)
            : this()
        {
            if (model == null)
                throw new ArgumentNullException();

            _model = model;
            _controler = controler;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_model == null)
                return;

            // Tile Family radionButtons
            const int constOffsetX = 6;
            const int constOffsetY = 19;
            const int constStepY = 42 - constOffsetY;

            int x = constOffsetX;
            int y = constOffsetY;
            _tileFamilyByRadioButtons = new Dictionary<RadioButton, EnumTileFamily> { };
            _radioButtonByTileFamilys = new Dictionary<EnumTileFamily, RadioButton> { };
            foreach (EnumTileFamily tileFamily in Enum.GetValues(typeof(EnumTileFamily)))
            {
                if (tileFamily == EnumTileFamily.Base)
                    continue;

                RadioButton radioButton = new RadioButton()
                {
                    Name = tileFamily.ToString(),
                    Location = new Point(x, y),
                    Text = tileFamily.ToString(),
                };
                radioButton.CheckedChanged += TileFamilyRadioButton_CheckedChanged;
                _groupBoxTileFamily.Controls.Add(radioButton);
                _tileFamilyByRadioButtons[radioButton] = tileFamily;
                _radioButtonByTileFamilys[tileFamily] = radioButton;
                y += constStepY;
            }
            _groupBoxTileFamily.Height = y + constOffsetY;

            // Tile sub familly
            Point point = new Point(_groupBoxTileFamily.Right + 5, _groupBoxTileFamily.Top);
            Size size = _groupBoxTileFamily.Size;
            _groupBoxByTileFamilys = new Dictionary<EnumTileFamily, GroupBox> { };
            _tileSubNumberByRadioButtons = new Dictionary<RadioButton, EnumTileSubNumber> { };
            _radionButtonsByTileSubNumbers = new Dictionary<EnumTileSubNumber, List<RadioButton>> { };
            foreach (EnumTileFamily tileFamily in Enum.GetValues(typeof(EnumTileFamily)))
            {
                if (tileFamily == EnumTileFamily.None || tileFamily == EnumTileFamily.Base)
                    continue;

                GroupBox groupBox = new GroupBox()
                {
                    Text = String.Empty,
                    Location = point,
                    Size = size,
                    Visible = false,
                };
                this.Controls.Add(groupBox);
                _groupBoxByTileFamilys[tileFamily] = groupBox;

                x = constOffsetX;
                y = constOffsetY;
                List<EnumTileSubNumber> tileSubNumbers = TileSetFactory.TileSet.TileByTileSubTypeByTileFamilies[tileFamily].Keys
                    .ToList();
                if (!tileSubNumbers.Any(tsn => tsn == EnumTileSubNumber.None))
                    tileSubNumbers.Insert(0, EnumTileSubNumber.None);
                foreach (EnumTileSubNumber tileSubNumber in tileSubNumbers)
                {
                    RadioButton radioButton = new RadioButton()
                    {
                        Name = tileFamily.ToString(),
                        Location = new Point(x, y),
                        Text = tileSubNumber.ToString(),
                    };
                    radioButton.CheckedChanged += TileSubNumberRadioButton_CheckedChanged;
                    groupBox.Controls.Add(radioButton);
                    _tileSubNumberByRadioButtons[radioButton] = tileSubNumber;
                    List<RadioButton> radioButtons;
                    if (!_radionButtonsByTileSubNumbers.TryGetValue(tileSubNumber, out radioButtons))
                    {
                        radioButtons = new List<RadioButton> { };
                        _radionButtonsByTileSubNumbers[tileSubNumber] = radioButtons;
                    }
                    radioButtons.Add(radioButton);
                    y += constStepY;
                }
                groupBox.Height = y + constOffsetY;
            }

            // connect model
            MainFormModel model = _model;
            _model = null;
            Model = model;
        }

        #endregion Init

        #region Refresh

        private void RefreshUIState()
        {
            BeginUpdate();

            EnumTileType tileType = EnumTileType.None;
            if (_model != null)
                tileType = _model.CurrentTile;
            _buttonStartOrNext.Text = tileType == EnumTileType.None
                ? "Sart"
                : "Next";
            bool isGBEnable = tileType != EnumTileType.None;
            _groupBoxTileFamily.Enabled = isGBEnable;
            _groupBoxByTileFamilys.Values.Foreach(gb => gb.Enabled = isGBEnable);

            EndUpdate();
        }

        private void RefreshPictureBox()
        {
            BeginUpdate();

            EnumTileType tileType = EnumTileType.None;
            if (_model != null)
                tileType = _model.CurrentTile;
            Image image = null;
            Tile tile;
            if (TileSetFactory.TileSet.TileByTileTypes.TryGetValue(tileType, out tile))
                image = tile.Image;
            _pictureBox.Image = image;

            EndUpdate();
        }

        private void RefreshGroupBoxTileFamily()
        {
            BeginUpdate();

            EnumTileFamily tileFamily = EnumTileFamily.None;
            if (_model != null)
                tileFamily = _model.SelectedTileFamily;

            RadioButton radioButton;
            if (_radioButtonByTileFamilys.TryGetValue(tileFamily, out radioButton))
                radioButton.Checked = true;

            EndUpdate();
        }

        private void RefreshGroupBoxSubFamily()
        {
            BeginUpdate();

            EnumTileFamily tileFamily = EnumTileFamily.None;
            if (_model != null)
                tileFamily = _model.SelectedTileFamily;

            foreach (var gb in _groupBoxByTileFamilys.Values)
            {
                gb.Visible = false;
            }
            GroupBox groupBox;
            if (_groupBoxByTileFamilys.TryGetValue(tileFamily, out groupBox))
                groupBox.Visible = true;

            EndUpdate();
        }

        private void RefreshRadioButtonTileSubNumber()
        {
            BeginUpdate();

            EnumTileSubNumber tileSubNumber = EnumTileSubNumber.None;
            if (_model != null)
                tileSubNumber = _model.SelectedTileSubNumber;
            List<RadioButton> radioButtons;
            if (_radionButtonsByTileSubNumbers.TryGetValue(tileSubNumber, out radioButtons))
            {
                foreach (var radioButton in radioButtons)
                {
                    radioButton.Checked = true;
                }
            }

            EndUpdate();
        }

        private void RefresPictureBoxResult()
        {
            bool? result = null;
            if (_model != null)
                result = _model.Result;

            Image image = result == null
                ? null
                : result.Value
                ? Resources.ok
                : Resources.no;
            _pictureBoxResult.Image = image;
        }

        #endregion Refresh

        #region Model

        public MainFormModel Model
        {
            get { return _model; }
            set
            {
                if (_model != null)
                    _model.PropertyChanged -= _model_PropertyChanged;
                _model = value;
                if (_model != null)
                    _model.PropertyChanged += _model_PropertyChanged;
                UpdateModel();
            }
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            EnsureSync(() =>
            {
                switch (e.PropertyName)
                {
                    case MainFormModel.ConstPropertyNameCurrentTile:
                        UpdateCurrentTile();
                        break;

                    case MainFormModel.ConstPropertyNameSelectedTileFamily:
                        UpdateSelectedTileFamily();
                        break;

                    case MainFormModel.ConstPropertyNameSelectedTileSubNumber:
                        UpdateSelectedTileSubNumber();
                        break;

                    case MainFormModel.ConstPropertyNameResult:
                        UpdateResult();
                        break;
                }
            });
        }

        private void UpdateModel()
        {
            BeginUpdate();

            UpdateCurrentTile();
            UpdateSelectedTileFamily();
            UpdateSelectedTileSubNumber();
            UpdateResult();

            EndUpdate();
        }

        private void UpdateCurrentTile()
        {
            RefreshUIState();
            RefreshPictureBox();
        }

        private void UpdateSelectedTileFamily()
        {
            BeginUpdate();

            RefreshGroupBoxTileFamily();
            RefreshGroupBoxSubFamily();

            EndUpdate();
        }

        private void UpdateSelectedTileSubNumber()
        {
            RefreshRadioButtonTileSubNumber();
        }

        private void UpdateResult()
        {
            RefreshUIState();
            RefresPictureBoxResult();
        }

        #endregion Model

        #region User input

        private void TileFamilyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            EnumTileFamily tileFamily;
            RadioButton radioButton = sender as RadioButton;
            if (!radioButton.Checked)
                return;
            if (!_tileFamilyByRadioButtons.TryGetValue(radioButton, out tileFamily))
                tileFamily = EnumTileFamily.None;

            _controler.SetSelectedTileFamily(tileFamily);
        }

        private void TileSubNumberRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            EnumTileSubNumber tileSubNumber;
            RadioButton radioButton = sender as RadioButton;
            if (!radioButton.Checked)
                return;
            if (!_tileSubNumberByRadioButtons.TryGetValue(radioButton, out tileSubNumber))
                tileSubNumber = EnumTileSubNumber.None;

            _controler.SetSelectedTileSubNumberAndCheck(tileSubNumber);
        }

        private void _buttonStartOrNext_Click(object sender, EventArgs e)
        {
            _controler.Next();
        }

        #endregion User input
    }
}