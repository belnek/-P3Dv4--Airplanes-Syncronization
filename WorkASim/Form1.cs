using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LockheedMartin.Prepar3D.SimConnect;
using System.Runtime.InteropServices;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WorkASim
{
    public partial class Form1 : Form
    {
        bool paused = false;

        public Form1()
        {
            InitializeComponent();

        }
        const int WM_USER_SIMCONNECT = 0x0402;
        SimConnect simConnect = null;


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
        enum DEFINITIONS
        {
            DataStruct,
        }

        protected override void DefWndProc(ref Message m)
        {

            if (m.Msg == WM_USER_SIMCONNECT)
            {
                if (simConnect != null)
                {
                    simConnect.ReceiveMessage();
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        private void Connectbutton_Click(object sender, EventArgs e)
        {
            simConnect = new SimConnect("Data", this.Handle, WM_USER_SIMCONNECT, null, 0);
            
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


        }
        void simconnect_OnRecvSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data)
        {
            switch((DEFINITIONS)data.dwRequestID)
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
                    List<String> strs = new List<string>();
                    strs.Add("title: "  + title);
                    strs.Add("speed: " + speed);
                    strs.Add("latitude: " + latitude);
                    strs.Add("longitude: " + longitude);
                    strs.Add("altitude: " + altitude);
                    strs.Add("pitchd: " + pitchd);
                    strs.Add("bankd: " + bankd);
                    strs.Add("headingd: " + headingd);
                    DataTB.Lines = strs.ToArray();

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
            MessageBox.Show("Произошла ошибка при получении данных. Возможно, отсутствует SimConnect или симулятор не запущен: " + data.dwException);
        }

        private void closeConnection()
        {
            if (simConnect != null)
            {
                simConnect.Dispose();
                simConnect = null; 
            }
        }

        private void DataButton_Click(object sender, EventArgs e)
        {
            if (simConnect != null)
            {
                simConnect.RequestDataOnSimObject(DEFINITIONS.DataStruct, DEFINITIONS.DataStruct, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_PERIOD.SIM_FRAME, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            closeConnection();
        }
    }
}
