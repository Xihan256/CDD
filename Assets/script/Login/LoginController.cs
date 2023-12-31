using System;
using System.Net.Http;
using lln.Bluetooth.server;
using lln.Network.client;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour{
   public GameObject onlineBTN;
   public GameObject offlineBTN;
   public GameObject LoginPanel;
   public GameObject ErrText;

   private Text name;
   private Text password;

   private void Start(){
      name = LoginPanel.transform.GetChild(0).GetChild(1).GetComponent<Text>();
      password = LoginPanel.transform.GetChild(1).GetChild(1).GetComponent<Text>();
   }

   public void JumpToLogin(){
      onlineBTN.GetComponent<Button>().interactable = false;
      offlineBTN.GetComponent<Button>().interactable = false;
      
      LoginPanel.SetActive(true);
   }

   public void TryToRegist(){
      
      string n = name.text;
      string p = password.text;

      string req = "http://8.134.143.81:8080/put/user?name=" + n + "&password=" + p;
      
      try
      {
         // 使用 HttpClient 发送 GET 请求并获取响应内容
         using (HttpClient client = new HttpClient()){
            string responseBody = client.GetStringAsync(req).Result;
         }
         
         FailTxt();
      }
      catch (Exception ex)
      {
         Console.WriteLine("发生异常: " + ex.Message);
      }
      
   }
   
   public void FailTxt()
   {
      ErrText.SetActive(true);
      Invoke("HideFailTxt", 1.5f);
   }
   private void HideFailTxt()
   {
      ErrText.SetActive(false);
   }

   public void LoginTry(){
      
      string n = name.text;
      string p = password.text;

      string req = "http://8.134.143.81:8080/get/password?name=" + n;
      
      try
      {
         string responseBody;

         // 使用 HttpClient 发送 GET 请求并获取响应内容
         using (HttpClient client = new HttpClient()){
            responseBody = client.GetStringAsync(req).Result;
            Debug.Log(responseBody);
         }

         //Console.WriteLine(responseBody);
         string pwd = responseBody;
         
         if (pwd.Equals(p)){
            ClientMain.selfname = n;
            Main.selfname = n;
            SceneManager.LoadScene("002_chosing");
         } else{
            LoginPanel.transform.GetChild(3).gameObject.SetActive(true);
         }
      }
      catch (Exception ex)
      {
         Console.WriteLine("发生异常: " + ex.Message);
      }
      
      
   }

   public void toSingle(){
      SceneManager.LoadScene("005_Single");
   }
}

