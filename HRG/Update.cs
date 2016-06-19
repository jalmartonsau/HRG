using HRG.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace HRG
{
    /// <summary>
    /// Update handling
    /// </summary>
    class Update : StartVM
    {
        public static StartVM DataContext { get; private set; }

        public Update()
        {
            
        }

        #region DownloadLatest

        /// <summary>
        /// Downloads latest app
        /// </summary>
        public static void DownloadLatest()
        {
            //var vm = DataContext as Models.StartVM;
            //vm.Task = "downloading";

            if (!ApplicationDeployment.IsNetworkDeployed)
                return;

            try
            {
                var deployment = ApplicationDeployment.CurrentDeployment;

                if (deployment.CheckForUpdate())
                {
                    deployment.UpdateCompleted += new AsyncCompletedEventHandler(deployment_UpdateCompleted);
                    deployment.UpdateProgressChanged += new DeploymentProgressChangedEventHandler(deployment_UpdateProgress);
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

        public static void deployment_UpdateProgress(object sender, DeploymentProgressChangedEventArgs e)
        {
            var vm = DataContext as Models.StartVM;
            try
            {
                vm.Progress = (e.BytesCompleted / e.BytesTotal) * 100;
            }
            catch (Exception) {

            }
        }

        #endregion

        #endregion

    }
}
