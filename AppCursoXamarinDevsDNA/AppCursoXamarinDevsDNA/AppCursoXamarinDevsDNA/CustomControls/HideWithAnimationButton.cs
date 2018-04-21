using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.CustomControls
{
    public class HideWithAnimationButton : Button
    {
        public HideWithAnimationButton()
        {

        }

        public double AbsoluteLayoutY
        {
            get { return (double)GetValue(AbsoluteLayoutYProperty); }
            set { SetValue(AbsoluteLayoutYProperty, value); }
        }

        public static BindableProperty AbsoluteLayoutYProperty =
            BindableProperty.Create(nameof(AbsoluteLayoutY), typeof(double), typeof(HideWithAnimationButton), default(double), BindingMode.Default);


        public double TranslationInAbsoluteLayoutY
        {
            get { return (double)GetValue(TranslationInAbsoluteLayoutYProperty); }
            set { SetValue(TranslationInAbsoluteLayoutYProperty, value); }
        }

        public static BindableProperty TranslationInAbsoluteLayoutYProperty =
            BindableProperty.Create(nameof(TranslationInAbsoluteLayoutY), typeof(double), typeof(HideWithAnimationButton), default(double), BindingMode.Default);


        public bool IsVisibleWithBottomAnimation
        {
            get { return (bool)GetValue(IsVisibleWithBottomAnimationProperty); }
            set { SetValue(IsVisibleWithBottomAnimationProperty, value); }
        }

        public static readonly BindableProperty IsVisibleWithBottomAnimationProperty = BindableProperty.Create(
            propertyName: nameof(IsVisibleWithBottomAnimation),
            returnType: typeof(bool),
            declaringType: typeof(HideWithAnimationButton),
            defaultValue: default(bool),
            defaultBindingMode: BindingMode.Default,
            propertyChanged: IsVisiblePropertyChanged);

        private static void IsVisiblePropertyChanged(BindableObject view, object oldValue, object newValue)
        {
            var button = view as HideWithAnimationButton;

            if (button != null)
            {
                if ((bool)oldValue == false && (bool)newValue)
                {
                    Action<double> callback = input => {
                        double currentY = button.AbsoluteLayoutY + input;
                        AbsoluteLayout.SetLayoutBounds(button, new Rectangle(0.5, currentY, 1, -1));
                        AbsoluteLayout.SetLayoutFlags(button, AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.YProportional | AbsoluteLayoutFlags.WidthProportional);
                    };
                    double start = button.TranslationInAbsoluteLayoutY;
                    double end = 0;
                    uint rate = 16; // pace at which animation proceeds
                    uint length = 500; // 0.5 second animation
                    Easing easing = Easing.CubicOut;

                    button.IsEnabled = true;
                    button.Animate("visible", callback, start, end, rate, length, easing);
                }
                else if ((bool)newValue == false)
                {
                    Action<double> callback = input => {
                        double currentY = button.AbsoluteLayoutY + input;
                        AbsoluteLayout.SetLayoutBounds(button, new Rectangle(0.5, currentY, 1, -1));
                        AbsoluteLayout.SetLayoutFlags(button, AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.YProportional | AbsoluteLayoutFlags.WidthProportional);
                    };
                    Action<double, bool> finished = (value, cancel) => {
                        if (!cancel)
                        {
                            button.IsEnabled = false;
                        }
                    };
                    double start = 0;
                    double end = button.TranslationInAbsoluteLayoutY;
                    uint rate = 16; // pace at which animation proceeds
                    uint length = 500; // 0.5 second animation
                    Easing easing = Easing.CubicIn;

                    button.Animate("invisible", callback, start, end, rate, length, easing, finished);
                }
            }
        }

    }
}
