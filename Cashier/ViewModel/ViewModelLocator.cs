using Cashier.Services;

using Microsoft.Extensions.DependencyInjection;

using Mvvm;

namespace Cashier.ViewModel
{
    public class ViewModelLocator
    {
        public static ServiceProvider Services { get; private set; }

        static ViewModelLocator()
        {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            if (ViewModelBase.IsInDesignModeStatic)
            {
                serviceDescriptors.AddTransient<IProductItemViewModel,
                    ProductitemViewModel>();
                serviceDescriptors.AddTransient<IProductTableViewModel,
                    ProductTableViewModelDesign>();
                serviceDescriptors.AddTransient<IDocumentInfoViewModel,
                   DocumentInfoViewModelDesign>(); 
            }
            else
            {
                serviceDescriptors.AddTransient<MainViewModel>();
                serviceDescriptors.AddTransient<IProductItemViewModel,
                    ProductItemViewModel>();
                serviceDescriptors.AddTransient<IProductTableViewModel,
                    ProductTableViewModel>();
                serviceDescriptors.AddTransient<IDocumentInfoViewModel,
                   DocumentInfoViewModel>();

                //DataServices
                serviceDescriptors.AddSingleton<IProductDataService,
                    ProductDataService>();
                serviceDescriptors.AddSingleton<IDialogService,
                    DialogService>();
            }
            Services = serviceDescriptors.BuildServiceProvider();
        }

        public MainViewModel Main
        {
            get
            {
                return Services.GetRequiredService<MainViewModel>();
            }
        }

        public IProductItemViewModel ProductItem
        {
            get
            {
                return Services.GetRequiredService<IProductItemViewModel>();
            }
        }
        public IProductTableViewModel ProductTable
        {
            get
            {
                return Services.GetRequiredService<IProductTableViewModel>();
            }
        }
        public IDocumentInfoViewModel DocumentInfo
        {
            get
            {
                return Services.GetRequiredService<IDocumentInfoViewModel>();
            }
        }

    }
}
