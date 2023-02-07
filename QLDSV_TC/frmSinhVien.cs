﻿using C1.Win.C1FlexGrid;
using DevExpress.Utils.Extensions;
using DevExpress.XtraRichEdit.Model;
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
        int viTri;
        public frmSinhVien()
        {
            InitializeComponent();
        }

        private void sINHVIENBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsSV.EndEdit();
            this.tableAdapterManager.UpdateAll(this.qLDSV_TCDataSet);

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


        private void frmSinhVien_Load(object sender, EventArgs e)
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


            qLDSV_TCDataSet.EnforceConstraints = false;
            this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.SINHVIENTableAdapter.Fill(this.qLDSV_TCDataSet.SINHVIEN);

            // TODO: This line of code loads data into the 'qLDSV_TCDataSet.DANGKY' table. You can move, or remove it, as needed.
            this.DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.DANGKYTableAdapter.Fill(this.qLDSV_TCDataSet.DANGKY);
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
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viTri = bdsSV.Position;
            groupBox1.Enabled = true;
            bdsSV.AddNew();

            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            flexSinhVien.Enabled = false;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String maSinhVien = "";
            if (bdsDangKy.Count > 0)
            {
                MessageBox.Show("Không thể xóa môn học này vì đã có lớp tín chỉ", "");
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa lớp này không?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    maSinhVien = ((DataRowView)bdsSV[bdsSV.Position])["MASV"].ToString();//giu lai de reload
                    bdsSV.RemoveCurrent();
                    this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.SINHVIENTableAdapter.Update(this.qLDSV_TCDataSet.SINHVIEN);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa sinh viên, vui lòng xóa lại.\n" + ex.Message, "", MessageBoxButtons.OK);
                    this.SINHVIENTableAdapter.Fill(this.qLDSV_TCDataSet.SINHVIEN);
                    bdsSV.Position = bdsSV.Find("MASV", bdsSV);
                    return;
                }
            }
            if (bdsSV.Count == 0) btnXoa.Enabled = false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //kiem tra ma sv khong trung tren cac phan manh +++
            if (txtMaSV.Text.Trim() == "")
            {
                MessageBox.Show("Mã sinh viên không được để trống!", "", MessageBoxButtons.OK);
                txtMaSV.Focus();
                return;
            }

            if (txtHo.Text.Trim() == "")
            {
                MessageBox.Show("Họ không được để trống!", "", MessageBoxButtons.OK);
                txtHo.Focus();
                return;
            }
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Tên không được để trống!", "", MessageBoxButtons.OK);
                txtTen.Focus();
                return;
            }
            //if (txtMaLop.Text.Trim() == "")
            //{
            //    MessageBox.Show("Mã lớp không được để trống!", "", MessageBoxButtons.OK);
            //    txtMaLop.Focus();
            //    return;
            //}
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Địa chỉ không được để trống!", "", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return;
            }
            //if (dedtngaySinh.Text.Trim() == "")
            //{
            //    MessageBox.Show("Ngày sinh không được để trống!", "", MessageBoxButtons.OK);
            //    txtMaLop.Focus();
            //    return;
            //}


            try
            {
                bdsSV.EndEdit();
                bdsSV.ResetCurrentItem();
                this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.SINHVIENTableAdapter.Update(this.qLDSV_TCDataSet.SINHVIEN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm lớp.\n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
            flexSinhVien.Enabled = true;
            groupBox1.Enabled = false;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsSV.CancelEdit();
            if (btnThem.Enabled == false)
            {
                bdsSV.Position = viTri;
            }
            flexSinhVien.Enabled = true;
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
            this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.SINHVIENTableAdapter.Fill(this.qLDSV_TCDataSet.SINHVIEN);
            this.DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.DANGKYTableAdapter.Fill(this.qLDSV_TCDataSet.DANGKY);
        }

        private void labKhoa_Click(object sender, EventArgs e)
        {

        }

        private void flexSinhVien_Click(object sender, EventArgs e)
        {
            try
            {
                int row = flexSinhVien.Row;
                txtMaSV.Text = flexSinhVien[row, "MASV"].ToString();
                txtHo.Text = flexSinhVien[row, "HO"].ToString();
                txtTen.Text = flexSinhVien[row, "TEN"].ToString();
                if (flexSinhVien.GetData(row, "PHAI").ToString() == "True")
                {
                    cboGioiTinh.SelectedIndex = 0;
                }
                else
                {
                    cboGioiTinh.SelectedIndex = 1;
                }

                if (flexSinhVien.GetData(row, "DANGHIHOC").ToString() == "True")
                {
                    cboDaNghiHoc.SelectedIndex = 0;
                }
                else
                {
                    cboDaNghiHoc.SelectedIndex = 1;
                }
                dateEditNgaySinh.DateTime = Convert.ToDateTime(flexSinhVien[row, "NGAYSINH"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi", ex.ToString(), MessageBoxButtons.OK);
            }

        }

        private void flexSinhVien_SelChange(object sender, EventArgs e)
        {
            //C1FlexGrid flexSinhVien = (C1FlexGrid)sender;
            //int currentRow = flexSinhVien.Row;
            //for (int i = 1; i <= flexSinhVien.Rows.Count - 1; i++)
            //{
            //    if (i == currentRow)
            //    {
            //        flexSinhVien[i, 0].BackColor = Color.Yellow;
            //    }
            //    else
            //    {
            //        flexSinhVien[i, 0].BackColor = Color.White;
            //    }
            //}
        }
    }
}
