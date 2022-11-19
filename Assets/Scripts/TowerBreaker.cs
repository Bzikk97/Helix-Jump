using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class TowerBreaker : MonoBehaviour
{
    #region Variables

    public static TowerBreaker Instance { get; private set; }

    [Header("Settings for break platform")]
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _upwardsModifier;
    [SerializeField] private float _delayBeforeDestroy;

    [Header("Scripts")]
    [SerializeField] private BrokenPlatformCounter _brokenPlatformCounterScr;
    [SerializeField] private AudioManager _audionManagerScr;


    #endregion

    #region Unity Methods

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Public Methods

    public void BreakPlatform(Platform platform)
    {
        platform.PrepareForBreak();

        Collider[] fragments = Physics.OverlapSphere(platform.transform.position, _explosionRadius);
        for (int i = 0; i < fragments.Length; i++)
        {
            Rigidbody fragmentRb = fragments[i].GetComponent<Rigidbody>();
            if (fragmentRb != null)
            {
                fragmentRb.AddExplosionForce(_explosionForce, platform.transform.position, _explosionRadius, _upwardsModifier);
                Debug.Log(123123);
            }
        }

        StartCoroutine(DestroyPLatform(platform));
        _brokenPlatformCounterScr.IncreaseAmount();
        _audionManagerScr.Play("PlatformBreakAudio");
    }

    #endregion

    #region Private Methods

    private IEnumerator DestroyPLatform(Platform platform)
    {
        yield return new WaitForSeconds(_delayBeforeDestroy);
        Destroy(platform.gameObject);
    }

    #endregion 
}
