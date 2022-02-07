using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeAreaManager : MonoBehaviour
{
    public GameObject camera;
    public GameObject escapeArea;
    public Transform[] areaTrans;
    public GameObject player;
    public float moveTime;

    private int posNum;

    private PlayerFollowCamera followCamera;
    private float countTime;
    private bool createOnce = true;
    private Vector3 areaPos;
    private Vector3 startPos;
    private bool isToArea = true;
    private bool moveOnce = true;

    private const float stopLimit = 2;
    private bool isStop = false;
    private float stopCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        followCamera = camera.GetComponent<PlayerFollowCamera>();
        createOnce = true;
        posNum = areaTrans.Length;
        areaPos = Vector3.zero;
        startPos = Vector3.zero;
        isToArea = true;
        moveOnce = true;
        isStop = false;
        stopCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (RoundTripManager.GetIsBackWay() && createOnce)
            Create();

        if (createOnce || !moveOnce)
            return;

        countTime += Time.deltaTime;

        if (moveOnce && isToArea && !isStop)
            MoveToCamera();
        else if (moveOnce && isStop && isToArea)
            StopCamera();
        else if (moveOnce && !isToArea && !isStop)
            MoveReCamera();
    }

    void Create()
    {
        int rnd;
        rnd = Random.Range(0, posNum);
        GameObject obj;
        obj = Instantiate(escapeArea,areaTrans[rnd]);
        areaPos = new Vector3(obj.transform.position.x, camera.transform.position.y, obj.transform.position.z);
        startPos = new Vector3(player.transform.position.x, camera.transform.position.y, player.transform.position.z);
        createOnce = false;
        followCamera.SetMoveFlag(true);
    }

    void MoveToCamera()
    {
        float rate = countTime / moveTime;
        camera.transform.position = Vector3.Lerp(startPos, areaPos, rate);

        if(rate >=1.0f)
        {
            //isToArea = false;
            isStop = true;
            countTime = 0.0f;
        }
    }

    void StopCamera()
    {
        stopCount += Time.deltaTime;
        camera.transform.position = areaPos;

        if (stopCount>=stopLimit)
        {
            isStop = false;
            isToArea = false;
            stopCount = 0;
            countTime = 0.0f;
        }
    }

    void MoveReCamera()
    {
        float rate = countTime / moveTime;
        camera.transform.position = Vector3.Lerp(areaPos, startPos, rate);

        if (rate >= 1.0f)
        {
            moveOnce = false;
            countTime = 0.0f;
            followCamera.SetMoveFlag(false);
        }
    }
}
