using System;
using System.Windows.Forms;
using Server.Services;

namespace Client.Forms;

public partial class GenericForm : Form
{
    public Services Services { get; set; }

    public GenericForm()
    {
        InitializeComponent();
    }

}