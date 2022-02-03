using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCursor : MonoBehaviour
{
    //�S�[��
    [SerializeField] Transform target;
    //�J�[�\��
    [SerializeField] Transform cursor;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (RoundTripManager.GetIsBackWay())
        {
            this.gameObject.SetActive(true);
            cursor.LookAt(target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
