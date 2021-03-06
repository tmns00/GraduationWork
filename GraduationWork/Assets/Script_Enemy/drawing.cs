using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawing : MonoBehaviour
{
    [Header("ギズモの長さ")]
    [SerializeField, Range(0, 100)]
    private float _sight_range;

    [Header("ギズモの角度")]
    [SerializeField, Range(0, 360)]
    private float _sight_angle;

    [Header("ギズモに割り当てるマテリアル")]
    [SerializeField]
    private Material mat;
    private GameObject _gizumo;
    private fan _fanGizumo;

    public enemyMove em;
    public Fear fear;
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
        if(fear.FearLevel==0)
        {
            if(em.isShot)
            {
                _sight_range = 3;
            }
            if(!em.isShot)
            {
                _sight_range = 6;
            }
        }
        if (fear.FearLevel == 1)
        {
            if (em.isShot)
            {
                _sight_range = 4.5f;
            }
            if (!em.isShot)
            {
                _sight_range = 6;
            }
        }
       
        _fanGizumo.RefreshGizumo(ref _gizumo, this.gameObject, _sight_angle, _sight_range);
    }
}
