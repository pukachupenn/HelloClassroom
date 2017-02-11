using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

namespace HelloClassroom.IoT
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
					// When the navigation stack isn't restored navigate to the first page,
					// configuring the new page by passing required information as a navigation
					// parameter
	                //string json;
	                //using (var client = new HttpClient())
	                //{
		               // string uri =
			              //  "http://helloclassroom.azurewebsites.net/api/go/test/i%20want%20to%20know%20about%20Seattle";


					string json = "{\"type\":\"Count\",\"data\":{\"from\":0,\"to\":15}}";
                    rootFrame.Navigate(typeof(MainPage), json);

                    ThreadPoolTimer.CreatePeriodicTimer(timer => CheckIncomingCommand(rootFrame), TimeSpan.FromSeconds(1));
					//dynamic deserializeObject = JsonConvert.DeserializeObject(json);
					//string command = deserializeObject.type;

	    //            if (command.Equals("Timer"))
	    //            {
		   //             rootFrame.Navigate(typeof (Timer), json);
	    //            }
	    //            else if (command.Equals("Count"))
	    //            {
					//	rootFrame.Navigate(typeof(Count), json);
					//}
					//else if (command.Equals("Location"))
					//{
					//	rootFrame.Navigate(typeof(Location), json);
					//}

                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        private void CheckIncomingCommand(Frame frame)
        {
            string json = AzureIoTHub.ReceiveCloudToDeviceMessageAsync().Result;

            dynamic deserializeObject = JsonConvert.DeserializeObject(json);
            string command = deserializeObject.Type;

            frame.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (command.Equals("Timer"))
                {
                    frame.Navigate(typeof(Timer), json);
                }
                else if (command.Equals("Count"))
                {
                    frame.Navigate(typeof(Count), json);
                }
                else if (command.Equals("Location"))
                {
                    frame.Navigate(typeof(Location), json);
                }
            }).GetResults();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
