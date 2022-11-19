using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float BounceSpeed;
    public Rigidbody Rigidbody;
    [SerializeField] private MeshRenderer _playerMesh;
    [SerializeField] private GameObject _line;
    public Game Game;

    public Platform CurrentPlatform;
    private AudioManager audioManager;

    [Header("Settings for bounce")]
    [SerializeField] private GameObject _bounceEffect;
    [SerializeField] private GameObject _blot;

    [Header("Settings for die")]
    [SerializeField] private Material _dieMaterial;
    [SerializeField] private float _delayToDie;

    [Header("Settings for boost")]
    [SerializeField] private int _minStreakAmount;
    public int MinStreakAmount { get { return _minStreakAmount; } }
    public int Streak { get; private set; }

    private void Start()
    {
        Instance = this;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Bounce()
    {
        Streak = 0;
        audioManager.Play("BounseAudio");
        Rigidbody.velocity = new Vector3(0, BounceSpeed, 0);

        GameObject effect = Instantiate(_bounceEffect);
        effect.transform.position = transform.position - new Vector3(0f, 0.5f, 0f);

        //StartCoroutine(SpawnBlot());
    }

    public void Die()
    {
        StartCoroutine(WaitToDie());
    }
    private IEnumerator WaitToDie()
    {
        _playerMesh.material = _dieMaterial;
        _line.gameObject.SetActive(false);

        float value = 2;
        for (float t = 0; t < _delayToDie; t += Time.deltaTime)
        {
            float newValue = Mathf.Lerp(value, -1, t / _delayToDie);
            _playerMesh.material.SetFloat("_CutoffHeight", newValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);

        Game.OnPlayerDied();
        Rigidbody.velocity = Vector3.zero;
    }

    public void ReachFinish()
    {
        Game.OnPlayerReachedFinish();
        Rigidbody.velocity = Vector3.zero;
    }

    public void IncreaseStreak() => Streak++;

    private IEnumerator SpawnBlot()
    {
        GameObject blot = Instantiate(_blot, CurrentPlatform.transform);
        blot.transform.position = transform.position - new Vector3(0f, 0.48f, 0f);
        blot.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        yield return new WaitForSeconds(5);

        Destroy(blot);
    }
}
