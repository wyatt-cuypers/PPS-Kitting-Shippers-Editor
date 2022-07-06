using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPSKittingShippersEditor
{
    public partial class JobIDForm : Form 
    {
        string testString = "";
        SqlCommand comm = new SqlCommand();
        
        public JobIDForm()
        {
            InitializeComponent();
            Form1.DB_Connect();
        }

        // ***
        //Data binding methods to dynamically update the quantity textbox
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private string name;
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }
        // ***

        //On this click, the method ensures the Job ID is valid, and if it is, it writes and executes a SQL statement and then updates the data gridview control
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblValid.Text == "Valid Job ID" && txtQty.Text != "")
            {
                try
                {
                    decimal test = 0;
                    string commandText = "";
                    SqlCommand newComm = new SqlCommand();
                    newComm.CommandType = CommandType.Text;
                    if (decimal.TryParse(txtQty.Text, out test))
                    {
                        commandText = "exec up_Update_PPSKittingShippers @PassedMachineID='" + Form1.dayCode[11] + "',@PassedGMTTime='" + DateTime.Now + "',@PassedLocalTime='" + DateTime.Now + "',@PassedDayCode='" + Form1.dayCode + "',@PassedLastBoxOnPallet=1,@PassedLastBoxOnPalletManual=1,@PassedJobID='" + txtJobID.Text + "',@PassedShipperQty=" + decimal.Parse(txtQty.Text);
                    }
                    newComm.CommandText = commandText;
                    newComm.Connection = Form1.conn;
                    newComm.ExecuteNonQuery();
                    Form1.btnSearch.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            this.Close();
        }

        // ***
        //Delegate methods used to avoid cross-thread exceptions
        private delegate void AppendTextBoxDelegate(TextBox txt, string str);
        private static void AppendTextBox(TextBox txt, string str)
        {
            if (txt.InvokeRequired)
            {
                txt.Invoke(new AppendTextBoxDelegate(AppendTextBox), new object[] { txt, str });
            }
            else
            {
                txt.Text = str;
            }
        }
        private delegate void ChangeLabelDelegate(Label lbl, string str);
        private static void ChangeLabel(Label lbl, string str)
        {
            if (lbl.InvokeRequired)
            {
                lbl.Invoke(new ChangeLabelDelegate(ChangeLabel), new object[] { lbl, str });
            }
            else
            {
                if (str == "Invalid Job ID")
                {
                    lbl.ForeColor = Color.Red;
                }
                else
                {
                    lbl.ForeColor = Color.Green;
                }
                lbl.Text = str;
            }
        }
        // ***

        //This background worker loads data into a data table and then looks to see if the text in the job id text matches a record in the data table. 
        //If no records match, nothing is entered into the quantity text box, and an error message is written to the label lblValid
        //If there is a matching record, the corresponding quantity is updated into the quantity text box, and a valid message is written to the label lblValid
        //If a checkbox on the form is checked, data binding is essentially shut off, and the user is allowed to choose the quantity
        //However, it still checks to see if the job id is valid, and the label lblValid is updated accordingly
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            AppendTextBoxDelegate del = new AppendTextBoxDelegate(AppendTextBox);
            ChangeLabelDelegate lb = new ChangeLabelDelegate(ChangeLabel);
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT jmpJobID, imrManufacturingLotSize FROM M1_P1.dbo.Jobs Jobs INNER JOIN M1_P1.dbo.PartRevisions PartRevisions ON jmpPartID = imrPartID AND jmpPartRevisionID = imrPartRevisionID WHERE jmpJobID = '" + txtJobID.Text + "'";
            comm.Connection = Form1.conn;
            SqlDataReader reader = comm.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            if (!chkUserDefined.Checked)
            {
                try
                {
                    if (txtJobID.Text == "")
                    {
                        del(txtQty, "");
                    }
                    if (table.Rows.Count == 0)
                    {
                        lb(lblValid, "Invalid Job ID");
                        del(txtQty, "");
                    }
                    else
                    {
                        OnPropertyChanged(txtJobID.Text);
                        SetField<string>(ref testString, table.Rows[0]["imrManufacturingLotSize"].ToString());
                        lb(lblValid, "Valid Job ID");
                        del(txtQty, testString);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                if (table.Rows.Count == 0)
                {
                    lb(lblValid, "Invalid Job ID");
                }
                else
                {
                    lb(lblValid, "Valid Job ID");
                }
            }
        }

        //Used to constantly run the background worker
        private void tmrHalfSec_Tick(object sender, EventArgs e)
        {
            if (!bgwMain.IsBusy)
            {
                bgwMain.RunWorkerAsync();
            }
        }
    }
}
