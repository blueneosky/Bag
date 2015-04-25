using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    public enum EnumTileType : uint
    {
        None = 0,

        NumbersOne = EnumTileFamily.Numbers + EnumTileSubNumber.One,
        NumbersTwo = EnumTileFamily.Numbers + EnumTileSubNumber.Two,
        NumbersThree = EnumTileFamily.Numbers + EnumTileSubNumber.Three,
        NumbersFour = EnumTileFamily.Numbers + EnumTileSubNumber.Four,
        NumbersFive = EnumTileFamily.Numbers + EnumTileSubNumber.Five,
        NumbersSix = EnumTileFamily.Numbers + EnumTileSubNumber.Six,
        NumbersSeven = EnumTileFamily.Numbers + EnumTileSubNumber.Seven,
        NumbersEight = EnumTileFamily.Numbers + EnumTileSubNumber.Eight,
        NumbersNine = EnumTileFamily.Numbers + EnumTileSubNumber.Nine,

        DotsOne = EnumTileFamily.Dots + EnumTileSubNumber.One,
        DotsTwo = EnumTileFamily.Dots + EnumTileSubNumber.Two,
        DotsThree = EnumTileFamily.Dots + EnumTileSubNumber.Three,
        DotsFour = EnumTileFamily.Dots + EnumTileSubNumber.Four,
        DotsFive = EnumTileFamily.Dots + EnumTileSubNumber.Five,
        DotsSix = EnumTileFamily.Dots + EnumTileSubNumber.Six,
        DotsSeven = EnumTileFamily.Dots + EnumTileSubNumber.Seven,
        DotsEight = EnumTileFamily.Dots + EnumTileSubNumber.Eight,
        DotsNine = EnumTileFamily.Dots + EnumTileSubNumber.Nine,

        BamboosOne = EnumTileFamily.Bamboos + EnumTileSubNumber.One,
        BamboosTwo = EnumTileFamily.Bamboos + EnumTileSubNumber.Two,
        BamboosThree = EnumTileFamily.Bamboos + EnumTileSubNumber.Three,
        BamboosFour = EnumTileFamily.Bamboos + EnumTileSubNumber.Four,
        BamboosFive = EnumTileFamily.Bamboos + EnumTileSubNumber.Five,
        BamboosSix = EnumTileFamily.Bamboos + EnumTileSubNumber.Six,
        BamboosSeven = EnumTileFamily.Bamboos + EnumTileSubNumber.Seven,
        BamboosEight = EnumTileFamily.Bamboos + EnumTileSubNumber.Eight,
        BamboosNine = EnumTileFamily.Bamboos + EnumTileSubNumber.Nine,

        WindsEast = EnumTileFamily.Winds + EnumTileSubNumber.East,
        WindsSouth = EnumTileFamily.Winds + EnumTileSubNumber.South,
        WindsWest = EnumTileFamily.Winds + EnumTileSubNumber.West,
        WindsNorth = EnumTileFamily.Winds + EnumTileSubNumber.North,

        DragonsWhite = EnumTileFamily.Dragons + EnumTileSubNumber.White,
        DragonsGreen = EnumTileFamily.Dragons + EnumTileSubNumber.Green,
        DragonsRed = EnumTileFamily.Dragons + EnumTileSubNumber.Red,
    }
}
