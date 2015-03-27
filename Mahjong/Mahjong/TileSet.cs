using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    internal class TileSet : IDisposable
    {
        private IReadOnlyDictionary<EnumTileType, Tile> _tileByTileTypes;
        private IReadOnlyDictionary<EnumTileFamily, IReadOnlyDictionary<EnumTileSubNumber, Tile>> _tileByTileSubTypeByTileFamilies;

        public TileSet(IEnumerable<Tile> tiles)
        {
            tiles = tiles.Execute();

            _tileByTileTypes = tiles.ToReadOnlyDictionary(t => t.TileType);
            _tileByTileSubTypeByTileFamilies = tiles
                .GroupBy(t => t.TileFamily)
                .ToReadOnlyDictionary(
                    grp => grp.Key
                    , grp => grp.ToReadOnlyDictionary(t => t.TileSubNumber)
                );
        }

        public IReadOnlyDictionary<EnumTileType, Tile> TileByTileTypes
        {
            get { return _tileByTileTypes; }
        }

        public IReadOnlyDictionary<EnumTileFamily, IReadOnlyDictionary<EnumTileSubNumber, Tile>> TileByTileSubTypeByTileFamilies
        {
            get { return _tileByTileSubTypeByTileFamilies; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposed)
        {
            if (_isDisposed)
                return;
            _isDisposed = true;

            if (_tileByTileTypes != null)
            {
                _tileByTileTypes.Values
                    .Foreach(t => t.Dispose());
                _tileByTileTypes = null;
            }

            if (_tileByTileSubTypeByTileFamilies != null)
            {
                _tileByTileSubTypeByTileFamilies.Values
                    .SelectMany(v => v.Values)
                    .Foreach(t => t.Dispose());
                _tileByTileSubTypeByTileFamilies = null;
            }
        }

        #endregion IDisposable Members
    }
}