using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Model.Domain.DTOs;
using Model.Domain.Enums;
using Model.Observer;
using Model.Services;
using Server.Services;

namespace Client.Forms;

public partial class MainForm : GenericForm, ISwimmingRaceObserver
{
    public LoginForm LoginForm { get; set; }
    public string LoggedUsername { get; set; }
    private SwimmingDistance? _swimmingDistance;
    private SwimmingStyle? _swimmingStyle;

    public MainForm()
    {
        InitializeComponent();
        Text = @"Swimming races administration";
    }

    private void InitializeGridViews()
    {
        racesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        raceDetailsDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        UpdateGridViews(); 
    }

    private void InitializeComboBoxes()
    {
        distanceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        distanceComboBox.Items.Add(SwimmingDistance._50m);
        distanceComboBox.Items.Add(SwimmingDistance._200m);
        distanceComboBox.Items.Add(SwimmingDistance._800m);
        distanceComboBox.Items.Add(SwimmingDistance._1500m);

        styleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        styleComboBox.Items.Add(SwimmingStyle.Freestyle);
        styleComboBox.Items.Add(SwimmingStyle.Backstroke);
        styleComboBox.Items.Add(SwimmingStyle.Butterfly);
        styleComboBox.Items.Add(SwimmingStyle.Mixed);

        _swimmingDistance = null;
        _swimmingStyle = null;

    }

    private void UpdateGridViews()
    {
        racesDataGridView.DataSource = SwimmingRaceServicesServer.FindAllRacesDetails();
        racesDataGridView.ClearSelection();
        if (_swimmingDistance != null)
        {
            if (_swimmingStyle != null)
            {
                raceDetailsDataGridView.DataSource =
                    SwimmingRaceServicesServer.FindAllSwimmersDetailsForRace((SwimmingDistance) _swimmingDistance,
                        (SwimmingStyle) _swimmingStyle);
            }
        }
        else
        {
            raceDetailsDataGridView.DataSource = new List<SwimmerDTO>();
        }
        raceDetailsDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
    }

    private void RefreshTextBoxes()
    {
        firstNameTextBox.Clear();
        lastNameTextBox.Clear();
        ageTextBox.Clear();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        InitializeGridViews();
        InitializeComboBoxes();
    }

    private void logoutButton_Click(object sender, EventArgs e)
    {
        try
        {
            SwimmingRaceServicesServer.Logout(LoggedUsername);
            Close();
            LoginForm.Show();
        }
        catch (ServicesException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ageTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void addSwimmerButton_Click(object sender, EventArgs e)
    {
        string firstName = firstNameTextBox.Text;
        string lastName = lastNameTextBox.Text;
        string age = ageTextBox.Text;
        var selectedRows = racesDataGridView.SelectedRows;
        if (firstName.Length == 0)
        {
            MessageBox.Show(@"Introduceti prenume!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else if (lastName.Length == 0)
        {
            MessageBox.Show(@"Introduceti nume!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else if (age.Length == 0)
        {
            MessageBox.Show(@"Introduceti varsta!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else if (selectedRows.Count < 1)
        {
            MessageBox.Show(@"Selectati cel putin o cursa!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            List<RaceDetailsDTO> raceDetailsDTOs = new List<RaceDetailsDTO>();
            List<RaceDTO> allRaces = (List<RaceDTO>) racesDataGridView.DataSource;
            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                int index = selectedRow.Index;
                raceDetailsDTOs.Add(new RaceDetailsDTO(allRaces[index].SwimmingDistance, allRaces[index].SwimmingStyle));
            }
            SwimmingRaceServicesServer.AddSwimmer(firstName, lastName, Int32.Parse(age), raceDetailsDTOs);
            RefreshTextBoxes();
        }
    }

    private void searchRaceButton_Click(object sender, EventArgs e)
    {
        var selectedDistance = distanceComboBox.SelectedItem;
        var selectedStyle = styleComboBox.SelectedItem;
        if (selectedDistance == null || selectedStyle == null)
        {
            MessageBox.Show(@"Valori invalide!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            _swimmingDistance = (SwimmingDistance?) selectedDistance;
            _swimmingStyle = (SwimmingStyle?) selectedStyle;
            raceDetailsDataGridView.DataSource =
                SwimmingRaceServicesServer.FindAllSwimmersDetailsForRace((SwimmingDistance) _swimmingDistance,
                    (SwimmingStyle) _swimmingStyle);
            raceDetailsDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }

    public void RacesUpdated()
    {
        BeginInvoke(() =>
        {
            UpdateGridViews();
        });
    }
}