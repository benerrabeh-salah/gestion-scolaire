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
    public partial class IMPRIMER : Form
    {
        GestionScolariteEntities SE = new GestionScolariteEntities();
        string cin;
        public IMPRIMER(string z)
        {
            this.cin = z;
            InitializeComponent();
        }

        private void IMPRIMER_Load(object sender, EventArgs e)
        {
            CrystalReport1 cr = new CrystalReport1();
            DataSet ds = new DataSet();
            DataTable dt = new DataSet1.CrystalAfficheDataTable();

            //var a = (from x in SE.CrystalAffiches where x.CIN == cin select x).ToList();
            //foreach (var dr in a)
            //{
            //    DataRow row = dt.NewRow();
            //    row[0] = dr.CIN;
            //    row[1] = dr.Nom;
            //    row[2] = dr.Prenom;
            //    row[3] = dr.coefficient;
            //    row[4] = dr.libelle_M;
            //    row[5] = dr.Moyenne;
            //    dt.AcceptChanges();
            //    dt.Rows.Add(row);

            //}

            try
            {
                var a = (from x in SE.CrystalAffiches where x.CIN == cin select x).ToList();
                foreach (var dr in a)
                {
                    DataRow row = dt.NewRow();
                    row[0] = dr.CIN;
                    row[1] = dr.Nom;
                    row[2] = dr.Prenom;
                    row[3] = dr.coefficient;
                    row[4] = dr.libelle_M;
                    row[5] = dr.Moyenne;
                    dt.AcceptChanges();
                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ds.Tables.Add(dt);

            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("No data found ");
                return;
            }

            cr.SetDataSource(ds);
            crystalReportViewer1.ReportSource = cr;
        }
    }
}
