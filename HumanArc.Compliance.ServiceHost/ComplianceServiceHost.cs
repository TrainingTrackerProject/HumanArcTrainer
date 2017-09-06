using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace HumanArc.Compliance.ServiceHost
{
    public partial class ComplianceServiceHost : ServiceBase
    {
        private readonly ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private Thread _thread;

        public ComplianceServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _thread = new Thread(ComplianceThread);
            _thread.Name = "Compliance Thread";
            _thread.IsBackground = true;
            _thread.Start();
        }

        private void ComplianceThread()
        {
            while (_shutdownEvent.WaitOne(0))
            {
                //Perform magic.
                Thread.Sleep(1000);
            }
        }

        protected override void OnStop()
        {
            _shutdownEvent.Set();
            if (!_thread.Join(3000))
            {
                _thread.Abort();
            }
        }
    }
}
