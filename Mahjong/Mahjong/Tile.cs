using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    class Tile : IDisposable
    {
        private Image _image;
        private readonly EnumTileFamily _tileFamily;
        private readonly EnumTileSubNumber _tileSubNumber;

        public Tile(Image image, EnumTileFamily tileFamily, EnumTileSubNumber tileSubNumber)
        {
            _image = image;
            _tileFamily = tileFamily;
            _tileSubNumber = tileSubNumber;
        }

        public Tile(Image image, EnumTileType tileType)
            : this(image, (EnumTileFamily)((uint)tileType - ((uint)tileType % (uint)EnumTileFamily.Base)), (EnumTileSubNumber)((uint)tileType % (uint)EnumTileFamily.Base))
        {
        }

        public Image Image
        {
            get { return _image; }
        }


        public EnumTileFamily TileFamily
        {
            get { return _tileFamily; }
        }


        public EnumTileSubNumber TileSubNumber
        {
            get { return _tileSubNumber; }
        }

        public EnumTileType TileType
        {
            get { return (EnumTileType)((uint)_tileFamily + (uint)_tileSubNumber); }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        bool _isDisposed = false;

        protected virtual void Dispose(bool disposed)
        {
            if (_isDisposed)
                return;

            if (_image != null)
            {
                _image.Dispose();
                _image = null;
            }
        }

        #endregion
    }
}
