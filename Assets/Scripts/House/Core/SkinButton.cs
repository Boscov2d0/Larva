using Larva.House.Data;
using Larva.House.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Larva.House.Core
{
    public class SkinButton : MonoBehaviour
    {
        [field : SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public Material Material { get; private set; }

        private HouseManager _houseManager;
        private SkinType _skinType;

        public void Initialize(HouseManager houseManager, SkinType skinType)
        {
            _houseManager = houseManager;
            _skinType = skinType;

            Button.onClick.AddListener(ChangeSkin);
        }
        private void OnDestroy()
        {
            Button.onClick.RemoveListener(ChangeSkin);
        }
        private void ChangeSkin()
        {
            switch (_skinType) 
            {
                case SkinType.Head:
                    _houseManager.ChoosedHeadSkin.Value = Material;
                    break;
                case SkinType.Body:
                    _houseManager.ChoosedBodySkin.Value = Material;
                    break;
            }
        }
    }
}