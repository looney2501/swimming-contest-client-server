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
        this.racesDataGridView = new System.Windows.Forms.DataGridView();
        this.label1 = new System.Windows.Forms.Label();
        this.firstNameLabel = new System.Windows.Forms.Label();
        this.firstNameTextBox = new System.Windows.Forms.TextBox();
        this.lastNameTextBox = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.ageTextBox = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.addSwimmerButton = new System.Windows.Forms.Button();
        this.label2 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.distanceComboBox = new System.Windows.Forms.ComboBox();
        this.styleComboBox = new System.Windows.Forms.ComboBox();
        this.searchRaceButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize) (this.racesDataGridView)).BeginInit();
        this.SuspendLayout();
        // 
        // exitButton
        // 
        this.exitButton.Location = new System.Drawing.Point(793, 510);
        this.exitButton.Name = "exitButton";
        this.exitButton.Size = new System.Drawing.Size(87, 48);
        this.exitButton.TabIndex = 0;
        this.exitButton.Text = "Exit";
        this.exitButton.UseVisualStyleBackColor = true;
        this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
        // 
        // logoutButton
        // 
        this.logoutButton.Location = new System.Drawing.Point(700, 510);
        this.logoutButton.Name = "logoutButton";
        this.logoutButton.Size = new System.Drawing.Size(87, 48);
        this.logoutButton.TabIndex = 1;
        this.logoutButton.Text = "Logout";
        this.logoutButton.UseVisualStyleBackColor = true;
        this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
        // 
        // racesDataGridView
        // 
        this.racesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.racesDataGridView.Location = new System.Drawing.Point(33, 56);
        this.racesDataGridView.Name = "racesDataGridView";
        this.racesDataGridView.RowTemplate.Height = 24;
        this.racesDataGridView.Size = new System.Drawing.Size(459, 502);
        this.racesDataGridView.TabIndex = 2;
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.label1.Location = new System.Drawing.Point(186, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(167, 44);
        this.label1.TabIndex = 3;
        this.label1.Text = "Races";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // firstNameLabel
        // 
        this.firstNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.firstNameLabel.Location = new System.Drawing.Point(576, 265);
        this.firstNameLabel.Name = "firstNameLabel";
        this.firstNameLabel.Size = new System.Drawing.Size(108, 27);
        this.firstNameLabel.TabIndex = 4;
        this.firstNameLabel.Text = "First name:";
        this.firstNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // firstNameTextBox
        // 
        this.firstNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.firstNameTextBox.Location = new System.Drawing.Point(699, 265);
        this.firstNameTextBox.Name = "firstNameTextBox";
        this.firstNameTextBox.Size = new System.Drawing.Size(181, 27);
        this.firstNameTextBox.TabIndex = 5;
        // 
        // lastNameTextBox
        // 
        this.lastNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.lastNameTextBox.Location = new System.Drawing.Point(699, 309);
        this.lastNameTextBox.Name = "lastNameTextBox";
        this.lastNameTextBox.Size = new System.Drawing.Size(181, 27);
        this.lastNameTextBox.TabIndex = 7;
        // 
        // label3
        // 
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.label3.Location = new System.Drawing.Point(576, 309);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(108, 27);
        this.label3.TabIndex = 6;
        this.label3.Text = "Last name:";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ageTextBox
        // 
        this.ageTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.ageTextBox.Location = new System.Drawing.Point(699, 352);
        this.ageTextBox.Name = "ageTextBox";
        this.ageTextBox.Size = new System.Drawing.Size(181, 27);
        this.ageTextBox.TabIndex = 9;
        this.ageTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ageTextBox_KeyPress);
        // 
        // label4
        // 
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.label4.Location = new System.Drawing.Point(618, 352);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(66, 27);
        this.label4.TabIndex = 8;
        this.label4.Text = "Age:";
        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // addSwimmerButton
        // 
        this.addSwimmerButton.Location = new System.Drawing.Point(700, 406);
        this.addSwimmerButton.Name = "addSwimmerButton";
        this.addSwimmerButton.Size = new System.Drawing.Size(180, 33);
        this.addSwimmerButton.TabIndex = 10;
        this.addSwimmerButton.Text = "Add swimmer";
        this.addSwimmerButton.UseVisualStyleBackColor = true;
        this.addSwimmerButton.Click += new System.EventHandler(this.addSwimmerButton_Click);
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.label2.Location = new System.Drawing.Point(578, 72);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(106, 32);
        this.label2.TabIndex = 11;
        this.label2.Text = "Distance:";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label5
        // 
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.label5.Location = new System.Drawing.Point(578, 121);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(106, 32);
        this.label5.TabIndex = 12;
        this.label5.Text = "Style:";
        this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // distanceComboBox
        // 
        this.distanceComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.distanceComboBox.FormattingEnabled = true;
        this.distanceComboBox.Location = new System.Drawing.Point(699, 76);
        this.distanceComboBox.Name = "distanceComboBox";
        this.distanceComboBox.Size = new System.Drawing.Size(181, 28);
        this.distanceComboBox.TabIndex = 13;
        // 
        // styleComboBox
        // 
        this.styleComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
        this.styleComboBox.FormattingEnabled = true;
        this.styleComboBox.Location = new System.Drawing.Point(699, 125);
        this.styleComboBox.Name = "styleComboBox";
        this.styleComboBox.Size = new System.Drawing.Size(181, 28);
        this.styleComboBox.TabIndex = 14;
        // 
        // searchRaceButton
        // 
        this.searchRaceButton.Location = new System.Drawing.Point(699, 173);
        this.searchRaceButton.Name = "searchRaceButton";
        this.searchRaceButton.Size = new System.Drawing.Size(181, 30);
        this.searchRaceButton.TabIndex = 15;
        this.searchRaceButton.Text = "Search";
        this.searchRaceButton.UseVisualStyleBackColor = true;
        this.searchRaceButton.Click += new System.EventHandler(this.searchRaceButton_Click);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(913, 581);
        this.Controls.Add(this.searchRaceButton);
        this.Controls.Add(this.styleComboBox);
        this.Controls.Add(this.distanceComboBox);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.addSwimmerButton);
        this.Controls.Add(this.ageTextBox);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.lastNameTextBox);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.firstNameTextBox);
        this.Controls.Add(this.firstNameLabel);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.racesDataGridView);
        this.Controls.Add(this.logoutButton);
        this.Controls.Add(this.exitButton);
        this.Name = "MainForm";
        this.Text = "MainForm";
        this.Load += new System.EventHandler(this.MainForm_Load);
        ((System.ComponentModel.ISupportInitialize) (this.racesDataGridView)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Button searchRaceButton;

    private System.Windows.Forms.ComboBox styleComboBox;

    private System.Windows.Forms.ComboBox distanceComboBox;

    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Button addSwimmerButton;

    private System.Windows.Forms.TextBox lastNameTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox ageTextBox;
    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Label firstNameLabel;
    private System.Windows.Forms.TextBox firstNameTextBox;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.DataGridView racesDataGridView;

    private System.Windows.Forms.Button exitButton;
    private System.Windows.Forms.Button logoutButton;

    #endregion
}