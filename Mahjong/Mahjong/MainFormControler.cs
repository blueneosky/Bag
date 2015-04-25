using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    public class MainFormControler
    {
        private readonly MainFormModel _model;
        private readonly Random _random;

        public MainFormControler(MainFormModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            _model = model;
            _random = new Random();
        }

        // TODO

        public void SetSelectedTileFamily(EnumTileFamily tileFamily)
        {
            _model.SelectedTileFamily = tileFamily;
            _model.SelectedTileSubNumber = EnumTileSubNumber.None;
            _model.Result = null;
        }

        public void SetSelectedTileSubNumber(EnumTileSubNumber tileSubNumber)
        {
            _model.SelectedTileSubNumber = tileSubNumber;
        }

        public void SetSelectedTileSubNumberAndCheck(EnumTileSubNumber tileSubNumber)
        {
            SetSelectedTileSubNumber(tileSubNumber);
            Check();
        }

        public void Check()
        {
            EnumTileFamily tileFamily = _model.SelectedTileFamily;
            if (tileFamily == EnumTileFamily.None)
            {
                _model.Result = null;
                return;
            }

            EnumTileSubNumber tileSubNumber = _model.SelectedTileSubNumber;
            if (tileSubNumber == EnumTileSubNumber.None)
            {
                _model.Result = null;
                return;
            }

            EnumTileType tileType = (EnumTileType)((int)tileFamily + (int)tileSubNumber);
            bool result = tileType == _model.CurrentTile;

            _model.Result = result;
        }

        public void Next()
        {
            EnumTileType[] tileTypes = TileSetFactory.TileSet.TileByTileTypes.Values
                .Where(t => t.TileFamily == EnumTileFamily.Dragons || t.TileFamily == EnumTileFamily.Winds || t.TileFamily == EnumTileFamily.Numbers)
                .Select(t => t.TileType)
                .ToArray();
            int count = tileTypes.Length;
            EnumTileType current = _model.CurrentTile;
            int indexCurrent = tileTypes.TakeWhile(tt => tt != current).Count();
            int index = indexCurrent;
            while (index == indexCurrent)
            {
                index = _random.Next(count);
            }
            _model.Result = null;
            _model.CurrentTile = tileTypes[index];
            _model.SelectedTileFamily = EnumTileFamily.None;
            _model.SelectedTileSubNumber = EnumTileSubNumber.None;
        }
    }
}