using QLDSV_TC.DAO;
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

namespace QLDSV_TC
{
    public partial class frmDKLTC : Form
    {
        public frmDKLTC()
        {
            InitializeComponent();
        }



        private void frmDKLTC_Load(object sender, EventArgs e)
        {



        }

        private void FormatFlex()
        {
            try
            {

                flexLopTinChi.Width = 1600;
                flexLopTinChi.Cols[0].Visible = false;
                flexLopTinChi.Cols["MALTC"].Caption = "Mã lớp tín chỉ";
                flexLopTinChi.Cols["MAMH"].Caption = "Mã môn học";
                flexLopTinChi.Cols["TENMH"].Caption = "Tên môn học";
                flexLopTinChi.Cols["NHOM"].Caption = "Nhóm";
                flexLopTinChi.Cols["HOTEN_GV"].Caption = "Giảng viên";
                flexLopTinChi.Cols["SO_SV_DANG_KI"].Caption = "Số sinh viên đăng ký";
                flexLopTinChi.Cols["DANGKY"].Caption = "Đăng ký";
                flexLopTinChi.Rows[0].Height = 50;
                flexLopTinChi.Cols["MALTC"].Width = 200;
                flexLopTinChi.Cols["MAMH"].Width = 200;
                flexLopTinChi.Cols["TENMH"].Width = 200;
                flexLopTinChi.Cols["NHOM"].Width = 200;
                flexLopTinChi.Cols["HOTEN_GV"].Width = 200;
                flexLopTinChi.Cols["SO_SV_DANG_KI"].Width = 200;
                flexLopTinChi.Cols["DANGKY"].Width = 200;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi format lưới.\n" + ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void refreshData()
        {
            String nienKhoa = cboNienKhoa.SelectedValue.ToString();
            String hocKi = cboHocKy.SelectedValue.ToString();
            LopTinChiDAO lctDAO = new LopTinChiDAO();
            flexLopTinChi.DataSource = lctDAO.GetLTCByNienKhoaHocKy(nienKhoa, Convert.ToInt32(hocKi), Program.username); ;
            FormatFlex();
        }

        private void btnTimLTC_Click(object sender, EventArgs e)
        {
            try
            {
                refreshData();
            }
            catch (Exception objEx)
            {
                MessageBox.Show("Lỗi tra cứu, bạn xem lại niên khóa và học kì", objEx.Message, MessageBoxButtons.OK);
                return;
            }

        }

        private void btnDKLTC_Click(object sender, EventArgs e)
        {
            try
            {
                String strlenh = "Declare @dk as DanhSachDangKiNew ";
                DataTable dataSource = (DataTable)flexLopTinChi.DataSource;
                foreach (DataRow dr in dataSource.Rows)
                {
                    String maLTC = dr["MALTC"].ToString();
                    if (dr["DANGKY"].ToString().Equals("False"))
                        strlenh += "insert into @dk(MALTC, MASV, LOAI) values(" + maLTC + ", '" + Program.username + "',0)";//Huy
                    else
                        strlenh += "insert into @dk(MALTC, MASV, LOAI) values(" + maLTC + ", '" + Program.username + "',1)";//Dang ky
                }
                strlenh += "exec DANG_KI_LTC @DS = @dk";
                Program.ExecSqlNonQuery(strlenh);
                MessageBox.Show("Lưu thông tin đăng ký thành công", "", MessageBoxButtons.OK);
                refreshData();
            }
            catch (Exception objEx){
                MessageBox.Show("Lỗi đăng ký lớp tín chỉ", objEx.Message, MessageBoxButtons.OK);
                return;
            }
        }
    }
}
