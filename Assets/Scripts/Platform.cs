using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Sector[] _sectors;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                if (player.CurrentPlatform != null) TowerBreaker.Instance.BreakPlatform(player.CurrentPlatform);
                player.CurrentPlatform = this;
                player.IncreaseStreak();
            }
        }

        public void PrepareForBreak()
        {
            for (int i = 0; i < _sectors.Length; i++)
            {
                _sectors[i].PrepareForBreak();
            }
        }
    }
}