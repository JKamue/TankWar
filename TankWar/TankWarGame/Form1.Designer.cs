namespace TankWarGame
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxIpAdress = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nbxServerPort = new System.Windows.Forms.NumericUpDown();
            this.lblMyPort = new System.Windows.Forms.Label();
            this.nbxOwnPort = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxServerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbxOwnPort)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDisconnect);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbxIpAdress);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nbxServerPort);
            this.panel1.Controls.Add(this.lblMyPort);
            this.panel1.Controls.Add(this.nbxOwnPort);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 37);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server Ip-Adress:";
            // 
            // tbxIpAdress
            // 
            this.tbxIpAdress.Location = new System.Drawing.Point(266, 10);
            this.tbxIpAdress.Name = "tbxIpAdress";
            this.tbxIpAdress.Size = new System.Drawing.Size(100, 20);
            this.tbxIpAdress.TabIndex = 4;
            this.tbxIpAdress.Text = "127.0.0.1";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(550, 9);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 24);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(382, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server Port:";
            // 
            // nbxServerPort
            // 
            this.nbxServerPort.Location = new System.Drawing.Point(458, 11);
            this.nbxServerPort.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nbxServerPort.Minimum = new decimal(new int[] {
            49995,
            0,
            0,
            0});
            this.nbxServerPort.Name = "nbxServerPort";
            this.nbxServerPort.Size = new System.Drawing.Size(86, 20);
            this.nbxServerPort.TabIndex = 2;
            this.nbxServerPort.Value = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            // 
            // lblMyPort
            // 
            this.lblMyPort.AutoSize = true;
            this.lblMyPort.Location = new System.Drawing.Point(12, 11);
            this.lblMyPort.Name = "lblMyPort";
            this.lblMyPort.Size = new System.Drawing.Size(51, 15);
            this.lblMyPort.TabIndex = 1;
            this.lblMyPort.Text = "My Port:";
            // 
            // nbxOwnPort
            // 
            this.nbxOwnPort.Location = new System.Drawing.Point(69, 11);
            this.nbxOwnPort.Maximum = new decimal(new int[] {
            50020,
            0,
            0,
            0});
            this.nbxOwnPort.Minimum = new decimal(new int[] {
            50001,
            0,
            0,
            0});
            this.nbxOwnPort.Name = "nbxOwnPort";
            this.nbxOwnPort.Size = new System.Drawing.Size(86, 20);
            this.nbxOwnPort.TabIndex = 0;
            this.nbxOwnPort.Value = new decimal(new int[] {
            50001,
            0,
            0,
            0});
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 413);
            this.panel2.TabIndex = 1;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(642, 10);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(94, 23);
            this.btnDisconnect.TabIndex = 6;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "TankWar";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxServerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbxOwnPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nbxServerPort;
        private System.Windows.Forms.Label lblMyPort;
        private System.Windows.Forms.NumericUpDown nbxOwnPort;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxIpAdress;
        private System.Windows.Forms.Button btnDisconnect;
    }
}

