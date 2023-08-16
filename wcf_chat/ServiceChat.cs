using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace wcf_chat
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private static List<ServerUser> users = new List<ServerUser>();
        private static int nextId = 1;
        private static bool waitASync = false;
        private static int countOfReady = 0;
        public int Connect(string name)
        {

            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;

            SendMsg(": " + user.Name + " подключился к чату!", 0);
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg(": " + user.Name + " покинул чат!", 0);
            }
        }

        public void SendMsg(string msg, int id)
        {
            Console.WriteLine(msg);
            if (waitASync)
            {
                if (msg.StartsWith("Ready!"))
                {
                    countOfReady++;
                    if (countOfReady >= users.Count - 1)
                    {
                        waitASync = false;
                        foreach (var item in users)
                        {
                            item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback("Ready!");

                        }
                    }
                }
                else if (msg.StartsWith("online"))
                {
                    countOfReady++;
                    if (countOfReady >= users.Count)
                    {
                        waitASync = false;
                        Console.WriteLine("Checked");
                    }
                    Console.WriteLine(countOfReady + " " + users.Count());
                }
            }
            
            else
            {
                if (msg.StartsWith("sync"))
                {
                    foreach (var item in users)
                    {
                        
                        waitASync = true;
                        countOfReady = 0;
                        item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(msg);

                    }
                }
                if(msg.StartsWith("check"))
                {
                    foreach (var item in users)
                    {
                        string answer = "";

                        Console.WriteLine("check");
                        answer = "check";
                        waitASync = true;
                        countOfReady = 0;
                        item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);

                    }
                }
                
                else
                {
                    foreach (var item in users)
                    {
                        string answer = DateTime.Now.ToShortTimeString();

                        var user = users.FirstOrDefault(i => i.ID == id);
                        if (user != null)
                        {
                            answer += ": " + user.Name + " ";
                        }
                        answer += msg;
                        item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
                    }
                }
            }
        }
    }
}