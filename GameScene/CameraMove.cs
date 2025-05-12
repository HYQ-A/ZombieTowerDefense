using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //ƫ��λ��
    public Vector3 offestPos;
    //Ŀ�����
    public Transform target;
    //�����Ŀ������ ��ֱƫ����
    public float bodyHeight;
    public float moveSpeed;
    public float rotationSpeed;

    private Vector3 targetPos;
    private Quaternion targetRotation;

    // Update is called once per frame
    void Update()
    {
        //���û��Ŀ����� ��ֱ�ӷ���
        if (target == null)
            return;

        //����Ŀ����� ���� �������ǰ��λ�úͽǶ�
        //λ�õļ���
        //���ƫ��z����
        targetPos = target.position + target.forward * offestPos.z;
        //����ƫ��y����
        targetPos += target.up * offestPos.y;
        //����ƫ��x����
        targetPos += target.right * offestPos.x;
        //��ֵ���� ������� ��ͣ��Ŀ��㿿£
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);

        //��ת�ļ���
        targetRotation = Quaternion.LookRotation(target.position + Vector3.up * bodyHeight - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// ��������������Ŀ�����
    /// </summary>
    /// <param name="palyer"></param>
    public void SetTarget(Transform palyer)
    {
        target = palyer;
    }
}
