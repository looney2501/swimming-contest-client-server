using System.ComponentModel;

namespace MPP_lab_project.Forms;

partial class RaceForm
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
        this.raceDetailsDataGridView = new System.Windows.Forms.DataGridView();
        this.backButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize) (this.raceDetailsDataGridView)).BeginInit();
        this.SuspendLayout();
        // 
        // raceDetailsDataGridView
        // 
        this.raceDetailsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.raceDetailsDataGridView.Location = new System.Drawing.Point(21, 24);
        this.raceDetailsDataGridView.Name = "raceDetailsDataGridView";
        this.raceDetailsDataGridView.RowTemplate.Height = 24;
        this.raceDetailsDataGridView.Size = new System.Drawing.Size(727, 476);
        this.raceDetailsDataGridView.TabIndex = 0;
        // 
        // backButton
        // 
        this.backButton.Location = new System.Drawing.Point(754, 460);
        this.backButton.Name = "backButton";
        this.backButton.Size = new System.Drawing.Size(79, 40);
        this.backButton.TabIndex = 1;
        this.backButton.Text = "Back";
        this.backButton.UseVisualStyleBackColor = true;
        this.backButton.Click += new System.EventHandler(this.backButton_Click);
        // 
        // RaceForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(845, 521);
        this.Controls.Add(this.backButton);
        this.Controls.Add(this.raceDetailsDataGridView);
        this.Name = "RaceForm";
        this.Text = "RaceForm";
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RaceForm_FormClosed);
        this.Load += new System.EventHandler(this.RaceForm_Load);
        ((System.ComponentModel.ISupportInitialize) (this.raceDetailsDataGridView)).EndInit();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Button backButton;

    private System.Windows.Forms.DataGridView raceDetailsDataGridView;

    #endregion
}