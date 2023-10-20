using Larva.Data;
using Larva.House.Data;
using Larva.House.Tools;
using Larva.House.UI.View;
using Larva.Tools;

using static Larva.Tools.AudioKeys;

namespace Larva.House.UI.Controller
{
    public class KitchenUIController : ObjectsDisposer
    {
        private readonly HouseManager _houseManager;
        private readonly AudioManager _audioManager;

        public KitchenUIController(HouseManager houseManager, UIManager uiManager, AudioManager audioManager)
        {
            _houseManager = houseManager;
            _audioManager = audioManager;

            KitchenCanvasView kitchenCanvas = ResourcesLoader.InstantiateAndGetObject<KitchenCanvasView>(uiManager.PathForUIObjects + uiManager.KitchenCanvasPath);
            AddGameObject(kitchenCanvas.gameObject);
            kitchenCanvas.Initialize(GoToMainHall);
        }
        private void GoToMainHall()
        {
            _houseManager.RoomState.Value = HouseState.RoomState.MainHall;
            _audioManager.State.Value = AudioStates.Button;
        }
    }
}