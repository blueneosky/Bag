using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mahjong
{
    public partial class MainForm : Form
    {


        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TileSet tileSet = TileSetFactory.TileSet;

            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = tileSet.TileByTileTypes.Count - 1;
            hScrollBar1.Value = 1;
            hScrollBar1.Value = 0;
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            TileSet tileSet = TileSetFactory.TileSet;
            int index = hScrollBar1.Value;
            Tile tile = tileSet.TileByTileTypes.Values.Skip(index).FirstOrDefault();

            if (tile == null)
            {
                label1.Text = "#errror";
                pictureBox1.Image = null;
                return;
            }

            label1.Text = index+" : "+ tile.TileType.ToString();
            pictureBox1.Image = tile.Image;
        }


    }
}
