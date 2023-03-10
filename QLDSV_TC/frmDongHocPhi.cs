using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmDongHocPhi : Form
    {
        public frmDongHocPhi()
        {
            InitializeComponent();
        }

        private void FormatFlexHocPhi()
        {
            try
            {
                flexHocPhi.Width = 1600;
                flexHocPhi.Cols[0].Visible = false;
                flexHocPhi.Cols["MASV"].Caption = "Mã sinh viên";
                flexHocPhi.Cols["NIENKHOA"].Caption = "Niên khóa";
                flexHocPhi.Cols["HOCKY"].Caption = "Học kỳ";
                flexHocPhi.Cols["HOCPHI"].Caption = "Học phí";
                flexHocPhi.Cols["DADONG"].Caption = "Đã đóng";
                flexHocPhi.Cols["CONLAI"].Caption = "Còn lại";
                flexHocPhi.Rows[0].Height = 50;
                flexHocPhi.Cols["MASV"].Width = 250;
                flexHocPhi.Cols["NIENKHOA"].Width = 250;
                flexHocPhi.Cols["HOCKY"].Width = 250;
                flexHocPhi.Cols["HOCPHI"].Width = 250;
                flexHocPhi.Cols["DADONG"].Width = 250;
                flexHocPhi.Cols["CONLAI"].Width = 250;


            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi format lưới học phí.\n" + ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void FormatFlexChiTietHocPhi()
        {
            try
            {
                //MASV, NIENKHOA, HOCKY, NGAYDONG, SOTIENDONG
                flexChiTietHocPhi.Width = 1600;
                flexChiTietHocPhi.Cols[0].Visible = false;
                flexChiTietHocPhi.Cols["MASV"].Caption = "Mã sinh viên";
                flexChiTietHocPhi.Cols["NIENKHOA"].Caption = "Niên khóa";
                flexChiTietHocPhi.Cols["HOCKY"].Caption = "Học kỳ";
                flexChiTietHocPhi.Cols["NGAYDONG"].Caption = "Ngày đóng";
                flexChiTietHocPhi.Cols["SOTIENDONG"].Caption = "Số tiền đóng";
                flexChiTietHocPhi.Rows[0].Height = 50;
                flexChiTietHocPhi.Cols["MASV"].Width = 250;
                flexChiTietHocPhi.Cols["NIENKHOA"].Width = 250;
                flexChiTietHocPhi.Cols["HOCKY"].Width = 250;
                flexChiTietHocPhi.Cols["NGAYDONG"].Width = 250;
                flexChiTietHocPhi.Cols["SOTIENDONG"].Width = 250;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi format lưới học phí chi tiết.\n" + ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void RefreshDataHocPhi()
        {
            string maSV = cboSinhVien.SelectedValue.ToString().Trim();
            HocPhiDAO hocPhiDAO = new HocPhiDAO();
            DataTable dtbHocPhi = hocPhiDAO.DanhSachHocPhi(maSV);
            flexHocPhi.DataSource = dtbHocPhi;
            FormatFlexHocPhi();
        }

        private void frmDongHocPhi_Load(object sender, EventArgs e)
        {
        }


        private void btnThemHocPhi_Click(object sender, EventArgs e)
        {
            HocPhiDAO hocPhiDAO = new HocPhiDAO();
            HocPhi hocphi = new HocPhi();
            hocphi.MASV = cboSinhVien.SelectedValue.ToString().Trim();
            hocphi.NIENKHOA = cboNienKhoa.SelectedValue.ToString().Trim();
            hocphi.HOCKY = Convert.ToInt32(cboHocKy.SelectedValue);
            hocphi.HOCPHI = Convert.ToInt32(txthocPhi.Text.ToString());
            hocPhiDAO.ThemHocPhi(hocphi);
            RefreshDataHocPhi();
        }

        private void btnThemChiTietHocPhi_Click(object sender, EventArgs e)
        {
            try
            {
                int row = flexHocPhi.Row;
                string maSV = Convert.ToString(flexHocPhi.GetData(row, "MASV"));
                string nienKhoa = Convert.ToString(flexHocPhi.GetData(row, "NIENKHOA"));
                int hocKy = Convert.ToInt32(flexHocPhi.GetData(row, "HOCKY"));
                int conlai = Convert.ToInt32(flexHocPhi.GetData(row, "CONLAI"));
                int longSum = Convert.ToInt32(txtChiTietTienHocPhi.Text);
                if (longSum > conlai)
                {
                    MessageBox.Show("Vui lòng nhập số tiền nhỏ hơn số tiền còn lại", "Thông báo ", MessageBoxButtons.OK);
                    return;
                }
                HocPhiDAO hocPhiDAO = new HocPhiDAO();
                HocPhi hocphi = new HocPhi();
                hocphi.MASV = maSV;
                hocphi.NIENKHOA = nienKhoa;
                hocphi.HOCKY = hocKy;
                hocphi.HOCPHI = longSum;
                hocPhiDAO.ThemChiTietHocPhi(hocphi);
                MessageBox.Show("Nhập học phí thành công", "", MessageBoxButtons.OK);
                flexHocPhi_Click(null, null);
                RefreshDataHocPhi();

            }
            catch (Exception objex)
            {
                MessageBox.Show(objex.ToString(), "Thông báo ", MessageBoxButtons.OK);
            }

        }

        private void txtChiTietTienHocPhi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && txtChiTietTienHocPhi.Text.Length < 9; ;
        }

        private void txthocPhi_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && txthocPhi.Text.Length < 9;

        }

        private void btnTaoTaiKhoan_Click(object sender, EventArgs e)
        {
            frmTaoLoginPKT frmTaoLoginPKT = new frmTaoLoginPKT();
            frmTaoLoginPKT.ShowDialog(this);
        }

        private void flexHocPhi_Click(object sender, EventArgs e)
        {
            int row = flexHocPhi.Row;
            HocPhiDAO hocPhiDAO = new HocPhiDAO();
            string maSV = Convert.ToString(flexHocPhi.GetData(row, "MASV"));
            string nienKhoa = Convert.ToString(flexHocPhi.GetData(row, "NIENKHOA"));
            int hocKy = Convert.ToInt32(flexHocPhi.GetData(row, "HOCKY"));
            DataTable dtbChiTietHocPhi = hocPhiDAO.GetCTDongHocPhi(maSV, nienKhoa, hocKy);
            flexChiTietHocPhi.DataSource = dtbChiTietHocPhi;
            FormatFlexChiTietHocPhi();
            lbCurrentSelectedRow.Text = "Đang chọn : " + String.Join(" - ", new string[] { "Mã sinh viên: " + maSV, "Niên khóa: " + nienKhoa, "Học kỳ: " + hocKy });
        }

        private void flexChiTietHocPhi_Click(object sender, EventArgs e)
        {
           
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshDataHocPhi();
        }
    }
}
