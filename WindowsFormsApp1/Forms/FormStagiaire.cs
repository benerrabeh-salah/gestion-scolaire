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
    public partial class FormStagiaire : Form
    {
        GestionScolariteEntities SE = new GestionScolariteEntities();
        public FormStagiaire()
        {
            InitializeComponent();
        }
        // Combobox
        public void ComboFill()
        {
            var comboFil = (from x in SE.Filieres select new {  x.libelle ,x.Idfil }).ToList();
            comboBox1.DataSource = comboFil;
            comboBox1.DisplayMember = "libelle";
            comboBox1.ValueMember = "Idfil";
            comboBox1.SelectedIndex = -1;
        }

        // Remplissage DataGridView
        private void FormProduct_Load(object sender, EventArgs e)
        {
            ChargeDGV();
        }
        private void ChargeDGV()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = (from x in SE.Stagiaires select new { x.CIN , x.Nom, x.Prenom, CNE = x.CNE, Date_De_Naissance = x.Date_Naissance, Telephone = x.Numero_telephone, x.Adresse, Numero_Filiere = x.Filiere.Code_F, Filiere = x.Filiere.libelle, Type = x.Filiere.type_F}).ToList();
        }

        // Button Ajouter
        private void Ajouter_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox6.Text != "" || textBox7.Text != "" || comboBox1.SelectedIndex != -1)
            {
                String cin = textBox1.Text;
                var et = SE.Stagiaires.Where(x => x.CIN == cin).Count();
                if (et == 0)
                {
                    Stagiaire Etud = new Stagiaire();
                    Etud.CIN = cin;
                    Etud.Nom = textBox2.Text;
                    Etud.Prenom = textBox3.Text;
                    Etud.CNE = textBox4.Text;
                    Etud.Date_Naissance = dateTimePicker1.Value;
                    Etud.Numero_telephone = textBox7.Text;
                    Etud.Adresse = textBox6.Text;
                    Etud.Idfil = Convert.ToInt32(comboBox2.SelectedValue);
                    SE.Stagiaires.Add(Etud);
                    SE.SaveChanges();
                    ChargeDGV();
                    MessageBox.Show("Stagiaire Ajouteé");
                }
                else
                {
                    MessageBox.Show("Stagiaire Déja Existe");
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
                var Etud = SE.Stagiaires.Where(x => x.CIN == cin).First();
                if (Etud != null)
                {
                    SE.Stagiaires.Remove(Etud);
                    SE.SaveChanges();
                    ChargeDGV();
                    MessageBox.Show("Stagiaire Sont Supprimé");
                }
                else
                {
                    MessageBox.Show("Stagiaire n'existe Pas");
                }
            }
            else
            {
                MessageBox.Show("Saisi CIN Etudiant");
            }
        }
        // Button Modifier
        private void Modifier_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox6.Text != "" || textBox7.Text != "" || comboBox1.SelectedIndex != -1 || comboBox2.SelectedIndex != -1)
                {
                    String cin = textBox1.Text;
                    var Etud = SE.Stagiaires.Where(x => x.CIN == cin).FirstOrDefault();
                    if (Etud != null)
                    {
                        Etud.CIN = cin;
                        Etud.Nom = textBox2.Text;
                        Etud.Prenom = textBox3.Text;
                        Etud.CNE = textBox4.Text;
                        Etud.Date_Naissance = dateTimePicker1.Value;
                        Etud.Numero_telephone = textBox7.Text;
                        Etud.Adresse = textBox6.Text;
                        Etud.Idfil = Convert.ToInt32(comboBox2.SelectedValue);
                        SE.SaveChanges();
                        ChargeDGV();
                        MessageBox.Show("Stagiaire Modifier Avec Succes");
                    }
                    else
                    {
                        MessageBox.Show("Stagiaire N'Existe pas");
                    }
                }
                else
                {
                    MessageBox.Show("Tous les Champs doit etre insére");
                }
            }
            catch
            {
                MessageBox.Show("CNE Existe Deja ");
            }
        }
        // Button Rechercher
        private void Rechercher_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                String cin = textBox1.Text;
                var Etud = SE.Stagiaires.Where(x => x.CIN == cin).SingleOrDefault();
                if (Etud != null)
                {
                    textBox1.Text = Etud.CIN.ToString();
                    textBox2.Text = Etud.Nom.ToString();
                    textBox3.Text = Etud.Prenom.ToString();
                    textBox4.Text = Etud.CNE.ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(Etud.Date_Naissance);
                    textBox7.Text = Etud.Numero_telephone;
                    textBox6.Text = Etud.Adresse;
                    string xx = Etud.Filiere.type_F;
                    if (xx=="1er Annee")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                    comboBox1.Text = Etud.Filiere.libelle;
                    comboBox2.Text = Etud.Filiere.Code_F.ToString();
                }
                else
                {
                    MessageBox.Show("Stagiaire N'Existe pas");
                }
            }
            else
            {
                MessageBox.Show("Saisi CIN Etudiant");
            }
        }

        // Button Vider
        private void Vider_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        // Button De Retour 
        private void Retour_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tt = comboBox1.Text;
            string b;
            if (radioButton1.Checked)
            {
                b = radioButton1.Text;
            }
            else
            {
                b = radioButton2.Text;
            }
            var comboFil = (from x in SE.Filieres where x.libelle == tt && x.type_F ==b  select new { x.Code_F, x.Idfil }).ToList();
            comboBox2.DataSource = comboFil;
            comboBox2.DisplayMember = "Code_F";
            comboBox2.ValueMember = "Idfil";
            comboBox2.SelectedIndex = -1;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var comboFil = (from x in SE.Filieres where x.type_F == radioButton1.Text select new { x.libelle, x.Idfil }).Distinct().ToList();
                comboBox1.DataSource = comboFil;
                comboBox1.DisplayMember = "libelle";
                comboBox1.ValueMember = "Idfil";
                comboBox1.SelectedIndex = -1;
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                var comboFil = (from x in SE.Filieres where x.type_F == radioButton2.Text select new { x.libelle, x.Idfil }).Distinct().ToList();
                comboBox1.DataSource = comboFil;
                comboBox1.DisplayMember = "libelle";
                comboBox1.ValueMember = "Idfil";
                comboBox1.SelectedIndex = -1;
            }
        }
    }
}
