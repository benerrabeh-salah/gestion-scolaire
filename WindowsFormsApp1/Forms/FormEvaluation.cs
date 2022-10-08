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
using CrystalDecisions.Windows.Forms;

namespace WindowsFormsApp1.Forms
{
    public partial class FormEvaluation : Form
    {
        GestionScolariteEntities SE = new GestionScolariteEntities();

        public FormEvaluation()
        {
            InitializeComponent();
        }
        // Combobox Modulles
        public void ComboFill1()
        {
            var comboMat = (from x in SE.Modules select new { x.Code_M, x.libelle_M }).ToList();
            comboBox4.DataSource = comboMat;
            comboBox4.ValueMember = "Code_M";
            comboBox4.DisplayMember = "libelle_M";
            comboBox4.SelectedIndex = -1;
        }
        private void Evaluation_Load(object sender, EventArgs e)
        {
            // remplissage DataGrid View Evaluation
            ChargeDGV();

            // remplissage DataGrid View Etudiant
            dataGridView2.DataSource = (from x in SE.Stagiaires select new { CNE = x.CNE, x.Nom, x.Prenom, x.CIN, Date_De_Naissance = x.Date_Naissance, Telephone = x.Numero_telephone, x.Adresse, Numero_Filiere = x.Filiere.Code_F, Filiere = x.Filiere.libelle, Type = x.Filiere.type_F }).ToList();
            ComboFill1();
        }
        private void ChargeDGV()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = (from x in SE.Evaluations select new { Stagiaire = x.Stagiaire.Nom, Matiere = x.Module.libelle_M, Note = x.note }).ToList();
        }
        // Button Ajouter
        private void Ajouter_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex != -1 || comboBox4.SelectedIndex != -1 || textBox3.Text != "")
            {
                String cin = comboBox3.SelectedValue.ToString();
                int codeM = Convert.ToInt32(comboBox4.SelectedValue);
                string dt = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                var ev = SE.Evaluations.Where(x => x.CIN == cin && x.Code_M == codeM && x.dateEval == dt).Count();
                if (ev == 0)
                {
                    Evaluation eval = new Evaluation();
                    eval.CIN = cin;
                    eval.Code_M = codeM;
                    eval.dateEval = dt;
                    eval.note = Convert.ToInt32(textBox3.Text);
                    SE.Evaluations.Add(eval);
                    SE.SaveChanges();
                    MessageBox.Show("Evaluation Ajouteé");
                    ChargeDGV();
                }
                else
                {
                    MessageBox.Show("Evaluation Déja Existe");
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
            if (comboBox3.SelectedIndex != -1 || comboBox4.SelectedIndex != -1 || textBox3.Text != "")
            {
                String cin = comboBox3.SelectedValue.ToString();
                int codeM = Convert.ToInt32(comboBox4.SelectedValue);
                string dt = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                var eval = SE.Evaluations.Where(x => x.CIN == cin && x.Code_M == codeM && x.dateEval == dt).FirstOrDefault();
                if (eval != null)
                {
                    eval.note = Convert.ToInt32(textBox3.Text);
                    SE.SaveChanges();
                    MessageBox.Show("Evaluation Modifier");
                    ChargeDGV();
                }
                else
                {
                    MessageBox.Show("Evaluation N'Existe pas");
                }
            }
            else
            {
                MessageBox.Show("Tous les Champs doit etre insére");
            }
        }

        //Button Supprimer
        private void Supprimer_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                String cin = comboBox3.SelectedValue.ToString();
                int codeM = Convert.ToInt32(comboBox4.SelectedValue);
                string dt = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                var eval = SE.Evaluations.Where(x => x.CIN == cin && x.Code_M == codeM && x.dateEval == dt).First();
                if (eval != null)
                {
                    SE.Evaluations.Remove(eval);
                    SE.SaveChanges();
                    MessageBox.Show("Evaluation Sont Supprimé");
                    ChargeDGV();
                }
                else
                {
                    MessageBox.Show("Evaluation n'existe Pas");
                }
            }
            else
            {
                MessageBox.Show("Saisi Code Filiere");
            }
        }
        //Button Vider
        private void Vider_Click(object sender, EventArgs e)
        {
            comboBox3.Text = "";
            comboBox4.Text = "";
            textBox3.Text = "";
        }
        //Button Vider
        private void Retour_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Button Imprimer
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex!=-1)
            {
                string x = comboBox3.SelectedValue.ToString();
                IMPRIMER imp = new IMPRIMER(x);
                imp.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select Etudient");
            }    
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
            var comboFil = (from x in SE.Filieres where x.libelle == tt && x.type_F == b select new { x.Code_F, x.Idfil }).ToList();
            comboBox2.DataSource = comboFil;
            comboBox2.DisplayMember = "Code_F";
            comboBox2.ValueMember = "Idfil";
            comboBox2.SelectedIndex = -1;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
             int tt = Convert.ToInt32(comboBox2.SelectedValue);
             var comboFil = (from x in SE.Stagiaires where x.Idfil == tt select new { x.CIN, x.Nom }).ToList();
             comboBox3.DataSource = comboFil;
             comboBox3.DisplayMember = "Nom";
             comboBox3.ValueMember = "CIN";
             comboBox3.SelectedIndex = -1;
        }
    }
}
