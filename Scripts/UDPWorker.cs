using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPWorker : MonoBehaviour
{
    public static string remoteAddress = "192.168.0.60"; // хост для отправки данных
    public static int remotePort = 10000; // порт для отправки данных
    public static int localPort = 10000; // локальный порт для прослушивания входящих подключений
    static UdpClient sender;
    Thread thread;

    public bool LeftButton;
    public bool RightButton;

    public bool stop;

    private void Start()
    {
        while (true) {
            try
            {
                thread = new Thread(new ThreadStart(SendMessage));
                thread.Start();
                break;
            }
            catch {

            }
            Thread.Sleep(100);
        }
    }
    public void StopThread()
    {
        try
        {
            thread.Abort();
            sender.Close();
        }
        catch {
            
        }
    }
    private void SendMessage()
    {
        sender = new UdpClient(); // создаем UdpClient для отправки сообщений
        try
        {
            while (true)
            {
                if (stop) {
                    sender.Close();
                    thread.Abort();
                }
                Thread.Sleep(100);
                byte[] data = Encoding.UTF8.GetBytes("main");
                sender.Send(data, data.Length, remoteAddress, remotePort); // отправка
                IPEndPoint client = null;
                string message = Encoding.UTF8.GetString(sender.Receive(ref client)).Trim();
                
                string[] args = message.Split('&');
                Dictionary<string, string> parameters = args.Skip(1).ToDictionary(arg => arg.Split('=')[0], arg => arg.Split('=')[1]);
                
                switch (args[0])
                {
                    case "main_answer=ok":
                        if (parameters.ContainsKey("jb1")) {
                            if (parameters["jb1"] == "0000") {
                                LeftButton = false;
                            } 
                            else LeftButton = true;
                        }
                        if (parameters.ContainsKey("jb2"))
                        {
                            if (parameters["jb2"] == "0000") {
                                RightButton = false;
                            } 
                            else RightButton = true;
                        }break;
                }
                }
        }
        catch
        {
        }
        finally
        {
            sender.Close();
        }
    }
}
