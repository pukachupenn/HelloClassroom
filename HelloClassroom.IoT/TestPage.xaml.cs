namespace HelloClassroom.IoT
{
    using System;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    using Newtonsoft.Json;

    public sealed partial class TestPage : Page
    {
        private TimerViewModel timerViewModel;

        public TestPage()
        {
            this.InitializeComponent();
        }
    }
}
