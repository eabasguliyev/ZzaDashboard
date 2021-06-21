using System.Reflection;
using System.Security.AccessControl;
using System.Windows;

namespace ZzaDashboard
{
    public class MvvmBehaviors
    {
        public static string GetLoadedMethodName(DependencyObject obj)
        {
            return (string)obj.GetValue(LoadedMethodNameProperty);
        }

        public static void SetLoadedMethodName(DependencyObject obj, string value)
        {
            obj.SetValue(LoadedMethodNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for MethodName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadedMethodNameProperty =
            DependencyProperty.RegisterAttached("LoadedMethodName", typeof(string), typeof(MvvmBehaviors), new PropertyMetadata(null, OnMethodNameChanged));

        private static void OnMethodNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FrameworkElement uiElement))
                return;

            uiElement.Loaded += (sender, args) =>
            {
                object viewModel = uiElement.DataContext;

                if (viewModel == null)
                    return;

                MethodInfo methodInfo = viewModel.GetType().GetMethod(e.NewValue.ToString());

                if (methodInfo == null)
                    return;

                methodInfo.Invoke(viewModel, null);
            };
        }
    }
}