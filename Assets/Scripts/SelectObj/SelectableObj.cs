using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectableObj: MonoBehaviour
{
    [SerializeField] private GameObject _selectionIndicator;
    private Material _magicSelectionMaterial;
    public bool Selected { get; private set; } = false;


    // Start is called before the first frame update
    void Start()
    {
        Selected = false;
        _selectionIndicator.SetActive(false);
        _magicSelectionMaterial = _selectionIndicator.GetComponent<MeshRenderer>().material;

        InitializeOnStart();
    }

    protected abstract void InitializeOnStart();

    public float GetAngle(Transform trans)
    {
        Vector3 relative = trans.InverseTransformPoint(transform.position);
        return Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
    }

    public void Select()
    {
        Selected = true;
        _selectionIndicator.GetComponent<MeshRenderer>().material = _magicSelectionMaterial;
        _selectionIndicator.SetActive(true);
    }

    public void DeSelect()
    {
        Selected = false;
        _selectionIndicator.SetActive(false);
    }
    public abstract void ProcessInput();
}
