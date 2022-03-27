using System.Windows.Forms;

namespace MPP_lab_project.Forms;

public partial class MainForm : GenericForm
{
    public MainForm(Service.Service service) : base(service)
    {
        InitializeComponent();
    }
}