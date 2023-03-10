using DevExpress.CodeParser.Diagnostics;
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
    public partial class frmLTC : Form
    {

        private string state;

        Stack<string> undoStack = new Stack<string>();
        public frmLTC()
        {
            InitializeComponent();
        }
        private void SetButtonState(bool value)
        {
            if (state == "edit")
            {
                btnThem.Enabled
                    = btnSua.Enabled
                    = btnXoa.Enabled                         
                    = flexLopTinChi.Enabled
                    = !value;
             
            }
            else if (state == "add")
            {

                btnThem.Enabled
                    = btnSua.Enabled
                    = btnXoa.Enabled
                    = btnPhucHoi.Enabled
                    = flexLopTinChi.Enabled
                    = groupBox1.Enabled
                    = !value;            

            }
        }


        private void FormatFlex()
        {
            try
            {
                flexLopTinChi.Width = 1600;
                flexLopTinChi.Cols[0].Visible = false;
                flexLopTinChi.Cols["MALTC"].Caption = "Mã lớp";
                flexLopTinChi.Cols["NIENKHOA"].Caption = "Niên khóa";
                flexLopTinChi.Cols["HOCKY"].Caption = "Học kỳ";
                flexLopTinChi.Cols["TENMH"].Caption = "Tên môn học";
                flexLopTinChi.Cols["NHOM"].Caption = "Nhóm";
                flexLopTinChi.Cols["TENGIANGVIEN"].Caption = "Tên giảng viên";
                flexLopTinChi.Cols["SOSVTOITHIEU"].Caption = "Số sinh viên tối thiểu";
                flexLopTinChi.Cols["TRANGTHAI"].Caption = "Trạng thái";
                flexLopTinChi.Cols["ROWGUID"].Visible = false;
                flexLopTinChi.Cols["MAMH"].Visible = false;
                flexLopTinChi.Cols["MAGV"].Visible = false;
                flexLopTinChi.Cols["HUYLOP"].Visible = false;
                flexLopTinChi.Rows[0].Height = 50;
                flexLopTinChi.Cols["MALTC"].Width = 200;
                flexLopTinChi.Cols["NIENKHOA"].Width = 200;
                flexLopTinChi.Cols["HOCKY"].Width = 200;
                flexLopTinChi.Cols["TENMH"].Width = 200;
                flexLopTinChi.Cols["NHOM"].Width = 200;
                flexLopTinChi.Cols["TENGIANGVIEN"].Width = 200;
                flexLopTinChi.Cols["SOSVTOITHIEU"].Width = 200;
                //flexLopTinChi.Cols["DANGHIHOC"].Width = 200;
                flexLopTinChi.Cols["TRANGTHAI"].Width = 200;



            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi format lưới.\n" + ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void refreshData()
        {
            //Lấy dữ liệu sinh viên đổ ra lưới
            LopTinChiDAO lopTinChiDAO = new LopTinChiDAO();
            DataTable dtbLopTinChi = lopTinChiDAO.GetAll();
            flexLopTinChi.DataSource = dtbLopTinChi;
            FormatFlex();
        }


        private void frmLTC_Load(object sender, EventArgs e)
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

        private void cmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch(Exception objEx)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới.\n", objEx.Message, MessageBoxButtons.OK);
            }
           
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            state = "add";
            groupBox1.Enabled = true;
            txtSoSVMin.Text = "";
            cboGiangVien.SelectedIndex = 0;
            cboHocKy.SelectedIndex = 0;
            cboMonHoc.SelectedIndex = 0;
            cboNhom.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            btnThem.Enabled  = btnXoa.Enabled = btnSua.Enabled = false;
            barButtonItem2.Enabled=btnGhi.Enabled = btnPhucHoi.Enabled = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //String maLop = "";
            //if (bdsDangKy.Count > 0)
            //{
            //    MessageBox.Show("Không thể xóa lớp này vì lớp đã có sinh viên", "");
            //    return;
            //}
            if (MessageBox.Show("Bạn có muốn xóa lớp này không?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    LopTinChiDAO lopTinChiDAO = new LopTinChiDAO();
                    int row = flexLopTinChi.Row;
                    string maLTC = flexLopTinChi[row, "MALTC"].ToString();
                    lopTinChiDAO.Delete(maLTC);
                    MessageBox.Show("Xóa lớp tín chỉ thanh cong.\n", "", MessageBoxButtons.OK);
                    refreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa lớp tín chỉ, vui lòng thử lại.\n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
            }
            //if (bdsLTC.Count == 0) btnXoa.Enabled = false;
        }
        private bool checkLTC()
        {
            //string query = string.Format(" EXEC sp_KiemTraLTC @NIENKHOA = N'{0}', @HOCKY = {1}, @MAMH = N'{2}', @NHOM = {3}", txtNienKhoa.Text.Trim(), Convert.ToInt32(cmbHocKy.Text), cbbMaMH.SelectedValue.ToString().Trim(), Convert.ToInt32(txtNhom.Text));

            //var check = Program.ExecSqlNonQuery(query);
            //if (check == 0) return true;
            return false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //kiem tra ma lop khong trung tren cac phan manh +++                  
            //if (state == "add" && !checkLTC()) return;
            try
            {              
                if (txtSoSVMin.Text.Trim() == "")
                {
                    MessageBox.Show("Số sinh viên tối thiểu không được để trống!", "", MessageBoxButtons.OK);
                    txtSoSVMin.Focus();
                    return;
                }
                LopTinChiDAO lopTinChiDAO= new LopTinChiDAO();
                LopTinChi lopTinChi = new LopTinChi();
                DataRowView row = (DataRowView)cmbKhoa.SelectedItem;
                string maKhoa = row["MAKHOA"].ToString();
                lopTinChi.MAKHOA = maKhoa;
                int currentRow = flexLopTinChi.Row;
                lopTinChi.MALTC = Convert.ToInt32(flexLopTinChi[currentRow, "MALTC"].ToString());
                lopTinChi.SOSVTOITHIEU = Convert.ToInt32(txtSoSVMin.Text);
                lopTinChi.NIENKHOA = Convert.ToString(cboNienKhoa.SelectedValue.ToString());
                lopTinChi.MAGV = cboGiangVien.SelectedValue.ToString();
                lopTinChi.HOCKY = Convert.ToInt32(cboHocKy.SelectedValue.ToString());
                lopTinChi.MAMH = cboMonHoc.SelectedValue.ToString();
                lopTinChi.NHOM = Convert.ToInt32(cboNhom.SelectedValue.ToString());
                lopTinChi.HUYLOP = Convert.ToBoolean(cboTrangThai.SelectedValue.ToString());
                if (state == "add")
                {
                    lopTinChiDAO.Insert(lopTinChi);
                    MessageBox.Show("Thêm mới lớp tín chỉ thành công!", "", MessageBoxButtons.OK);
                }
                else
                {
                    lopTinChiDAO.UpdateLTC(lopTinChi);
                    MessageBox.Show("Cập nhật thông tin lớp tín chỉ thành công!", "", MessageBoxButtons.OK);
                }
                refreshData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm lớp.\n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
            flexLopTinChi.Enabled = true;
            groupBox1.Enabled = false;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
         
            groupBox1.Enabled = false;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnPhucHoi.Enabled)
            {
                String message = "Dữ liệu bạn vừa sửa sẽ không thể hoàn tác! Bạn có xác nhận thoát.";
                DialogResult dr =MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) return;
            }
            else if (btnSua.Enabled)
            {
                String message = "Dữ liệu bạn đang sửa sẽ bị hủy! Bạn có xác nhận thoát.";
                DialogResult dr = MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) return;
            }

            this.Close();
        }

        private void flexLTC_Click(object sender, EventArgs e)
        {
            state = "edit";
            groupBox1.Enabled = true;
            btnGhi.Enabled = true;
            btnPhucHoi.Enabled = false;
            int row = flexLopTinChi.Row;
            txtSoSVMin.Text = flexLopTinChi[row, "SOSVTOITHIEU"].ToString();
            cboNienKhoa.SelectedValue = Convert.ToString(flexLopTinChi.GetData(row, "NIENKHOA"));
            cboGiangVien.SelectedValue = Convert.ToString(flexLopTinChi.GetData(row, "MAGV"));
            cboHocKy.SelectedValue = Convert.ToString(flexLopTinChi.GetData(row, "HOCKY"));
            cboMonHoc.SelectedValue = Convert.ToString(flexLopTinChi[row, "MAMH"]);
            cboNhom.SelectedValue = Convert.ToString(flexLopTinChi[row, "NHOM"]);
            cboTrangThai.SelectedValue = Convert.ToString(flexLopTinChi[row, "HUYLOP"]);
        }
    }
}
