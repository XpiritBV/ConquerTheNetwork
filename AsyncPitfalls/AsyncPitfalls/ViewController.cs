using System;
using System.Threading.Tasks;
using UIKit;

namespace AsyncPitfalls
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void performCalculation(UIButton sender)
		{
			actionButton.Enabled = false;
			statusLabel.Text = "working...";
			activitySpinner.StartAnimating();

			var result = PerformLongRunningTask().Result;

			statusLabel.Text = result.ToString();
			activitySpinner.StopAnimating();
			actionButton.Enabled = true;
		}















		private async Task<int> PerformLongRunningTask()
		{
			await Task.Delay(2000); // mimic a long running service call or calculation

			var i = 42;

			await Task.Delay(2000); // mimic another long running task

			return i;
		}
	}
}

