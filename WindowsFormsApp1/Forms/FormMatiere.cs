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
    public partial class FormMatiere : Form
    {
        GestionScolariteEntities SE = new GestionScolariteEntities();

        public FormMatiere()
        {
            InitializeComponent();
        }
        public void ComboFill()
        {
            var comboProfe = (from x in SE.Formateurs select new { x.Cin_F, x.Nom }).ToList();
            comboBox1.DataSource = comboProfe;
            comboBox1.ValueMember = "Cin_F";
            comboBox1.DisplayMember = "Nom";
            comboBox1.SelectedIndex = -1;
        }
        // Remplissage Data GridView

        private void Matiere_Load(object sender, EventArgs e)
        {
            ChargeDGV();
            ComboFill();
        }
        private void ChargeDGV()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = (from x in SE.Modules select new { Code = x.Code_M, Modulle = x.libelle_M, Coefficient = x.coefficient, Masse_Horaire = x.Mh_M, Formateur = x.Formateur.Nom }).ToList();
        }

        // Button Ajouter 
        private void Ajouter_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || comboBox1.SelectedIndex != -1)
            {
                int codeM = int.Parse(textBox1.Text);
                var mtt = SE.Modules.Where(x => x.Code_M == codeM).Count();
                if (mtt == 0)
                {
                    Module mt = new Module();
                    mt.Code_M = codeM;
                    mt.libelle_M = textBox2.Text;
                    mt.coefficient = int.Parse(textBox3.Text);
                    mt.Mh_M = int.Parse(textBox4.Text);
                    mt.Cin_F = comboBox1.SelectedValue.ToString();
                    SE.Modules.Add(mt);
                    SE.SaveChanges();
                    MessageBox.Show("Matiere Ajouter Avec Succes");
                    ChargeDGV();
                }
                else
                {
                    MessageBox.Show("Matiere N'Existe pas");
                }
            }
            else
            {
                MessageBox.Show("Tous les Champs doit etre insére");
            }
        }
        // Modifier 
        private void Modifier_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || comboBox1.SelectedIndex != -1)
            {
                int codeM = int.Parse(textBox1.Text);
                var mt = SE.Modules.Where(x => x.Code_M == codeM).FirstOrDefault();
                if(mt != null)
                {
                    mt.Code_M = codeM;
                    mt.libelle_M = textBox2.Text;
                    mt.coefficient = int.Parse(textBox3.Text);
                    mt.Mh_M = int.Parse(textBox4.Text);
                    mt.Cin_F =comboBox1.SelectedValue.ToString();
                    SE.SaveChanges();
                    MessageBox.Show("Matiere Modifier Avec Succes");
                    ChargeDGV();
                }
                else
                {
                    MessageBox.Show("Matiere N'Existe pas");
                }
            }
            else
            {
                MessageBox.Show("Tous les Champs doit etre insére");
            }
        }
        // Supprimer 
        private void Supprimer_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int codeM = int.Parse(textBox1.Text);
                var mt = SE.Modules.Where(x => x.Code_M == codeM).FirstOrDefault();
                if (mt != null)
                {
                    SE.Modules.Remove(mt);
                    SE.SaveChanges();
                    MessageBox.Show("Matiere Sont Supprimé");
                    ChargeDGV();
                }
                else
                {
                    MessageBox.Show("Matiere n'existe Pas");
                }
            }
            else
            {
                MessageBox.Show("Saisi Code Matiere");
            }
        }
        //Button Rechercher 
        private void Rechercher_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int codeM = int.Parse(textBox1.Text);
                var mt = SE.Modules.Where(x => x.Code_M == codeM).FirstOrDefault();
                if (mt != null)
                {
                    textBox2.Text = mt.libelle_M;
                    textBox3.Text = mt.coefficient.ToString();
                    textBox4.Text = mt.Mh_M.ToString();
                    comboBox1.Text = mt.Formateur.Nom.ToString();
                }
                else
                {
                    MessageBox.Show("Matiere n'existe Pas");
                }
            }
            else
            {
                MessageBox.Show("Saisi Code Matiere");
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
        }

        // Retour 
        private void Retour_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}





