using Firebase.Database;
using Firebase.Database.Query;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;

namespace TeamVaxxers
{
    public partial class ParkingLot : Form
    {
        FirebaseClient client = new FirebaseClient("https://heymotocarro-1a1d4.firebaseio.com/");
        Graphics G;
        Rectangle[] rect= new Rectangle[6];
        public ParkingLot()
        {
            InitializeComponent();
           // WindowState = FormWindowState.Maximized;
        }
        private void ParkingLot_Load(object sender, EventArgs e)
        {
            G = this.CreateGraphics();
        }
        private void loadData(object sender, EventArgs e)
        {
            getBeaconDataAsync();
        }
        private void DrawSlots(object sender, EventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 0);
            SolidBrush myBrush = new SolidBrush(Color.SkyBlue);
            SolidBrush myBrush2 = new SolidBrush(Color.YellowGreen);


            // Create rectangle and Draw rectangle to screen.
            for (int i = 0; i < 6; i++)
            {
               rect[i] = new Rectangle(100 * i, 100, 100, 200);
                G.FillRectangle(myBrush, rect[i]);
                G.DrawRectangle(blackPen, rect[i]);
            }

            //Update parking space rectangle color
            G.FillRectangle(myBrush2, rect[2]);
            G.FillRectangle(myBrush2, rect[5]);

            //draw parking numbers
            for (int i = 0; i < 6; i++)
            {
                 DrawStringFloatFormat((i + 1).ToString(), 100 * i + 50, 200.0F);
            }
        }
        private async void getBeaconDataAsync() // grabs population from database 
        {
            
    
            //******************** Get initial list of beacons ***********************//
            var BeaconsSet = await client
               .Child("Beacons/")//Prospect list
               .OnceSingleAsync<Beacons>();
            displayBeaconsData(BeaconsSet);

            //******************** Get changes on beacons ***********************//
            onChildChanged();


        }

        private void onChildChanged() // Waits for data base to start with variable
        {


            var child = client.Child("Beacons/data");
            var observable = child.AsObservable<Beacon>();
            var subscription = observable
                .Subscribe(x =>
                {
                    Console.WriteLine($"beacon id: { x.Object.Id} [{ x.Object.D1}]");                 
                });
           
        }
        public void DrawStringFloatFormat(String drawString, float x, float y)
        {
            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);          

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
           // drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;

            // Draw string to screen.
            G.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }

        private void displayBeaconsData(Beacons beacons) // display beacons
        {
            foreach (var beacon in beacons.data)
            {
                Console.WriteLine($"beacon id: { beacon.Id} [{ beacon.D1}]");
            }

        }
    }
}
