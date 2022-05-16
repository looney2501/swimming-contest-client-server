using System.Windows.Forms;
using Model.Services;

namespace Client.Forms;

public partial class GenericForm : Form
{
    public GenericForm()
    {
        InitializeComponent();
    }

    public ISwimmingRaceServices SwimmingRaceServicesServer { get; set; }
}