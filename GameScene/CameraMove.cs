using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //偏移位置
    public Vector3 offestPos;
    //目标对象
    public Transform target;
    //相对于目标对象的 垂直偏移量
    public float bodyHeight;
    public float moveSpeed;
    public float rotationSpeed;

    private Vector3 targetPos;
    private Quaternion targetRotation;

    // Update is called once per frame
    void Update()
    {
        //如果没有目标对象 则直接返回
        if (target == null)
            return;

        //根据目标对象 计算 摄像机当前的位置和角度
        //位置的计算
        //向后偏移z坐标
        targetPos = target.position + target.forward * offestPos.z;
        //向上偏移y坐标
        targetPos += target.up * offestPos.y;
        //左右偏移x坐标
        targetPos += target.right * offestPos.x;
        //插值运算 让摄像机 不停向目标点靠拢
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);

        //旋转的计算
        targetRotation = Quaternion.LookRotation(target.position + Vector3.up * bodyHeight - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 设置摄像机看向的目标对象
    /// </summary>
    /// <param name="palyer"></param>
    public void SetTarget(Transform palyer)
    {
        target = palyer;
    }
}
