using Larva.Data;
using Larva.House.Data;
using Larva.House.Tools;
using Larva.House.UI.View;
using Larva.Tools;

using static Larva.Tools.AudioKeys;

namespace Larva.House.UI.Controller
{
    public class ChildrenRoomUIController : ObjectsDisposer
    {
        private readonly HouseManager _houseManager;
        private readonly AudioManager _audioManager;

        public ChildrenRoomUIController(HouseManager houseManager, UIManager uiManager, AudioManager audioManager)
        {
            _houseManager = houseManager;
            _audioManager = audioManager;

            ChildrenRoomCanvasView childrenRoomCanvas = ResourcesLoader.InstantiateAndGetObject<ChildrenRoomCanvasView>(uiManager.PathForUIObjects + uiManager.ChildrenRoomCanvasPath);
            AddGameObject(childrenRoomCanvas.gameObject);
            childrenRoomCanvas.Initialize(GoToBedroon);
            
        }
        private void GoToBedroon() 
        {
            _houseManager.RoomState.Value = HouseState.RoomState.Bedroom;
            _audioManager.State.Value = AudioStates.Button;
        }
    }
}