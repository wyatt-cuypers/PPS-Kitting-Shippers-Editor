using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPSKittingShippersEditor
{
    public partial class Form1 : Form
    {
        public static SqlConnection conn = new SqlConnection();
        public static string dayCode = "";
        public Form1()
        {
            InitializeComponent();
            DB_Connect();
        }

        public static void DB_Connect()
        {
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = "Data Source=comsql;User ID=PPS_Writer; Password=writer";
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void DB_Close()
        {
            try
            {
                conn.Close();
                conn = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand comm = new SqlCommand();
            dayCode = txtDayCode.Text;
            try
            {
                DB_Connect();
                comm.CommandType = CommandType.Text;
                comm.CommandText = "SELECT TOP (100) MachineID, GMTTime, LocalTime, DayCode, JobID, ShipperQty, jmpPartShortDescription FROM PPS.dbo.PPSKittingShippers PPSKittingShippers INNER JOIN M1_P1.dbo.Jobs ON JobID = jmpJobID WHERE DayCode = '" + txtDayCode.Text + "' AND JobID > '' ORDER BY GMTTime, DayCode"; //WHERE DayCode = '" + txtDayCode.Text + "' AND JobID > ''
                comm.Connection = conn;
                SqlDataReader reader = comm.ExecuteReader(); 
                DataTable table = new DataTable();
                table.Load(reader);
                dgvCodes.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult m = MessageBox.Show("Are you sure you want to delete this record?\n", "", MessageBoxButtons.YesNo);
            DB_Connect();
            int selRows = dgvCodes.SelectedRows.Count;
            if (selRows != dgvCodes.Rows.Count && dgvCodes.Rows.Count > 1)
            {
                if (m == DialogResult.Yes)
                {
                    for (int i = 0; i < selRows; i++)
                    {
                        SqlCommand comm = new SqlCommand();
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = "DELETE FROM PPS.dbo.PPSKittingShippers WHERE DayCode = '" + dgvCodes.SelectedRows[0].Cells[3].Value.ToString() + "' AND GMTTime = '" + dgvCodes.SelectedRows[0].Cells[1].Value.ToString() + "'";
                        comm.Connection = conn;
                        comm.ExecuteNonQuery();
                        dgvCodes.Rows.Remove(dgvCodes.SelectedRows[0]);
                        dgvCodes.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot delete all rows that have Day Code " + txtDayCode.Text + "!");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            JobIDForm popup = new JobIDForm();
            popup.Show();
        }
    }
}
