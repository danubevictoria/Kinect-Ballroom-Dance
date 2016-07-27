// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Diagnostics;


namespace KinectBallroomDance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Application class members
        bool closing = false;
        const int skeletonCount = 6;
        Skeleton[] allSkeletons = new Skeleton[skeletonCount];
        bool arrowvisible = false;
        bool footcolored = false;
        int step = 1;
        bool arrowPass = false;
        Dictionary<TextBlock, int> messageDisplayCounters = new Dictionary<TextBlock, int>();
        int messageDisplayHoldLimit = 150;
        List<Point3D[]> remembered = new List<Point3D[]>(); //goes at the top 
        #endregion

        #region Menu class members
        bool inMenu = true;

        string menuName = "menu_title.jpg";
        List<string> backTrace = new List<string>();
        int posCounterX = 0;
        int posCounterY = 0;
        double thresholdX;
        double thresholdY;
        bool canSee = false;
        float initialX = 0.224f;
        float initialY = 0.082f;
        string learnAs = "";
        string postureOrDance = "";
        string postureType = "";
        string danceType = "";
        double[][] positionListX = new double[][]
        { 
            new double[] {175, 540},
            new double[] {71, 486},
            new double[] {210, 545},
            new double[] {210, 545} 
        };
        double[][] positionListY = new double[][]
        { 
            new double[] {263, 428},
            new double[] {169},
            new double[] {91, 199, 306, 416},
            new double[] {89, 177, 264, 353, 441} 
        };
        float cursorStartPosX;
        float cursorStartPosY;
        int cursorCounter = 0;
        int leftCursorPos = 36;
        int rightCursorPos = 501;
        int transitionCounter = 0;
        int backCounter = 0;
        int windowTracker = 0;
        int mediaStartCounter = 0;

        int totalFrames = 0;
        int lastFrames = 0;
        DateTime lastTime = DateTime.MaxValue;
        #endregion

        public MainWindow()
        {
            backTrace.Add("menu_title.jpg");
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //sign up for the event
            sensorChooser.KinectSensorChanged += new DependencyPropertyChangedEventHandler(KinectSensorChanged);
 
            messageDisplayCounters.Add(label1, 0);
            messageDisplayCounters.Add(label2, 0);
            messageDisplayCounters.Add(label3, 0);
        }

        void KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var oldSensor = (KinectSensor)e.OldValue;

            // Stop the old sensor
            if (oldSensor != null)
            {
                StopKinect(oldSensor);
            }

            // Get the new sensor
            var newSensor = (KinectSensor)e.NewValue;
            if (newSensor == null)
            {
                return;
            }

            newSensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(AllFramesReadyController);

            // Turn on features that you need
            newSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            newSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            newSensor.SkeletonStream.Enable();

            try
            {
                newSensor.Start();
            }
            catch (System.IO.IOException)
            {
                // This happens if another app is using the Kinect
                sensorChooser.AppConflictOccurred();
            }
        }

        void AllFramesReadyController(object sender, AllFramesReadyEventArgs e)
        {
            if (closing)
            {
                return;
            }

            if (menu.Visibility == System.Windows.Visibility.Visible)
                MenuAllFramesReady(sender, e);
            else
                AppAllFramesReady(sender, e);
        }

        void AppAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            Skeleton first = GetFirstSkeleton(e);

            if (first == null)
                return;

            runPosture(first); 
  
            GetFeetPositions(first);

            if (postureOrDance == "dance")
                setPostureDisplayOff();
            else
                setPostureDisplayOn();

            Debug.Print("danceType: " + danceType);
            switch (danceType)
            {
                case "cha-cha_basic":
                    runChaChaSequence(first);
                    break;
                case "closed_change":
                    runClosedChange(first);
                    break;
            }
        }

        void MenuAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            Skeleton first = GetFirstSkeleton(e);

            if (first == null)
                return;

            //*******************************************
            // ROHAN
            if (first.Joints[JointType.Head].Position.Y != 0)
            {
                //check if we can see the user
                if (!canSee)
                {
                    BitmapImage checkMark = new BitmapImage();
                    checkMark.BeginInit();
                    checkMark.UriSource = new Uri("Checkmark.jpg", UriKind.Relative);
                    checkMark.EndInit();
                    canSeePic.Source = checkMark;
                    canSee = true;
                    Console.WriteLine("STARTED");
                }
                //initialize cursor position + other first based on current screen
                switch (windowTracker)
                {
                    case 0:
                        rectangle1.Height = 98;
                        rectangle1.Width = 165;
                        thresholdX = 0.025;
                        thresholdY = 0.035;
                        break;
                    case 1:
                        rectangle1.Height = 205;
                        rectangle1.Width = 323;
                        break;
                    case 2:
                        rectangle1.Height = 76;
                        rectangle1.Width = 153;
                        break;
                    case 3:
                        rectangle1.Height = 60;
                        rectangle1.Width = 153;
                        postureOrDance = "dance";
                        thresholdY = 0.075;
                        break;
                }
                cursorCounter++;
                if (cursorCounter == 30)
                {
                    cursorStartPosX = initialX;
                    cursorStartPosY = initialY;
                    Console.WriteLine("FUCJH!\n");
                }
                if (cursorCounter > 30 && windowTracker < 8)
                {
                    //Left to right motion
                    if (first.Joints[JointType.HandRight].Position.X - cursorStartPosX >= thresholdX)
                    {
                        if (posCounterX + 1 < positionListX[windowTracker].Length)
                        {
                            Console.WriteLine("right hooray!\n");
                            Canvas.SetLeft(rectangle1, positionListX[windowTracker][posCounterX + 1]);
                            posCounterX++;
                            cursorStartPosX = first.Joints[JointType.HandRight].Position.X;
                        }
                    }
                    if (first.Joints[JointType.HandRight].Position.X - cursorStartPosX <= -thresholdX)
                    {
                        if (posCounterX > 0)
                        {
                            Console.WriteLine("left hooray!\n");
                            Canvas.SetLeft(rectangle1, positionListX[windowTracker][posCounterX - 1]);
                            posCounterX--;
                            cursorStartPosX = first.Joints[JointType.HandRight].Position.X;
                        }
                    }
                    //Up and down motion
                    if (first.Joints[JointType.HandRight].Position.Y - cursorStartPosY <= -thresholdY)
                    {
                        if (posCounterY + 1 < positionListY[windowTracker].Length)
                        {
                            Console.WriteLine("down hooray!\n");
                            Canvas.SetTop(rectangle1, positionListY[windowTracker][posCounterY + 1]);
                            posCounterY++;
                            cursorStartPosY = first.Joints[JointType.HandRight].Position.Y;
                        }
                    }
                    if (first.Joints[JointType.HandRight].Position.Y - cursorStartPosY >= thresholdY)
                    {
                        if (posCounterY > 0)
                        {
                            Console.WriteLine("up hooray!\n");
                            Canvas.SetTop(rectangle1, positionListY[windowTracker][posCounterY - 1]);
                            posCounterY--;
                            cursorStartPosY = first.Joints[JointType.HandRight].Position.Y;
                        }
                    }
                    if (first.Joints[JointType.Head].Position.Y - first.Joints[JointType.HandLeft].Position.Y < 0)
                    {
                        if (backCounter >= 0 && windowTracker > 0)
                        {
                            backCounter++;
                        }
                        if (backCounter > 10)
                        {
                            backCounter = -10;
                            rectangle1.Fill = Brushes.Blue;
                            string backSource = backTrace[backTrace.Count - 2];
                            backTrace.RemoveAt(backTrace.Count - 1);
                            BitmapImage backImg = new BitmapImage();
                            backImg.BeginInit();
                            backImg.UriSource = new Uri(backSource, UriKind.Relative);
                            backImg.EndInit();
                            menuImage.Source = backImg;
                            if (inMenu)
                            {
                                windowTracker = windowTracker == 3 ? 1 : windowTracker - 1;
                            }
                            else
                            {
                                inMenu = true;
                                rectangle1.Opacity = 0.5;
                            }
                            Canvas.SetLeft(rectangle1, positionListX[windowTracker][0]);
                            Canvas.SetTop(rectangle1, positionListY[windowTracker][0]);
                        }
                        else if (backCounter > 7 && windowTracker != 0)
                        {
                            rectangle1.Fill = Brushes.Red;
                        }
                    }
                    else if (first.Joints[JointType.Head].Position.Y - first.Joints[JointType.HandLeft].Position.Y < 0.15)
                    {
                        if (transitionCounter >= 0 && backCounter >= 0)
                        {
                            transitionCounter++;
                        }

                        if (transitionCounter > 6)
                        {
                            string imageSource = "";
                            switch (windowTracker)
                            {
                                case 0:
                                    string[,] imageSources = new string[,] {
                                        { "menu_leader.jpg", "menu_follower.jpg" },
                                        { "menu_partner.jpg", "undefined.jpg" }
                                    };
                                    string[,] learnAsStrings = new string[,] {
                                        { "leader", "follower" },
                                        { "partner", "" }
                                    };

                                    int[,] wtJump = new int[,] {
                                        { 1, 1 },
                                        { 1, windowTracker }
                                    };

                                    imageSource = imageSources[posCounterX, posCounterY];
                                    learnAs = learnAsStrings[posCounterX, posCounterY];
                                    windowTracker = wtJump[posCounterX, posCounterY];

                                    break;
                                case 1:
                                    //menu_leader, menu_partner, menu_follower
                                    if (posCounterX == 0)
                                    {
                                        imageSource = "menu_positions_frames.jpg";
                                        windowTracker = 2;
                                    }
                                    else
                                    {
                                        imageSource = "menu_dances.jpg";
                                        windowTracker = 3;
                                    }
                                    break;
                                case 2:
                                    // menu_positions_frames.jpg
                                    imageSource = "undefined.jpg";
                                    string[,] jumpTable = new string[,] {
                                        { "standard_posture", "standard_frame", "outside_partner", "promenade_position" },
                                        { "latin_posture", "latin_frame", "fan_position", "open_position" }
                                    };
                                    postureType = jumpTable[posCounterX, posCounterY];
                                    break;
                                case 3:
                                    // menu_dances.jpg
                                    imageSource = "undefined.jpg";
                                    string[,] jumpTable2 = new string[,] {
                                        { "cha-cha_basic", "tango", "waltz", "foxtrot", "quickstep" },
                                        { "closed_change", "samba", "rumba", "paso_doble", "jive" }
                                    };
                                    Debug.Print("posCounters:" + posCounterX + " " + posCounterY);
                                    danceType = jumpTable2[posCounterX, posCounterY];
                                    break;
                            }
                            BitmapImage nextImg = new BitmapImage();
                            nextImg.BeginInit();
                            nextImg.UriSource = new Uri(imageSource, UriKind.Relative);
                            nextImg.EndInit();
                            menuImage.Source = nextImg;
                            cursorStartPosX = initialX;
                            cursorStartPosY = initialY;
                            posCounterX = 0;
                            posCounterY = 0;
                            backTrace.Add(imageSource);
                            if (imageSource.Equals("undefined.jpg"))
                            {
                                inMenu = false;
                                rectangle1.Opacity = 0;
                            }
                            Canvas.SetLeft(rectangle1, positionListX[windowTracker][0]);
                            Canvas.SetTop(rectangle1, positionListY[windowTracker][0]);
                            rectangle1.Fill = Brushes.Blue;
                            transitionCounter = -15;
                        }
                        else if (transitionCounter > 4)
                        {
                            if (windowTracker >= 2) {
                                if (windowTracker == 3)
                                {
                                    string[,] jumpTable2 = new string[,] {
                                        { "cha-cha_basic", "tango", "waltz", "foxtrot", "quickstep" },
                                        { "closed_change", "samba", "rumba", "paso_doble", "jive" }
                                    };
                                    Debug.Print("posCounters:" + posCounterX + " " + posCounterY);
                                    danceType = jumpTable2[posCounterX, posCounterY];
                                }
                                else
                                {
                                    string[,] jumpTable = new string[,] {
                                        { "standard_posture", "standard_frame", "outside_partner", "promenade_position" },
                                        { "latin_posture", "latin_frame", "fan_position", "open_position" }
                                    };
                                    postureType = jumpTable[posCounterX, posCounterY];
                                }
                                menu.Visibility = System.Windows.Visibility.Hidden;
                                app.Visibility = System.Windows.Visibility.Visible;
                            } else 
                            rectangle1.Fill = Brushes.Green;
                        }
                    }
                    else
                    {
                        transitionCounter = 0;
                        backCounter = 0;
                    }
                }
            }
        }
    }
}