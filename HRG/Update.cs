using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HRG
{
    /// <summary>
    /// Update handling
    /// </summary>
    class Update : MainWindow
    {
        public Update()
        {
            
        }

        #region Properties

        private long _Progress;
        public long Progress { get { return _Progress; } set { _Progress = value; } }

        #endregion

        #region DownloadLatest

        /// <summary>
        /// Downloads latest app
        /// </summary>
        public void DownloadLatest()
        {
            if (!ApplicationDeployment.IsNetworkDeployed)
                return;

            try
            {
                var deployment = ApplicationDeployment.CurrentDeployment;

                if (deployment.CheckForUpdate())
                {
                    deployment.UpdateProgressChanged += new DeploymentProgressChangedEventHandler(deployment_UpdateProgress);
                    deployment.UpdateCompleted += new AsyncCompletedEventHandler(deployment_UpdateCompleted);
                    deployment.UpdateAsync();
                }
            }
            catch (Exception)
            {
                // TODO log exception here.
            }
        }

        #region UpdateCompleted

        /// <summary>
        /// Update Completed, Restart the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void deployment_UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            System.Windows.Forms.Application.Restart(); 
            System.Windows.Application.Current.Shutdown();
        }

        #endregion

        #region UpdateProgress

        public void deployment_UpdateProgress(object sender, DeploymentProgressChangedEventArgs e)
        {
            Progress = (e.BytesCompleted/e.BytesTotal) * 100;
        }

        #endregion

        #endregion
    }
}
