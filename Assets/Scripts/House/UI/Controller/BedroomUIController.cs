using Larva.Data;
using Larva.House.Data;
using Larva.House.UI.View;
using Larva.Tools;

using static Larva.House.Tools.HouseState;
using static Larva.Tools.AudioKeys;

namespace Larva.House.UI.Controller
{
    public class BedroomUIController : ObjectsDisposer
    {
        private readonly HouseManager _houseManager;
        private readonly AudioManager _audioManager;

        public  BedroomUIController(HouseManager houseManager, UIManager uiManager, AudioManager audioManager)
        {
            _houseManager = houseManager;
            _audioManager = audioManager;

            BedroomCanvasView bedroomCanvasView = ResourcesLoader.InstantiateAndGetObject<BedroomCanvasView>(uiManager.PathForUIObjects + uiManager.BedroomCanvasPath);
            AddGameObject(bedroomCanvasView.gameObject);
            bedroomCanvasView.Initialize(GotToMainHall, GoToChildrenRoom);
            
        }
        private void GotToMainHall()
        {
            _houseManager.RoomState.Value = RoomState.MainHall;
            _audioManager.State.Value = AudioStates.Button;
        }
        private void GoToChildrenRoom()
        {
            _houseManager.RoomState.Value = RoomState.ChildrenRoom;
            _audioManager.State.Value = AudioStates.Button;
        }
    }
}