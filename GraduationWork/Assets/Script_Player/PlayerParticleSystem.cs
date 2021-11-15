using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleSystem : MonoBehaviour
{
    public GameObject toParticle;
    public GameObject inParticle;
    public GameObject reParticle;

    [SerializeField]
    private GhostSystem ghostSystem;
    [SerializeField]
    private bool currentGhost = false;

    public float toTime = 1.0f;
    public float reTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        toParticle.SetActive(false);
        inParticle.SetActive(false);
        reParticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ghostSystem.GetIsGhost() && !currentGhost)
            return;


        if (ghostSystem.GetIsGhost() && !currentGhost)
            StartCoroutine(ToGhost());
        else if (ghostSystem.GetIsGhost() && currentGhost)
            InGhost();
        else if (!ghostSystem.GetIsGhost() && currentGhost)
            StartCoroutine(ReGhost());
    }

    IEnumerator ToGhost()
    {
        PauseSystem.SetPauseFlag(true);
        toParticle.SetActive(true);
        currentGhost = true;

        yield return new WaitForSecondsRealtime(toTime);

        toParticle.SetActive(false);
        PauseSystem.SetPauseFlag(false);
    }

    void InGhost()
    {
        inParticle.SetActive(true);
        inParticle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    IEnumerator ReGhost()
    {
        PauseSystem.SetPauseFlag(true);
        inParticle.SetActive(false);
        reParticle.SetActive(true);
        currentGhost = false;

        yield return new WaitForSecondsRealtime(reTime);

        reParticle.SetActive(false);
        PauseSystem.SetPauseFlag(false);
    }
}
