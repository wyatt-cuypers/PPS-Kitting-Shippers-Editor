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

        //Connects to the specified database
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

        //Disconnects from the currently open database
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

        //Upon this click, DB_Connect is called and a SQL query is executed. This query looks for records that match with the Day Code entered by the user
        //If any matching records are found, they are displayed on the form's data gridview
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand comm = new SqlCommand();
            dayCode = txtDayCode.Text;
            try
            {
                DB_Connect();
                comm.CommandType = CommandType.Text;
                comm.CommandText = "SELECT MachineID, GMTTime, LocalTime, DayCode, JobID, ShipperQty, jmpPartShortDescription FROM PPS.dbo.PPSKittingShippers PPSKittingShippers INNER JOIN M1_P1.dbo.Jobs ON JobID = jmpJobID WHERE DayCode = '" + txtDayCode.Text + "' AND JobID > '' ORDER BY GMTTime, DayCode"; 
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

        //Upon this click, a message box is displayed asking the user if they are sure they want to delete the record(s)
        //If yes is selected, the program checks to see if all records are selected. If they are, another message box is displayed
        //alerting the user than at least one record with the specified Day Code must remain
        //Otherwise, the program then executes a SQL query which deletes the selected row(s) from the database, and then 
        //from the data gridview as well
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult m = MessageBox.Show("Are you sure you want to delete this record?\n", "", MessageBoxButtons.YesNo);
            DB_Connect();
            int selRows = dgvCodes.SelectedRows.Count;
            try
            {
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
            catch (Exception ex)
            {

            }
            
        }

        //Upon this click, the program checks to see if the user has searched for a record
        //If not, a message box is popuped up advising them to do so
        //Otherwise, a JobIDForm is displayed
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dayCode != "")
            {
                JobIDForm popup = new JobIDForm();
                popup.Show();
            }
            else
            {
                MessageBox.Show("Must enter a Day Code and click Search before adding a record");
            }
        }
    }
}
