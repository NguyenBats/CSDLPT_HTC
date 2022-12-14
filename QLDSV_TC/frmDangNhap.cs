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

namespace QLDSV_TC
{

    public partial class frmDangNhap : Form
    {
        private SqlConnection connstr_publisher = new SqlConnection();

        private void LayDSPM(String cmd)
        {
            DataTable dt = new DataTable();
            if (connstr_publisher.State == ConnectionState.Closed)
                connstr_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, connstr_publisher);
            da.Fill(dt);
            connstr_publisher.Close();
            Program.bds_dspm.DataSource = dt;
            cmbKhoa.DataSource = Program.bds_dspm;
            cmbKhoa.DisplayMember = "TENKHOA";
            cmbKhoa.ValueMember = "TENSERVER";
        }

        public frmDangNhap()
        {
            InitializeComponent();
        }



        private int KetNoi_CSDLGOC()
        {
            if (connstr_publisher != null && connstr_publisher.State == ConnectionState.Open)
            {
                connstr_publisher.Close();
            }
            try
            {
                connstr_publisher.ConnectionString = Program.connstr_publisher;
                connstr_publisher.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối về cơ sở dữ liệu gốc. \nBạn xem lại tên Server của Publisher, và tên CSDL trong chuỗi kết nối.\n " + e.Message);
            }
            return 0;
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            if (KetNoi_CSDLGOC() == 0) return;
            LayDSPM("Select * FROM  [dbo].[Get_Subscribes]");
            cmbKhoa.SelectedIndex = 0;
            if (cmbKhoa.SelectedValue.ToString() == "System.Data.DataRowView") return;
            Program.servername = cmbKhoa.SelectedValue.ToString();
        }



        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text.Trim() == "" || txtPass.Text.Trim() == "")
            {
                MessageBox.Show("Login name và mật khẩu không được để trống", "", MessageBoxButtons.OK);
                return;
            }
            Program.mKhoa = cmbKhoa.SelectedIndex;
            Program.mlogin = txtLogin.Text;
            Program.password = txtPass.Text;
            if (Program.KetNoi() == 0)//Kết nối lần 1 bằng user giảng viên thất bại
            {
                Program.mlogin = "SV";
                Program.password = "123456";
                if (Program.KetNoi() == 0) return;//Kết nối lần 2 role sinh viên thất bại
                Program.mKhoa = cmbKhoa.SelectedIndex;
                Program.mloginDN = Program.mlogin;
                Program.passwordDN = Program.password;

                string maSv = txtLogin.Text;
                string password = txtPass.Text;
                string strlenh = "EXEC [dbo].[SP_LOGIN] '" + maSv + "'," + "'" + password + "'";

                Program.myReader = Program.ExecSqlDataReader(strlenh);
                //if (Program.myReader == null)
                //{
                //    Program.username = Program.mlogin;
                //    Program.mGroup = "PKT";
                //    Program.conn.Close();
                //    Program.frmChinh.MANV.Text = "Mã NV: " + Program.username;
                //    Program.frmChinh.HOTEN.Text = "Họ tên: " + Program.username;
                //    Program.frmChinh.NHOM.Text = "Phòng: " + "PKT";
                //    Program.frmChinh.HienThiMenu();
                //    this.Close();
                //    return;
                //}

                if (!Program.myReader.HasRows)
                {
                    MessageBox.Show("Lỗi đăng nhập", "", MessageBoxButtons.OK);
                    return;
                }
                while (Program.myReader.Read()) //data nhiều dòng thì dùng vòng lặp
                {
                    Program.username = Program.myReader.GetString(0);//lay user name
                    Program.mHoTen = Program.myReader.GetString(1);
                    Program.mGroup = Program.myReader.GetString(2);

                }

                if (Convert.IsDBNull(Program.username))
                {
                    MessageBox.Show("Login bạn nhập không có quyền truy cập dữ liệu\n Bạn xem lại username và password", "", MessageBoxButtons.OK);
                    return;
                }
                Program.myReader.Close();
                Program.conn.Close();
                Program.frmChinh.MANV.Text = "Mã SV: " + Program.username;
                Program.frmChinh.HOTEN.Text = "Họ tên: " + Program.mHoTen;
                Program.frmChinh.NHOM.Text = "Lớp: " + Program.mGroup;

                Program.frmChinh.HienThiMenu();
                this.Close();
            }
            else
            {
                Program.mKhoa = cmbKhoa.SelectedIndex;
                Program.mloginDN = Program.mlogin;
                Console.WriteLine("Tên DN:" + Program.mloginDN);
                Program.passwordDN = Program.password;
                String strlenh;
                strlenh = "EXEC [dbo].[SP_Lay_Thong_Tin_GV_Tu_Login] '" + Program.mlogin + "'";
                Program.myReader = Program.ExecSqlDataReader(strlenh);
                //if (Program.myReader == null)
                //{
                //    string maSv = txtLogin.Text;
                //    string password = txtPass.Text;

                //    if (Program.KetNoi() == 0) {
                //        MessageBox.Show("Bạn xem lại username và password", "", MessageBoxButtons.OK);

                //        return;
                //    }

                //    strlenh = "EXEC [dbo].[SP_LOGIN] '" + maSv + "'," + "'" + password + "'";
                //    Program.myReader = Program.ExecSqlDataReader(strlenh);
                //    if (Program.myReader == null)
                //    {
                //        Program.username = Program.mlogin;
                //        Program.mGroup = "PKT";
                //        Program.conn.Close();
                //        Program.frmChinh.MANV.Text = "Mã NV: " + Program.username;
                //        Program.frmChinh.HOTEN.Text = "Họ tên: " + Program.username;
                //        Program.frmChinh.NHOM.Text = "Phòng: " + "PKT";
                //        Program.frmChinh.HienThiMenu();
                //        this.Close();
                //        return;
                //    }

                //};
                if (!Program.myReader.HasRows)
                {
                    MessageBox.Show("Lỗi đăng nhập", "", MessageBoxButtons.OK);
                    return;
                }
                while (Program.myReader.Read()) //data nhiều dòng thì dùng vòng lặp
                {
                    Program.username = Program.myReader.GetString(0);//lay user name
                    Program.mGroup = Program.myReader.GetString(2);
                    if (Program.mGroup == "PKT")
                    {
                        Program.username = Program.mlogin;
                        Program.conn.Close();
                        Program.frmChinh.MANV.Text = "Mã NV: " + Program.username;
                        Program.frmChinh.HOTEN.Text = "Họ tên: " + Program.username;
                        Program.frmChinh.NHOM.Text = "Phòng: " + "PKT";
                        Program.frmChinh.HienThiMenu();
                        Program.myReader.Close();
                        this.Close();
                        return;
                    }
                    else
                        Program.mHoTen = Program.myReader.GetString(1);

                }

                if (Convert.IsDBNull(Program.username))
                {
                    MessageBox.Show("Login bạn nhập không có quyền truy cập dữ liệu\n Bạn xem lại username và password", "", MessageBoxButtons.OK);
                    return;
                }
                Program.myReader.Close();
                Program.conn.Close();
                Program.frmChinh.HienThiMenu();
                this.Close();
            }


        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
            Program.frmChinh.Close();
        }

        private void cmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbKhoa.SelectedValue.ToString() == "System.Data.DataRowView") return;
                Program.servername = cmbKhoa.SelectedValue.ToString();
            }
            catch (Exception ex) { }
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
