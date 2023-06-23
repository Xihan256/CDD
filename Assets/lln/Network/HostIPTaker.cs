using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class HostIPTaker : MonoBehaviour
{
    public GameObject smaker;
    public string ip;

    private void Start()
    {
        ip = "127.0.0.1";
    }

    private string GetLocalIPAddress()
    {
        string ipAddress = string.Empty;

        try
        {
            // ��ȡ����������
            string hostName = Dns.GetHostName();

            // ������������ȡ������Ϣ
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

            // ����������Ϣ�е�IP��ַ���ҵ�������IP��ַ
            foreach (IPAddress address in hostEntry.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = address.ToString();
                    break;
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error getting local IP address: 1" + ex.Message);
        }

        return ipAddress;
    }
}
