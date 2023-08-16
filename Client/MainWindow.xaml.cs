using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.ServiceChat;
using LockheedMartin.Prepar3D.SimConnect;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        private static bool isConnected = false;
        private static ServiceChatClient client;
        private static int ID;
        const int WM_USER_SIMCONNECT = 0x0402;
        private static SimConnect simConnect = null;
        private bool isChecking = false;
        private HwndSource gHs;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct DataStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String title;
            public double speed;
            public double latitude;
            public double longitude;
            public double altitude;
            public double pitchd;
            public double bankd;
            public double headingd;


        };
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct SDataStruct
        {
            public double speed;
            public double latitude;
            public double longitude;
            public double altitude;
            public double pitchd;
            public double bankd;
            public double headingd;


        };
        enum DEFINITIONS
        {
            DataStruct,
            SDataStruct,
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper lWih = new WindowInteropHelper(Application.Current.MainWindow);
            IntPtr lHwnd = lWih.Handle;

            gHs = HwndSource.FromHwnd(lHwnd);

            gHs.AddHook(new HwndSourceHook(WndProc));
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            handled = false;

            // if message is coming from simconnect and the connection is not null;
            // continue and receive message
            if (msg == WM_USER_SIMCONNECT && simConnect != null)
            {
                simConnect.ReceiveMessage();


                handled = true;
            }

            return (IntPtr)0;
        }
        void ConnectUser()
        {
            try
            {
                if (!isConnected)
                {
                    client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                    ID = client.Connect(tbUserName.Text);
                    tbUserName.IsEnabled = false;
                    bConnDicon.Content = "Disconnect";
                    isConnected = true;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = true;
                bConnDicon.Content = "Connect";
                isConnected = false;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }

        }

        public void MsgCallback(string msg)
        {
            if (msg.StartsWith("sync"))
            {
                lbChat.Items.Add("Sync Started");
                lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
                startSynchro(msg.Split(' '));
                client.SendMsg("Ready!", ID);
            }
            else if(msg.StartsWith("check"))
            {
                client.SendMsg("online", ID);
            }
            else
            {
                lbChat.Items.Add(msg);
                lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }
        private void startSynchro(string[] data)
        {
            double speed = Convert.ToDouble(data[1]);
            double latitude = Convert.ToDouble(data[2]);
            double longitude = Convert.ToDouble(data[3]);
            double altitude = Convert.ToDouble(data[4]);
            double pitchd = Convert.ToDouble(data[5]);
            double bankd = Convert.ToDouble(data[6]);
            double headingd = Convert.ToDouble(data[7]);
            SDataStruct sdata = new SDataStruct
            {
                speed = speed,
                latitude = latitude,
                longitude = longitude,
                altitude = altitude,
                pitchd = pitchd,
                bankd = bankd,
                headingd = headingd
            };
            var wih = new System.Windows.Interop.WindowInteropHelper(this);
            var hWnd = wih.Handle;
            try
            {
                simConnect = new SimConnect("Data", hWnd, WM_USER_SIMCONNECT, null, 0);
                simConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(simconnect_OnRecvOpen);
                simConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(simconnect_OnRecvQuit);
                simConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(simconnect_OnRecvException);
                simConnect.AddToDataDefinition(DEFINITIONS.SDataStruct, "AIRSPEED TRUE", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED); ;
                simConnect.AddToDataDefinition(DEFINITIONS.SDataStruct, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.SDataStruct, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.SDataStruct, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.SDataStruct, "Plane Pitch Degrees", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.SDataStruct, "Plane Bank Degrees", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.SDataStruct, "Plane Heading Degrees Magnetic", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

                simConnect.RegisterDataDefineStruct<SDataStruct>(DEFINITIONS.SDataStruct);

                simConnect.SetDataOnSimObject(DEFINITIONS.SDataStruct, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_DATA_SET_FLAG.DEFAULT, sdata);
                //MessageBox.Show("Соединение установить возможно. Можете летать спокойно!");

            }
            catch (Exception er)
            {
                MessageBox.Show("Не удалось установить соединение с симулятором. Возможно, симулятор не запущен или не установлен SimConnect. " + er.ToString());

            }
        }

        private void bCheckConn_Click(object sender, RoutedEventArgs e)
        {
            var wih = new System.Windows.Interop.WindowInteropHelper(this);
            var hWnd = wih.Handle;
            try
            {
                simConnect = new SimConnect("Data", hWnd, WM_USER_SIMCONNECT, null, 0);
                simConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(simconnect_OnRecvOpen);
                simConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(simconnect_OnRecvQuit);
                simConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(simconnect_OnRecvException);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStruct, "title", null, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStruct, "AIRSPEED TRUE", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED); ;
                simConnect.AddToDataDefinition(DEFINITIONS.DataStruct, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStruct, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStruct, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStruct, "Plane Pitch Degrees", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStruct, "Plane Bank Degrees", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStruct, "Plane Heading Degrees Magnetic", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

                simConnect.RegisterDataDefineStruct<DataStruct>(DEFINITIONS.DataStruct);

                simConnect.OnRecvSimobjectData += new SimConnect.RecvSimobjectDataEventHandler(simconnect_OnRecvSimobjectData);
                isChecking = true;
                simConnect.RequestDataOnSimObject(DEFINITIONS.DataStruct, DEFINITIONS.DataStruct, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_PERIOD.ONCE, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);

                //MessageBox.Show("Соединение установить возможно. Можете летать спокойно!");

            }
            catch (Exception er)
            {
                MessageBox.Show("Не удалось установить соединение с симулятором. Возможно, симулятор не запущен или не установлен SimConnect. " + er.ToString());

            }



        }
        void simconnect_OnRecvSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data)
        {
            switch ((DEFINITIONS)data.dwRequestID)
            {
                case DEFINITIONS.DataStruct:

                    DataStruct sData = (DataStruct)data.dwData[0];
                    String title = sData.title;
                    double speed = sData.speed;
                    double latitude = sData.latitude;
                    double longitude = sData.longitude;
                    double altitude = sData.altitude;
                    double pitchd = sData.pitchd;
                    double bankd = sData.bankd;
                    double headingd = sData.headingd;
                    if (isChecking)
                    {
                        MessageBox.Show("Соединение установлено. Можете летать спокойно!");
                        isChecking = false;
                    }


                    break;
            }
        }
        void simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {

        }

        void simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            closeConnection();
        }

        void simconnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            MessageBox.Show("Произошла ошибка при получении данных: " + data.dwException);
            
            simConnect = null;
        }
        private void closeConnection()
        {
            if (simConnect != null)
            {
                simConnect.Dispose();
                simConnect = null;
            }
        }
    }
}
