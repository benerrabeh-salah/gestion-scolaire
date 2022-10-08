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
namespace WindowsFormsApp1.Forms
{
    public partial class FormLogin : Form
    {
        GestionScolariteEntities SE = new GestionScolariteEntities();
        Home H1;
        public FormLogin(Home H)
        {
            InitializeComponent();
            this.H1 = H;
        }
        // Button De Connexion 
        static int cmp = 0;
        private void connexion_Click(object sender, EventArgs e)
        {
            var req = (from x in SE.Logins select new { x.LoginA, x.Mdp, x.NomA, x.SerieA }).ToList();
            foreach(var x in req)
            {
                string l = x.LoginA;
                string p = x.Mdp;
                if (l == LoginTextBox.Text && p == PasswordTextBox.Text)
                {
                    H1.student.Enabled = true;
                    H1.prof.Enabled = true;
                    H1.Absence.Enabled = true;
                    H1.matieres.Enabled = true;
                    H1.Filiere.Enabled = true;
                    H1.Evaluation.Enabled = true;
                    H1.Login.Enabled = false;
                    this.Close();
                    break;
                }
                else if (l == LoginTextBox.Text && p != PasswordTextBox.Text) { cmp++; MessageBox.Show("Mot De Passe Incorrect"); PasswordTextBox.Clear(); break; }
                else if (l != LoginTextBox.Text && p == PasswordTextBox.Text) { cmp++; MessageBox.Show("Login Incorrect"); LoginTextBox.Clear(); PasswordTextBox.Clear(); break; }
                else { cmp++; MessageBox.Show("Les Champs Incorrect"); LoginTextBox.Clear(); PasswordTextBox.Clear(); break; }
            }
            
            // Pour desactiver le compte apres 4 tenta 
            if (cmp == 3)
            {
                connexion.Enabled = false;
            }
        }
        // Button Cancel 
        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MDP_oublier mDP = new MDP_oublier();
            mDP.Show();
            this.Close();
        }
    }
}

