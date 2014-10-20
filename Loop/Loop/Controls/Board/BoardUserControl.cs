using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loop.Controls.Board.Model;
using Loop.Model;

namespace Loop.Controls.Board
{
    public partial class BoardUserControl : UserControl
    {
        #region Constante

        private const int ConstDashSize = 2;
        private const int ConstDotSize = 6;
        private const int ConstDashDotZoneSize = 10;
        private const int ConstNumberZoneSize = 14;
        private const int ConstBoardMargin = 10;

        private static readonly StringFormat ConstNumberStringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        };

        private static readonly Brush ConstBackgroundBrush = Brushes.White;
        private static readonly Pen ConstBorderPen = Pens.BlueViolet;
        private static readonly Brush ConstNumberBrush = Brushes.Black;
        private static readonly Brush ConstDotBrush = Brushes.Black;
        private static readonly Brush ConstDashBrush = Brushes.DarkBlue; // Brushes.Black;

        #endregion Constante

        #region Members

        private IBoardUserControlModel _model;
        private BoardUserControlController _controller;

        #endregion Members

        #region ctor

        public BoardUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(IBoardUserControlModel model, BoardUserControlController controller)
        {
            _model = model;
            _controller = controller;

            Disposed += BoardUserControl_Disposed;

            _model.PropertyChanged += _model_PropertyChanged;

            RefreshModel();
        }

        private void BoardUserControl_Disposed(object sender, EventArgs e)
        {
            _model.PropertyChanged -= _model_PropertyChanged;
        }

        #endregion ctor

        #region Properties

        private IBoardModel _boardModel;

        public IBoardModel BoardModel
        {
            get { return _boardModel; }
            set
            {
                if (_boardModel != null) _boardModel.CellChanged += _boardModel_CellChanged;
                _boardModel = value;
                if (_boardModel != null) _boardModel.CellChanged -= _boardModel_CellChanged;
            }
        }

        #endregion Properties

        #region Model events

        private void _boardModel_CellChanged(object sender, CellChangedEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(_pictureBox.Image))
            {
                DrawCell(g, e.Line, e.Column, e.Cell);
            }
            _pictureBox.Invalidate();
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstIBoardUserControlModel.ConstPropertyBoardModel:
                    RefreshBoardModel();
                    break;

                default:
                    break;
            }
        }

        #endregion Model events

        #region Refreshes

        private void RefreshModel()
        {
            RefreshBoardModel();
        }

        private void RefreshBoardModel()
        {
            BoardModel = _model.BoardModel;

            //** initialize or change new image for board **
            // release old Image
            _pictureBox.Image = null;

            // make new Bitmap from board size
            int nbLines = 0;
            int nbColumns = 0;
            if (BoardModel != null)
            {
                nbLines = BoardModel.NbLines;
                nbColumns = BoardModel.NbColumns;
            }
            int width = BoardToScreenTopLeftCellCorner(nbColumns) + ConstBoardMargin;
            int height = BoardToScreenTopLeftCellCorner(nbLines) + ConstBoardMargin;
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Rectangle bounds = new Rectangle(0, 0, width - 1, height - 1);
                g.FillRectangle(ConstBackgroundBrush, bounds);
                g.DrawRectangle(ConstBorderPen, bounds);

                for (int i = 0; i < nbLines; i++)
                {
                    for (int j = 0; j < nbColumns; j++)
                    {
                        DrawCell(g, i, j, BoardModel[i, j]);
                    }
                }
            }
            _pictureBox.Size = Bounds.Size;
            _pictureBox.Image = bitmap;
        }

        private void DrawCell(Graphics g, int line, int column, ICell cell)
        {
            bool dotDashLine = (line % 2) == 0;
            bool dotDashColumn = (column % 2) == 0;
            if (dotDashLine && dotDashColumn)
            {
                DrawDotCell(g, line, column);
            }
            else if (dotDashLine || dotDashColumn)
            {
                DrawDashCell(g, line, column, cell);
            }
            else
            {
                DrawNumberCell(g, line, column, cell);
            }
        }

        private void DrawNumberCell(Graphics g, int line, int column, ICell cell)
        {
            int x = BoardToScreenTopLeftCellCorner(column);
            int y = BoardToScreenTopLeftCellCorner(line);
            Rectangle bounds = new Rectangle(x, y, ConstNumberZoneSize, ConstNumberZoneSize);

            g.FillRectangle(ConstBackgroundBrush, bounds);
            g.DrawString("" + cell.Value, this.Font, ConstNumberBrush, bounds, ConstNumberStringFormat);
        }

        private void DrawDashCell(Graphics g, int line, int column, ICell cell)
        {
            const int ConstDashDotZoneSize_2 = ConstDashDotZoneSize / 2;
            const int ConstDashSize_2 = ConstDashSize / 2;
            const int ConstDashDotNumberSize = ConstDashDotZoneSize + ConstNumberZoneSize;

            // vertical or horizontal
            bool dotDashLine = (line % 2) == 0;
            int lineOffset = dotDashLine ? 0 : 1;
            int columnOffset = dotDashLine ? 1 : 0;

            // line
            int x = BoardToScreenTopLeftCellCorner(column - columnOffset) + ConstDashDotZoneSize_2;
            int y = BoardToScreenTopLeftCellCorner(line - lineOffset) + ConstDashDotZoneSize_2;
            int width = columnOffset * ConstDashDotNumberSize;
            int height = lineOffset * ConstDashDotNumberSize;

            // dash (line with size greater than 1 pixel)
            x -= lineOffset * ConstDashSize_2;
            y -= columnOffset * ConstDashSize_2;
            width += lineOffset * ConstDashSize;
            height += columnOffset * ConstDashSize;

            Rectangle bounds = new Rectangle(x, y, width, height);

            if (cell.Value == 0)
            {
                // clear
                g.FillRectangle(ConstBackgroundBrush, bounds);
                DrawDotCell(g, line + lineOffset, column + columnOffset);
                DrawDotCell(g, line - lineOffset, column - columnOffset);
            }
            else
            {
                // draw dash
                g.FillRectangle(ConstDashBrush, bounds);
                DrawDotCell(g, line + lineOffset, column + columnOffset);
                DrawDotCell(g, line - lineOffset, column - columnOffset);
            }
        }

        private void DrawDotCell(Graphics g, int line, int column)
        {
            const int ConstDashDotZoneSize_2 = ConstDashDotZoneSize / 2;
            const int ConstDotSize_2 = ConstDotSize / 2;

            int x = BoardToScreenTopLeftCellCorner(column) + ConstDashDotZoneSize_2 - ConstDotSize_2;
            int y = BoardToScreenTopLeftCellCorner(line) + ConstDashDotZoneSize_2 - ConstDotSize_2;
            Rectangle bounds = new Rectangle(x, y, ConstDotSize, ConstDotSize);

            g.FillRectangle(ConstDotBrush, bounds);
        }

        private static int BoardToScreenTopLeftCellCorner(int i)
        {
            int result = ConstBoardMargin + (i / 2) * (ConstDashDotZoneSize + ConstNumberZoneSize) + (i % 2) * ConstDashDotZoneSize;
            return result;
        }

        #endregion Refreshes
    }
}