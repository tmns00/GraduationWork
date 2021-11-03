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
    void FixedUpdate()
    {
        Debug.Log(ghostSystem.GetIsGhost());
        if (!ghostSystem.GetIsGhost() && !currentGhost)
        {
            Debug.Log("ret");
            return;
        }

        if (ghostSystem.GetIsGhost() && !currentGhost)
            StartCoroutine(ToGhost());
        else if (ghostSystem.GetIsGhost() && currentGhost)
            InGhost();
        else if (!ghostSystem.GetIsGhost() && currentGhost)
            StartCoroutine(ReGhost());
    }

    IEnumerator ToGhost()
    {
        Debug.Log("to");
        toParticle.SetActive(true);
        currentGhost = true;

        yield return new WaitForSeconds(toTime);

        toParticle.SetActive(false);
    }

    void InGhost()
    {
        Debug.Log("in");
        inParticle.SetActive(true);
    }

    IEnumerator ReGhost()
    {
        Debug.Log("re");
        inParticle.SetActive(false);
        reParticle.SetActive(true);
        currentGhost = false;

        yield return new WaitForSeconds(reTime);

        reParticle.SetActive(false);
    }
}
