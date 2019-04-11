using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneCtrl : MonoBehaviour
{
    //昵称 文本框
    public Text nickName;

    //角色按钮 技能按钮 设置按钮
    public Button roleBtn, skillBtn, settingBtn, BGBtn;

    //三个面板:角色面板 技能面板  设置面板
    public GameObject rolePanel, skillPanel, settingPanel;

    void Start()
    {
        //从本地持久化数据里取得 坦克类型 Set 存储 Get 获取
        int tankType = PlayerPrefs.GetInt("TankType");//1,2,3
        if (tankType > 0 && tankType < 4)
        {
            //通过Resources.Load 加载Resources文件夹下的资源 参数是子路径
            Object tankObj = Resources.Load("tank0" + tankType);
            //克隆物体 as 转化类型的关键字
            GameObject go = GameObject.Instantiate(tankObj) as GameObject;
            //设置父物体
            go.transform.parent = this.transform;
            //设置本身的局部坐标
            go.transform.localPosition = new Vector3(0, 0, 0);
        }

        //从本地持久数据 获取 NickName 键 所对应的值 判断 不等于空
        if (PlayerPrefs.GetString("NickName") != "")
        {
            //从本地持久化数据里取得 昵称
            nickName.text = PlayerPrefs.GetString("NickName");
        }

        //添加点击事件
        //角色按钮
        roleBtn.onClick.AddListener(delegate { ShowPanel(rolePanel); });
        //技能按钮
        skillBtn.onClick.AddListener(delegate { ShowPanel(skillPanel); });
        //设置按钮
        settingBtn.onClick.AddListener(delegate { ShowPanel(settingPanel); });
        //背景按钮点击
        BGBtn.onClick.AddListener(delegate { CloseAllPanle(); });

    }

    public Image LevelUI;//经验UI条
    public Text LevelText;//经验比例显示文本
    private void Update()
    {
        //按下E键的时候 执行的方法 GetKeyUP 调用一次
        if (Input.GetKeyDown(KeyCode.E))
        {
            //图片填充++ 
            LevelUI.fillAmount += 0.2f;
            //更新文本比例
            LevelText.text = LevelUI.fillAmount * 100 + "%";
        }
    }

    //显示面板 主要给各个按钮点击用的 Obj是要显示的物体
    public void ShowPanel(GameObject obj)
    {
        //关闭所有面板
        CloseAllPanle();
        //传递过来的形参 开启的面板 状态设置true
        obj.SetActive(true);

        if (obj.GetComponent<Canvas>() == null)
        {

            Canvas c = obj.AddComponent<Canvas>();
            //重写层级的属性 设置为true
            c.overrideSorting = true;
            //通过sortingOrder 来设置它的层级 使它可以保持在其它Canvas的上下层级 BG
            c.sortingOrder = 50;
            //添加图形射线 面板才接受鼠标点击
            obj.AddComponent<GraphicRaycaster>();
        }
        else
        {
            //如果已经存在Canvas 就直接设置层级
            obj.GetComponent<Canvas>().overrideSorting = true;
            obj.GetComponent<Canvas>().sortingOrder = 50;
        }
    }

    //关闭所有面板
    void CloseAllPanle()
    {
        rolePanel.SetActive(false);
        skillPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    public AudioSource ad;

    //设置音量大小
    public void SetAudioSize(float sin)
    {

        ad.volume = sin;
    }

    public GameObject headPanel;

    /// <summary>
    /// 设置头部面板激活/关闭
    /// </summary>
    public void UpdateHeadPanel(bool t)
    {
        headPanel.SetActive(t);
    }

    public Texture[] imageList;

    /// <summary>
    /// 更新背景图片
    /// </summary>
    public void UpdateBGImage(int i)
    {
        BGBtn.GetComponent<RawImage>().texture = imageList[i];
    }
}

//资源动态加载
//数据本地持久化 获取对应键的值
//通过脚本绑定按钮点击事件
//Canvas需要与图形射线一起配合使用 面板才能接受鼠标的点击
//通过Canvas 控制面板的层级关系