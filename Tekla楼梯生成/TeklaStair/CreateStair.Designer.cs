
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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(89, 123);
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
            this.Cmb_Section.Location = new System.Drawing.Point(133, 62);
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
            this.label1.Location = new System.Drawing.Point(23, 67);
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
            "通用钢楼梯(型钢)",
            "板式梯梁钢楼梯"});
            this.Cmb_stair_type.Location = new System.Drawing.Point(133, 22);
            this.Cmb_stair_type.Name = "Cmb_stair_type";
            this.Cmb_stair_type.Size = new System.Drawing.Size(172, 27);
            this.Cmb_stair_type.TabIndex = 4;
            this.Cmb_stair_type.SelectedIndexChanged += new System.EventHandler(this.Cmb_stair_type_SelectedIndexChanged);
            // 
            // CreateStair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 180);
            this.Controls.Add(this.Cmb_stair_type);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.beam_section_text);
            this.Controls.Add(this.Cmb_Section);
            this.Controls.Add(this.button1);
            this.Name = "CreateStair";
            this.Text = "生成楼梯深化模型 V2.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox Cmb_Section;
        private System.Windows.Forms.Label beam_section_text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Cmb_stair_type;
    }
}

