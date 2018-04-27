using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(Page), typeof(UXDivers.Artina.Shared.ArtinaPageRenderer2))]
namespace UXDivers.Artina.Shared
{
	public class ArtinaPageRenderer2 : PageRenderer
	{
		private NSObject _keyboardShowObserver;
		private NSObject _keyboardHideObserver;
		private double _activeViewBottom;
		private double _originalY;
		private bool _pageWasShiftedUp;
		private bool _isKeyboardShown;

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			var page = Element as ContentPage;

			if (page != null)
			{
				var contentScrollView = page.Content as ScrollView;

				if (contentScrollView != null)
				{
					// With a ScrollView as content Forms takes care of scrolling
					// altough it doesn't take into account the Margin.Bottom value 
					// on the first responder
					return;
				}

				RegisterForKeyboardNotifications();
			}
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			UnregisterForKeyboardNotifications();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			UnregisterForKeyboardNotifications();
		}

		private void RegisterForKeyboardNotifications()
		{
			if (_keyboardShowObserver == null)
			{
				_keyboardShowObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardShow);
			}

			if (_keyboardHideObserver == null)
			{
				_keyboardHideObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardHide);
			}
		}

		private void UnregisterForKeyboardNotifications()
		{
			_isKeyboardShown = false;
			if (_keyboardShowObserver != null)
			{
				NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardShowObserver);
				_keyboardShowObserver.Dispose();
				_keyboardShowObserver = null;
			}

			if (_keyboardHideObserver != null)
			{
				NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardHideObserver);
				_keyboardHideObserver.Dispose();
				_keyboardHideObserver = null;
			}
		}

		protected virtual void OnKeyboardShow(NSNotification notification)
		{
			if (!IsViewLoaded || _isKeyboardShown)
			{
				return;
			}

			var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
			if (keyboardFrame.Height == 0)
			{
				// Happens when rotating the device, this is called multiple times and eventually
				// the frame is correct (height > 0)
				return;
			}

			_isKeyboardShown = true;
			var activeView = FindFirstResponder(View);

			if (activeView == null)
			{
				return;
			}

			double offset = 0;
			if (activeView.Superview is IVisualElementRenderer renderer)
			{
				if (renderer.Element is View view)
				{
					// Honors the Xamarin.Forms element margin
					offset = view.Margin.Bottom;
				}
			}

			var isOverlapping = IsKeyboardOverlapping(activeView, keyboardFrame, offset);

			if (!isOverlapping)
			{
				return;
			}

			if (isOverlapping)
			{
				_activeViewBottom = GetViewRelativeBottom(activeView, offset);
				ShiftPageUp(keyboardFrame, _activeViewBottom);
			}
		}

		private void OnKeyboardHide(NSNotification notification)
		{
			if (!IsViewLoaded)
			{
				return;
			}

			_isKeyboardShown = false;
			var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);

			if (_pageWasShiftedUp)
			{
				ShiftPageDown(keyboardFrame, _activeViewBottom);
			}
		}

		private void ShiftPageUp(CGRect keyboardFrame, double activeViewBottom)
		{
			var pageFrame = Element.Bounds;

			_originalY = pageFrame.Y;

			var newY = pageFrame.Y + keyboardFrame.Y - activeViewBottom;
						        
			Element.LayoutTo(new Rectangle(pageFrame.X, newY, pageFrame.Width, pageFrame.Height));

			_pageWasShiftedUp = true;
		}

		private void ShiftPageDown(CGRect keyboardFrame, double activeViewBottom)
		{
			var pageFrame = Element.Bounds;

			Element.LayoutTo(new Rectangle(pageFrame.X, _originalY, pageFrame.Width, pageFrame.Height));

			_pageWasShiftedUp = false;
		}

		private static UIView FindFirstResponder(UIView view)
		{
			if (view.IsFirstResponder)
			{
				return view;
			}

			foreach (UIView subView in view.Subviews)
			{
				var firstResponder = FindFirstResponder(subView);
				if (firstResponder != null)
				{
					return firstResponder;
				}
			}

			return null;
		}

		private static bool IsKeyboardOverlapping(UIView activeView, CGRect keyboardFrame, double requiredBottomOffset)
		{
			var activeViewBottom = GetViewRelativeBottom(activeView, requiredBottomOffset);
			var keyboardTop = keyboardFrame.Y;

			var isOverlapping = activeViewBottom >= keyboardTop;

			return isOverlapping;
		}

		private static double GetViewRelativeBottom(UIView view, double requiredBottomOffset)
		{
			var viewAbsoluteCoordinates = UIScreen.MainScreen.CoordinateSpace.ConvertPointToCoordinateSpace(view.Frame.Location, view); 

			return -viewAbsoluteCoordinates.Y + view.Frame.Height + requiredBottomOffset;
		}
	}
}
