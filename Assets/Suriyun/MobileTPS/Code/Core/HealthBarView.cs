using UnityEngine;
using UnityEngine.UI;

namespace Suriyun.MobileTPS
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void Change(float currentHealth)
        {
            float fillAmount = currentHealth / GameSession.Instance.PlayerInfo.Health;
            _bar.fillAmount = fillAmount;
        }
    }
}