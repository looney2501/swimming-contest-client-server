using System.ComponentModel;

namespace MPP_lab_project.Forms;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        this.exitButton = new System.Windows.Forms.Button();
        this.logoutButton = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // exitButton
        // 
        this.exitButton.Location = new System.Drawing.Point(788, 512);
        this.exitButton.Name = "exitButton";
        this.exitButton.Size = new System.Drawing.Size(87, 48);
        this.exitButton.TabIndex = 0;
        this.exitButton.Text = "Exit";
        this.exitButton.UseVisualStyleBackColor = true;
        this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
        // 
        // logoutButton
        // 
        this.logoutButton.Location = new System.Drawing.Point(695, 512);
        this.logoutButton.Name = "logoutButton";
        this.logoutButton.Size = new System.Drawing.Size(87, 48);
        this.logoutButton.TabIndex = 1;
        this.logoutButton.Text = "Logout";
        this.logoutButton.UseVisualStyleBackColor = true;
        this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(887, 572);
        this.Controls.Add(this.logoutButton);
        this.Controls.Add(this.exitButton);
        this.Name = "MainForm";
        this.Text = "MainForm";
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Button exitButton;
    private System.Windows.Forms.Button logoutButton;

    #endregion
}