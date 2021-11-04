//#define WDQfmt


using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
//using Dataq.Files.Wdq;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using System.Threading;


namespace UsbDevBulkHostApp
{
  
    /// <summary>
    /// This simple form allows the user to input a test string and transmit it to a USB Bulk device EP1.
    /// Whatever the device sends back on EP1 is receieved and displayed in a text box.
    /// 
    /// Device connect/disconnect events are subscribed to, allowing the application to automatically connect
    /// to a USB device with the VID/PID of interest.
    /// 
    /// The LibUsbDotNet C# USB Library (http://sourceforge.net/projects/libusbdotnet/) is used to facilitate communication
    /// with the device.
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        LibUsbDotNet.Main.UsbRegDeviceList DeviceList = new LibUsbDotNet.Main.UsbRegDeviceList();
        //VB version DeviceList New LibUsbDotNet.Main.UsbRegDeviceList;
        int ps = 6;
        int channelsEnabled = 1;
        int sr = 1000;  //sample rate
        int df = 10;    //decimation factor
        BinaryWriter bw = null;
        //private byte[] byteData = new byte[16384];
         private int packetCount = 0;
        short[] data = new short[1024];  
        short[] dataWDQ = new short[1024]; 
        int dataHead = 0;
        int dataTail = 0;
        int deviceCount = 0;
        static short dataqPID = 0;
        //Wdq w = null;
        //HeaderLite h = null;
        //Mark startMark = new Mark();
 
        bool startFlg = false;
        bool storeFlg = false;
        bool recordFlg = false;
         /// USB device instance.  This is the 'handle' to our device.  It is used to create reade and write objects.
        /// </summary>
        LibUsbDotNet.UsbDevice m_usbDevice;
        /// <summary>
        /// This USB reader is setup to asynchronously recieve data from our device.
        /// </summary>
        LibUsbDotNet.UsbEndpointReader m_usbReader;

        /// <summary>
        /// The notifier is used to subscribe to event based notifications regarding device connect/disconnect events.
        /// We use it recognize and respond to our USB device connecting or disconnecting.
        /// </summary>
        LibUsbDotNet.DeviceNotify.IDeviceNotifier m_usbNotifier;

        /// <summary>
        /// The USB device finder specifies the criteria that LibUsbDotNet will use to find and connect to a USB device.
        /// The VID/PID used here matches the TI Stellaris LaunchPad usb_dev_bulk example.  You may change these to match
        /// the values for your particular device.  They reside in the Settings.settings XML file.
        /// </summary>
        //short i = Form1.dataqPID;
        LibUsbDotNet.Main.UsbDeviceFinder m_usbFinder = new LibUsbDotNet.Main.UsbDeviceFinder(
            //Properties.Settings.Default.UsbVid, Properties.Settings.Default.UsbPid);
            Properties.Settings.Default.UsbVid, 0x4208);

        /// <summary>
        /// Upon construction, the USB notifier is started and an attempt is made to connect to a USB device.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //dataqPID  = Convert.ToInt16(textBoxPID.Text, 16);
            // Bail if we are running inside of the Visual Studio designer view.
            //
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;

