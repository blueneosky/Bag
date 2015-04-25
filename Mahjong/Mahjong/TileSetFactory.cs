using Mahjong.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    internal static class TileSetFactory
    {
        private const int ConstNumbersXOffset = 124;
        private const int ConstNumbersYOffset = 46;
        private const int ConstNumbersWidth = 64;
        private const int ConstNumbersHeight = 84;
        private const int ConstNumbersXStep = (901 - ConstNumbersXOffset) / 8;
        private const int ConstNumbersYStep = 0;

        private const int ConstDotsXOffset = ConstNumbersXOffset;
        private const int ConstDotsYOffset = 141;
        private const int ConstDotsWidth = ConstNumbersWidth;
        private const int ConstDotsHeight = ConstNumbersHeight;
        private const int ConstDotsXStep = ConstNumbersXStep;
        private const int ConstDotsYStep = ConstNumbersYStep;

        private const int ConstBamboosXOffset = ConstNumbersXOffset;
        private const int ConstBamboosYOffset = 236;
        private const int ConstBamboosWidth = ConstNumbersWidth;
        private const int ConstBamboosHeight = ConstNumbersHeight;
        private const int ConstBamboosXStep = ConstNumbersXStep;
        private const int ConstBamboosYStep = ConstNumbersYStep;

        private const int ConstWindsXOffset = ConstNumbersXOffset + 1 * ConstNumbersXStep;
        private const int ConstWindsYOffset = 355;
        private const int ConstWindsWidth = ConstNumbersWidth;
        private const int ConstWindsHeight = ConstNumbersHeight;
        private const int ConstWindsXStep = ConstNumbersXStep;
        private const int ConstWindsYStep = ConstNumbersYStep;

        private const int ConstDragonsXOffset = ConstNumbersXOffset + 6 * ConstNumbersXStep;
        private const int ConstDragonsYOffset = ConstWindsYOffset;
        private const int ConstDragonsWidth = ConstNumbersWidth;
        private const int ConstDragonsHeight = ConstNumbersHeight;
        private const int ConstDragonsXStep = ConstNumbersXStep;
        private const int ConstDragonsYStep = ConstNumbersYStep;

        private static TileSet _tileSet;

        public static TileSet TileSet
        {
            get
            {
                if (_tileSet == null)
                    _tileSet = GetTileSet();
                return _tileSet;
            }
        }

        private static TileSet GetTileSet()
        {
            IEnumerable<Tile> tiles = Enumerable.Empty<Tile>()
                .Concat(GetNumbersTiles())
                .Concat(GetDotsTiles())
                .Concat(GetBamboosTiles())
                .Concat(GetWindsTiles())
                .Concat(GetDragonsTiles())
                ;

            TileSet tileSet = new TileSet(tiles);

            return tileSet;
        }

        private static IEnumerable<Tile> GetNumbersTiles()
        {
            IEnumerable<Tile> tiles = GetTiles(
                EnumTileType.NumbersOne
                , 9
                , Resources.NumbersDotsBamboosWindsDragons
                , ConstNumbersXOffset
                , ConstNumbersYOffset
                , ConstNumbersWidth
                , ConstNumbersHeight
                , ConstNumbersXStep
                , ConstNumbersYStep
                )
                .Execute();

            Debug.Assert(tiles.All(t => t.TileFamily == EnumTileFamily.Numbers));

            return tiles;
        }

        private static IEnumerable<Tile> GetDotsTiles()
        {
            IEnumerable<Tile> tiles = GetTiles(
                EnumTileType.DotsOne
                , 9
                , Resources.NumbersDotsBamboosWindsDragons
                , ConstDotsXOffset
                , ConstDotsYOffset
                , ConstDotsWidth
                , ConstDotsHeight
                , ConstDotsXStep
                , ConstDotsYStep
                )
                .Execute();

            Debug.Assert(tiles.All(t => t.TileFamily == EnumTileFamily.Dots));

            return tiles;
        }

        private static IEnumerable<Tile> GetBamboosTiles()
        {
            IEnumerable<Tile> tiles = GetTiles(
                EnumTileType.BamboosOne
                , 9
                , Resources.NumbersDotsBamboosWindsDragons
                , ConstBamboosXOffset
                , ConstBamboosYOffset
                , ConstBamboosWidth
                , ConstBamboosHeight
                , ConstBamboosXStep
                , ConstBamboosYStep
                )
                .Execute();

            Debug.Assert(tiles.All(t => t.TileFamily == EnumTileFamily.Bamboos));

            return tiles;
        }

        private static IEnumerable<Tile> GetWindsTiles()
        {
            IEnumerable<Tile> tiles = GetTiles(
                EnumTileType.WindsEast
                , 4
                , Resources.NumbersDotsBamboosWindsDragons
                , ConstWindsXOffset
                , ConstWindsYOffset
                , ConstWindsWidth
                , ConstWindsHeight
                , ConstWindsXStep
                , ConstWindsYStep
                )
                .Execute();

            Debug.Assert(tiles.All(t => t.TileFamily == EnumTileFamily.Winds));

            return tiles;
        }

        private static IEnumerable<Tile> GetDragonsTiles()
        {
            IEnumerable<Tile> tiles = GetTiles(
                EnumTileType.DragonsWhite
                , 3
                , Resources.NumbersDotsBamboosWindsDragons
                , ConstDragonsXOffset
                , ConstDragonsYOffset
                , ConstDragonsWidth
                , ConstDragonsHeight
                , ConstDragonsXStep
                , ConstDragonsYStep
                )
                .Execute();

            Debug.Assert(tiles.All(t => t.TileFamily == EnumTileFamily.Dragons));

            return tiles;
        }

        private static IEnumerable<Tile> GetTiles(EnumTileType firtTileType, int count, Image source, int xOffset, int yOffset, int width, int height, int xStep, int yStep)
        {
            for (uint i = 0; i < count; i++)
            {
                EnumTileType tileType = firtTileType + i;
                int x = xOffset + xStep * (int)i;
                int y = yOffset + yStep * (int)i;

                Tile tile = GetTiles(tileType, source, x, y, width, height);

                yield return tile;
            }
        }

        private static Tile GetTiles(EnumTileType tileType, Image source, int x, int y, int width, int height)
        {
            Image image = GetSubImage(source, x, y, width, height);
            Tile tile = new Tile(image, tileType);

            return tile;
        }

        private static Image GetSubImage(Image source, int x, int y, int width, int height)
        {
            Bitmap image = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.White);
                //g.DrawImageUnscaled(source, x, y, width, height);
                g.DrawImage(source, -x, -y);
            }

            return image;
        }
    }
}