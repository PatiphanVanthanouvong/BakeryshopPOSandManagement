using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oma_s_bakery.models
{

    public partial class ucproduct : UserControl
    {
        public ucproduct()
        {
            InitializeComponent();
        }
        public event EventHandler onSelect = null;
        public int id { get; set; }
        public string PName { 
            get { return label1.Text; }
            set { label1.Text = value; }
        
        }
        public string PPrice { get; set; }
    
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, EventArgs.Empty);
        }
    }
}
