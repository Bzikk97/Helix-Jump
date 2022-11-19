using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public bool IsGood = true;
    public Material GoodMaterial;
    public Material BadMaterial;

    [Header("Objects")]
    [SerializeField] private MeshRenderer _sectorMesh;
    [SerializeField] private Collider _sectorCollider;
    [SerializeField] private MeshRenderer[] _fragments;

    private void Awake()
    {
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        Renderer sectorRenderer = GetComponent<Renderer>();
        sectorRenderer.sharedMaterial = IsGood ? GoodMaterial : BadMaterial;

        for (int i = 0; i < _fragments.Length; i++)
        {
            _fragments[i].material = sectorRenderer.sharedMaterial;
        }
    }

    public void PrepareForBreak()
    {
        _sectorMesh.enabled = false;
        _sectorCollider.enabled = false;

        for (int i = 0; i < _fragments.Length; i++)
        {
            _fragments[i].gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.TryGetComponent(out Player player)) return;
        {
            Vector3 normal = -collision.contacts[0].normal.normalized;
            float dot = Vector3.Dot(normal,Vector3.up);
            if (dot < 0.5) return;
            {
                if (IsGood)
                {
                    if (player.Streak >= player.MinStreakAmount) TowerBreaker.Instance.BreakPlatform(player.CurrentPlatform);

                    player.Bounce();
                }
                else
                {
                    player.Die();
                }
            }
        }
    }
    
    private void OnValidate()
    {
        UpdateMaterial();
    }
}
