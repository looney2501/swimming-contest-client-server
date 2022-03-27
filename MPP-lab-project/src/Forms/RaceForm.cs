using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MPP_lab_project.Domain;
using MPP_lab_project.Domain.DTOs;

namespace MPP_lab_project.Forms;

public partial class RaceForm : GenericForm
{
    private SwimmingDistances _swimmingDistance;
    private SwimmingStyles _swimmingStyle;
    
    public RaceForm(Service.Service service, SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle) : base(service)
    {
        _swimmingDistance = swimmingDistance;
        _swimmingStyle = swimmingStyle;
        InitializeComponent();
    }

    private void InitializeRaceDetailsDataGridView()
    {
        raceDetailsDataGridView.DataSource = service.FindAllSwimmersDetailsForRace(_swimmingDistance, _swimmingStyle);
        raceDetailsDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
    }

    private void backButton_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void RaceForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        new MainForm(service).Show();
    }

    private void RaceForm_Load(object sender, EventArgs e)
    {
        InitializeRaceDetailsDataGridView();
    }
}