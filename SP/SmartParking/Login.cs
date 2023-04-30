using System;
using System.Windows.Forms;
using System.IO;

namespace TeamVaxxers
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void loginBtn_Click_1(object sender, EventArgs e)
        {
            //string username, password;
            // string filePath = Path.Combine(Application.StartupPath, "login.txt");

            /*            try
                        {
                            using (StreamReader sr = new StreamReader(filePath))
                            {
                                username = sr.ReadLine();
                                password = sr.ReadLine();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error reading credentials file: " + ex.Message);  // error getting username & password of login
                            return;
                        }
            */
            User user = new User();
            if (user.username == usernameBox.Text && user.password == passwordBox.Text)
            {
                this.Hide();
                ParkingLot engine = new ParkingLot();
                engine.ShowDialog();
                this.Close();
            }
            else
            {
                label.Text = "Wrong Username or Password";
                usernameBox.Text = passwordBox.Text = "";
            }
        
        }

    /*            try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] parts = line.Split(':');
                            if (parts.Length == 2)
                            {
                                if (parts[0] == username && parts[1] == password)
                                {
                                    // Successful login
                                    this.Hide();
                                    ParkingLot engine = new ParkingLot();
                                    engine.ShowDialog();
                                    this.Close();
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error reading credentials file: " + ex.Message);
                    label.Text = "Wrong Username or Password"; // Incorrect username or password
                    usernameBox.Text = passwordBox.Text = "";
                    return;
                }
            }*/




    /*
    string username, password;
    try
    {
        string filePath = Path.Combine(Application.StartupPath, "login.txt");
        //string filePath = "C:\Users\varga\OneDrive\Desktop\SP\SmartParking\login.txt";
        using (StreamReader sr = new StreamReader(filePath))
        //using (StreamReader sr = new StreamReader("login.txt"))
        {
            username = sr.ReadLine();
            password = sr.ReadLine();
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error reading credentials file: " + ex.Message);
        return;
    }
    */

    /*            User user = new User();
                if (user.username == usernameBox.Text && user.password == passwordBox.Text)
                {
                    this.Hide();
                    ParkingLot engine = new ParkingLot();
                    engine.ShowDialog();
                    this.Close();

                }
                else
                {
                    label.Text = "Wrong Username or Password";
                    usernameBox.Text = passwordBox.Text = "";
                }
            }*/


    private void passwordBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void usernameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
