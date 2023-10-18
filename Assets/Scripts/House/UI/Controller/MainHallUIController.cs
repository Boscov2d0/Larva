using Larva.Data;
using Larva.House.Data;
using Larva.House.UI.View;
using Larva.Tools;

using static Larva.House.Tools.HouseState;
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
            _houseManager.RoomState.Value = RoomState.OutSideMenu;
            _audioManager.State.Value = AudioStates.Button;
        }
        private void GoToBedroom()
        {
            _houseManager.RoomState.Value = RoomState.Bedroom;
            _audioManager.State.Value = AudioStates.Button;
        }
        private void OpenKitchenPanel()
        {
            _houseManager.RoomState.Value = RoomState.Kitchen;
            _audioManager.State.Value = AudioStates.Button;
        }
    }
}