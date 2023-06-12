
namespace TeklaStair
{
    partial class CreateStair
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.Cmb_Section = new System.Windows.Forms.ComboBox();
            this.beam_section_text = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Cmb_stair_type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Cmb_Section_type = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CMB_DC = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(85, 481);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成楼梯";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cmb_Section
            // 
            this.Cmb_Section.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cmb_Section.FormattingEnabled = true;
            this.Cmb_Section.ItemHeight = 19;
            this.Cmb_Section.Location = new System.Drawing.Point(133, 294);
            this.Cmb_Section.Name = "Cmb_Section";
            this.Cmb_Section.Size = new System.Drawing.Size(172, 27);
            this.Cmb_Section.TabIndex = 1;
            // 
            // beam_section_text
            // 
            this.beam_section_text.AutoSize = true;
            this.beam_section_text.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.beam_section_text.Location = new System.Drawing.Point(23, 27);
            this.beam_section_text.Name = "beam_section_text";
            this.beam_section_text.Size = new System.Drawing.Size(104, 16);
            this.beam_section_text.TabIndex = 2;
            this.beam_section_text.Text = "楼梯类型选择";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(23, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "调整梯梁截面";
            // 
            // Cmb_stair_type
            // 
            this.Cmb_stair_type.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cmb_stair_type.FormattingEnabled = true;
            this.Cmb_stair_type.ItemHeight = 19;
            this.Cmb_stair_type.Items.AddRange(new object[] {
            "型钢梯梁钢楼梯",
            "板式梯梁钢楼梯"});
            this.Cmb_stair_type.Location = new System.Drawing.Point(133, 22);
            this.Cmb_stair_type.Name = "Cmb_stair_type";
            this.Cmb_stair_type.Size = new System.Drawing.Size(172, 27);
            this.Cmb_stair_type.TabIndex = 4;
            this.Cmb_stair_type.SelectedIndexChanged += new System.EventHandler(this.Cmb_stair_type_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(23, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "截面型号";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(23, 267);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "截面类型";
            // 
            // Cmb_Section_type
            // 
            this.Cmb_Section_type.Font = new System.Drawing.Font("宋体", 14.25F);
            this.Cmb_Section_type.FormattingEnabled = true;
            this.Cmb_Section_type.ItemHeight = 19;
            this.Cmb_Section_type.Location = new System.Drawing.Point(133, 262);
            this.Cmb_Section_type.Name = "Cmb_Section_type";
            this.Cmb_Section_type.Size = new System.Drawing.Size(172, 27);
            this.Cmb_Section_type.TabIndex = 7;
            this.Cmb_Section_type.SelectedIndexChanged += new System.EventHandler(this.Cmb_Section_type_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(53, 94);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(23, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "调整梯梁截面";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(23, 399);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "截面类型";
            // 
            // CMB_DC
            // 
            this.CMB_DC.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CMB_DC.FormattingEnabled = true;
            this.CMB_DC.Location = new System.Drawing.Point(133, 394);
            this.CMB_DC.Name = "CMB_DC";
            this.CMB_DC.Size = new System.Drawing.Size(172, 27);
            this.CMB_DC.TabIndex = 11;
            this.CMB_DC.SelectedIndexChanged += new System.EventHandler(this.CMB_DC_SelectedIndexChanged);
            // 
            // CreateStair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 550);
            this.Controls.Add(this.CMB_DC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Cmb_Section_type);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Cmb_stair_type);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.beam_section_text);
            this.Controls.Add(this.Cmb_Section);
            this.Controls.Add(this.button1);
            this.Name = "CreateStair";
            this.Text = "生成楼梯深化模型 V3.3";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox Cmb_Section;
        private System.Windows.Forms.Label beam_section_text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Cmb_stair_type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Cmb_Section_type;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CMB_DC;
    }
}

