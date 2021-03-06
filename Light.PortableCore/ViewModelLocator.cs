using GalaSoft.MvvmLight.Ioc;
using Light.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace Light
{
    public class ViewModelLocator
    {
        public const string ShowChildViewModelMessage = "ShowChildViewModelMessage";

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ChildViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public ChildViewModel ChildViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ChildViewModel>(); }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}