            //HIGH PRIORITY for high speed DA
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            m_usbNotifier = LibUsbDotNet.DeviceNotify.DeviceNotifier.OpenDeviceNotifier();
            m_usbNotifier.OnDeviceNotify += new EventHandler<LibUsbDotNet.DeviceNotify.DeviceNotifyEventArgs>(UsbNotifier_OnDeviceNotify);
        }

        /// <summary>
        /// Event handler for USB device enumeration changes.
        /// This will look for connections (DeviceArrival) for our VID/PID and open the device if needed,
        /// or for disconnections it will close/release the instance.
        /// </summary>
        void UsbNotifier_OnDeviceNotify(object sender, LibUsbDotNet.DeviceNotify.DeviceNotifyEventArgs eventArgs)
        {

            CommandLog("Device notification event: {0} VID={1:X4} PID={2:X4}",
                eventArgs.EventType, eventArgs.Device.IdVendor, eventArgs.Device.IdProduct);
            //dataqPID = (short)eventArgs.Device.IdProduct;
            // Ignore events for other devices
            //
            if (m_usbFinder.Vid != eventArgs.Device.IdVendor) return;
            //if (m_usbFinder.Pid != eventArgs.Device.IdProduct) return;

            if (m_usbDevice == null && eventArgs.EventType == LibUsbDotNet.DeviceNotify.EventType.DeviceArrival)
            {
                // Grab this device
                //
                OpenUsbDevice();
                m_usbReader.DataReceivedEnabled = true;
            }
            // If this was our device being removed, then close the handle.  The only thing I could glean from inspecting 
            // m_usbDevice after a remove event on that actual device was that the number of Configs went from non-zero
            // to zero.
            //
            
            else //if (eventArgs.EventType == LibUsbDotNet.DeviceNotify.EventType.DeviceRemoveComplete && m_usbDevice.Configs.Count == 0)
            {
                CloseUsbDevice();
            }
            
        }

        /// <summary>
        /// Open a new instance of a USB device using the finder criteria.  (Innocuous if device instance already exists).
        /// This will also enable the Transmit button as well as setup the asynchronous reader and event.
        /// </summary>
        void OpenUsbDevice()
        {

            deviceCount = LibUsbDotNet.WinUsb.WinUsbDevice.AllDevices.Count;
            if (deviceCount > 1)
            {
                //CloseUsbDevice();
                string message = "Disconnect all but one DATAQ USB devices!";
                string caption = "Error more than one DATAQ USB device connected!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Exclamation);
            }
            if (deviceCount == 0)
            {
                string message = "Connect DATAQ USB device!";
                string caption = "DATAQ USB device not connected!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Exclamation);
                return;
            }


            if (m_usbDevice != null) return;
            m_usbFinder.Pid = LibUsbDotNet.WinUsb.WinUsbDevice.AllDevices[0].Pid;
            m_usbDevice = LibUsbDotNet.UsbDevice.OpenUsbDevice(m_usbFinder);

            
            if (m_usbDevice == null)
            {
                CloseUsbDevice();
                CommandLog("No device found with VID={0:X4} PID={1:X4}", m_usbFinder.Vid, m_usbFinder.Pid);
                return;
            }
            int pid = m_usbDevice.UsbRegistryInfo.Pid;
            
            transmitButton.Enabled = true;

            m_usbReader = m_usbDevice.OpenEndpointReader(LibUsbDotNet.Main.ReadEndpointID.Ep01);
            m_usbReader.DataReceived += new EventHandler<LibUsbDotNet.Main.EndpointDataEventArgs>(UsbReader_DataReceived);
            m_usbReader.DataReceivedEnabled = false;
            m_usbReader.ReadBufferSize = 512;
            // UsbRegistryInfo is null in Linux, so only include it if defined.
            //
            string sSymbolicInfo = m_usbDevice.UsbRegistryInfo == null ? string.Empty :
                string.Format(" ({0})", m_usbDevice.UsbRegistryInfo.SymbolicName);
            CommandLog("Opened device VID={0:X4} PID={1:X4}{2}.",
                m_usbDevice.Info.Descriptor.VendorID, m_usbDevice.Info.Descriptor.ProductID, sSymbolicInfo);
            m_usbReader.DataReceivedEnabled = true;
        }

        /// <summary>
        /// Event handler for USB data received from the device.  All that this really does is decode the bytes
        /// into a string and log it for your viewing pleasure.
        /// </summary>
        void UsbReader_DataReceived(object sender, LibUsbDotNet.Main.EndpointDataEventArgs eventArgs)
        {
            packetCount = eventArgs.Count;
            DataLog(" {0}", packetCount);
            //if (packetCount == 1)
           // {
                //string stx = System.Text.Encoding.ASCII.GetString(eventArgs.Buffer, 0, eventArgs.Count);
            //}

            if ((packetCount&1) == 1)
            {
 
                string sReceived = System.Text.Encoding.ASCII.GetString(eventArgs.Buffer, 0, eventArgs.Count);
                CommandLog("RX: {0}", sReceived.Substring(0));
             }
            else
            {
                //if (startFlg)
                //{
                    if (storeFlg && recordFlg)   //startFlg && 
                    {
#if WDQfmt                        
                        int i; int j = 0;
                        //usb buffer bytes to data buffer short
                        for (i = 0; i < eventArgs.Count; i += 2)
                        {
                            data[dataHead] = eventArgs.Buffer[i + 1];
                            data[dataHead] <<= 8;
                            data[dataHead++] += eventArgs.Buffer[i];
                            if (dataHead == data.Length)
                                dataHead = 0;
                        }
                        if (dataHead > dataTail)
                            j = dataHead - dataTail;
                        else
                            j = data.Length + dataHead - dataTail;
                        j = (j / channelsEnabled) * channelsEnabled;
                        i = j;
                        for (i = 0; i < j;i++)
                        {
                            dataWDQ[i] = data[dataTail];
                            if (++dataTail == dataWDQ.Length)
                                dataTail = 0;
                        }
                            
                        w.Append(dataWDQ, (uint)j);
#else
                        //fs.Write(eventArgs.Buffer, 0, eventArgs.Count);
                        bw.Write(eventArgs.Buffer, 0, eventArgs.Count);
                      
#endif

                    }
                   
                //}
                //else
                //{
                    //string sReceived = System.Text.Encoding.ASCII.GetString(eventArgs.Buffer, 0, eventArgs.Count);
                    //CommandLog("RX: {0}", sReceived.Substring(eventArgs.Count - "stop\r".Length));
                //}

            }
        }

        /// <summary>
        /// Gracefully dispose and close the USB device and the related reader.
        /// </summary>
        void CloseUsbDevice()
        {
            transmitButton.Enabled = false;

            if (m_usbReader != null)
            {
                m_usbReader.Abort();
                m_usbReader.Dispose();
                m_usbReader = null;
            }

            if (null == m_usbDevice) return;

            m_usbDevice.Close();
            m_usbDevice = null;

            CommandLog("Closed device.");
        }

        /// <summary>
        /// Synchronously send the argument string to the device, encoded as ASCII bytes.
        /// </summary>
        /// <remarks>
        /// In your application you likely want to send some kind of binary data, maybe like Google Protocol Buffers.
        /// But for the usb_dev_bulk firmware, all that the device does is take a string, flip the character case,
        /// and send it back.  So this is why a string is the argument here.
        /// In a production application you'd probably want to asynchronously send data to the device, or even consider
        /// a buffering stream.
        /// Also, there's no error checking in this example.  You'll want to check the return value of the Write call, at the 
        /// very least.</remarks>
        void TransmitToDevice(string sData)
        {
            using (LibUsbDotNet.UsbEndpointWriter writer = m_usbDevice.OpenEndpointWriter(LibUsbDotNet.Main.WriteEndpointID.Ep01))
            {
                int iBytesTransmitted;
                string cmdData = textBoxToTransmit.Text;

                CommandLog("TX: {0}", cmdData);

                byte[] abyTestData = System.Text.Encoding.ASCII.GetBytes(cmdData); //149
                writer.Write(abyTestData, 20, out iBytesTransmitted);

                if (String.Equals(cmdData, "start"))
                {
                    startFlg = true;
                    TextBoxData.Clear();
                }

                if (String.Equals(cmdData, "stop"))
                {
                    //System.Threading.Thread.Sleep(100);
                    startFlg = false;
                    //int bytesRead = 1000;
                    /*
                    ErrorCode ec = ErrorCode.None;
                    while (ec == ErrorCode.None)
                    {
                        ec = m_usbReader.Read(byteData, 10, out bytesRead);
                    }
                     * */
                }
            }
        }

        /// <summary>
        /// When the Transmit button is clicked, grab the text from the edit box and send it to the USB device.
        /// </summary>
        void transmitButton_Click(object sender, EventArgs e)
        {
            if (textBoxToTransmit.Text == "start")
                startFlg = true;
            if (textBoxToTransmit.Text == "stop")
            {
                startFlg = false;
                //stopFlg = true;

            }

            TransmitToDevice(textBoxToTransmit.Text);
            textBoxToTransmit.Text = "";
            if(startFlg)
                TextBoxData.Focus();
                //System.Threading.Thread.Sleep(1000);

        }

        /// <summary>
        /// Simple output logger. This takes the argument text, formats it, and appends it to the the rich edit box.
        /// </summary>
        int cycle = 0;
        void CommandLog(string sText, params object[] values)
        {
            // If coming in from a thread other than the GUI thread (which is the case with async receives), then post this to the GUI
            // thread queue to avoid exceptions that would otherwise occur when attemping to manipulate a control.
            //
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => CommandLog(sText, values)));
                return;
            }
            ;
            String s = String.Format(sText + "\n", values);
            richTextBoxCommand.AppendText(s); // + "\n"
            richTextBoxCommand.ScrollToCaret();
            
            
        }

        /// <summary>
        /// Simple output logger. This takes the argument text, formats it, and appends it to the the rich edit box.
        /// </summary>
        void DataLog(string sText, params object[] values)
        {
            // If coming in from a thread other than the GUI thread (which is the case with async receives), then post this to the GUI
            // thread queue to avoid exceptions that would otherwise occur when attemping to manipulate a control.
            //
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => DataLog(sText, values)));
                return;
            }

            TextBoxData.AppendText(string.Format(sText, values)); // + "\n"
            //TextBoxData.ScrollToCaret();
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            OpenUsbDevice();
            //int i = GetDeviceList();
            //DeviceList.Count;

            listBox1.SelectedItem = 5;
            checkBox2.Checked = true;
            channelCheckBox.SetItemChecked(0, true);
            channelCheckBox.SetItemChecked(1, true);
            channelCheckBox.SetItemChecked(2, true);
            channelCheckBox.SetItemChecked(3, true);
            channelCheckBox.SetItemChecked(4, false);
            channelCheckBox.SetItemChecked(5, false);
            channelCheckBox.SetItemChecked(6, false);
            channelCheckBox.SetItemChecked(7, false);
            channelCheckBox.SetItemChecked(8, false);
            channelCheckBox.SetItemChecked(9, false);
            channelCheckBox.SetItemChecked(10, false);
            filterCheckBox.SetItemChecked(0, true);
            filterCheckBox.SetItemChecked(1, true);
            filterCheckBox.SetItemChecked(2, true);
            filterCheckBox.SetItemChecked(3, true);
            filterCheckBox.SetItemChecked(4, false);
            filterCheckBox.SetItemChecked(5, false);
            filterCheckBox.SetItemChecked(6, false);
            filterCheckBox.SetItemChecked(7, false);
            listBox1.SetSelected(5, true);
            if (m_usbReader != null)
                m_usbReader.DataReceivedEnabled = true;
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseUsbDevice();
            m_usbNotifier.Enabled = false;
            LibUsbDotNet.UsbDevice.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                //windaqFileInit();

                    //timer1.Enabled = true;
                    storeFlg = true;

            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            storeFlg = false;
