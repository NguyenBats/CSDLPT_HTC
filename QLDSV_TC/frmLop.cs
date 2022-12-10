using DevExpress.DataAccess.Native.Excel;
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
        int viTri;
        String maKhoa = "";
        public frmLop()
        {
            InitializeComponent();
        }



        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsLop.EndEdit();
            this.tableAdapterManager.UpdateAll(this.qLDSV_TCDataSet);

        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLDSV_TCDataSet.NIENKHOA' table. You can move, or remove it, as needed.
            this.nIENKHOATableAdapter.Fill(this.qLDSV_TCDataSet.NIENKHOA);
            // TODO: This line of code loads data into the 'qLDSV_TCDataSet.KHOA' table. You can move, or remove it, as needed.
            this.kHOATableAdapter.Fill(this.qLDSV_TCDataSet.KHOA);
            qLDSV_TCDataSet.EnforceConstraints = false;
            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.qLDSV_TCDataSet.LOP);

            this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            // TODO: This line of code loads data into the 'qLDSV_TCDataSet.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSV_TCDataSet.SINHVIEN);

            maKhoa = ((DataRowView)bdsLop[0])["MAKHOA"].ToString();
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


        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String maLop = "";
            if (bdsSV.Count > 0)
            {
                MessageBox.Show("Không thể xóa lớp này vì lớp đã có sinh viên", "");
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa lớp này không?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    maLop = ((DataRowView)bdsLop[bdsLop.Position])["MALOP"].ToString();//giu lai de reload
                    bdsLop.RemoveCurrent();
                    this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.LOPTableAdapter.Update(this.qLDSV_TCDataSet.LOP);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa lớp, vui lòng xóa lại.\n" + ex.Message, "", MessageBoxButtons.OK);
                    this.LOPTableAdapter.Fill(this.qLDSV_TCDataSet.LOP);
                    bdsLop.Position = bdsLop.Find("MALOP", maLop);
                    return;
                }
            }
            if (bdsLop.Count == 0) btnXoa.Enabled = false;
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
                //string prefix = "D" + Convert.ToInt32(txtStartYear.Text) % 2000;
                //DataTable dtbLop = this.qLDSV_TCDataSet.LOP as DataTable;
                //if (dtbLop != null && dtbLop.Rows.Count > 0)
                //{
                //    DataRow dr = dtbLop.AsEnumerable().FirstOrDefault(x => Convert.ToString(x["MALOP"]) == malop);//validate lần đầu, không trùng mã => insert thẳng
                //    if (dr != null)
                //    {
                //        int index = Convert.ToInt32(dr["MALOP"].ToString().Substring(-2,2));
                //    }
                //}

                //From this
                //bdsLop.AddNew();
                //To this
                //DataRow newRow = this.qLDSV_TCDataSet.LOP.NewRow();
                //newRow["KHOAHOC"] = txtStartYear.Text + "-" + txtEndYear.Text;
                //newRow["MAKHOA"] = maKhoa.Trim();
                //newRow["MALOP"] = txtMaLop.Text;
                //newRow["TENLOP"] = txtTenLop.Text;
                //bdsLop.Add(newRow);
                String malop = txtMaLop.Text;
                if (Program.ExecSqlToInt("EXEC [dbo].[sp_KiemTraMaLop] '" + malop + "'") == 1)
                    return;
                if (Program.ExecSqlNonQuery("INSERT INTO LOP(MALOP,TENLOP,KHOAHOC,MAKHOA) VALUES ('" + txtMaLop.Text + "','" + txtTenLop.Text + "','" + txtStartYear.Text + "-" + txtEndYear.Text + "','" + maKhoa.Trim() + "')") != 0)
                    return;
                else
                {
                    MessageBox.Show("Thêm mới lớp thành công.\n" ,"Thành công", MessageBoxButtons.OK);
                    this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.LOPTableAdapter.Fill(this.qLDSV_TCDataSet.LOP);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm lớp.\n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
            LopGridControl.Enabled = true;
            textPanel.Enabled = false;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            LopGridControl.Enabled = false;
           
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //bdsLop.CancelEdit();
            if (btnThem.Enabled == false)
            {
                bdsLop.Position = viTri;
            }
            setCurrentValue();
            LopGridControl.Enabled = true;
            textPanel.Enabled = false;
            btnThem.Enabled = btnThoat.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
           
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
            LopGridControl.Enabled = true;
            

            viTri = bdsLop.Position;
            txtMaLop.Text = "";
            txtTenLop.Text = "";
            txtStartYear.Text = "";
            txtEndYear.Text = "";
            textPanel.Enabled = true;
            //bdsLop.AddNew();
            //cboKhoa.Sekected = maKhoa;

            
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
            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.qLDSV_TCDataSet.LOP);
            this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.sINHVIENTableAdapter.Fill(this.qLDSV_TCDataSet.SINHVIEN);
            try
            {
                maKhoa = ((DataRowView)bdsLop[0])["MAKHOA"].ToString();
            }
            catch (Exception ex) { }
        }

        private void txtMaLop_TextChanged(object sender, EventArgs e)
        {
            //if (bdsLop.Count == 0) return;
            //string nienKhoa = ((DataRowView)bdsLop[bdsLop.Position])["KHOAHOC"].ToString();//giu lai de reload
            //DataTable dtbNienKhoa = this.qLDSV_TCDataSet.NIENKHOA as DataTable;
            //cboNienKhoaFinal.DataSource = dtbNienKhoa;
            //cboNienKhoaFinal.DisplayMember = "NIENKHOA";
            //if (dtbNienKhoa != null && dtbNienKhoa.Rows.Count > 0)
            //{
            //    DataRow dr = dtbNienKhoa.AsEnumerable().First(x => Convert.ToString(x["NIENKHOA"]) == nienKhoa);
            //    int selectedIndex = dtbNienKhoa.Rows.IndexOf(dr);
            //    cboNienKhoaFinal.SelectedIndex = selectedIndex;
            //}
        }


        private void txtMaLopUnvisible_TextChanged(object sender, EventArgs e)
        {
            try
            {
                setCurrentValue();
            }
            catch (Exception objEx)
            {

            }
        }

        private void setCurrentValue()
        {
            viTri = bdsLop.Position;
            DataTable dtbLop = this.qLDSV_TCDataSet.LOP as DataTable;
            if (dtbLop != null && dtbLop.Rows.Count > 0)
            {
                txtMaLop.Text = dtbLop.Rows[viTri]["MALOP"].ToString();
                txtTenLop.Text = dtbLop.Rows[viTri]["TENLOP"].ToString();
                txtStartYear.Text = dtbLop.Rows[viTri]["KHOAHOC"].ToString().Split('-')[0];
                txtEndYear.Text = dtbLop.Rows[viTri]["KHOAHOC"].ToString().Split('-')[1];
            }
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
    }
}
