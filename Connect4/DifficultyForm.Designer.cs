namespace Connect4
{
    partial class DifficultyForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.simulationsTextBox = new System.Windows.Forms.TextBox();
            this.simulationsLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(16, 97);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(115, 32);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Anuleaza";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // simulationsTextBox
            // 
            this.simulationsTextBox.Location = new System.Drawing.Point(16, 60);
            this.simulationsTextBox.Name = "simulationsTextBox";
            this.simulationsTextBox.Size = new System.Drawing.Size(270, 22);
            this.simulationsTextBox.TabIndex = 2;
            // 
            // simulationsLabel
            // 
            this.simulationsLabel.AutoSize = true;
            this.simulationsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simulationsLabel.Location = new System.Drawing.Point(12, 22);
            this.simulationsLabel.Name = "simulationsLabel";
            this.simulationsLabel.Size = new System.Drawing.Size(253, 22);
            this.simulationsLabel.TabIndex = 3;
            this.simulationsLabel.Text = "Introduceti numarul de simulari";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(171, 97);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(115, 32);
            this.submitButton.TabIndex = 4;
            this.submitButton.Text = "Confirma";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // DifficultyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 153);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.simulationsLabel);
            this.Controls.Add(this.simulationsTextBox);
            this.Controls.Add(this.cancelButton);
            this.Name = "DifficultyForm";
            this.Text = "Game Difficulty";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox simulationsTextBox;
        private System.Windows.Forms.Label simulationsLabel;
        private System.Windows.Forms.Button submitButton;
    }
}