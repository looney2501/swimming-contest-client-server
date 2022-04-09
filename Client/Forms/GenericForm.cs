using System;
using System.Windows.Forms;
using Server.Services;

namespace Client.Forms;

public partial class GenericForm : Form
{
    public SwimmingRaceServicesServer SwimmingRaceServicesServer { get; set; }

    public GenericForm()
    {
        InitializeComponent();
    }

}