namespace EdgeConnectivity
{
    partial class MainForm
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
            this.loadGraph_button = new System.Windows.Forms.Button();
            this.evaluate_button = new System.Windows.Forms.Button();
            this.descprition_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loadGraph_button
            // 
            this.loadGraph_button.Location = new System.Drawing.Point(62, 93);
            this.loadGraph_button.Name = "loadGraph_button";
            this.loadGraph_button.Size = new System.Drawing.Size(119, 33);
            this.loadGraph_button.TabIndex = 0;
            this.loadGraph_button.Text = "Wczytaj graf";
            this.loadGraph_button.UseVisualStyleBackColor = true;
            this.loadGraph_button.Click += new System.EventHandler(this.loadGraph_button_Click);
            // 
            // evaluate_button
            // 
            this.evaluate_button.Location = new System.Drawing.Point(294, 93);
            this.evaluate_button.Name = "evaluate_button";
            this.evaluate_button.Size = new System.Drawing.Size(136, 34);
            this.evaluate_button.TabIndex = 1;
            this.evaluate_button.Text = "Wyznacz spóność";
            this.evaluate_button.UseVisualStyleBackColor = true;
            this.evaluate_button.Click += new System.EventHandler(this.evaluate_button_Click);
            // 
            // descprition_label
            // 
            this.descprition_label.AutoSize = true;
            this.descprition_label.Location = new System.Drawing.Point(79, 12);
            this.descprition_label.Name = "descprition_label";
            this.descprition_label.Size = new System.Drawing.Size(320, 34);
            this.descprition_label.TabIndex = 2;
            this.descprition_label.Text = "Program do wyznaczania spójności krawędziowej \r\ndanego grafu nieskierowanego.";
            this.descprition_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EdgeConnectivity.Properties.Resources.graph;
            this.ClientSize = new System.Drawing.Size(473, 185);
            this.Controls.Add(this.descprition_label);
            this.Controls.Add(this.evaluate_button);
            this.Controls.Add(this.loadGraph_button);
            this.Name = "MainForm";
            this.Text = "Spójność krawędziowa";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadGraph_button;
        private System.Windows.Forms.Button evaluate_button;
        private System.Windows.Forms.Label descprition_label;
    }
}

