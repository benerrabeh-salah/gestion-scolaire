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
    public partial class FormAbsence : Form
    {
        GestionScolariteEntities SE = new GestionScolariteEntities(); 
       
        public FormAbsence()
        {
            InitializeComponent();
        }

        // Remplissage Combo2
        public void ComboFill2()
        {
            var combofil = (from x in SE.Modules select new { x.Code_M, x.libelle_M }).ToList();
            comboBox2.DataSource = combofil;
            comboBox2.DisplayMember = "libelle_M";
            comboBox2.ValueMember = "Code_M";
            comboBox2.SelectedIndex = -1;
        }
        //Remplissage Combo3
        public void ComboFill3()
        {
            var combofil = (from x in SE.Formateurs select new { x.Nom, x.Cin_F }).ToList();
            comboBox3.DataSource = combofil;
            comboBox3.DisplayMember = "Nom";
            comboBox3.ValueMember = "Cin_F";
            comboBox3.SelectedIndex = -1;
        }

        //Affichage de DatagridView
        private void Absence_Load(object sender, EventArgs e)
        {
            ChargeDGVAbsenceFormateur();
            ChargeDGVAbsenceStagiaire();
            
            ComboFill2();
            ComboFill3();
        }

        private void ChargeDGVAbsenceStagiaire()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = (from x in SE.AbsenceS select new { x.Stagiaire.Nom, x.Module.libelle_M, x.date_absence, x.Nombre_Heure }).ToList();
        }

        private void ChargeDGVAbsenceFormateur()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = (from x in SE.AbsenceFs select new { x.Formateur.Nom, x.date_absence, x.Nombre_Heure }).ToList();
        }

        //Button Ajouter  Absence Stagiaire 
        private void AjouterE_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || comboBox1.SelectedIndex != -1 || comboBox2.SelectedIndex != -1)
            {
                int matiere = Convert.ToInt32(comboBox2.SelectedValue);
                string cin = comboBox1.SelectedValue.ToString();
                string datee = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                var abE = SE.AbsenceS.Where(x => x.CIN == cin && x.date_absence == datee && x.Code_M == matiere).Count();
                if (abE == 0) 
                { 
                    Absence AbsenE = new Absence();
                    AbsenE.CIN = cin;
                    AbsenE.Code_M = matiere;
                    AbsenE.date_absence = datee;
                    AbsenE.Nombre_Heure = int.Parse(textBox1.Text);
                    SE.AbsenceS.Add(AbsenE);
                    SE.SaveChanges();
                    ChargeDGVAbsenceStagiaire();
                    MessageBox.Show("Absence de Stagiaire Ajouter Avec Succes");
                } 
                else
                {
                    MessageBox.Show("Stagiare Deja Ajouter");
                }
            }
            else
            {
                MessageBox.Show("Tous les Champs doit etre insére");
            }
        }

        //Button Ajouter Absence Formateur
        private void AjouterP_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" || comboBox2.SelectedIndex != -1)
            {
                string cin = comboBox3.SelectedValue.ToString();
                string datee = dateTimePicker2.Value.Date.ToString("dd/MM/yyyy");
                var absP = SE.AbsenceFs.Where(x => x.Cin_F == cin && x.date_absence == datee).Count();
                if (absP == 0)
                {
                    AbsenceF AbsenP = new AbsenceF();
                    AbsenP.Cin_F = cin;
                    AbsenP.date_absence = datee;
                    AbsenP.Nombre_Heure = int.Parse(textBox2.Text);
                    SE.AbsenceFs.Add(AbsenP);
                    SE.SaveChanges();
                    ChargeDGVAbsenceFormateur();
                    MessageBox.Show("Absence de Formateur Ajouter Avec Succes");
                }
                else
                {
                    MessageBox.Show("Formateur Deja Ajouter");
                }
            }
            else
            {
                MessageBox.Show("Tous les Champs doit etre insére");
            }
        }

        //Button Supprimer Absence Stagiaire
        private void SupprimerE_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || comboBox1.SelectedIndex != -1 || comboBox2.SelectedIndex != -1)
            {
                int matiere = Convert.ToInt32(comboBox2.SelectedValue);
                string cin = comboBox1.SelectedValue.ToString();
                string datee = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                var AbsenE = SE.AbsenceS.Where(x => x.CIN == cin && x.date_absence == datee && x.Code_M == matiere).First();
                if (AbsenE != null)
                {
                    SE.AbsenceS.Remove(AbsenE);
                    SE.SaveChanges();
                    ChargeDGVAbsenceStagiaire();
                    MessageBox.Show("Absence de Formateur Supprimer Avec Succes");
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

        //Button Supprimer Absence Formateur
        private void SupprimerP_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" || comboBox3.SelectedIndex != -1)
            {
                string cin = comboBox3.SelectedValue.ToString();
                string datee = dateTimePicker2.Value.Date.ToString("dd/MM/yyyy");
                var AbsenP = SE.AbsenceFs.Where(x => x.Cin_F == cin && x.date_absence == datee).First();
                if (AbsenP != null)
                {
                    SE.AbsenceFs.Remove(AbsenP);
                    SE.SaveChanges();
                    ChargeDGVAbsenceFormateur();
                    MessageBox.Show("Absence de Formateur Supprimer Avec Succes");
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

        //Button Modifer Absence Stagiaire
        private void ModifierE_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || comboBox1.SelectedIndex != -1 || comboBox2.SelectedIndex != -1)
            {
                int matiere = Convert.ToInt32(comboBox2.SelectedValue);
                string cin = comboBox1.SelectedValue.ToString();
                string datee = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                var AbsenE = SE.AbsenceS.Where(x => x.CIN == cin && x.date_absence == datee && x.Code_M == matiere).First();
                if (AbsenE != null)
                {
                    AbsenE.CIN = cin;
                    AbsenE.Code_M = matiere;
                    AbsenE.date_absence = datee;
                    AbsenE.Nombre_Heure = int.Parse(textBox1.Text);
                    SE.SaveChanges();
                    ChargeDGVAbsenceStagiaire();
                    MessageBox.Show("Absence de Stagiaire Modifier Avec Succes");
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

        //Button Modifer Absence Formateur
        private void ModifierP_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" || comboBox3.SelectedIndex != -1)
            {
                string cin = comboBox3.SelectedValue.ToString();
                string datee = dateTimePicker2.Value.Date.ToString("dd/MM/yyyy");
                var AbsenP = SE.AbsenceFs.Where(x => x.Cin_F == cin && x.date_absence == datee).First();
                if (AbsenP != null)
                {
                    AbsenP.Cin_F = cin;
                    AbsenP.date_absence = datee;
                    AbsenP.Nombre_Heure = int.Parse(textBox2.Text);
                    SE.SaveChanges();
                    ChargeDGVAbsenceFormateur();
                    MessageBox.Show("Absence de Formateur Modifier Avec Succes");
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

        //Vider Les Champs d'absence Stagiaire
        private void Vider_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox1.Text = "";
        }

        //Vider Les Champs d'absence Formateur 
        private void button2_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Text = "";
            comboBox3.Text = "";
            textBox2.Text = "";
        }

        //Button Retourne 
        private void Retour_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var comboFil = (from x in SE.Filieres where x.type_F == radioButton1.Text select new { x.libelle, x.Idfil }).Distinct().ToList();
                comboBox5.DataSource = comboFil;
                comboBox5.DisplayMember = "libelle";
                comboBox5.ValueMember = "Idfil";
                comboBox5.SelectedIndex = -1;
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                var comboFil = (from x in SE.Filieres where x.type_F == radioButton2.Text select new { x.libelle, x.Idfil }).Distinct().ToList();
                comboBox5.DataSource = comboFil;
                comboBox5.DisplayMember = "libelle";
                comboBox5.ValueMember = "Idfil";
                comboBox5.SelectedIndex = -1;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tt = comboBox5.Text;
            string b;
            if (radioButton1.Checked)
            {
                b = radioButton1.Text;
            }
            else
            {
                b = radioButton2.Text;
            }
            var comboFil = (from x in SE.Filieres where x.libelle == tt && x.type_F == b select new { x.Code_F, x.Idfil }).Distinct().ToList();
            comboBox4.DataSource = comboFil;
            comboBox4.DisplayMember = "Code_F";
            comboBox4.ValueMember = "Idfil";
            comboBox4.SelectedIndex = -1;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tt = Convert.ToInt32(comboBox4.SelectedValue);
            var comboFil = (from x in SE.Stagiaires where x.Idfil == tt select new { x.CIN, x.Nom }).ToList();
            comboBox1.DataSource = comboFil;
            comboBox1.DisplayMember = "Nom";
            comboBox1.ValueMember = "CIN";
            comboBox1.SelectedIndex = -1;
        }           
    }
}
