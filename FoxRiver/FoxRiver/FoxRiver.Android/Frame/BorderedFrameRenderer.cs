using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.FastRenderers;
using AColor = Android.Graphics.Color;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Frame), typeof(FoxRiver.Frame.BorderedFrameRenderer))]
namespace FoxRiver.Frame
{
    public class BorderedFrameRenderer : Xamarin.Forms.Platform.Android.FastRenderers.FrameRenderer
    {
        float _defaultElevation = -1f;
        float _defaultCornerRadius = -1f;

        GradientDrawable _backgroundDrawable;

        public BorderedFrameRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _backgroundDrawable = new GradientDrawable();
                _backgroundDrawable.SetShape(ShapeType.Rectangle);
                this.SetBackground(_backgroundDrawable);

                UpdateShadow();
                UpdateBackgroundColor();
                UpdateCornerRadius();
                UpdateOutlineColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.Frame.HasShadowProperty.PropertyName)
                UpdateShadow();
            else if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
                UpdateBackgroundColor();
            else if (e.PropertyName == Xamarin.Forms.Frame.CornerRadiusProperty.PropertyName)
                UpdateCornerRadius();
            else if (e.PropertyName == Xamarin.Forms.Frame.OutlineColorProperty.PropertyName)
                UpdateOutlineColor();
        }

        void UpdateBackgroundColor()
        {
            Color bgColor = Element.BackgroundColor;
            _backgroundDrawable.SetColor(bgColor.IsDefault ? AColor.White : bgColor.ToAndroid());
        }

        void UpdateOutlineColor()
        {
            Color outlineColor = Element.OutlineColor;
            _backgroundDrawable.SetStroke(3, outlineColor.IsDefault ? AColor.White : outlineColor.ToAndroid());
        }

        void UpdateShadow()
        {
            float elevation = _defaultElevation;

            if (elevation == -1f)
                _defaultElevation = elevation = CardElevation;

            if (Element.HasShadow)
                CardElevation = elevation;
            else
                CardElevation = 0f;
        }

        void UpdateCornerRadius()
        {
            if (_defaultCornerRadius == -1f)
            {
                _defaultCornerRadius = Radius;
            }

            float cornerRadius = Element.CornerRadius;

            if (cornerRadius == -1f)
                cornerRadius = _defaultCornerRadius;
            else
                cornerRadius = Context.ToPixels(cornerRadius);

            _backgroundDrawable.SetCornerRadius(cornerRadius);
        }
    }
}