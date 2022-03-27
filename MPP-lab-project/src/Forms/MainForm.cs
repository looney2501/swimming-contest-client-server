using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using MPP_lab_project.Domain;
using MPP_lab_project.Domain.DTOs;

namespace MPP_lab_project.Forms;

public partial class MainForm : GenericForm
{
    private Service.Service _service;

    public MainForm(Service.Service service) : base(service)
    {
        _service = service;
        InitializeComponent();
    }

    private void InitializeRacesDataGridView()
    {
        racesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        RefreshRacesDataGridView(); 
    }

    private void InitializeComboBoxes()
    {
        distanceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        distanceComboBox.Items.Add(SwimmingDistances._50m);
        distanceComboBox.Items.Add(SwimmingDistances._200m);
        distanceComboBox.Items.Add(SwimmingDistances._800m);
        distanceComboBox.Items.Add(SwimmingDistances._1500m);

        styleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        styleComboBox.Items.Add(SwimmingStyles.Freestyle);
        styleComboBox.Items.Add(SwimmingStyles.Backstroke);
        styleComboBox.Items.Add(SwimmingStyles.Butterfly);
        styleComboBox.Items.Add(SwimmingStyles.Mixed);
    }

    private void RefreshRacesDataGridView()
    {
        racesDataGridView.DataSource = service.FindAllRacesDetails();
        racesDataGridView.ClearSelection();
    }

    private void RefreshTextBoxes()
    {
        firstNameTextBox.Clear();
        lastNameTextBox.Clear();
        ageTextBox.Clear();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        InitializeRacesDataGridView();
        InitializeComboBoxes();
    }

    private void exitButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void logoutButton_Click(object sender, EventArgs e)
    {
        Close();
        new LoginForm(service).Show();
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
            service.AddSwimmer(firstName, lastName, Int32.Parse(age), raceDetailsDTOs);
            RefreshRacesDataGridView();
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
            Close();
            new RaceForm(service, (SwimmingDistances) selectedDistance, (SwimmingStyles) selectedStyle).Show();
        }
    }
}