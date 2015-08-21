namespace WindowsTime
{
    partial class FrmDebug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDebug));
            this.lblNomeJanela = new System.Windows.Forms.Label();
            this.lblHandle = new System.Windows.Forms.Label();
            this.lblPrograma = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblIdProcesso = new System.Windows.Forms.Label();
            this.picIconeProcesso = new System.Windows.Forms.PictureBox();
            this.lblTempo = new System.Windows.Forms.Label();
            this.lblBloqueouPC = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblExecutavel = new System.Windows.Forms.Label();
            this.btnTeste = new System.Windows.Forms.Button();
            this.btnDetalheProcesso = new System.Windows.Forms.Button();
            this.propertyGridEx1 = new href.Controls.PropGridEx.PropertyGridEx();
            this.propertyGridEx2 = new href.Controls.PropGridEx.PropertyGridEx();
            ((System.ComponentModel.ISupportInitialize)(this.picIconeProcesso)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNomeJanela
            // 
            this.lblNomeJanela.AutoSize = true;
            this.lblNomeJanela.Location = new System.Drawing.Point(125, 57);
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
            // lblPrograma
            // 
            this.lblPrograma.AutoSize = true;
            this.lblPrograma.Location = new System.Drawing.Point(125, 80);
            this.lblPrograma.Name = "lblPrograma";
            this.lblPrograma.Size = new System.Drawing.Size(60, 13);
            this.lblPrograma.TabIndex = 2;
            this.lblPrograma.Text = "{Programa}";
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
            this.label2.Location = new System.Drawing.Point(50, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Id Processo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Título:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Programa:";
            // 
            // lblIdProcesso
            // 
            this.lblIdProcesso.AutoSize = true;
            this.lblIdProcesso.Location = new System.Drawing.Point(125, 33);
            this.lblIdProcesso.Name = "lblIdProcesso";
            this.lblIdProcesso.Size = new System.Drawing.Size(73, 13);
            this.lblIdProcesso.TabIndex = 7;
            this.lblIdProcesso.Text = "{ID Processo}";
            // 
            // picIconeProcesso
            // 
            this.picIconeProcesso.Location = new System.Drawing.Point(12, 10);
            this.picIconeProcesso.Name = "picIconeProcesso";
            this.picIconeProcesso.Size = new System.Drawing.Size(32, 32);
            this.picIconeProcesso.TabIndex = 10;
            this.picIconeProcesso.TabStop = false;
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Location = new System.Drawing.Point(9, 57);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(34, 13);
            this.lblTempo.TabIndex = 11;
            this.lblTempo.Text = "00:00";
            // 
            // lblBloqueouPC
            // 
            this.lblBloqueouPC.AutoSize = true;
            this.lblBloqueouPC.Location = new System.Drawing.Point(235, 10);
            this.lblBloqueouPC.Name = "lblBloqueouPC";
            this.lblBloqueouPC.Size = new System.Drawing.Size(94, 13);
            this.lblBloqueouPC.TabIndex = 12;
            this.lblBloqueouPC.Text = "{Não travou o PC}";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Executável:";
            // 
            // lblExecutavel
            // 
            this.lblExecutavel.AutoSize = true;
            this.lblExecutavel.Location = new System.Drawing.Point(125, 105);
            this.lblExecutavel.Name = "lblExecutavel";
            this.lblExecutavel.Size = new System.Drawing.Size(33, 13);
            this.lblExecutavel.TabIndex = 13;
            this.lblExecutavel.Text = "{Exe}";
            // 
            // btnTeste
            // 
            this.btnTeste.Location = new System.Drawing.Point(12, 80);
            this.btnTeste.Name = "btnTeste";
            this.btnTeste.Size = new System.Drawing.Size(31, 23);
            this.btnTeste.TabIndex = 15;
            this.btnTeste.Text = "img";
            this.btnTeste.UseVisualStyleBackColor = true;
            this.btnTeste.Click += new System.EventHandler(this.btnTeste_Click);
            // 
            // btnDetalheProcesso
            // 
            this.btnDetalheProcesso.Location = new System.Drawing.Point(12, 109);
            this.btnDetalheProcesso.Name = "btnDetalheProcesso";
            this.btnDetalheProcesso.Size = new System.Drawing.Size(31, 23);
            this.btnDetalheProcesso.TabIndex = 16;
            this.btnDetalheProcesso.Text = "\\/";
            this.btnDetalheProcesso.UseVisualStyleBackColor = true;
            this.btnDetalheProcesso.Click += new System.EventHandler(this.btnDetalheProcesso_Click);
            this.btnDetalheProcesso.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDetalheProcesso_MouseDown);
            // 
            // propertyGridEx1
            // 
            this.propertyGridEx1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGridEx1.CommandsActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.propertyGridEx1.CommandsDisabledLinkColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGridEx1.CommandsLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.propertyGridEx1.DrawFlat = true;
            this.propertyGridEx1.Location = new System.Drawing.Point(12, 138);
            this.propertyGridEx1.Name = "propertyGridEx1";
            this.propertyGridEx1.Size = new System.Drawing.Size(384, 370);
            this.propertyGridEx1.TabIndex = 17;
            // 
            // propertyGridEx2
            // 
            this.propertyGridEx2.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGridEx2.CommandsActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.propertyGridEx2.CommandsDisabledLinkColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGridEx2.CommandsLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.propertyGridEx2.DrawFlat = true;
            this.propertyGridEx2.Location = new System.Drawing.Point(402, 138);
            this.propertyGridEx2.Name = "propertyGridEx2";
            this.propertyGridEx2.Size = new System.Drawing.Size(392, 370);
            this.propertyGridEx2.TabIndex = 18;
            // 
            // FrmDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 520);
            this.Controls.Add(this.propertyGridEx2);
            this.Controls.Add(this.propertyGridEx1);
            this.Controls.Add(this.btnDetalheProcesso);
            this.Controls.Add(this.btnTeste);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblExecutavel);
            this.Controls.Add(this.lblBloqueouPC);
            this.Controls.Add(this.lblTempo);
            this.Controls.Add(this.picIconeProcesso);
            this.Controls.Add(this.lblIdProcesso);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPrograma);
            this.Controls.Add(this.lblHandle);
            this.Controls.Add(this.lblNomeJanela);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmDebug";
            this.Text = "Windows Time - TMenezes 2015";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDebug_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picIconeProcesso)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNomeJanela;
        private System.Windows.Forms.Label lblHandle;
        private System.Windows.Forms.Label lblPrograma;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblIdProcesso;
        private System.Windows.Forms.PictureBox picIconeProcesso;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.Label lblBloqueouPC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblExecutavel;
        private System.Windows.Forms.Button btnTeste;
        private System.Windows.Forms.Button btnDetalheProcesso;
        private href.Controls.PropGridEx.PropertyGridEx propertyGridEx1;
        private href.Controls.PropGridEx.PropertyGridEx propertyGridEx2;
    }
}

