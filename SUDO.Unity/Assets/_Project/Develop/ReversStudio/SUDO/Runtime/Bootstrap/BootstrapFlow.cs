using ReversStudio.SUDO.Runtime.Bootstrap.Units;
using ReversStudio.SUDO.Runtime.Utilities;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace ReversStudio.SUDO.Runtime.Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        private readonly LoadingService _loadingService;
        private readonly SceneManager _sceneManager;

        public BootstrapFlow(LoadingService loadingService, SceneManager sceneManager)
        {
            _loadingService = loadingService;
            _sceneManager = sceneManager;
        }
        
        public async void Start()
        {
            var fooLoadingUnit = new FooLoadingUnit();
            await _loadingService.BeginLoading(fooLoadingUnit);

            _sceneManager.LoadScene(RuntimeConstants.Scenes.Loading).Forget();
        }
    }
}
