using Larva.Data;
using Larva.House.Data;
using Larva.House.Tools;
using Larva.House.UI.View;
using Larva.Tools;

using static Larva.Tools.AudioKeys;

namespace Larva.House.UI.Controller
{
    public class MainHallUIController : ObjectsDisposer
    {
        private readonly HouseManager _houseManager;
        private readonly AudioManager _audioManager;

        public MainHallUIController(HouseManager houseManager, UIManager uiManager, AudioManager audioManager) 
        {
            _houseManager = houseManager;
            _audioManager = audioManager;

            MainHallCanvasView mainHallCanvasView = ResourcesLoader.InstantiateAndGetObject<MainHallCanvasView>(uiManager.PathForUIObjects + uiManager.MainHallCanvasPath);
            AddGameObject(mainHallCanvasView.gameObject);
            mainHallCanvasView.Initialize(GoOutSide, GoToBedroom, OpenKitchenPanel);
        }
        private void GoOutSide()
        {
            _houseManager.HouseState.Value = HouseState.OutSideMenu;
            _audioManager.State.Value = AudioStates.Button;
        }
        private void GoToBedroom()
        {
            _houseManager.HouseState.Value = HouseState.Bedroom;
            _audioManager.State.Value = AudioStates.Button;
        }
        private void OpenKitchenPanel()
        {
            _houseManager.HouseState.Value = HouseState.Kitchen;
            _audioManager.State.Value = AudioStates.Button;
        }
    }
}