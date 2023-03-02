using DevExpress.DataAccess.Native.Excel;
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

namespace QLDSV_TC
{
    public partial class frmLop : Form
    {
        public frmLop()
        {
            InitializeComponent();
        }


        private void refreshData()
        {
            try
            {
                //Lấy dữ liệu lớp đổ ra lưới
                LopDAO lopDAO = new LopDAO();
                DataTable dtbSinhVien = lopDAO.GetAll();
                if (dtbSinhVien != null && dtbSinhVien.Rows.Count > 0)
                {
                    flexLop.DataSource = dtbSinhVien;

                }
                else
                {
                    btnXoa.Enabled = false;
                }

                FormatFlex();
            }
            catch (Exception e)
            {
            }

        }

        private void FormatFlex()
        {
            try
            {
                flexLop.Width = 1600;
                flexLop.Cols[0].Visible = false;
                flexLop.Cols["MALOP"].Caption = "Mã lớp";
                flexLop.Cols["TENLOP"].Caption = "Tên lớp";
                flexLop.Cols["KHOAHOC"].Caption = "Khóa học";
                flexLop.Cols["MAKHOA"].Caption = "Mã khoa";
                flexLop.Cols["ROWGUID"].Visible = false;
                flexLop.Cols["MAKHOA"].Visible = false;
                flexLop.Rows[0].Height = 50;
                flexLop.Cols["MALOP"].Width = 200;
                flexLop.Cols["TENLOP"].Width = 200;
                flexLop.Cols["KHOAHOC"].Width = 200;
                flexLop.Cols["MAKHOA"].Width = 200;



            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi format lưới.\n" + ex.Message, "", MessageBoxButtons.OK);
            }
        }


        private void frmLop_Load(object sender, EventArgs e)
        {
            cmbKhoa.DataSource = Program.bds_dspm; //sao chep bds_pm o form dang nhap
            cmbKhoa.DisplayMember = "TENKHOA";
            cmbKhoa.ValueMember = "TENSERVER";
            cmbKhoa.SelectedIndex = Program.mKhoa;
            if (Program.mGroup == "PGV")
            {
                cmbKhoa.Enabled = true;//bat tat theo phan quyen                
            }
            else
            {
                cmbKhoa.Enabled = false;
            }
            btnThem.Enabled = btnGhi.Enabled = btnXoa.Enabled = true;
            btnPhucHoi.Enabled = false;
            txtMaLop.Enabled = false;
            refreshData();
        }


        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (bdsSV.Count > 0)
            //{
            //    MessageBox.Show("Không thể xóa lớp này vì lớp đã có sinh viên", "");
            //    return;
            //}
            if (MessageBox.Show("Bạn có muốn xóa lớp này không?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    SinhVienDAO sinhVienDAO = new SinhVienDAO();
                    sinhVienDAO.Delete(txtMaLop.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa lớp, vui lòng xóa lại.\n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
            }
            refreshData();
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////kiem tra ma lop khong trung tren cac phan manh +++
            if (txtMaLop.Text.Trim() == "")
            {
                MessageBox.Show("Mã không được để trống!", "", MessageBoxButtons.OK);
                txtMaLop.Focus();
                return;
            }
            else
            {
                string prefix = "D" + Convert.ToInt32(txtStartYear.Text) % 2000;
                if (!txtMaLop.Text.Contains(prefix))
                {
                    MessageBox.Show("Mã lớp phải có tiền tố " + prefix, "", MessageBoxButtons.OK);
                    txtMaLop.Focus();
                    return;
                }
            }
            if (txtTenLop.Text.Trim() == "")
            {
                MessageBox.Show("Tên lớp không được để trống!", "", MessageBoxButtons.OK);
                txtTenLop.Focus();
                return;
            }
            if (txtStartYear.Text.Trim() == "")
            {
                MessageBox.Show("Năm bắt đầu không được để trống!", "", MessageBoxButtons.OK);
                txtStartYear.Focus();
                return;
            }
            if (txtEndYear.Text.Trim() == "")
            {
                MessageBox.Show("Năm kết thúc không được để trống!", "", MessageBoxButtons.OK);
                txtEndYear.Focus();
                return;
            }
            try
            {
                String malop = txtMaLop.Text;
                LopDAO lopDAO = new LopDAO();
                Lop lop = new Lop();
                lop.MaLop = malop;
                lop.TenLop = txtTenLop.Text.Trim();
                lop.KhoaHoc = txtStartYear.Text.Trim() + "-" + txtEndYear.Text.Trim();
                lop.MaKhoa = cmbKhoa.SelectedValue.ToString(); ;
                if (btnThem.Enabled == false)
                {
                    if (lopDAO.CheckMaLop(malop))
                        return;
                   
                    if (!lopDAO.CreateLop(lop))
                        return;
                    else
                    {
                        MessageBox.Show("Thêm mới lớp thành công.\n", "Thành công", MessageBoxButtons.OK);

                    }
                    textPanel.Enabled = false;
                    btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
                    btnGhi.Enabled = btnPhucHoi.Enabled = false;
                }
                else
                {
                    if (!lopDAO.UpdateLop(lop))
                        return;
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin lớp thành công.\n", "Thành công", MessageBoxButtons.OK);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm lớp.\n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }


        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            flexLop.Enabled = true;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            txtMaLop.Enabled = false;
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
            Program.frmChinh.Close();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            txtMaLop.Text = "";
            txtTenLop.Text = "";
            txtStartYear.Text = "";
            txtEndYear.Text = "";
            textPanel.Enabled = true;
            txtMaLop.Enabled = true;
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
            refreshData();
        }

        private void txtStartYear_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtStartYear.Text))
                {
                    return;
                }
                if (Convert.ToInt32(txtStartYear.Text) > 2000 && Convert.ToInt32(txtStartYear.Text) < 2050)
                {
                    int endYear = Convert.ToInt32(txtStartYear.Text) + 4;
                    txtEndYear.Text = Convert.ToString(endYear);
                    string malop = "D" + Convert.ToInt32(txtStartYear.Text) % 2000 + "CQ";
                    txtMaLop.Text = malop;
                }
            }
            catch (Exception)
            {

            }
        }

        private void flexLop_Click(object sender, EventArgs e)
        {
            try
            {
                btnGhi.Enabled = true;
                btnPhucHoi.Enabled = false;
                int row = flexLop.Row;
                txtMaLop.Text = flexLop[row, "MALOP"].ToString();
                txtMaLop.Enabled = false;
                txtTenLop.Text = flexLop[row, "TENLOP"].ToString();
                txtStartYear.Text = flexLop[row, "KHOAHOC"].ToString().Split('-')[0];
                txtEndYear.Text = flexLop[row, "KHOAHOC"].ToString().Split('-')[1];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi", ex.ToString(), MessageBoxButtons.OK);
            }
        }
    }
}
