using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawing : MonoBehaviour
{
    [Header("ƒMƒYƒ‚‚Ì’·‚³")]
    [SerializeField, Range(0, 100)]
    private float _sight_range;

    [Header("ƒMƒYƒ‚‚ÌŠp“x")]
    [SerializeField, Range(0, 360)]
    private float _sight_angle;

    [Header("ƒMƒYƒ‚‚ÉŠ„‚è“–‚Ä‚éƒ}ƒeƒŠƒAƒ‹")]
    [SerializeField]
    private Material mat;
    private GameObject _gizumo;
    private fan _fanGizumo;

    public enemyMove em;
    // Start is called before the first frame update
    void Start()
    {
        _fanGizumo = new fan();
        _gizumo = _fanGizumo.CreateGizmo(this.gameObject, Vector3.zero, Vector3.zero, mat);
        _gizumo.GetComponent<BoxCollider>();
        _sight_range = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if(em.isShot)
        {
            _sight_range = 3;
        }
        else
        {
            _sight_range = 6;
        }
        _fanGizumo.RefreshGizumo(ref _gizumo, this.gameObject, _sight_angle, _sight_range);
    }
}
