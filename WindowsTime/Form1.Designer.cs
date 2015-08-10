namespace WindowsTime
{
    partial class Form1
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
            this.lblNomeJanela = new System.Windows.Forms.Label();
            this.lblHandle = new System.Windows.Forms.Label();
            this.lblExecutavel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblIdProcesso = new System.Windows.Forms.Label();
            this.picIconeProcesso = new System.Windows.Forms.PictureBox();
            this.lblTempo = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picIconeProcesso)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNomeJanela
            // 
            this.lblNomeJanela.AutoSize = true;
            this.lblNomeJanela.Location = new System.Drawing.Point(121, 57);
            this.lblNomeJanela.Name = "lblNomeJanela";
            this.lblNomeJanela.Size = new System.Drawing.Size(43, 13);
            this.lblNomeJanela.TabIndex = 0;
            this.lblNomeJanela.Text = "{Nome}";
            // 
            // lblHandle
            // 
            this.lblHandle.AutoSize = true;
            this.lblHandle.Location = new System.Drawing.Point(121, 10);
            this.lblHandle.Name = "lblHandle";
            this.lblHandle.Size = new System.Drawing.Size(26, 13);
            this.lblHandle.TabIndex = 1;
            this.lblHandle.Text = "{ID}";
            // 
            // lblExecutavel
            // 
            this.lblExecutavel.AutoSize = true;
            this.lblExecutavel.Location = new System.Drawing.Point(121, 80);
            this.lblExecutavel.Name = "lblExecutavel";
            this.lblExecutavel.Size = new System.Drawing.Size(33, 13);
            this.lblExecutavel.TabIndex = 2;
            this.lblExecutavel.Text = "{Exe}";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Handle:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Id Processo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Título:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Executável:";
            // 
            // lblIdProcesso
            // 
            this.lblIdProcesso.AutoSize = true;
            this.lblIdProcesso.Location = new System.Drawing.Point(121, 33);
            this.lblIdProcesso.Name = "lblIdProcesso";
            this.lblIdProcesso.Size = new System.Drawing.Size(73, 13);
            this.lblIdProcesso.TabIndex = 7;
            this.lblIdProcesso.Text = "{ID Processo}";
            // 
            // picIconeProcesso
            // 
            this.picIconeProcesso.Location = new System.Drawing.Point(8, 10);
            this.picIconeProcesso.Name = "picIconeProcesso";
            this.picIconeProcesso.Size = new System.Drawing.Size(32, 32);
            this.picIconeProcesso.TabIndex = 10;
            this.picIconeProcesso.TabStop = false;
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Location = new System.Drawing.Point(5, 57);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(34, 13);
            this.lblTempo.TabIndex = 11;
            this.lblTempo.Text = "00:00";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 112);
            this.Controls.Add(this.lblTempo);
            this.Controls.Add(this.picIconeProcesso);
            this.Controls.Add(this.lblIdProcesso);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblExecutavel);
            this.Controls.Add(this.lblHandle);
            this.Controls.Add(this.lblNomeJanela);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Windows Time - TMenezes 2015";
            ((System.ComponentModel.ISupportInitialize)(this.picIconeProcesso)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNomeJanela;
        private System.Windows.Forms.Label lblHandle;
        private System.Windows.Forms.Label lblExecutavel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblIdProcesso;
        private System.Windows.Forms.PictureBox picIconeProcesso;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.Timer timer1;
    }
}

