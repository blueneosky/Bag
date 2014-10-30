using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loop.Common.Extension;
using Loop.Controls.Board.Model;
using Loop.Model;

namespace Loop.Controls.Board
{
    public partial class BoardUserControl : UserControl
    {
        #region Constante

        private const int ConstDotZoneSize = 10;
        private const int ConstNumberZoneSize = 14;
        private const int ConstBoardMargin = 10;

        private static readonly StringFormat ConstNumberStringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        };

        private static readonly Brush ConstBackgroundBrush = Brushes.White;
        private static readonly Pen ConstBackgroundPen = new Pen(ConstBackgroundBrush);
        private static readonly Pen ConstBorderPen = Pens.BlueViolet;
        private static readonly Brush ConstNumberBrush = Brushes.Black;
        private static readonly Brush ConstDotBrush = Brushes.Black;
        private static readonly Brush ConstDashBrush = Brushes.DarkBlue; // Brushes.Black;
        private static readonly Pen ConstCrossPen = Pens.Red;

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
            return;
            int x = BoardToScreenTopLeftCellCorner(column);
            int y = BoardToScreenTopLeftCellCorner(line);
            Rectangle bounds = new Rectangle(x, y, ConstNumberZoneSize, ConstNumberZoneSize);

            g.FillRectangle(ConstBackgroundBrush, bounds);
            int? number = cell.Value;
            if (number.HasValue)
                g.DrawString("" + number, this.Font, ConstNumberBrush, bounds, ConstNumberStringFormat);
        }

        private void DrawDashCell(Graphics g, int line, int column, ICell cell)
        {
            const int ConstDashSize = 3;
            const int ConstCrossSize = 7;
            const int ConstDotZoneSize_2 = ConstDotZoneSize / 2;
            const int ConstCrossSize_2 = ConstCrossSize / 2;
            const int ConstDashDotNumberSize = ConstDotZoneSize + ConstNumberZoneSize;
            const int ConstNumberZoneSize_2 = ConstNumberZoneSize / 2;

            // vertical or horizontal
            bool dotDashLine = (line % 2) == 0;
            int lineOffset = dotDashLine ? 0 : 1;
            int columnOffset = dotDashLine ? 1 : 0;

            // line
            int centerX = BoardToScreenTopLeftCellCorner(column) + (dotDashLine ? ConstNumberZoneSize_2 : ConstDotZoneSize_2);
            int centerY = BoardToScreenTopLeftCellCorner(line) + (dotDashLine ? ConstDotZoneSize_2 : ConstNumberZoneSize_2);

            // always clear first
            int dashWidth = dotDashLine ? ConstDashDotNumberSize : ConstDashSize;
            int dashHeight = dotDashLine ? ConstDashSize : ConstDashDotNumberSize;
            g.FillCenteredRectangle(ConstBackgroundBrush, centerX, centerY, dashWidth, dashHeight);
            int crossX1 = centerX - ConstCrossSize_2;
            int crossX2 = centerX + ConstCrossSize_2;
            int crossY1 = centerY - ConstCrossSize_2;
            int crossY2 = centerY + ConstCrossSize_2;
            g.DrawLine(ConstBackgroundPen, crossX1, crossY1, crossX2, crossY2);
            g.DrawLine(ConstBackgroundPen, crossX2, crossY1, crossX1, crossY2);
            g.DrawLine(ConstBackgroundPen, crossX1, crossY1 + 1, crossX2, crossY2 + 1);
            g.DrawLine(ConstBackgroundPen, crossX2, crossY1 + 1, crossX1, crossY2 + 1);

            switch ((EnumDash)(cell.Value ?? 0))
            {
                case EnumDash.Dash:
                    // draw dash
                    g.FillCenteredRectangle(ConstDashBrush, centerX, centerY, dashWidth, dashHeight);
                    break;

                case EnumDash.Cross:
                    // draw cross
                    g.DrawLine(ConstCrossPen, crossX1, crossY1, crossX2, crossY2);
                    g.DrawLine(ConstCrossPen, crossX2, crossY1, crossX1, crossY2);
                    g.DrawLine(ConstCrossPen, crossX1, crossY1 + 1, crossX2, crossY2 + 1);
                    g.DrawLine(ConstCrossPen, crossX2, crossY1 + 1, crossX1, crossY2 + 1);

                    break;

                case EnumDash.Empty:
                default:
                    // clear - nothing
                    break;
            }

            // always refresh corresponding dot
            DrawDotCell(g, line + lineOffset, column + columnOffset);
            DrawDotCell(g, line - lineOffset, column - columnOffset);
        }

        private void DrawDotCell(Graphics g, int line, int column)
        {
            const int ConstDashDotZoneSize_2 = ConstDotZoneSize / 2;

            int centerX = BoardToScreenTopLeftCellCorner(column) + ConstDashDotZoneSize_2;
            int centerY = BoardToScreenTopLeftCellCorner(line) + ConstDashDotZoneSize_2;
            Point center = new Point(centerX, centerY);

            Size sizeV = new System.Drawing.Size(3, 7);
            Size sizeH = new System.Drawing.Size(7, 3);
            Size sizeC = new System.Drawing.Size(5, 5);

            g.FillCenteredRectangle(ConstDotBrush, center, sizeV);
            g.FillCenteredRectangle(ConstDotBrush, center, sizeH);
            g.FillCenteredRectangle(ConstDotBrush, center, sizeC);
        }

        private static int BoardToScreenTopLeftCellCorner(int i)
        {
            int result = ConstBoardMargin + (i / 2) * (ConstDotZoneSize + ConstNumberZoneSize) + (i % 2) * ConstDotZoneSize;
            return result;
        }

        #endregion Refreshes
    }
}