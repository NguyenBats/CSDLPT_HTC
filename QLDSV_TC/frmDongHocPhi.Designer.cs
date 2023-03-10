
namespace QLDSV_TC
{
    partial class frmDongHocPhi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDongHocPhi));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sPDanhSachHocPHiBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.qLDSV_TCDataSet4 = new QLDSV_TC.QLDSV_TCDataSet4();
            this.sPDanhSachHocPHiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qLDSV_TCDataSet31 = new QLDSV_TC.QLDSV_TCDataSet3();
            this.cTDONGHOCPHIBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qldsV_TCDataSet3 = new QLDSV_TC.QLDSV_TCDataSet();
            this.dangkyTableAdapter1 = new QLDSV_TC.QLDSV_TCDataSetGocTableAdapters.DANGKYTableAdapter();
            this.sP_DanhSachHocPHiTableAdapter = new QLDSV_TC.QLDSV_TCDataSet3TableAdapters.SP_DanhSachHocPHiTableAdapter();
            this.cT_DONGHOCPHITableAdapter = new QLDSV_TC.QLDSV_TCDataSetGocTableAdapters.CT_DONGHOCPHITableAdapter();
            this.btnThemChiTietHocPhi = new System.Windows.Forms.Button();
            this.btnThemHocPhi = new System.Windows.Forms.Button();
            this.txthocPhi = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbCurrentSelectedRow = new DevExpress.XtraEditors.LabelControl();
            this.sP_DanhSachHocPHiTableAdapter1 = new QLDSV_TC.QLDSV_TCDataSet4TableAdapters.SP_DanhSachHocPHiTableAdapter();
            this.btnTaoTaiKhoan = new System.Windows.Forms.Button();
            this.txtChiTietTienHocPhi = new System.Windows.Forms.TextBox();
            this.cboSinhVien = new QLDSV_TC.Combobox.SinhVienCombobox();
            this.cboNienKhoa = new QLDSV_TC.Combobox.NienKhoaCombobox();
            this.cboHocKy = new QLDSV_TC.Combobox.HocKyCombobox();
            this.flexHocPhi = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.flexChiTietHocPhi = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.sPDanhSachHocPHiBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLDSV_TCDataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPDanhSachHocPHiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLDSV_TCDataSet31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cTDONGHOCPHIBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qldsV_TCDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txthocPhi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flexHocPhi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flexChiTietHocPhi)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(186, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sinh viên";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(503, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tra cứu";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Danh sách học phí qua các kì";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Chi tiết đóng học phí";
            // 
            // sPDanhSachHocPHiBindingSource1
            // 
            this.sPDanhSachHocPHiBindingSource1.DataMember = "SP_DanhSachHocPHi";
            this.sPDanhSachHocPHiBindingSource1.DataSource = this.qLDSV_TCDataSet4;
            // 
            // qLDSV_TCDataSet4
            // 
            this.qLDSV_TCDataSet4.DataSetName = "QLDSV_TCDataSet4";
            this.qLDSV_TCDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sPDanhSachHocPHiBindingSource
            // 
            this.sPDanhSachHocPHiBindingSource.DataMember = "SP_DanhSachHocPHi";
            this.sPDanhSachHocPHiBindingSource.DataSource = this.qLDSV_TCDataSet31;
            // 
            // qLDSV_TCDataSet31
            // 
            this.qLDSV_TCDataSet31.DataSetName = "QLDSV_TCDataSet3";
            this.qLDSV_TCDataSet31.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qldsV_TCDataSet3
            // 
            this.qldsV_TCDataSet3.DataSetName = "QLDSV_TCDataSet";
            this.qldsV_TCDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dangkyTableAdapter1
            // 
            this.dangkyTableAdapter1.ClearBeforeFill = true;
            // 
            // sP_DanhSachHocPHiTableAdapter
            // 
            this.sP_DanhSachHocPHiTableAdapter.ClearBeforeFill = true;
            // 
            // cT_DONGHOCPHITableAdapter
            // 
            this.cT_DONGHOCPHITableAdapter.ClearBeforeFill = true;
            // 
            // btnThemChiTietHocPhi
            // 
            this.btnThemChiTietHocPhi.Location = new System.Drawing.Point(584, 316);
            this.btnThemChiTietHocPhi.Name = "btnThemChiTietHocPhi";
            this.btnThemChiTietHocPhi.Size = new System.Drawing.Size(75, 21);
            this.btnThemChiTietHocPhi.TabIndex = 10;
            this.btnThemChiTietHocPhi.Text = "Thêm";
            this.btnThemChiTietHocPhi.UseVisualStyleBackColor = true;
            this.btnThemChiTietHocPhi.Click += new System.EventHandler(this.btnThemChiTietHocPhi_Click);
            // 
            // btnThemHocPhi
            // 
            this.btnThemHocPhi.Location = new System.Drawing.Point(1017, 46);
            this.btnThemHocPhi.Name = "btnThemHocPhi";
            this.btnThemHocPhi.Size = new System.Drawing.Size(75, 23);
            this.btnThemHocPhi.TabIndex = 12;
            this.btnThemHocPhi.Text = "Thêm";
            this.btnThemHocPhi.UseVisualStyleBackColor = true;
            this.btnThemHocPhi.Click += new System.EventHandler(this.btnThemHocPhi_Click);
            // 
            // txthocPhi
            // 
            this.txthocPhi.Location = new System.Drawing.Point(839, 46);
            this.txthocPhi.Name = "txthocPhi";
            this.txthocPhi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txthocPhi.Properties.Appearance.Options.UseFont = true;
            this.txthocPhi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txthocPhi.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txthocPhi.Size = new System.Drawing.Size(132, 24);
            this.txthocPhi.TabIndex = 17;
            this.txthocPhi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txthocPhi_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Niên khóa";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(500, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Học kỳ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(779, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Học phí";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(227, 319);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Số tiền";
            // 
            // lbCurrentSelectedRow
            // 
            this.lbCurrentSelectedRow.Location = new System.Drawing.Point(715, 320);
            this.lbCurrentSelectedRow.Name = "lbCurrentSelectedRow";
            this.lbCurrentSelectedRow.Size = new System.Drawing.Size(0, 13);
            this.lbCurrentSelectedRow.TabIndex = 22;
            // 
            // sP_DanhSachHocPHiTableAdapter1
            // 
            this.sP_DanhSachHocPHiTableAdapter1.ClearBeforeFill = true;
            // 
            // btnTaoTaiKhoan
            // 
            this.btnTaoTaiKhoan.Location = new System.Drawing.Point(15, 9);
            this.btnTaoTaiKhoan.Name = "btnTaoTaiKhoan";
            this.btnTaoTaiKhoan.Size = new System.Drawing.Size(110, 23);
            this.btnTaoTaiKhoan.TabIndex = 23;
            this.btnTaoTaiKhoan.Text = "Tạo tài khoản";
            this.btnTaoTaiKhoan.UseVisualStyleBackColor = true;
            this.btnTaoTaiKhoan.Click += new System.EventHandler(this.btnTaoTaiKhoan_Click);
            // 
            // txtChiTietTienHocPhi
            // 
            this.txtChiTietTienHocPhi.Location = new System.Drawing.Point(285, 316);
            this.txtChiTietTienHocPhi.MaxLength = 9;
            this.txtChiTietTienHocPhi.Name = "txtChiTietTienHocPhi";
            this.txtChiTietTienHocPhi.Size = new System.Drawing.Size(293, 20);
            this.txtChiTietTienHocPhi.TabIndex = 24;
            this.txtChiTietTienHocPhi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChiTietTienHocPhi_KeyPress);
            // 
            // cboSinhVien
            // 
            this.cboSinhVien.DisplayMember = "HOTEN";
            this.cboSinhVien.FormattingEnabled = true;
            this.cboSinhVien.Location = new System.Drawing.Point(258, 12);
            this.cboSinhVien.Name = "cboSinhVien";
            this.cboSinhVien.Size = new System.Drawing.Size(203, 21);
            this.cboSinhVien.TabIndex = 25;
            this.cboSinhVien.ValueMember = "MASV";
            // 
            // cboNienKhoa
            // 
            this.cboNienKhoa.DisplayMember = "KHOAHOC";
            this.cboNienKhoa.FormattingEnabled = true;
            this.cboNienKhoa.Location = new System.Drawing.Point(258, 54);
            this.cboNienKhoa.Name = "cboNienKhoa";
            this.cboNienKhoa.Size = new System.Drawing.Size(203, 21);
            this.cboNienKhoa.TabIndex = 26;
            this.cboNienKhoa.ValueMember = "KHOAHOC";
            // 
            // cboHocKy
            // 
            this.cboHocKy.DisplayMember = "TENHOCKY";
            this.cboHocKy.FormattingEnabled = true;
            this.cboHocKy.Location = new System.Drawing.Point(556, 51);
            this.cboHocKy.Name = "cboHocKy";
            this.cboHocKy.Size = new System.Drawing.Size(188, 21);
            this.cboHocKy.TabIndex = 27;
            this.cboHocKy.ValueMember = "HOCKY";
            // 
            // flexHocPhi
            // 
            this.flexHocPhi.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.flexHocPhi.Location = new System.Drawing.Point(12, 78);
            this.flexHocPhi.Name = "flexHocPhi";
            this.flexHocPhi.Rows.DefaultSize = 18;
            this.flexHocPhi.Size = new System.Drawing.Size(1080, 224);
            this.flexHocPhi.StyleInfo = resources.GetString("flexHocPhi.StyleInfo");
            this.flexHocPhi.TabIndex = 28;
            this.flexHocPhi.Click += new System.EventHandler(this.flexHocPhi_Click);
            // 
            // flexChiTietHocPhi
            // 
            this.flexChiTietHocPhi.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.flexChiTietHocPhi.Location = new System.Drawing.Point(12, 356);
            this.flexChiTietHocPhi.Name = "flexChiTietHocPhi";
            this.flexChiTietHocPhi.Rows.DefaultSize = 18;
            this.flexChiTietHocPhi.Size = new System.Drawing.Size(1080, 211);
            this.flexChiTietHocPhi.StyleInfo = resources.GetString("flexChiTietHocPhi.StyleInfo");
            this.flexChiTietHocPhi.TabIndex = 29;
            this.flexChiTietHocPhi.Click += new System.EventHandler(this.flexChiTietHocPhi_Click);
            // 
            // frmDongHocPhi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 644);
            this.Controls.Add(this.flexChiTietHocPhi);
            this.Controls.Add(this.flexHocPhi);
            this.Controls.Add(this.cboHocKy);
            this.Controls.Add(this.cboNienKhoa);
            this.Controls.Add(this.cboSinhVien);
            this.Controls.Add(this.txtChiTietTienHocPhi);
            this.Controls.Add(this.btnTaoTaiKhoan);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txthocPhi);
            this.Controls.Add(this.btnThemHocPhi);
            this.Controls.Add(this.btnThemChiTietHocPhi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbCurrentSelectedRow);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Name = "frmDongHocPhi";
            this.Text = "Đóng học phí";
            this.Load += new System.EventHandler(this.frmDongHocPhi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sPDanhSachHocPHiBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLDSV_TCDataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPDanhSachHocPHiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLDSV_TCDataSet31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cTDONGHOCPHIBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qldsV_TCDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txthocPhi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flexHocPhi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flexChiTietHocPhi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private QLDSV_TCDataSet qldsV_TCDataSet3;
        private QLDSV_TCDataSetGocTableAdapters.DANGKYTableAdapter dangkyTableAdapter1;
        private System.Windows.Forms.BindingSource sPDanhSachHocPHiBindingSource;
        private QLDSV_TCDataSet3 qLDSV_TCDataSet31;
        private QLDSV_TCDataSet3TableAdapters.SP_DanhSachHocPHiTableAdapter sP_DanhSachHocPHiTableAdapter;
        private System.Windows.Forms.BindingSource cTDONGHOCPHIBindingSource;
        private QLDSV_TCDataSetGocTableAdapters.CT_DONGHOCPHITableAdapter cT_DONGHOCPHITableAdapter;
        private System.Windows.Forms.Button btnThemChiTietHocPhi;
        private System.Windows.Forms.Button btnThemHocPhi;
        private DevExpress.XtraEditors.TextEdit txthocPhi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.LabelControl lbCurrentSelectedRow;
        private System.Windows.Forms.BindingSource sPDanhSachHocPHiBindingSource1;
        private QLDSV_TCDataSet4 qLDSV_TCDataSet4;
        private QLDSV_TCDataSet4TableAdapters.SP_DanhSachHocPHiTableAdapter sP_DanhSachHocPHiTableAdapter1;
        private System.Windows.Forms.Button btnTaoTaiKhoan;
        private System.Windows.Forms.TextBox txtChiTietTienHocPhi;
        private Combobox.SinhVienCombobox cboSinhVien;
        private Combobox.NienKhoaCombobox cboNienKhoa;
        private Combobox.HocKyCombobox cboHocKy;
        private C1.Win.C1FlexGrid.C1FlexGrid flexHocPhi;
        private C1.Win.C1FlexGrid.C1FlexGrid flexChiTietHocPhi;
    }
}