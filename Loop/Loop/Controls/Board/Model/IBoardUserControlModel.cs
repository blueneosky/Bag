using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Loop.Model;

namespace Loop.Controls.Board.Model
{
    public interface IBoardUserControlModel : INotifyPropertyChanged
    {
        IBoardModel BoardModel { get; }
    }
}