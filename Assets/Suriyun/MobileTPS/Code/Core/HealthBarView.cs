using UnityEngine;
using UnityEngine.UI;

namespace Suriyun.MobileTPS
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void Change(float currentHealth)
        {
            _bar.fillAmount = currentHealth / GameSession.Instance.PlayerInfo.Health;
        }
    }
}