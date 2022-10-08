using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class MDP_oublier : Form
    {
        GestionScolariteEntities SE = new GestionScolariteEntities();
        public MDP_oublier()
        {
            InitializeComponent();
        }
        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Button Modifier 
        private void Modifier_Click(object sender, EventArgs e)
        {
            var login = (from x in SE.Logins select x).ToList();
            foreach(var x in login)
            {
                string S = x.SerieA;
                string N = x.NomA;

                if (LoginTextBox.Text == S && textBox4.Text == N)
                {
                    x.Mdp = textBox2.Text;
                    MessageBox.Show("Le Mot de Passe est Modifié ");
                }
            }
        }

        private void MDP_oublier_Load(object sender, EventArgs e)
        {

        }
    }
}
