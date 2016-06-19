using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRG
{
    /// <summary>
    /// Checks for updates and download them
    /// </summary>
    class Update : MainWindow
    {
        public Update()
        {
            
        }

        /// <summary>
        /// Downloads latest app
        /// </summary>
        public static void DownloadLatest()
        {
            if (!ApplicationDeployment.IsNetworkDeployed)
                return;

            try
            {
                var deployment = ApplicationDeployment.CurrentDeployment;

                if (deployment.CheckForUpdate())
                {
                    deployment.UpdateCompleted += new AsyncCompletedEventHandler( deployment_UpdateCompleted);
                    deployment.UpdateAsync();
                }
            }
            catch (Exception)
            {
                // TODO log exception here.
            }
        }

        public static void deployment_UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Debug.WriteLine("Update Done");
            
        }
    }
}
