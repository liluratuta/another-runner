using AnotherRunner.Model.Players;
using TMPro;
using UnityEngine;

namespace AnotherRunner.View
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _speedField;
        [SerializeField] private TMP_Text _jumpHeightField;
        [SerializeField] private TMP_Text _hpField;

        private IPlayer _player;
    
        public void Init(IPlayer player)
        {
            _player = player;
        }

        private void LateUpdate()
        {
            _speedField.text = $"Speed: {_player.RunningSpeed:F2}";
            _jumpHeightField.text = $"Jump Height: {_player.JumpHeight:F2}";
            _hpField.text = $"HP: {_player.HP:F2}";
        }
    }
}