#if WDQfmt
            w.Close(false);
#else
            bw.Close();
#endif
            TextBoxData.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                recordFlg = true;
            else
                recordFlg = false;
        }


        private void TextBoxData_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxToTransmit.Focus();
        }


        private void startStop_Click(object sender, EventArgs e)
        {
            int i;
            int iBytesTransmitted;
            richTextBoxCommand.Clear();
            TextBoxData.Clear();
            using (LibUsbDotNet.UsbEndpointWriter writer = m_usbDevice.OpenEndpointWriter(LibUsbDotNet.Main.WriteEndpointID.Ep01))

            //if(startStop.Text == "Push to Start")
            {
                // If so, loop through all checked items and print results.
                string s = "";
                byte[] abyTestData;
                channelsEnabled = 0;
                for (int x = 0; x <= channelCheckBox.CheckedItems.Count - 1; x++)
                {
                    ++channelsEnabled;
                    s = "slist " + (x).ToString() + " " + channelCheckBox.CheckedItems[x].ToString().Remove(0, 5);
                    abyTestData = System.Text.Encoding.ASCII.GetBytes(s);
                    writer.Write(abyTestData, 250, out iBytesTransmitted);
                    Thread.Sleep(10);

                }
                for (int x = 0; x < 8; x++)
                {
                    //s = s + "slist " + (x).ToString() + " " + channelCheckBox.CheckedItems[x].ToString().Remove(0, 3) + "\n";
                    
                    s = filterCheckBox.GetItemCheckState(x).ToString();
                    if (s == "Checked")
                        s = "filter " + x.ToString() + " 2";
                    else
                        s = "filter " + x.ToString() + " 0";
                    abyTestData = System.Text.Encoding.ASCII.GetBytes(s);
                    writer.Write(abyTestData, 250, out iBytesTransmitted);
                    Thread.Sleep(10);

                }
                s = "ps " + ps.ToString();
                abyTestData = System.Text.Encoding.ASCII.GetBytes(s);
                writer.Write(abyTestData, 250, out iBytesTransmitted);
                Thread.Sleep(200);

                s = "dec " + decFactorTB.Text;
                df = Convert.ToInt32(decFactorTB.Text);
                Thread.Sleep(10);

                abyTestData = System.Text.Encoding.ASCII.GetBytes(s);
                writer.Write(abyTestData, 250, out iBytesTransmitted);
                s = "srate " + sampleRate.Text;
                Thread.Sleep(10);

                sr = Convert.ToInt32(sampleRate.Text);

                abyTestData = System.Text.Encoding.ASCII.GetBytes(s);
                writer.Write(abyTestData, 250, out iBytesTransmitted);
                Thread.Sleep(10);

 

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ps =listBox1.SelectedIndex;
            
        }

        static UInt16 count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBoxToTransmit.Text = "dout " + count.ToString();
            TransmitToDevice(textBoxToTransmit.Text);
            ++count;
            count &= 0x3f;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == true)
                m_usbReader.DataReceivedEnabled = true;
            else
                m_usbReader.DataReceivedEnabled = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;

        }

        private void start_Click(object sender, EventArgs e)
        {
            richTextBoxCommand.Clear();
            textBoxToTransmit.Text = "start";
            TransmitToDevice(textBoxToTransmit.Text);
            textBoxToTransmit.Text = "";
            startFlg = true;
            TransmitToDevice("start\r");
            TextBoxData.Focus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxToTransmit.Text = "eeinit DATAQ\r";
            TransmitToDevice(textBoxToTransmit.Text);
            Thread.Sleep(1000);
            textBoxToTransmit.Text = "defaultdescriptor 1";
            TransmitToDevice(textBoxToTransmit.Text);
            Thread.Sleep(200);

        }

        private void textBoxPID_Leave(object sender, EventArgs e)
        {
            //dataqPID = Convert.ToInt16(textBoxPID.Text, 16);
            //Properties.Settings.Default.UsbPid = (ushort)dataqPID;
        }
        private int GetDeviceList()
        {
            //LibUsbDotNet.Main.UsbRegistry myDevice = new LibUsbDotNet.Main.UsbRegistry;
            DeviceList = UsbDevice.AllLibUsbDevices.FindAll(new UsbDeviceFinder(0X683));
            //DeviceList.
            m_usbFinder.Vid = 0X683;
            //.Pid = DeviceList.IndexOf(0).

            return DeviceList.Count;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBoxToTransmit.Text = "syncget 6";
            TransmitToDevice("syncget 6");
            textBoxToTransmit.Text = "syncget 7";
            TransmitToDevice("syncget 7");
        }
    }
}
