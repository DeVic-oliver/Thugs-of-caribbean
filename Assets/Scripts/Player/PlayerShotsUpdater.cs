namespace Assets.Scripts.Player
{
    using Assets.Scripts.Core.Enums;
    using Assets.Scripts.Core.Enums.Parser;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    

    public class PlayerShotsUpdater : MonoBehaviour
    {
        [Header("Cannon Setup")]
        [SerializeField] private Image _ammoTypeImage;
        [SerializeField] private Image _ammoTypeBackgroundImage;
        [SerializeField] private Sprite _singleCannonSprite;
        [SerializeField] private Sprite _multipleCannonSprite;

        [Header("Shots Setup")]
        [SerializeField] private TextMeshProUGUI _shotsRemainingTMP;
        [SerializeField] private GameObject _reloadingTMP;


        public void SwapCannonSprite(CannonTypes cannonType)
        {
            if(CannonTypeParser.IsSingleCannon(cannonType))
            {
                _ammoTypeImage.sprite = _singleCannonSprite;
                _ammoTypeBackgroundImage.sprite = _singleCannonSprite;
            }
            else
            {
                _ammoTypeImage.sprite = _multipleCannonSprite;
                _ammoTypeBackgroundImage.sprite = _multipleCannonSprite;
            }
        }

        private void Update()
        {
            UpdateShotsRemaining();
            _reloadingTMP.SetActive(PlayerAttack.IsReloading);
        }

        private void UpdateShotsRemaining()
        {
            _shotsRemainingTMP.text = PlayerAttack.ShootsRemaing.ToString();
            _ammoTypeImage.fillAmount = PlayerAttack.ShootsRemaingPercentage;
        }
    }
}