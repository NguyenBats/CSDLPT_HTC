﻿using C1.Win.C1FlexGrid;
using DevExpress.Utils.Extensions;
using DevExpress.XtraRichEdit.Model;
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
    public partial class frmSinhVien : Form
    {
        public frmSinhVien()
        {
            InitializeComponent();
        }

        private void FormatFlex()
        {
            try
            {
                flexSinhVien.Width = 1600;
                flexSinhVien.Cols[0].Visible = false;
                flexSinhVien.Cols["MASV"].Caption = "Mã sinh viên";
                flexSinhVien.Cols["HO"].Caption = "Họ";
                flexSinhVien.Cols["TEN"].Caption = "Tên";
                flexSinhVien.Cols["SEXSTRING"].Caption = "Giới tính";
                flexSinhVien.Cols["OFFSTRING"].Caption = "Trạng thái";
                flexSinhVien.Cols["DIACHI"].Caption = "Địa chỉ";
                flexSinhVien.Cols["NGAYSINH"].Caption = "Mgày sinh";
                flexSinhVien.Cols["MALOP"].Caption = "Mã lớp";
                flexSinhVien.Cols["DANGHIHOC"].Visible = false;
                flexSinhVien.Cols["PHAI"].Visible = false;
                flexSinhVien.Cols["PASSWORD"].Visible = false;
                flexSinhVien.Cols["ROWGUID"].Visible = false;
                flexSinhVien.Rows[0].Height = 50;
                flexSinhVien.Cols["MASV"].Width = 200;
                flexSinhVien.Cols["HO"].Width = 200;
                flexSinhVien.Cols["TEN"].Width = 200;
                flexSinhVien.Cols["PHAI"].Width = 200;
                flexSinhVien.Cols["DIACHI"].Width = 200;
                flexSinhVien.Cols["NGAYSINH"].Width = 200;
                flexSinhVien.Cols["MALOP"].Width = 200;
                flexSinhVien.Cols["DANGHIHOC"].Width = 200;
                flexSinhVien.Cols["PASSWORD"].Width = 200;
                flexSinhVien.Cols["SEXSTRING"].Width = 200;
                flexSinhVien.Cols["OFFSTRING"].Width = 200;


            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi format lưới.\n" + ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void refreshData()
        {
            //Lấy dữ liệu sinh viên đổ ra lưới
            SinhVienDAO sinhVienDAO = new SinhVienDAO();
            DataTable dtbSinhVien = sinhVienDAO.GetAll();
            DataColumn sexStringColumn = new DataColumn("SEXSTRING");
            DataColumn offStringColumn = new DataColumn("OFFSTRING");
            sexStringColumn.DataType = typeof(string);
            offStringColumn.DataType = typeof(string);
            dtbSinhVien.Columns.Add(sexStringColumn);
            dtbSinhVien.Columns.Add(offStringColumn);
            foreach (DataRow row in dtbSinhVien.Rows)
            {
                row["SEXSTRING"] = Convert.ToBoolean(row["PHAI"]) ? "Nam" : "Nữ";
                row["OFFSTRING"] = Convert.ToBoolean(row["DANGHIHOC"]) ? "Đã nghỉ học" : "Đang học";
            }
            flexSinhVien.DataSource = dtbSinhVien;
            FormatFlex();
        }


        private void frmSinhVien_Load(object sender, EventArgs e)
        {

            refreshData();
            // TODO: This line of code loads data into the 'qLDSV_TCDataSet.SINHVIEN' table. You can move, or remove it, as needed.
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
            cmbKhoa.Enabled = true;//them vao de test
            txtMatKhau.Visible = true;
            labelMatKhau.Visible = true;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMaSV.Text = "";
            txtMaSV.Enabled = true;
            txtHo.Text = "";
            txtTen.Text = "";
            cboGioiTinh.SelectedIndex = 0;
            cboDaNghiHoc.SelectedIndex = 0;
            dateEditNgaySinh.DateTime = System.DateTime.Now;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            flexSinhVien.Enabled = false;
            txtMatKhau.Visible = true;
            labelMatKhau.Visible = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bdsDangKy.Count > 0)
            {
                MessageBox.Show("Không thể xóa môn học này vì đã có lớp tín chỉ", "");
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa sinh viên này không?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    SinhVienDAO sinhVienDAO = new SinhVienDAO();
                    sinhVienDAO.Delete(txtMaSV.Text.Trim());
                    refreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa sinh viên, vui lòng xóa lại.\n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //kiem tra ma sv khong trung tren cac phan manh +++
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaSV.Text))
                {
                    MessageBox.Show("Mã sinh viên không được để trống!", "", MessageBoxButtons.OK);
                    txtMaSV.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtHo.Text))
                {
                    MessageBox.Show("Họ không được để trống!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTen.Text))
                {
                    MessageBox.Show("Tên không được để trống!", "", MessageBoxButtons.OK);
                    txtTen.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
                {
                    MessageBox.Show("Địa chỉ không được để trống!", "", MessageBoxButtons.OK);
                    txtDiaChi.Focus();
                    return;
                }
                SinhVienDAO sinhVienDAO = new SinhVienDAO();
                SinhVien sinhVien = new SinhVien();
                sinhVien.Masv = txtMaSV.Text.Trim();
                sinhVien.Ho = txtHo.Text.Trim();
                sinhVien.Ten = txtTen.Text.Trim();
                sinhVien.Phai = Convert.ToBoolean(cboGioiTinh.SelectedValue);
                sinhVien.Diachi = txtDiaChi.Text.Trim();
                sinhVien.Ngaysinh = dateEditNgaySinh.DateTime;
                sinhVien.Malop = cboLop.SelectedValue.ToString().Trim();
                sinhVien.Danghihoc = Convert.ToBoolean(cboDaNghiHoc.SelectedValue);
                sinhVien.Password = txtMatKhau.Text.Trim();
                if (!btnThem.Enabled)
                {
                    // Đang thêm mới
                    sinhVienDAO.Insert(sinhVien);
                }
                else
                {
                    sinhVienDAO.Update(sinhVien);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm sinh viên.\n{ex.Message}", "", MessageBoxButtons.OK);
                return;
            }
            flexSinhVien.Enabled = true;
            txtMatKhau.Visible = true;
            labelMatKhau.Visible = true;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            refreshData();
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            flexSinhVien.Enabled = true;
            groupBox1.Enabled = false;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            txtMatKhau.Visible = true;
            labelMatKhau.Visible = true;
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
            refreshData();
        }

        private void flexSinhVien_Click(object sender, EventArgs e)
        {
            try
            {
                btnGhi.Enabled = true;
                btnPhucHoi.Enabled = false;
                int row = flexSinhVien.Row;
                txtMaSV.Text = flexSinhVien[row, "MASV"].ToString();
                txtMaSV.Enabled = false;
                txtHo.Text = flexSinhVien[row, "HO"].ToString();
                txtTen.Text = flexSinhVien[row, "TEN"].ToString();
                txtDiaChi.Text = flexSinhVien[row, "DIACHI"].ToString();
                cboGioiTinh.SelectedIndex = Convert.ToBoolean(flexSinhVien.GetData(row, "PHAI")) ? 0 : 1;
                cboDaNghiHoc.SelectedIndex = Convert.ToBoolean(flexSinhVien.GetData(row, "DANGHIHOC")) ? 0 : 1;
                dateEditNgaySinh.DateTime = Convert.ToDateTime(flexSinhVien[row, "NGAYSINH"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi", ex.ToString(), MessageBoxButtons.OK);
            }

        }

    }
}
