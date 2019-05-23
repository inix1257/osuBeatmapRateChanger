using System;

namespace osuBeatmapRateChanger
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.browse = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.percentTXT = new System.Windows.Forms.Label();
            this.ratebox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.adjustCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtArtist = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtCreator = new System.Windows.Forms.TextBox();
            this.txtDiff = new System.Windows.Forms.TextBox();
            this.convert_btn = new System.Windows.Forms.Button();
            this.bpminput = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.bpmoutput = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ratebox)).BeginInit();
            this.SuspendLayout();
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(340, 12);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(32, 24);
            this.browse.TabIndex = 1;
            this.browse.Text = "...";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(13, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(321, 24);
            this.txtSource.TabIndex = 2;
            this.txtSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(47, 221);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(325, 20);
            this.progressBar1.TabIndex = 3;
            // 
            // percentTXT
            // 
            this.percentTXT.AutoSize = true;
            this.percentTXT.Location = new System.Drawing.Point(14, 225);
            this.percentTXT.Name = "percentTXT";
            this.percentTXT.Size = new System.Drawing.Size(21, 12);
            this.percentTXT.TabIndex = 4;
            this.percentTXT.Text = "0%";
            this.percentTXT.Click += new System.EventHandler(this.percentTXT_Click);
            // 
            // ratebox
            // 
            this.ratebox.DecimalPlaces = 2;
            this.ratebox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ratebox.Location = new System.Drawing.Point(47, 43);
            this.ratebox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ratebox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ratebox.Name = "ratebox";
            this.ratebox.Size = new System.Drawing.Size(77, 21);
            this.ratebox.TabIndex = 5;
            this.ratebox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ratebox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ratebox.ValueChanged += new System.EventHandler(this.ratebox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "rate:";
            // 
            // adjustCheckBox
            // 
            this.adjustCheckBox.AutoSize = true;
            this.adjustCheckBox.Location = new System.Drawing.Point(131, 45);
            this.adjustCheckBox.Name = "adjustCheckBox";
            this.adjustCheckBox.Size = new System.Drawing.Size(114, 16);
            this.adjustCheckBox.TabIndex = 7;
            this.adjustCheckBox.Text = "adjust mapstats";
            this.adjustCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Artist";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Title";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Diff";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Creator";
            // 
            // txtArtist
            // 
            this.txtArtist.Location = new System.Drawing.Point(63, 72);
            this.txtArtist.Name = "txtArtist";
            this.txtArtist.Size = new System.Drawing.Size(309, 21);
            this.txtArtist.TabIndex = 12;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(63, 100);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(309, 21);
            this.txtTitle.TabIndex = 12;
            // 
            // txtCreator
            // 
            this.txtCreator.Location = new System.Drawing.Point(63, 128);
            this.txtCreator.Name = "txtCreator";
            this.txtCreator.Size = new System.Drawing.Size(309, 21);
            this.txtCreator.TabIndex = 12;
            // 
            // txtDiff
            // 
            this.txtDiff.Location = new System.Drawing.Point(63, 156);
            this.txtDiff.Name = "txtDiff";
            this.txtDiff.Size = new System.Drawing.Size(309, 21);
            this.txtDiff.TabIndex = 12;
            // 
            // convert_btn
            // 
            this.convert_btn.Location = new System.Drawing.Point(275, 40);
            this.convert_btn.Name = "convert_btn";
            this.convert_btn.Size = new System.Drawing.Size(97, 26);
            this.convert_btn.TabIndex = 14;
            this.convert_btn.Text = "Convert";
            this.convert_btn.UseVisualStyleBackColor = true;
            this.convert_btn.Click += new System.EventHandler(this.convert_btn_Click);
            // 
            // bpminput
            // 
            this.bpminput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bpminput.Location = new System.Drawing.Point(16, 191);
            this.bpminput.Name = "bpminput";
            this.bpminput.Size = new System.Drawing.Size(43, 21);
            this.bpminput.TabIndex = 15;
            this.bpminput.TextChanged += new System.EventHandler(this.bpminput_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(65, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "BPM →";
            // 
            // bpmoutput
            // 
            this.bpmoutput.Location = new System.Drawing.Point(115, 191);
            this.bpmoutput.Name = "bpmoutput";
            this.bpmoutput.Size = new System.Drawing.Size(43, 21);
            this.bpmoutput.TabIndex = 17;
            this.bpmoutput.TextChanged += new System.EventHandler(this.bpmoutput_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(163, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "BPM";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(328, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "by 404 AimNotFound (https://osu.ppy.sh/users/2688581)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(384, 276);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bpmoutput);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bpminput);
            this.Controls.Add(this.convert_btn);
            this.Controls.Add(this.txtDiff);
            this.Controls.Add(this.txtCreator);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtArtist);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.adjustCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ratebox);
            this.Controls.Add(this.percentTXT);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.browse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ratebox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
       

        #endregion
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.Label txtSource;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label percentTXT;
        private System.Windows.Forms.NumericUpDown ratebox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox adjustCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtArtist;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtCreator;
        private System.Windows.Forms.TextBox txtDiff;
        private System.Windows.Forms.Button convert_btn;
        private System.Windows.Forms.TextBox bpminput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox bpmoutput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

