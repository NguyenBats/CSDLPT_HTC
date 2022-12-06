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
        int viTri;
        String maKhoa = "";
        String maGV = "";
        String maMH = "";
        private int position = -1;
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
                    = gcLTC.Enabled
                    = !value;

                btnHuy.Enabled
                    = btnGhi.Enabled
                    = panelControl1.Enabled
                    = value;
                txtNienKhoa.Enabled = cmbHocKy.Enabled = cbbMaMH.Enabled = !value;
             //   barButtonUndo.Enabled = undoStack.Count > 0;
            }
            else if (state == "add")
            {

                btnThem.Enabled
                    = btnSua.Enabled
                    = btnXoa.Enabled
                    = btnPhucHoi.Enabled
                    = gcLTC.Enabled
                    = groupBox1.Enabled
                    = !value;

                btnHuy.Enabled
                   = btnGhi.Enabled
                   = panelControl1.Enabled
                   = value;

            }
        }

       
        private void LoadMaMH()
        {
            DataTable dt = new DataTable();
            String sqlStr = "Select MaMH, (MaMH + ' - ' + TenMH) as TenMH  from MONHOC";
            dt = Program.ExecSqlDataTable(sqlStr);

            try
            {
                DataTable datatable = (DataTable)dt;
                cbbMaMH.DataSource = datatable;
            }
            catch
            {
                cbbMaMH.DataSource = dt;
            }
            cbbMaMH.DisplayMember = "TenMH";
            cbbMaMH.ValueMember = "MaMH";
            cbbMaMH.SelectedIndex = 1;
            cbbMaMH.AutoCompleteMode = AutoCompleteMode.Suggest;
            cbbMaMH.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void LoadMaGV()
        {
            DataTable dt = new DataTable();
            String sqlStr ="SELECT MaGV, (MaGV + ' - ' + Ho + ' ' + Ten) as TenGV  from GIANGVIEN";
            dt = Program.ExecSqlDataTable(sqlStr);

            try
            {
                DataTable datatable = (DataTable)dt;
                cbbMaGV.DataSource = datatable;
            }
            catch
            {
                cbbMaGV.DataSource = dt;
            }
            cbbMaGV.DisplayMember = "TenGV";
            cbbMaGV.ValueMember = "MaGV";
            cbbMaGV.SelectedIndex = 1;
            cbbMaGV.AutoCompleteMode = AutoCompleteMode.Suggest;
            cbbMaGV.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void LoadData()
        {
            DSGoc.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'qLDSV_TCDataSetGoc.SP_GetMaGVMH_LTC' table. You can move, or remove it, as needed.
            this.SP_GetMaGVMH_LTCTableAdapter.Fill(this.DSGoc.SP_GetMaGVMH_LTC);
            // TODO: This line of code loads data into the 'qLDSV_TCDataSetGoc.SP_GetMaGVMH_LTC' table. You can move, or remove it, as needed.
            this.SP_GetMaGVMH_LTCTableAdapter.Fill(this.DSGoc.SP_GetMaGVMH_LTC);
            // TODO: This line of code loads data into the 'qLDSV_TCDataSetGoc.LOPTINCHI' table. You can move, or remove it, as needed.

            qLDSV_TCDataSet.EnforceConstraints = false;
            this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTINCHITableAdapter.Fill(this.qLDSV_TCDataSet.LOPTINCHI);
            // TODO: This line of code loads data into the 'qLDSV_TCDataSet.DANGKY' table. You can move, or remove it, as needed.
            this.DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.DANGKYTableAdapter.Fill(this.qLDSV_TCDataSet.DANGKY);
            btnXoa.Enabled = btnSua.Enabled = bdsLopTC.Count > 0;


            if (position > 0)
            {
                if (position <= bdsLopTC.Count)
                    bdsLopTC.Position = position;
                else
                    bdsLopTC.Position = position = 0;
            }

        }
        private void frmLTC_Load(object sender, EventArgs e)
        {
           
            DSGoc.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'qLDSV_TCDataSetGoc.SP_GetMaGVMH_LTC' table. You can move, or remove it, as needed.
            this.SP_GetMaGVMH_LTCTableAdapter.Fill(this.DSGoc.SP_GetMaGVMH_LTC);
            // TODO: This line of code loads data into the 'qLDSV_TCDataSetGoc.SP_GetMaGVMH_LTC' table. You can move, or remove it, as needed.
            this.SP_GetMaGVMH_LTCTableAdapter.Fill(this.DSGoc.SP_GetMaGVMH_LTC);
            // TODO: This line of code loads data into the 'qLDSV_TCDataSetGoc.LOPTINCHI' table. You can move, or remove it, as needed.
           
            qLDSV_TCDataSet.EnforceConstraints = false;
            this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTINCHITableAdapter.Fill(this.qLDSV_TCDataSet.LOPTINCHI);
            // TODO: This line of code loads data into the 'qLDSV_TCDataSet.DANGKY' table. You can move, or remove it, as needed.
            this.DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.DANGKYTableAdapter.Fill(this.qLDSV_TCDataSet.DANGKY);
            maKhoa = ((DataRowView)bdsLTC[0])["MAKHOA"].ToString();
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
            this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTINCHITableAdapter.Fill(this.qLDSV_TCDataSet.LOPTINCHI);
            this.DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.DANGKYTableAdapter.Fill(this.qLDSV_TCDataSet.DANGKY);
            try
            {
                maKhoa = ((DataRowView)bdsLTC[0])["MAKHOA"].ToString();
            }
            catch (Exception ex) { }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadMaGV();
            LoadMaMH();
            viTri = bdsLTC.Position;
            groupBox1.Enabled = true;
            bdsLTC.AddNew();
            txtMaKhoa.Text = maKhoa;
          
            btnThem.Enabled  = btnXoa.Enabled = btnSua.Enabled = false;
            barButtonItem2.Enabled=btnGhi.Enabled = btnPhucHoi.Enabled = true;
            gcLTC.Enabled = false;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String maLop = "";
            if (bdsDangKy.Count > 0)
            {
                MessageBox.Show("Không thể xóa lớp này vì lớp đã có sinh viên", "");
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa lớp này không?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    maLop = ((DataRowView)bdsLTC[bdsLTC.Position])["MALTC"].ToString();//giu lai de reload
                    bdsLTC.RemoveCurrent();
                    this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
                    this.LOPTINCHITableAdapter.Update(this.qLDSV_TCDataSet.LOPTINCHI);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa lớp, vui lòng xóa lại.\n" + ex.Message, "", MessageBoxButtons.OK);
                    this.LOPTINCHITableAdapter.Fill(this.qLDSV_TCDataSet.LOPTINCHI);
                    bdsLTC.Position = bdsLTC.Find("MALTC", maLop);
                    return;
                }
            }
            if (bdsLTC.Count == 0) btnXoa.Enabled = false;
        }
        private bool checkLTC()
        {
            string query = string.Format(" EXEC sp_KiemTraLTC @NIENKHOA = N'{0}', @HOCKY = {1}, @MAMH = N'{2}', @NHOM = {3}", txtNienKhoa.Text.Trim(), Convert.ToInt32(cmbHocKy.Text), cbbMaMH.SelectedValue.ToString().Trim(), Convert.ToInt32(txtNhom.Text));

            var check = Program.ExecSqlNonQuery(query);
            if (check == 0) return true;
            return false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //kiem tra ma lop khong trung tren cac phan manh +++
           
            if (txtNienKhoa.Text.Trim() == "")
            {
                MessageBox.Show("Tên lớp không được để trống!", "", MessageBoxButtons.OK);
                txtNienKhoa.Focus();
                return;
            }
            if (cbbMaMH.Text.Trim() == "")
            {
                MessageBox.Show("Mã môn học không được để trống!", "", MessageBoxButtons.OK);
                cbbMaMH.Focus();
                return;
            }
            if (txtNhom.Text.Trim() == "")
            {
                MessageBox.Show("Nhóm không được để trống!", "", MessageBoxButtons.OK);
                txtNhom.Focus();
            }
            if (cbbMaGV.Text.Trim() == "")
            {
                MessageBox.Show("Mã giảng viên không được để trống!", "", MessageBoxButtons.OK);
                cbbMaGV.Focus();
                return;
            }
            if (txtSoSVMin.Text.Trim() == "")
            {
                MessageBox.Show("Số sinh viên tối thiểu không được để trống!", "", MessageBoxButtons.OK);
                txtSoSVMin.Focus();
                return;
            }
            String query = "Exec SP_InsertLTC '" + txtNienKhoa.Text + "'," + cmbHocKy.Text + ",'" + cbbMaMH.SelectedValue.ToString().Trim() + "',"
            + txtNhom.Text + ",'" + cbbMaGV.SelectedValue.ToString().Trim() + "','" + txtMaKhoa.Text + "'," + txtSoSVMin.Text+","+cmbHuyLop.Text;
            Console.WriteLine("SQL thêm ltc: " + query);

            if (state == "add" && !checkLTC()) return;
            try
            {
                bdsLopTC.EndEdit();
                bdsLopTC.ResetCurrentItem();
                this.SP_GetMaGVMH_LTCTableAdapter.Connection.ConnectionString = Program.connstr;
                if (state == "edit")
                    if ( bdsDangKy.Count > 0)
                    {
                        MessageBox.Show("Số SV tối thiểu sau khi sửa phải lớn hơn SV tối thiểu trước đó", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                this.bdsLopTC.EndEdit();
                this.bdsLopTC.ResetCurrentItem();
                //this.LOPTINCHITableAdapter.Update(this.qLDSV_TCDataSet.LOPTINCHI);
                //  undoStack.Push(string.Format("INSERT INTO LOPTINCHI([NIENKHOA],[HOCKY],[MAMH],[NHOM],[MAGV],[MAKHOA],[SOSVTOITHIEU],[HUYLOP])VALUES(N'{0}',N'{1}',N'{2}',{3},N'{4}','{5}','{6}',{7},{8})",txtNienKhoa.Text, cmbHocKy.Text,maMH,txtNhom.Text,txtMaKhoa.Text,txtSoSVMin.Text,cmbHuyLop.Text));
                
                Program.ExecSqlNonQuery(query);
               
                this.SP_GetMaGVMH_LTCTableAdapter.Fill(this.DSGoc.SP_GetMaGVMH_LTC);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm lớp.\n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
            gcLTC.Enabled = true;
            groupBox1.Enabled = false;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsLTC.CancelEdit();
            if (btnThem.Enabled == false)
            {
                bdsLTC.Position = viTri;
            }
            gcLTC.Enabled = true;
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

        private void cmbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbMaGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbMaGV.SelectedValue.ToString() == "System.Data.DataRowView") return;
                maGV = cbbMaGV.SelectedValue.ToString();
            }
            catch (Exception ex) { }
        }

        private void cbbMaMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbMaMH.SelectedValue.ToString() == "System.Data.DataRowView") return;
                maMH = cbbMaMH.SelectedValue.ToString();
            }
            catch (Exception ex) { }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            position = bdsLTC.Position;
            state = "edit";
            SetButtonState(true);
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsLopTC.CancelEdit();
            SetButtonState(false);
            LoadData();
        }

    }
}
