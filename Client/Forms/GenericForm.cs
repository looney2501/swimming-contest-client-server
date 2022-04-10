using System;
using System.Windows.Forms;
using Model.Services;
using Server.Services;

namespace Client.Forms;

public partial class GenericForm : Form
{
    public ISwimmingRaceServices SwimmingRaceServicesServer { get; set; }

    public GenericForm()
    {
        InitializeComponent();
    }

}