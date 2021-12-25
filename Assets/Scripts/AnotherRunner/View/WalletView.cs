using AnotherRunner.Model.Players;
using TMPro;
using UnityEngine;

namespace AnotherRunner.View
{
    [RequireComponent(typeof(TMP_Text))]
    public class WalletView : MonoBehaviour
    {
        private Wallet _wallet;
        private TMP_Text _field;

        public void Init(Wallet wallet)
        {
            _wallet = wallet;
            _wallet.CountChanged += DisplayCount;
        }

        private void Awake() => _field = GetComponent<TMP_Text>();
        private void Start() => DisplayCount(_wallet.Count);
        private void DisplayCount(int count) => _field.text = $"Wallet: {count}";

        private void OnDestroy() => _wallet.CountChanged -= DisplayCount;
    }
}
