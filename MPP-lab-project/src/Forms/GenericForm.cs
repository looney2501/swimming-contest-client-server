using System.Windows.Forms;

namespace MPP_lab_project.Forms;

public partial class GenericForm : Form
{
    protected Service.Service service;
    
    public GenericForm(Service.Service service)
    {
        this.service = service;
        InitializeComponent();
    }
}