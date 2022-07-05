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
    public partial class JobIDForm : Form
    {
        public JobIDForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlCommand comm = new SqlCommand();
            try
            {
                Form1.DB_Connect();
                int test = 0;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "SELECT jmpJobID, imrManufacturingLotSize FROM M1_P1.dbo.Jobs Jobs INNER JOIN M1_P1.dbo.PartRevisions PartRevisions ON jmpPartID = imrPartID AND jmpPartRevisionID = imrPartRevisionID WHERE jmpJobID = '" + txtJobID.Text + "'";
                comm.Connection = Form1.conn;
                SqlDataReader reader = comm.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Not a valid Job ID, must return one or more rows"); //BG00562195LST  
                }
                else
                {
                    SqlCommand newComm = new SqlCommand();
                    newComm.CommandType = CommandType.Text;
                    if (txtQty.Text != "" && int.TryParse(txtQty.Text, out test))
                    {
                        newComm.CommandText = "exec up_Update_PPSKittingShippers @PassedMachineID='" + Form1.dayCode[11] + "',@PassedGMTTime='" + DateTime.Now + "',@PassedLocalTime='" + DateTime.Now + "',@PassedDayCode='" + Form1.dayCode + "',@PassedLastBoxOnPallet=1,@PassedLastBoxOnPalletManual=1,@PassedJobID='" + txtJobID.Text + "',@PassedShipperQty=" + int.Parse(txtQty.ToString());
                    }
                    else
                    {
                        newComm.CommandText = "exec up_Update_PPSKittingShippers @PassedMachineID='" + Form1.dayCode[11] + "',@PassedGMTTime='" + DateTime.Now + "',@PassedLocalTime='" + DateTime.Now + "',@PassedDayCode='" + Form1.dayCode + "',@PassedLastBoxOnPallet=1,@PassedLastBoxOnPalletManual=1,@PassedJobID='" + txtJobID.Text + "',@PassedShipperQty=" + (int)double.Parse(table.Rows[0]["imrManufacturingLotSize"].ToString());
                    }
                    newComm.Connection = Form1.conn;
                    newComm.ExecuteNonQuery();
                    Form1.btnSearch.PerformClick();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
