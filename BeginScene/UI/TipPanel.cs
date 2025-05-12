using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Button sureBtn;
    public Text tipContentTex;

    public override void Init()
    {
        sureBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanle<TipPanel>();
        });
    }

    /// <summary>
    /// 改变提示内容的方法
    /// </summary>
    /// <param name="info"></param>
    public void ChangeInfo(string info)
    {
        tipContentTex.text = info;
    }
}
