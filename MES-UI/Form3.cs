using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES_UI
{
    public partial class Form3 : Form
    {
        OracleCommand cmd = new OracleCommand();
        OracleDataReader rdr;
        OracleConnection conn = new OracleConnection(strConn);
        static string strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                                "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=hr ;Password=hr;";
        OracleDataAdapter adapt = new OracleDataAdapter();
        DataSet ds = new DataSet();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn.Open();
            cmd.Connection = conn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = $"select PMId from PdMaster where PMName = '{comboBox1.SelectedItem}'";
                rdr = cmd.ExecuteReader();
                rdr.Read();
                string id = rdr["PMId"].ToString();

                cmd.CommandText = $"insert into Stock(StId, StDate, StQty, PMId) values('St'||trim(to_char(Stock_seq.nextval,'000')),'{dateTimePicker1.Text}',{textBox1.Text},'{id}')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("완료되었습니다.", "알림");
            }
            catch
            {
               // MessageBox.Show("","알림");
            }
            

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd.CommandText = $"select PMUnit from PdMaster where PMName = '{comboBox1.SelectedItem}'";
            rdr = cmd.ExecuteReader();
            rdr.Read();
            string unit = rdr["PMUnit"].ToString();
            label4.Text = unit;
        }
    }
}
