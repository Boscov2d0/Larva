using Larva.Data;
using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;

using static Larva.Tools.AudioKeys;

public class GameInstructionUIController : ObjectsDisposer
{
    protected readonly GameManager _gameManager;
    protected readonly AudioManager _audioManager;
    public GameInstructionUIController(GameManager gameManager, UIManager uiManager, AudioManager audioManager) 
    {
        _gameManager = gameManager;
        _audioManager = audioManager;

        GameInstructionCanvasView gameInstructionCanvas = ResourcesLoader.InstantiateAndGetObject<GameInstructionCanvasView>(uiManager.PathForUIObjects + uiManager.GameInstructionCanvasPath);
        AddGameObject(gameInstructionCanvas.gameObject);
        gameInstructionCanvas.Initialize(Back);
    }
    private void Back()
    {
        _gameManager.GameState.Value = GameState.Menu;
        _audioManager.State.Value = AudioStates.ButtonApply;
    }
}
