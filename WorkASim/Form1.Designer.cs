namespace WorkASim
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Connectbutton = new System.Windows.Forms.Button();
            this.DataButton = new System.Windows.Forms.Button();
            this.DataTB = new System.Windows.Forms.TextBox();
            this.StopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Connectbutton
            // 
            this.Connectbutton.Location = new System.Drawing.Point(538, 153);
            this.Connectbutton.Name = "Connectbutton";
            this.Connectbutton.Size = new System.Drawing.Size(193, 60);
            this.Connectbutton.TabIndex = 0;
            this.Connectbutton.Text = "Connect";
            this.Connectbutton.UseVisualStyleBackColor = true;
            this.Connectbutton.Click += new System.EventHandler(this.Connectbutton_Click);
            // 
            // DataButton
            // 
            this.DataButton.Location = new System.Drawing.Point(538, 232);
            this.DataButton.Name = "DataButton";
            this.DataButton.Size = new System.Drawing.Size(193, 60);
            this.DataButton.TabIndex = 2;
            this.DataButton.Text = "Data";
            this.DataButton.UseVisualStyleBackColor = true;
            this.DataButton.Click += new System.EventHandler(this.DataButton_Click);
            // 
            // DataTB
            // 
            this.DataTB.Location = new System.Drawing.Point(70, 29);
            this.DataTB.Multiline = true;
            this.DataTB.Name = "DataTB";
            this.DataTB.Size = new System.Drawing.Size(375, 391);
            this.DataTB.TabIndex = 3;
            this.DataTB.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(538, 311);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(193, 60);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.DataTB);
            this.Controls.Add(this.DataButton);
            this.Controls.Add(this.Connectbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connectbutton;
        private System.Windows.Forms.Button DataButton;
        private System.Windows.Forms.TextBox DataTB;
        private System.Windows.Forms.Button StopButton;
    }
}

