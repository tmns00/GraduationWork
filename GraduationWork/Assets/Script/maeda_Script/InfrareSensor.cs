using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InfrareSensor : MonoBehaviour
{
    [SerializeField] bool laserIrradiation = true;//���[�U�[�Ǝ�
    [SerializeField] GameObject laserObj = null;//�ԊO���I�u�W�F
    bool playerLaserHit;//�v���C���[�����[�U�[�ɐG�ꂽ

    // Update is called once per frame
    void Update()
    {
        OnLaserIrradiation();
    }

    /// <summary>
    /// �ԊO�����[�U�[�̏Ǝ�
    /// </summary>
    void OnLaserIrradiation()
    {
        laserObj.SetActive(laserIrradiation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerLaserHit = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerLaserHit = false;
    }

    /// <summary>
    /// �v���C���[���m
    /// </summary>
    /// <returns></returns>
    public bool PlayerHit()
    {
        return playerLaserHit;
    }

    /// <summary>
    /// �ԊO�����[�U�[�̏Ǝ˃X�C�b�`
    /// </summary>
    /// <param name="IrradiationSwitch"></param>
    public void InfraredIrradiation(bool irradiationSwitch)
    {
        laserIrradiation = irradiationSwitch;
    }

}
