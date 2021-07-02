using Unity;
using Unity.Lifetime;
using ZzaDashboard.Services;

namespace ZzaDashboard
{
    public static class ContainerHelper
    {
        public static IUnityContainer Container { get; private set; }

        static ContainerHelper()
        {
            Container = new UnityContainer();
            Container.RegisterType<ICustomersRepository, CustomersRepository>(
                new ContainerControlledLifetimeManager());
        }

    }
}