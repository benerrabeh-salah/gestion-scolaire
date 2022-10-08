using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Forms
{
    public partial class FormFormateur : Form
    {
        GestionScolariteEntities SE = new GestionScolariteEntities();

        public FormFormateur()
        {
            InitializeComponent();
        }
        private void FormProfesseur_Load(object sender, EventArgs e)
        {
            ChargeDGV();

            String[] Sexe = { "Homme", "Femme" };
            comboBox1.DataSource = Sexe;
            comboBox1.SelectedIndex = -1;
        }
        private void ChargeDGV()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = (from x in SE.Formateurs select new { CIN = x.Cin_F, x.Nom, x.Prenom, Date_De_NAissance = x.DateNP, x.Sexe, x.Adresse, Telephone = x.TelP }).ToList();
        }
        // Button Ajouter 
        private void Ajouter_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox6.Text != "" || textBox7.Text != "" || comboBox1.SelectedIndex != -1)
            {
                String cin = textBox1.Text;
                var pro = SE.Formateurs.Where(x => x.Cin_F == cin).Count();
                if (pro == 0)
                {
                    Formateur prof = new Formateur();
                    prof.Cin_F = textBox1.Text;
                    prof.Nom = textBox2.Text;
                    prof.Prenom = textBox3.Text;
                    prof.DateNP = dateTimePicker1.Value;
                    prof.Sexe = comboBox1.Text;
                    prof.Adresse = textBox6.Text;
                    prof.TelP = textBox7.Text;
                    SE.Formateurs.Add(prof);
                    SE.SaveChanges();
                    ChargeDGV();
                    MessageBox.Show("Formateur Ajouteé");
                }
                else
                {
                    MessageBox.Show("Formateur Déja Existe");
                }
            }
            else
            {
                MessageBox.Show("Tous les Champs doit etre insére");
            }
        }
        // Button Modifier
        private void Modifier_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox6.Text != "" || textBox7.Text != "" || comboBox1.SelectedIndex != -1)
            {
                String cin = textBox1.Text;
                var prof = SE.Formateurs.Where(x => x.Cin_F == cin).FirstOrDefault();
                if (prof != null)
                {
                    prof.Cin_F = textBox1.Text;
                    prof.Nom = textBox2.Text;
                    prof.Prenom = textBox3.Text;
                    prof.DateNP = dateTimePicker1.Value;
                    prof.Sexe = comboBox1.Text;
                    prof.Adresse = textBox6.Text;
                    prof.TelP = textBox7.Text;
                    SE.SaveChanges();
                    ChargeDGV();
                    MessageBox.Show("Formateur Modifier");
                }
                else
                {
                    MessageBox.Show("Formateur N'Existe pas");
                }
            }
            else
            {
                MessageBox.Show("Tous les Champs doit etre insére");
            }
        }

        // Button Supprimer
        private void Supprimer_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                String cin = textBox1.Text;
                var prof = SE.Formateurs.Where(x => x.Cin_F == cin).FirstOrDefault();
                if (prof != null)
                {
                    SE.Formateurs.Remove(prof);
                    SE.SaveChanges();
                    ChargeDGV();
                    MessageBox.Show("Formateur Sont Supprimé");
                }
                else
                {
                    MessageBox.Show("Formateur n'existe Pas");
                }
            }
            else
            {
                MessageBox.Show("Saisi CIN Formatur");
            }
        }

        // Button rechercher
        private void Rechercher_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                String cin = textBox1.Text;
                var prof = SE.Formateurs.Where(x => x.Cin_F == cin).SingleOrDefault();
                if (prof != null)
                {
                    textBox2.Text = prof.Nom.ToString();
                    textBox3.Text = prof.Prenom.ToString();
                    comboBox1.Text = prof.Sexe.ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(prof.DateNP);
                    textBox7.Text = prof.TelP;
                    textBox6.Text = prof.Adresse;
                }
                else
                {
                    MessageBox.Show("formateur N'Existe pas");
                }
            }
            else
            {
                MessageBox.Show("Saisi CIN Formateur");
            }
        }

        //Button Vider
        private void Vider_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }

        //Button de Retour
        private void Retour_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
