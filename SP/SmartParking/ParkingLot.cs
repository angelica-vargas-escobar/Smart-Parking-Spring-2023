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
        Rectangle[] rect = new Rectangle[6];
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
            getParkingMapDataAsync();
            // getSensorDataAsync();
            // DrawSlots();
        }
        private void DrawSlots(object sender, EventArgs e)
        //private async void DrawSlots()
        {
            /*                int startX1 = 50;
                            int startY1 = 50;
                            // int distance = 50;

                            Pen blackPen = new Pen(Color.Thistle, 10);
                            SolidBrush myBrush = new SolidBrush(Color.GreenYellow);
                            SolidBrush myBrush2 = new SolidBrush(Color.Red);*/

            // hard coded drawing rectangles
            int startX1 = 50;
            int startY1 = 50;
            // int distance = 50;

            // Pen blackPen = new Pen(Color.Black, 0);
            Pen blackPen = new Pen(Color.Thistle, 10);
            //Pen blackPen2 = new Pen(Color.Thistle, 10);
            SolidBrush myBrush = new SolidBrush(Color.GreenYellow);
            SolidBrush myBrush2 = new SolidBrush(Color.Red);

            for (int i = 0; i < 3; i++)
            {
                if (i != 6) // find out how to fill specific slot
                {
                    rect[i] = new Rectangle(startX1 + 100 * i, startY1, 100, 200);
                    G.FillRectangle(myBrush, rect[i]);
                    G.DrawRectangle(blackPen, rect[i]);

                    DrawStringFloatFormat((i + 1).ToString(), startX1 + 100 * i + 50, startY1 + 100);
                }
                else
                {
                    rect[i] = new Rectangle(startX1 + 100 * i, startY1, 100, 200);
                    G.FillRectangle(myBrush2, rect[i]);
                    G.DrawRectangle(blackPen, rect[i]);

                    DrawStringFloatFormat((i + 1).ToString(), startX1 + 100 * i + 50, startY1 + 100);
                }
            }

            startY1 += 200;

            for (int i = 0; i < 3; i++)
            {
                if ((i + 3 != 5) && (i + 3 != 3)) // find out how to fill specific slots
                {
                    rect[i + 3] = new Rectangle(startX1 + 100 * i, startY1, 100, 200);
                    G.FillRectangle(myBrush, rect[i + 3]);
                    G.DrawRectangle(blackPen, rect[i + 3]);

                    DrawStringFloatFormat((i + 4).ToString(), startX1 + 100 * i + 50, startY1 + 100);
                }
                else
                {
                    rect[i + 3] = new Rectangle(startX1 + 100 * i, startY1, 100, 200);
                    G.FillRectangle(myBrush2, rect[i + 3]);
                    G.DrawRectangle(blackPen, rect[i + 3]);

                    DrawStringFloatFormat((i + 4).ToString(), startX1 + 100 * i + 50, startY1 + 100);
                }
            }
        }

        /*
        G.FillRectangle(myBrush, rect[j + 2]);
        G.FillRectangle(myBrush2, rect[j + 2]);
        G.FillRectangle(myBrush, rect[j + 5]);
        G.FillRectangle(myBrush2, rect[j + 5]);
        */


        // original code
        /*
        Pen blackPen = new Pen(Color.Black, 0);
        SolidBrush myBrush = new SolidBrush(Color.GreenYellow);
        SolidBrush myBrush2 = new SolidBrush(Color.Red);


        // Create rectangle and Draw rectangle to screen.
        for (int i = 0; i < 6; i++)
        {
           rect[i] = new Rectangle(100 * i, 100, 100, 200);
            G.FillRectangle(myBrush, rect[i]);
            G.DrawRectangle(blackPen, rect[i]);
        }

        //Update parking space rectangle color
        G.FillRectangle(myBrush2, rect[3]);
        G.FillRectangle(myBrush2, rect[5]);

        //draw parking numbers
        for (int i = 0; i < 6; i++)
        {
             DrawStringFloatFormat((i + 1).ToString(), 100 * i + 50, 200.0F);
        }
        */

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
                    Console.WriteLine($"beacon id: { x.Object.Id} [{ x.Object.D2}]");
                    Console.WriteLine($"beacon id: { x.Object.Id} [{ x.Object.D3}]");
                    Console.WriteLine($"beacon id: { x.Object.Id} [{ x.Object.D4}]");
                });

        }

        private async void getParkingMapDataAsync() // grabs population from database 
        {
            var parkingMapData = await client
               .Child("ParkingMap/")//Prospect list
               .OnceSingleAsync<ParkingMap>(); 
            // displayParkingMapData(parkingMapData);

            parkingMapChanged();

        }

        private void parkingMapChanged() // Waits for data base to start with variable
        {
            var child = client.Child("ParkingMap/data");
            var observable = child.AsObservable<ParkingMap>();
            var subscription = observable.Subscribe(x =>
                {
                    Console.WriteLine($"Parking Map data received");

                    Console.WriteLine($"    X: {x.Object.position[0].X}, Y: { x.Object.position[0].Y}");
                    Console.WriteLine($"    X: {x.Object.position[1].X}, Y: { x.Object.position[1].Y}");
                    Console.WriteLine($"    X: {x.Object.position[2].X}, Y: { x.Object.position[2].Y}");
                    Console.WriteLine($"    X: {x.Object.position[3].X}, Y: { x.Object.position[3].Y}");

                    Console.WriteLine();
                });

        }

/*        private async void getSensorDataAsync()
        {
            var SensorsSet = await client
               .Child("Sensors/")//Prospect list
               .OnceSingleAsync<Sensor>();
            //displaySensorData(SensorSet);

            updateSensor();

        }
        private void updateSensor() // Waits for data base to start with variable
        {
            var child = client.Child("Sensor/data");
            var observable = child.AsObservable<Sensor>();
            var subscription = observable.Subscribe(x =>
                {
                    Console.WriteLine($"Sensor data received");

                    Console.WriteLine($"    X: {x.Object.data[0].X}, Y: { x.Object.data[0].Y}");
                    Console.WriteLine($"    X: {x.Object.data[1].X}, Y: { x.Object.data[1].Y}");
                    Console.WriteLine($"    X: {x.Object.data[2].X}, Y: { x.Object.data[2].Y}");
                    Console.WriteLine($"    X: {x.Object.data[3].X}, Y: { x.Object.data[3].Y}");
                    //Console.WriteLine($"beacon id: { x.Object.Id} [{ x.Object.D1}]");
                });
        }*/


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


        /*        private void displayParkingMapData(Position Position) // display beacons
                {
                    foreach (var Position in Position.Position)
                    {
                        Console.WriteLine($" Sensor id: { Position.X} [{ Position.Y}]");
                    }

                }
        */


    }
}
