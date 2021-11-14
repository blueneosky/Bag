using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Mahjong
{
    public class MainFormModel : INotifyPropertyChanged
    {
        private const string ConstPrefixPropertyName = "MainFormModel_";
        public const string ConstPropertyNameCurrentTile = ConstPrefixPropertyName + "CurrentTile";
        public const string ConstPropertyNameSelectedTileFamily = ConstPrefixPropertyName + "SelectedTileFamily";
        public const string ConstPropertyNameSelectedTileSubNumber = ConstPrefixPropertyName + "SelectedTileSubNumber";
        public const string ConstPropertyNameResult = ConstPrefixPropertyName + "Result";

        private EnumTileType _currentTile;
        private EnumTileFamily _selectedTileFamily;
        private EnumTileSubNumber _selectedTileSubNumber;
        private bool? _result;

        public MainFormModel()
        {
            // load tiles images
            if (TileSetFactory.TileSet == null)
                throw new NullReferenceException();

            CurrentTile = EnumTileType.None;
            SelectedTileFamily = EnumTileFamily.None;
            SelectedTileSubNumber = EnumTileSubNumber.None;
            Result = null;
        }


        public EnumTileType CurrentTile
        {
            get { return _currentTile; }
            set
            {
                if (_currentTile == value)
                    return;
                _currentTile = value;

                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstPropertyNameCurrentTile));
            }
        }


        public EnumTileFamily SelectedTileFamily
        {
            get { return _selectedTileFamily; }
            set
            {
                if (_selectedTileFamily == value)
                    return;
                _selectedTileFamily = value;

                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstPropertyNameSelectedTileFamily));
            }
        }

        public EnumTileSubNumber SelectedTileSubNumber
        {
            get { return _selectedTileSubNumber; }
            set
            {
                if (_selectedTileSubNumber == value)
                    return;
                _selectedTileSubNumber = value;

                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstPropertyNameSelectedTileSubNumber));
            }
        }


        public bool? Result
        {
            get { return _result; }
            set
            {
                if (_result == value)
                    return;
                _result = value;

                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstPropertyNameResult));
            }
        }


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion INotifyPropertyChanged
    }
}