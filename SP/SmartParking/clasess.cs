using System;
using System.Drawing;
using System.Windows.Forms;

namespace TeamVaxxers
{

    // function collects beacon data
    public class Beacon
    {
        public double D1 { get; set; } // grabs distances from beacon to four sensors
        public double D2 { get; set; }
        public double D3 { get; set; }
        public double D4 { get; set; }
        public long Id { get; set; }  // grabs beacon id
        public long Time { get; set; } // grabs 

        // function updates the beacon with new data
        public void update(Beacon data) 
        {
            D1 = data.D1;  // updates distances from beacon to four sensors
            D2 = data.D2;
            D3 = data.D3;
            D4 = data.D4;
            Time = data.Time; // updates timestamp from last update?
        }

        // calculates x,y coordinates of car based on distance (beacons -> sensors)
        public Point getxy(Sensors sensor)
        {
            Point point = new Point(0, 0);
            //Point P1 = s.data[0].location;
            double x1 = sensor.data[0].location.x; // array of sensor object in location x
            double x2 = sensor.data[1].location.x;
            double x3 = sensor.data[2].location.x;
            double x4 = sensor.data[3].location.x;

            double y1 = sensor.data[0].location.y; // array of sensor object in location y
            double y2 = sensor.data[1].location.y;
            double y3 = sensor.data[2].location.y;
            double y4 = sensor.data[3].location.y;

        /*
            double A = sensor.data[1].location.x * 2 - sensor.data[0].location.x * 2;
            double B = sensor.data[1].location.y * 2 - sensor.data[0].location.y * 2;
            double C = Math.Pow(sensor.data[0].D1, 2) - Math.Pow(sensor.data[1].D2, 2) - Math.Pow(sensor.data[0].location.x, 2) + Math.Pow(sensor.data[1].location.x, 2) - Math.Pow(sensor.data[0].location.y, 2) + Math.Pow(sensor.data[1].location.y, 2);
            double D = sensor.data[2].location.x * 2 - sensor.data[1].location.x * 2;
            double E = sensor.data[2].location.y * 2 - sensor.data[1].location.y * 2;
            double F = Math.Pow(sensor.data[1].D2, 2) - Math.Pow(sensor.data[2].D3, 2) - Math.Pow(sensor.data[1].location.x, 2) + Math.Pow(sensor.data[2].location.x, 2) - Math.Pow(sensor.data[1].location.y, 2) + Math.Pow(sensor.data[2].location.y, 2);
            point.x = (C * E - F * B) / (E * A - B * D);
            point.y = (C * D - A * F) / (B * D - A * E);
        */
            // calculation to find the location of the beacon
            double A = 2 * x2 - 2 * x1;
            double B = 2 * y2 - 2 * y1;
            double C = Math.Pow(D1, 2) - Math.Pow(D2, 2) - Math.Pow(x1, 2) + Math.Pow(x2, 2) - Math.Pow(y1, 2) + Math.Pow(y2, 2);
            double D = 2 * x3 - 2 * x2;
            double E = 2 * y3 - 2 * y2;
            double F = Math.Pow(D2, 2) - Math.Pow(D3, 2) - Math.Pow(x2, 2) + Math.Pow(x3, 2) - Math.Pow(y2, 2) + Math.Pow(y3, 2);
            point.x = (C * E - F * B) / (E * A - B * D);
            point.y = (C * D - A * F) / (B * D - A * E);

            Console.WriteLine($"(x,y) = {point.x},{point.y}");

            return point;
        }
        // public int slotId { get; set; }  // ID of the slot the car is parked in
    }

    // 
    public class Beacons
    {
        public int Total { get; set; } // counts the total # of beacons
        public Beacon[] data { get; set; } // array of beacon data
        
        /*
        public Beacons(int total)
        {
            data = new Beacon[total];
            data[0] = new Beacon();
            data[1] = new Beacon();
            data[2] = new Beacon();
            data[3] = new Beacon();
        }
        */
    }

    // receives two data points
    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }

        public Point(double X, double Y)
        {
            x = X;
            y = Y;
        }
    }
    
    // receives data for 4 sensors
    public class Sensors
    {
        public int Total { set; get; }  // total sensor
        // record total
        public Sensor[] data { get; set; }  // get info for every sensor

        // initialize function that gets data from four sensors, counts total
        public Sensors(int total)
        {

            data = new Sensor[total];
            data[0] = new Sensor();
            data[1] = new Sensor();
            data[2] = new Sensor();
            data[3] = new Sensor();

        }
    }

    /* public class Sensor
    {
        public double S1 { get; set; }
        public double S2 { get; set; }
        public double S3 { get; set; }
        public double S4 { get; set; }
        public Point[] position { get; set; } // receives position x and y
    }
*/

    // locates the x,y coordinates of sensors
    public class Sensor
    {
        public Point location { get; set; }
        public Sensor()
        {
            location = new Point(0, 0);
        }
        public void setCoordinates(double x, double y)
        {
            location.x = x;
            location.y = y;
        }

    }

    // function that creates a slot
    public class Slot
    {
        int slotNum;
        public Point[] coordinates = new Point[4]; // four corners in one slot
        Rectangle rectangle; // initialize rectangle that i going to be drawn

        //  DrawTool drawTool = new DrawTool();
        SolidBrush fillRectangle = new SolidBrush(Color.Black);
        StringFormat drawFormat = new StringFormat();
        //G.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);

        Pen blackPen = new Pen(Color.Black, 0);
        SolidBrush fillG = new SolidBrush(Color.Green); // signals availability
        SolidBrush fillR = new SolidBrush(Color.Red); // signals occupancy

        public Slot(int i, Graphics G)
        {
            slotNum = i + 1;
            int b = i < 6 ? 0 : 2;
            coordinates[0] = new Point(1.5 * i, b);
            coordinates[1] = new Point((i + 1) * 1.5, b);
            coordinates[2] = new Point(1.5 * i, b + 2);
            coordinates[3] = new Point((i + 1) * 1.5, b + 2);

            Draw(slotNum, G);

        }
        public void ColorR(Graphics G)
        {
            G.FillRectangle(fillR, rectangle);
            G.DrawRectangle(blackPen, rectangle);
        }
        public void ColorG(Graphics G)
        {
            G.FillRectangle(fillG, rectangle);
            G.DrawRectangle(blackPen, rectangle);
        }

        // draws for 6 parking slots
        public void Draw(int index, Graphics G)
        {
            int j, i = index;
            if (i < 6)
            {
                j = 100;
            }
            else
            {
                j = 300;
                i = index - 6;
            }
            rectangle = new Rectangle(100 * i, j, 100, 200);
            G.FillRectangle(fillRectangle, rectangle);
            G.DrawRectangle(blackPen, rectangle);
        }
    }


    // function that draws parking map
    public class ParkingMap
    {
        public Slot[] parkingSlots { get; set; }

        public int Total { get; set; }
        public int slotID { get; set; }
        public int free { get; set; }

        // Graphics g = this.CreateGraphics();
        Graphics G;  // initializing for C# graphics

        // creates as many amount of slots needed as in total
        public ParkingMap(int total, Graphics g)
        {
            parkingSlots = new Slot[total];
            for (int i = 0; i < total; i++)
            {
                parkingSlots[i] = new Slot(i, g);
            }
        }
    }

        public class Response
    {
        public bool success { get; set; }
        public int index { get; set; }
        public string message { get; set; }
        public string received { get; set; }
        public string companyId { get; set; }
        public string color { get; set; }
        public int[] infected { get; set; }
    }
}

