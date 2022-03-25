using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Config;
using MPP_lab_project.Domain;
using MPP_lab_project.Repository.DBRepository;
using MPP_lab_project.Utils;

namespace MPP_lab_project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            XmlConfigurator.Configure();
        }
    }
}