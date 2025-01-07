using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class DifficultyForm : Form
    {

        public int Simulations { get; private set; } = 7;

        public DifficultyForm(int simulations)
        {
            InitializeComponent();
            this.Simulations = simulations;
            this.simulationsTextBox.Text = simulations.ToString();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            int newSimulations = Convert.ToInt32(simulationsTextBox.Text);
            if(newSimulations <= 0)
            {
                MessageBox.Show("Numarul de simulari trebuie sa fie mai mare decat 0");
                return;
            }

            Simulations = newSimulations;

            DialogResult = DialogResult.OK;
            this.Tag = Simulations;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
