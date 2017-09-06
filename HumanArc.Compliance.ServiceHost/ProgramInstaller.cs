using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace HumanArc.Compliance.ServiceHost
{
    [RunInstaller(true)]
    public partial class ProgramInstaller : System.Configuration.Install.Installer
    {
        public ProgramInstaller()
        {
            InitializeComponent();
        }
    }
}
