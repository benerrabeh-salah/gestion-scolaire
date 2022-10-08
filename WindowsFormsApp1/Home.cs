using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Forms;

namespace WindowsFormsApp1
{
    public partial class Home : Form
    {
        
        private Form activeForm;
        public Home()
        {
            InitializeComponent();
            this.Text =String.Empty;
            this.ControlBox = false;
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor= Color.White;
                    previousBtn.Font = new System.Drawing.Font("Nirmala UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        // Desactivation des Buttons de Navigation //
        private void Form1_Load(object sender, EventArgs e)
        {
            student.Enabled = false;
            prof.Enabled = false;
            Absence.Enabled = false;
            matieres.Enabled = false;
            Filiere.Enabled = false;
            Evaluation.Enabled = false;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                 activeForm.Close();
            }
          //  ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(childForm);
            this.panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lbllTitle.Text = childForm. Text;
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Login_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new Forms.Login(), sender);
            FormLogin lg = new FormLogin(this);
            lg.Show();
        }
        private void student_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormStagiaire(),sender);
        }

        private void prof_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormFormateur(), sender);
        }
        private void matieres_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormMatiere(), sender);
        }
        private void Filiere_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormFiliere(), sender);
        }
        private void Absence_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormAbsence(), sender);
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormEvaluation(), sender);
        }
    }
}
