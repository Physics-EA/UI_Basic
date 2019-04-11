using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogOnSceneCtrl : MonoBehaviour
{
    /// <summary>
    /// 字典 通过键值对的形式化 存储数据  键:帐号  值:密码。
    /// </summary>
    Dictionary<string, string> userInfo = new Dictionary<string, string>();

    /// <summary>
    /// //登录面板 注册面板  创建角色面板
    /// </summary>
    public GameObject logOnPanel, regPanel, roleCreatePanel;

    /// <summary>
    /// 登录面板 用户名输入框  密码输入框
    /// </summary>
    public InputField logOnPanelUserInput, logOnPanelPwdInput;

    /// <summary>
    /// 警告窗
    /// </summary>
    public Text noticeText;

    void Start()
    {
        userInfo.Add("111", "111");//添加一个键 值 ,实际上就是给添加一个测试用的帐号和密码 111,111
    }

    /// <summary>
    /// 这个方法 提供给登录面板 登录按钮点击的时候
    /// </summary>
    public void LogOnPanelLogOnBtnClick()
    {
        //首先判断 用户名 对应的密码是不是准确的 
        //如果包含用户名 
        if (userInfo.ContainsKey(logOnPanelUserInput.text))
        {
            //用户名在字典中存储的值(密码)跟登录面板 密码输入框  两个值一样的话
            if (userInfo[logOnPanelUserInput.text] == logOnPanelPwdInput.text)//userInfo[logOnPanelUserInput.text] 取到字典中的相应的值(密码)
            {
                //登录成功
                noticeText.text = "登录成功";
                logOnPanel.SetActive(false);//登录面板 关闭
                noticeText.text = "";
                //创建角色
                roleCreatePanel.SetActive(true);
            }
            else
            {
                //帐号或者密码错误  -->实际上只是密码错误,这样混淆提示而已.
                noticeText.text = "帐号或者密码错误";
            }
        }
        else
        {
            //帐号不存在
            noticeText.text = "帐号不存在";
        }

    }

    /// <summary>
    /// 登录面板 -->注册按钮
    /// </summary>
    public void LogOnPanelRegBtnClick()
    {
        //只需要跳转到注册界面就可以了
        logOnPanel.SetActive(false);
        noticeText.text = "";
        regPanel.SetActive(true);
    }

    /// <summary>
    /// 注册面板的: 用户名 密码 手机 输入框
    /// </summary>
    public InputField regPanelUserInput, regPanelPwdInput, regPanelPhoneInput;

    /// <summary>
    /// 注册面板 -->注册按钮
    /// </summary>
    public void RegPanelRegBtnClick()
    {
        //每次注册的时候 把帐号和密码 存储到字典里 userInfo
        //判断帐号 密码 手机号码是否为空 否则注册成功
        if (regPanelUserInput.text != "" && regPanelUserInput.text != "" && regPanelUserInput.text != "")
        {
            noticeText.text = "注册成功,请创建角色";//警告窗 text 进行赋值 提示创建角色成功
            userInfo.Add(regPanelUserInput.text, regPanelUserInput.text);//键 注册面板的用户名 值 密码
            regPanel.SetActive(false);//注册成功之后 把注册面板 关闭
            roleCreatePanel.SetActive(true);//打开创建角色的面板
        }
        else
        {
            //注册失败 请填写完整信息
            noticeText.text = "注册失败,请填写完整信息";
        }
    }

    /// <summary>
    /// 注册面板的返回按钮
    /// </summary>
    public void RegPanelReturnClick()
    {
        regPanel.SetActive(false);//注册面板 关闭掉
        logOnPanel.SetActive(true);//打开 登录面板
    }

    /// <summary>
    /// 选择的坦克编号
    /// </summary>
    int seletTank = 0;
     
    /// <summary>
    /// 选择的坦克背景
    /// </summary>
    public Image[] tankSeletBG;

    /// <summary>
    /// 坦克点击时候 产生的效果
    /// </summary>
    public void OnTankClick(GameObject tank)
    {

        foreach (var item in tankSeletBG)
        {
            item.enabled = false;
        }
        tank.GetComponent<Image>().enabled = true;
        seletTank = int.Parse(tank.gameObject.name);
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    public void RoleCreatePanelCreateBtnClick(InputField NickName)
    {
        //判断 注册昵称是否为空
        if (NickName.text != "")
        {
            Debug.Log(NickName.text);
            if (seletTank == 0)
            {
                //请选择坦克
                noticeText.text = "请选择坦克";
            }
            else
            {
                noticeText.text = "";
                PlayerPrefs.SetInt("TankType", seletTank);//数据持久化 保存选择的坦克类型到本地 键值对形式 在本地保存数据
                PlayerPrefs.SetString("NickName", NickName.text);//数据持久化 保存坦克昵称到本地
                SceneManager.LoadScene(1);//场景切换的API
            }
        }
        //如果是空 就提醒 请创建昵称
        else
        {
            noticeText.text = "请创建昵称";
        }

    }


}
