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
    public partial class Form2 : Form
    {

        OracleCommand cmd = new OracleCommand();
        OracleConnection conn = new OracleConnection(strConn);
        static string strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                                "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=hr ;Password=hr;";

        OracleDataAdapter adapt = new OracleDataAdapter();
        static string main_query = "select S.StId as 재고아이디, PD.PMName as 재고명, S.StQty as 수량, PD.PMUnit as 단위, S.StDate as 입고날짜 " +
                                   "from PdMaster PD join Stock S on PD.PMId = S.PMId ";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn.Open();
            cmd.Connection = conn;
            ViewDetail();
            
        }
        public void ViewDetail()
        {
            string query_1 = main_query + "order by stid";
            OracleDataAdapter adapt = new OracleDataAdapter();
            adapt.SelectCommand = new OracleCommand(query_1, conn);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] col_value = new string[] { comboBox1.Text, dateTimePicker1.Text, dateTimePicker2.Text };
            string[] col_name = new string[] { "PMName", "StDate"};
            view(main_query,col_name, col_value);

        }
       
        public void view(string query, string[] col_name, string[] col_value)
        {
            query = main_query;

            if (col_value[0] == "")
            {
                query += $" where S.StDate between '{col_value[1]}' and '{col_value[2]}' order by stid";
                OracleDataAdapter adapt = new OracleDataAdapter();
                adapt.SelectCommand = new OracleCommand(query, conn);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            else
            {
                query += $" where S.StDate between '{col_value[1]}' and '{col_value[2]}' and PD.PMName = '{col_value[0]}' order by stid";
                        
                OracleDataAdapter adapt = new OracleDataAdapter();
                adapt.SelectCommand = new OracleCommand(query, conn);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ViewDetail();
        }
    }
}
