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
    public partial class FormFiliere : Form
    {
        GestionScolariteEntities SE = new GestionScolariteEntities();
        public FormFiliere()
        {
            InitializeComponent();
        }

        private void filiere_Load(object sender, EventArgs e)
        {
            ChargeDGV();
        }
        private void ChargeDGV()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = (from x in SE.Filieres select new { Code_Filiere = x.Code_F, Filiere = x.libelle, Type_Filiere = x.type_F }).ToList();
        }

        //Buttton Ajouter
        private void Ajouter_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "")
            {
                if (radioButton1.Checked || radioButton2.Checked)
                {
                    try
                    {
                        String b;
                        if (radioButton1.Checked == true)
                        {
                            b = "1er Annee";
                        }
                        else
                        {
                            b = "2eme Annee";
                        }
                        SE.ProcInsert(int.Parse(textBox1.Text), textBox2.Text, b);
                        ChargeDGV();
                        MessageBox.Show("Filiere Ajouter avec Succes");
                    }
                    catch
                    {
                        MessageBox.Show("Filiere Existe Deja");
                    }
                }
                else
                {
                    MessageBox.Show("Selectionner une Annee");
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
            if (textBox1.Text != "" || textBox2.Text != "" )
            {
                if (radioButton1.Checked || radioButton2.Checked)
                {
                    int codef = int.Parse(textBox1.Text);
                    string libelle = textBox2.Text;
                    string b ;
                    if (radioButton1.Checked == true)
                    {
                        b = radioButton1.Text;
                    }
                    else
                    {
                        b = radioButton2.Text;
                    }
                    var fil = SE.Filieres.Where(x => x.Code_F == codef && x.libelle == libelle && x.type_F == b).First();
                    if (fil != null)
                    {
                        SE.Filieres.Remove(fil);
                        SE.SaveChanges();
                        ChargeDGV();
                        MessageBox.Show("Filiere Sont Supprimé");
                    }
                    else
                    {
                        MessageBox.Show("Filiere n'existe Pas");
                    }
                }
                else
                {
                    MessageBox.Show("Selectionner une Annee");
                }
            }
            else
            {
                MessageBox.Show("Saisi Code Filiere");
            }
        }

        //Button Rechercher 
        private void Rechercher_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int codef = int.Parse(textBox1.Text);
                var fil = SE.Filieres.Where(x => x.Code_F == codef).SingleOrDefault();
                if (fil != null)
                {
                    textBox2.Text = fil.libelle;
                    string b = fil.type_F;
                    if (b=="1er Annee")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                }
                else
                {
                    MessageBox.Show("Filiere n'existe Pas");
                }
            }
            else
            {
                MessageBox.Show("Saisi Code Filiere");
            }
        }

        // Button Vider 
        private void Vider_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        // Button retour 
        private void Retour_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
