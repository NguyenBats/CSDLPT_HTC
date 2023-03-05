using QLDSV_TC.BO;
using QLDSV_TC.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace QLDSV_TC
{
    public partial class frmMonHoc : Form
    {

        public frmMonHoc()
        {
            InitializeComponent();
            
        }

        private void FormatFlex()
        {
            try
            {
                flexMonHoc.Width = 1600;
                flexMonHoc.Cols[0].Visible = false;
                flexMonHoc.Cols["MAMH"].Caption = "Mã môn học";
                flexMonHoc.Cols["TENMH"].Caption = "Tên môn học";
                flexMonHoc.Cols["SOTIET_LT"].Caption = "Số tiết lý thuyết";
                flexMonHoc.Cols["SOTIET_TH"].Caption = "Số tiết thực hành";

                flexMonHoc.Cols["ROWGUID"].Visible = false;
                flexMonHoc.Rows[0].Height = 50;
                flexMonHoc.Cols["MAMH"].Width = 300;
                flexMonHoc.Cols["TENMH"].Width = 300;
                flexMonHoc.Cols["SOTIET_LT"].Width = 300;
                flexMonHoc.Cols["SOTIET_TH"].Width = 300;



            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi format lưới.\n" + ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void refreshData()
        {
            //Lấy dữ liệu sinh viên đổ ra lưới
            MonHocDAO monhocDAO = new MonHocDAO();
            DataTable dtbMonHoc = monhocDAO.GetAll();
            flexMonHoc.DataSource = dtbMonHoc;
            FormatFlex();
        }


        private void frmMonHoc_Load(object sender, EventArgs e)
        {
            refreshData();
            cmbKhoa.DataSource = Program.bds_dspm; //sao chep bds_pm o form dang nhap
            cmbKhoa.DisplayMember = "TENKHOA";
            cmbKhoa.ValueMember = "TENSERVER";
            cmbKhoa.SelectedIndex = Program.mKhoa;         
            if (Program.mGroup == "PGV")
            {
                cmbKhoa.Enabled = true;//bat tat theo phan quyen
                btnThem.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = false;
            }
            else
            {
                cmbKhoa.Enabled = false;
                btnThem.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = true;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMaMH.Text = "";
            txtMaMH.Enabled = true;
            txtTenMH.Text = "";
            txtSoTietLT.Text = "";
            txtSoTietTH.Text = "";
            groupBox1.Enabled = true;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (bdsLTC.Count > 0)
            //{
            //    MessageBox.Show("Không thể xóa môn học này vì đã có lớp tín chỉ", "");
            //    return;
            //}
            if (MessageBox.Show("Bạn có muốn xóa lớp này không?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    MonHocDAO monHocDAO = new MonHocDAO();
                    monHocDAO.Delete(txtMaMH.Text.Trim());
                    MessageBox.Show("Xóa môn học thành công.\n", "", MessageBoxButtons.OK);
                    refreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa môn học, vui lòng thử lại lại.\n" + ex.Message, "", MessageBoxButtons.OK);

                    return;
                }
            }
            //if (bdsMonHoc.Count == 0) btnXoa.Enabled = false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //kiem tra ma lop khong trung tren cac phan manh +++
            if (txtMaMH.Text.Trim() == "")
            {
                MessageBox.Show("Mã môn học không được để trống!", "", MessageBoxButtons.OK);
                txtMaMH.Focus();
                return;
            }
            if (txtTenMH.Text.Trim() == "")
            {
                MessageBox.Show("Tên môn học không được để trống!", "", MessageBoxButtons.OK);
                txtTenMH.Focus();
                return;
            }
            if (txtSoTietLT.Text.Trim() == "")
            {
                MessageBox.Show("Số tiết lý thuyết không được để trống!", "", MessageBoxButtons.OK);
                txtSoTietLT.Focus();
                return;
            }
            if (txtSoTietTH.Text.Trim() == "")
            {
                MessageBox.Show("Số tiết thực hành không được để trống!", "", MessageBoxButtons.OK);
                txtSoTietTH.Focus();
                return;
            }
            try
            {
                MonHocDAO monHocDAO = new MonHocDAO();
                MonHoc monHoc = new MonHoc();
                monHoc.MaMH = txtMaMH.Text.Trim();
                monHoc.TenMH = txtTenMH.Text.Trim();
                monHoc.SoTietLT = Convert.ToInt32(txtSoTietLT.Text.Trim());
                monHoc.SoTietTH = Convert.ToInt32(txtSoTietTH.Text.Trim());
                if (!btnThem.Enabled)
                {
                    // Đang thêm mới
                    monHocDAO.Insert(monHoc);
                    MessageBox.Show("Thêm mới môn học thành công.\n", "", MessageBoxButtons.OK);
                }
                else
                {
                    monHocDAO.Update(monHoc);
                    MessageBox.Show("Cập nhật thông tin môn học thành công.\n", "", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm môn học.\n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
            groupBox1.Enabled = false;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            refreshData();
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            groupBox1.Enabled = false;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
            Program.frmChinh.Close();
        }

        private void cmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKhoa.SelectedValue.ToString() == "System.Data.DataRowView") return;
            Program.servername = cmbKhoa.SelectedValue.ToString();
            if (cmbKhoa.SelectedIndex != Program.mKhoa)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }
            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới.\n", "", MessageBoxButtons.OK);
            }
            
        }

        private void flexMonHoc_Click(object sender, EventArgs e)
        {
            try
            {
                groupBox1.Enabled = true;
                btnGhi.Enabled = true;
                btnPhucHoi.Enabled = false;
                int row = flexMonHoc.Row;
                txtMaMH.Text = flexMonHoc[row, "MAMH"].ToString();
                txtMaMH.Enabled = false;
                txtTenMH.Text = flexMonHoc[row, "TENMH"].ToString();
                txtSoTietLT.Text = flexMonHoc[row, "SOTIET_LT"].ToString();
                txtSoTietTH.Text = flexMonHoc[row, "SOTIET_TH"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi", ex.ToString(), MessageBoxButtons.OK);
            }
        }
    }
}
