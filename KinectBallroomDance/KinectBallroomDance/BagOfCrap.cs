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
    public partial class MainWindow : Window
    {
        void setPostureDisplayOff()
        {
            Canvas.SetTop(border5, 400);
            Canvas.SetLeft(border5, 20);
            border5.Height = 300;
            border5.Width = 400;
            Canvas.SetTop(CameraVideo, 400);
            Canvas.SetLeft(CameraVideo, 20);
            CameraVideo.Height = 300;
            CameraVideo.Width = 400;
            border5.Clip = new RectangleGeometry(new Rect(0, 0, 400, 300));
            leftfoot.Visibility = Visibility.Visible;
            rightfoot.Visibility = Visibility.Visible;
        }

        void setPostureDisplayOn()
        {
            Canvas.SetTop(border5, 20);
            Canvas.SetLeft(border5, -230);
            border5.Height = 675;
            border5.Width = 900;
            Canvas.SetTop(CameraVideo, 20);
            Canvas.SetLeft(CameraVideo, -230);
            CameraVideo.Height = 675;
            CameraVideo.Width = 900;
            border5.Clip = new RectangleGeometry(new Rect(250, 0, 400, 675));
            leftfoot.Visibility = Visibility.Hidden;
            rightfoot.Visibility = Visibility.Hidden;
        }


        void runChaChaSequence(Skeleton first)
        {
            if (HandsAboveHead())
            {
                dancepause.Visibility = Visibility.Visible;
            }
            if (step == 1)
            {
                rightarrow.Height = 30;
                rightarrow.Width = 30;
                doRightArrow(rightfoot);
                label1.Text = "Move your right foot to the right";
            }
            else if (step == 2)
            {
                checkWeightChange(rightfoot, first);
                label1.Text = "Put your weight over your right foot";
            }
            else if (step == 3)
            {
                doRightArrow(leftfoot);
                label1.Text = "Close your left foot to your right foot";
            }
            else if (step == 4)
            {
                checkWeightChange(leftfoot, first);
                label1.Text = "Put your weight over your left foot";
            }
            else if (step == 5)
            {
                doRightArrow(rightfoot);
                label1.Text = "Move your right foot to the right";
            }
            else if (step == 6)
            {
                checkWeightChange(rightfoot, first);
                label1.Text = "Put your weight over your right foot";
            }
            else if (step == 7)
            {
                crossrightarrow.Width = 100;
                label1.Text = "Turn a quarter to your right on your right foot and step forward with your left foot";
                turnCWandStepForward();
            }
            else if (step == 8)
            {
                checkSplitWeight(first);
                label1.Text = "Have your weight split between both feet";
            }
            else if (step == 9)
            {
                checkWeightChange(rightfoot, first);
                label1.Text = "Put your weight over your right foot";
            }
            else if (step == 10)
            {
                crossleftarrow.Width = 100;
                label1.Text = "Turn a quarter to your left on your right foot and step to the side with your left foot";
                turnCCWandStepSide();
            }
            else if (step == 11)
            {
                checkWeightChange(leftfoot, first);
                label1.Text = "Put your weight over your left foot";
            }
            else if (step == 12)
            {
                leftarrow.Height = 30;
                leftarrow.Width = 30;
                doLeftArrow(rightfoot);
                label1.Text = "Close your right foot to your left foot";
            }
            else if (step == 13)
            {
                checkWeightChange(rightfoot, first);
                label1.Text = "Put your weight over your right foot";
            }
            else if (step == 14)
            {
                doLeftArrow(leftfoot);
                label1.Text = "Move your left foot to the left";
            }
            else if (step == 15)
            {
                checkWeightChange(leftfoot, first);
                label1.Text = "Put your weight over your left foot";
            }
            else if (step == 16)
            {
                turnCCWandStepForward();
                label1.Text = "Turn a quarter to your left on your left foot and step forward with your right";
            }
            else if (step == 17)
            {
                checkSplitWeight(first);
                label1.Text = "Have your weight split between both feet";
            }
            else if (step == 18)
            {
                checkWeightChange(leftfoot, first);
                label1.Text = "Put your weight over your left foot";
            }
            else if (step == 19)
            {
                turnCWandStepSide();
                label1.Text = "Turn a quarter to your right on your left foot and step to the side with your right";
            }
            else
            {
                //step = 2;
                dancepause.Visibility = Visibility.Visible;
            }

        }


        void runPosture(Skeleton first)
        {
            if (HandsAboveHead())
            {
                posturepause.Visibility = Visibility.Visible;
            }

            SkeletonPoint HandLeftPos = first.Joints[JointType.HandLeft].Position;
            SkeletonPoint HandRightPos = first.Joints[JointType.HandRight].Position;
            SkeletonPoint HeadPos = first.Joints[JointType.Head].Position;
            SkeletonPoint HipPos = first.Joints[JointType.HipCenter].Position;
            SkeletonPoint SpinePos = first.Joints[JointType.Spine].Position;
            SkeletonPoint ChestPos = first.Joints[JointType.ShoulderCenter].Position;
            SkeletonPoint ShoulderLeftPos = first.Joints[JointType.ShoulderLeft].Position;
            SkeletonPoint ShoulderRightPos = first.Joints[JointType.ShoulderRight].Position;
            SkeletonPoint ElbowLeftPos = first.Joints[JointType.ElbowLeft].Position;
            SkeletonPoint ElbowRightPos = first.Joints[JointType.ElbowRight].Position;
            SkeletonPoint FootLeftPos = first.Joints[JointType.FootLeft].Position;
            SkeletonPoint FootRightPos = first.Joints[JointType.FootRight].Position;
            SkeletonPoint KneeLeftPos = first.Joints[JointType.KneeLeft].Position;
            SkeletonPoint KneeRightPos = first.Joints[JointType.KneeRight].Position;

            if (messageDisplayCounters[label1] < messageDisplayHoldLimit)
                messageDisplayCounters[label1] += 1;
            else
            {
                string oldMessage = label1.Text;

                if (!checkStraightSpine(HeadPos, ChestPos, SpinePos, HipPos))
                    label1.Text = "Keep your blocks of weight aligned";
                else if (!checkFeetClose(FootLeftPos, FootRightPos))
                    label1.Text = "Keep your feet together";
                else if (!checkKneesClose(KneeLeftPos, KneeRightPos))
                    label1.Text = "Keep your knees together";
                else
                    label1.Text = "Great job!";
                if (label1.Text != oldMessage) // If the same message is shown, let other messages have a crack at it without having to wait another 5 seconds
                    messageDisplayCounters[label1] = 0;
            }

            if (messageDisplayCounters[label2] < messageDisplayHoldLimit)
                messageDisplayCounters[label2] += 1;
            else
            {
                string oldMessage = label2.Text;
                if (!checkElbowLeftAlign(ElbowLeftPos, ShoulderLeftPos, ShoulderRightPos))
                    label2.Text = "Keep your left elbow aligned with your shoulders";
                else if (!checkLeftHandHeight(HeadPos, HandLeftPos))
                    label2.Text = "Your left hand should be at eye level";
                else if (!checkLeftHandExtension(ElbowLeftPos, HandLeftPos))
                    label2.Text = "Extend your left arm further";
                else
                    label2.Text = "Good job!";
                if (label2.Text != oldMessage)
                    messageDisplayCounters[label2] = 0;
            }

            if (messageDisplayCounters[label3] < messageDisplayHoldLimit)
                messageDisplayCounters[label3] += 1;
            else
            {
                string oldMessage = label3.Text;
                if (!checkElbowRightInFront(ElbowRightPos, ShoulderRightPos))
                    label3.Text = "Make sure your right elbow is in front of you";
                else if (!checkHandRightHeight(HandRightPos, ShoulderRightPos))
                    label3.Text = "Keep your right hand at chest level";
                else if (!checkHandRightPlacement(HandRightPos, ElbowRightPos))
                    label3.Text = "Bring your right hand out further";
                else if (!checkElbowRightHeight(ElbowRightPos, ShoulderRightPos))
                    label3.Text = "Keep your right elbow up";
                else
                    label3.Text = "Good job!";
                if (label3.Text != oldMessage)
                    messageDisplayCounters[label3] = 0;
            }
        }

        void runClosedChange(Skeleton first)
        {
            if (step == 1)
            {
                uparrow.Height = 75;
                uparrow.Width = 50;
                doUpArrow(rightfoot);
                label1.Text = "Move your right foot forward";
            }
            else if (step == 2)
            {
                checkWeightChange(rightfoot, first);
                label1.Text = "Put your weight over your right foot";
            }
            else if (step == 3)
            {
                curveupleftarrow.Height = 100;
                curveupleftarrow.Width = 100;
                doCurveUpLeftArrow(leftfoot);
                label1.Text = "Move your left foot forward and to the left";
            }
            else if (step == 4)
            {
                checkWeightChange(leftfoot, first);
                label1.Text = "Put your weight over your left foot";
            }
            else if (step == 5)
            {
                leftarrow.Height = 50;
                label1.Text = "Bring your right foot in";
                leftarrow.Width = 50;
                doLeftArrow(rightfoot);
            }
            else if (step == 6)
            {
                checkWeightChange(rightfoot, first);
            }
            else
            {
                dancepause.Visibility = Visibility.Visible;
            }

        }

        void checkWeightChange(Ellipse foot, Skeleton first)
        {
            if (!footcolored)
            {
                foot.Fill = Brushes.Red;
                footcolored = true;
            }

            double xSpine = first.Joints[JointType.Spine].Position.X;
            double zSpine = first.Joints[JointType.Spine].Position.Z;
            double xFoot, zFoot;
            if (foot.Equals(leftfoot))
            {
                xFoot = first.Joints[JointType.FootLeft].Position.X;
                zFoot = first.Joints[JointType.FootLeft].Position.Z;
            }
            else
            {
                xFoot = first.Joints[JointType.FootRight].Position.X;
                zFoot = first.Joints[JointType.FootRight].Position.Z;
            }
            if (Math.Abs(xSpine - xFoot) < .15 && Math.Abs(zSpine - zFoot) < .15)
            {
                foot.Fill = Brushes.Black;
                footcolored = false;
                step += 1;
            }

        }

        void checkSplitWeight(Skeleton first)
        {
            if (!footcolored)
            {
                leftfoot.Fill = Brushes.Purple;
                rightfoot.Fill = Brushes.Purple;
                footcolored = true;
            }

            double xSpine = first.Joints[JointType.Spine].Position.X;
            double zSpine = first.Joints[JointType.Spine].Position.Z;

            double lxFoot = first.Joints[JointType.FootLeft].Position.X;
            double lzFoot = first.Joints[JointType.FootLeft].Position.Z;
            double rxFoot = first.Joints[JointType.FootRight].Position.X;
            double rzFoot = first.Joints[JointType.FootRight].Position.Z;

            double xFoot = (lxFoot + rxFoot) / 2;
            double zFoot = (lzFoot + rzFoot) / 2;

            if (Math.Abs(xSpine - xFoot) < .15 && Math.Abs(zSpine - zFoot) < .15)
            {
                leftfoot.Fill = Brushes.Black;
                rightfoot.Fill = Brushes.Black;
                footcolored = false;
                step += 1;
            }

        }

        void doUpArrow(Ellipse foot)
        {
            if (!arrowvisible)
            {
                double fx = Canvas.GetLeft(foot);
                double fy = Canvas.GetTop(foot);
                Canvas.SetLeft(uparrow, fx - foot.Width / 2);
                Canvas.SetTop(uparrow, fy - 80);
                uparrow.Visibility = System.Windows.Visibility.Visible;
                arrowvisible = true;
            }
            arrowPassThroughCenter(foot, uparrow);
            if ((Canvas.GetTop(foot) <= Canvas.GetTop(uparrow)) && arrowPass)
            {
                uparrow.Visibility = System.Windows.Visibility.Hidden;
                arrowvisible = false;
                arrowPass = false;
                step += 1;
            }
        }

        void doCurveUpLeftArrow(Ellipse foot)
        {
            if (!arrowvisible)
            {
                double fx = Canvas.GetLeft(foot);
                double fy = Canvas.GetTop(foot);
                Canvas.SetLeft(curveupleftarrow, fx - foot.Width / 2 - 50);
                Canvas.SetTop(curveupleftarrow, fy - 100);
                curveupleftarrow.Visibility = System.Windows.Visibility.Visible;
                arrowvisible = true;
            }
            arrowPassThroughBend(foot, curveupleftarrow);
            if ((Canvas.GetTop(foot) <= (Canvas.GetTop(curveupleftarrow) + 20)) &&
                (Canvas.GetLeft(foot) <= Canvas.GetLeft(curveupleftarrow)) &&
                arrowPass)
            {
                curveupleftarrow.Visibility = System.Windows.Visibility.Hidden;
                arrowvisible = false;
                arrowPass = false;
                step += 1;
            }
        }

        void doLeftArrow(Ellipse foot)
        {
            if (!arrowvisible)
            {
                double fx = Canvas.GetLeft(foot);
                double fy = Canvas.GetTop(foot);
                Canvas.SetLeft(leftarrow, fx - leftarrow.Width);
                Canvas.SetTop(leftarrow, fy);
                leftarrow.Visibility = System.Windows.Visibility.Visible;
                arrowvisible = true;
            }
            arrowPassThroughCenter(foot, leftarrow);
            if ((Canvas.GetLeft(foot) <= Canvas.GetLeft(leftarrow)) && arrowPass)
            {
                leftarrow.Visibility = System.Windows.Visibility.Hidden;
                arrowvisible = false;
                arrowPass = false;
                step += 1;
            }
        }

        void doRightArrow(Ellipse foot)
        {
            if (!arrowvisible)
            {
                double fx = Canvas.GetLeft(foot);
                double fy = Canvas.GetTop(foot);
                Canvas.SetLeft(rightarrow, fx + 30);
                Canvas.SetTop(rightarrow, fy);
                rightarrow.Visibility = System.Windows.Visibility.Visible;
                arrowvisible = true;
            }
            arrowPassThroughCenter(foot, rightarrow);
            if ((Canvas.GetLeft(foot) >= Canvas.GetLeft(rightarrow)) && arrowPass)
            {
                rightarrow.Visibility = System.Windows.Visibility.Hidden;
                arrowvisible = false;
                arrowPass = false;
                step += 1;
            }
        }

        void turnCWandStepForward()
        {
            if (!arrowvisible)
            {
                double lfx = Canvas.GetLeft(leftfoot);
                double lfy = Canvas.GetTop(leftfoot);
                Canvas.SetLeft(crossrightarrow, lfx + 30);
                Canvas.SetTop(crossrightarrow, lfy);
                crossrightarrow.Visibility = System.Windows.Visibility.Visible;
                arrowvisible = true;
            }

            rightfoot.Fill = Brushes.Yellow;

            double rfx = Canvas.GetLeft(rightfoot);
            double rfy = Canvas.GetTop(rightfoot);
            Canvas.SetLeft(cwturnarrow, rfx + rightfoot.Width / 4);
            Canvas.SetTop(cwturnarrow, rfy + rightfoot.Height / 4);
            cwturnarrow.Visibility = System.Windows.Visibility.Visible;

            arrowPassThroughCenter(leftfoot, crossrightarrow);
            if ((Canvas.GetLeft(leftfoot) >= Canvas.GetLeft(crossrightarrow)) && arrowPass)
            {
                crossrightarrow.Visibility = System.Windows.Visibility.Hidden;
                cwturnarrow.Visibility = System.Windows.Visibility.Hidden;
                rightfoot.Fill = Brushes.Black;
                rightfoot.Height = 30;
                rightfoot.Width = 60;
                leftfoot.Height = 30;
                leftfoot.Width = 60;
                arrowvisible = false;
                arrowPass = false;
                step += 1;
            }
        }

        void turnCCWandStepForward()
        {
            if (!arrowvisible)
            {
                double rfx = Canvas.GetLeft(rightfoot);
                double rfy = Canvas.GetTop(rightfoot);
                Canvas.SetLeft(crossleftarrow, rfx - 70);
                Canvas.SetTop(crossleftarrow, rfy);
                crossleftarrow.Visibility = System.Windows.Visibility.Visible;
                arrowvisible = true;
            }

            leftfoot.Fill = Brushes.Yellow;

            double lfx = Canvas.GetLeft(leftfoot);
            double lfy = Canvas.GetTop(leftfoot);
            Canvas.SetLeft(ccwturnarrow, lfx + leftfoot.Width / 4);
            Canvas.SetTop(ccwturnarrow, lfy + leftfoot.Height / 4);
            ccwturnarrow.Visibility = System.Windows.Visibility.Visible;

            arrowPassThroughCenter(rightfoot, crossleftarrow);
            if ((Canvas.GetLeft(rightfoot) <= Canvas.GetLeft(crossleftarrow)) && arrowPass)
            {
                crossleftarrow.Visibility = System.Windows.Visibility.Hidden;
                ccwturnarrow.Visibility = System.Windows.Visibility.Hidden;
                leftfoot.Fill = Brushes.Black;
                leftfoot.Height = 30;
                leftfoot.Width = 60;
                rightfoot.Height = 30;
                rightfoot.Width = 60;
                arrowvisible = false;
                arrowPass = false;
                step += 1;
            }
        }

        void turnCCWandStepSide()
        {
            if (!arrowvisible)
            {
                double lfx = Canvas.GetLeft(leftfoot);
                double lfy = Canvas.GetTop(leftfoot);
                Canvas.SetLeft(crossleftarrow, lfx - 70);
                Canvas.SetTop(crossleftarrow, lfy);
                crossleftarrow.Visibility = System.Windows.Visibility.Visible;
                arrowvisible = true;
            }

            rightfoot.Fill = Brushes.Yellow;

            double rfx = Canvas.GetLeft(rightfoot);
            double rfy = Canvas.GetTop(rightfoot);
            Canvas.SetLeft(ccwturnarrow, rfx + rightfoot.Width / 4);
            Canvas.SetTop(ccwturnarrow, rfy + rightfoot.Height / 4);
            ccwturnarrow.Visibility = System.Windows.Visibility.Visible;

            arrowPassThroughCenter(leftfoot, crossleftarrow);
            if ((Canvas.GetLeft(leftfoot) <= Canvas.GetLeft(crossleftarrow)) && arrowPass)
            {
                crossleftarrow.Visibility = System.Windows.Visibility.Hidden;
                ccwturnarrow.Visibility = System.Windows.Visibility.Hidden;
                rightfoot.Fill = Brushes.Black;
                rightfoot.Height = 60;
                rightfoot.Width = 30;
                leftfoot.Height = 60;
                leftfoot.Width = 30;
                arrowvisible = false;
                arrowPass = false;
                step += 1;
            }
        }

        void turnCWandStepSide()
        {
            if (!arrowvisible)
            {
                double rfx = Canvas.GetLeft(rightfoot);
                double rfy = Canvas.GetTop(rightfoot);
                Canvas.SetLeft(crossrightarrow, rfx + 30);
                Canvas.SetTop(crossrightarrow, rfy);
                crossrightarrow.Visibility = System.Windows.Visibility.Visible;
                arrowvisible = true;
            }

            leftfoot.Fill = Brushes.Yellow;

            double lfx = Canvas.GetLeft(leftfoot);
            double lfy = Canvas.GetTop(leftfoot);
            Canvas.SetLeft(cwturnarrow, lfx + leftfoot.Width / 4);
            Canvas.SetTop(cwturnarrow, lfy + leftfoot.Height / 4);
            cwturnarrow.Visibility = System.Windows.Visibility.Visible;

            arrowPassThroughCenter(rightfoot, crossrightarrow);
            if ((Canvas.GetLeft(rightfoot) >= Canvas.GetLeft(crossrightarrow)) && arrowPass)
            {
                crossrightarrow.Visibility = System.Windows.Visibility.Hidden;
                cwturnarrow.Visibility = System.Windows.Visibility.Hidden;
                leftfoot.Fill = Brushes.Black;
                leftfoot.Height = 60;
                leftfoot.Width = 30;
                rightfoot.Height = 60;
                rightfoot.Width = 30;
                arrowvisible = false;
                arrowPass = false;
                step += 1;
            }
        }

        void arrowPassThroughCenter(Ellipse foot, Image arrow)
        {
            double xcenter = (arrow.Width / 2) + Canvas.GetLeft(arrow);
            double ycenter = (arrow.Height / 2) + Canvas.GetTop(arrow);
            if ((Math.Abs(Canvas.GetLeft(foot) - xcenter) < 40) &&
                (Math.Abs(Canvas.GetTop(foot) - ycenter) < 40))
            {
                arrowPass = true;
            }

        }

        void arrowPassThroughBend(Ellipse foot, Image arrow)
        {
            double xcenter = (arrow.Width * .75) + Canvas.GetLeft(arrow);
            double ycenter = (arrow.Height * .25) + Canvas.GetTop(arrow);
            if ((Math.Abs(Canvas.GetLeft(foot) - xcenter) < 40) &&
                (Math.Abs(Canvas.GetTop(foot) - ycenter) < 40))
            {
                arrowPass = true;
            }
        }

        Boolean checkStraightSpine(SkeletonPoint HeadPos, SkeletonPoint ChestPos, SkeletonPoint SpinePos, SkeletonPoint HipPos)
        {
            //-straight spine (head, shoulder center, spine, hip center)
            List<Double> Xpositions = new List<Double>();
            Xpositions.Add(HeadPos.X);
            Xpositions.Add(ChestPos.X);
            Xpositions.Add(SpinePos.X);
            Xpositions.Add(HipPos.X);
            Xpositions.Sort();
            if (Xpositions[3] - Xpositions[0] > 0.09)
            {
                return false;
            }

            List<Double> Zpositions = new List<Double>();
            Zpositions.Add(HeadPos.Z);
            Zpositions.Add(ChestPos.Z);
            Zpositions.Add(SpinePos.Z);
            Zpositions.Add(HipPos.Z);
            Zpositions.Sort();
            if (Zpositions[3] - Zpositions[0] > 0.06)
            {
                return false;
            }

            return true;
        }

        Boolean checkFeetClose(SkeletonPoint FootLeftPos, SkeletonPoint FootRightPos)
        {
            //-feet are close
            if (Math.Abs(FootLeftPos.X - FootRightPos.X) > 0.1)
            {
                return false;
            }

            if (Math.Abs(FootLeftPos.Z - FootRightPos.Z) > 0.06)
            {
                return false;
            }

            return true;
        }

        Boolean checkKneesClose(SkeletonPoint KneeLeftPos, SkeletonPoint KneeRightPos)
        {
            //-feet are close
            if (Math.Abs(KneeLeftPos.X - KneeRightPos.X) > 0.2)
            {
                return false;
            }

            if (Math.Abs(KneeLeftPos.Z - KneeRightPos.Z) > 0.06)
            {
                return false;
            }

            return true;
        }

        //LEFT ARM
        Boolean checkLeftHandHeight(SkeletonPoint HeadPos, SkeletonPoint HandLeftPos)
        {
            //-lefthand about same Y as head, return true
            if (Math.Abs(HeadPos.Y - HandLeftPos.Y) > 0.2)
            {
                return false;
            }

            return true;
        }

        Boolean checkLeftHandExtension(SkeletonPoint ElbowLeftPos, SkeletonPoint HandLeftPos)
        {
            //-if lefthand further left than elbow, return true
            if (HandLeftPos.X > ElbowLeftPos.X)
            {
                return false;
            }

            return true;
        }

        Boolean checkElbowLeftAlign(SkeletonPoint ElbowLeftPos, SkeletonPoint ShoulderLeftPos, SkeletonPoint ShoulderRightPos)
        {

            //if left shoulder, right shoulder, left elbow aligned, return true
            List<Double> Ypositions = new List<Double>();
            Ypositions.Add(ElbowLeftPos.Y);
            Ypositions.Add(ShoulderLeftPos.Y);
            Ypositions.Add(ShoulderRightPos.Y);
            Ypositions.Sort();

            Double ShoulderSlope = (ShoulderRightPos.Z - ShoulderLeftPos.Z) / (ShoulderRightPos.X - ShoulderLeftPos.X);
            Double ElbowSlope = (ShoulderLeftPos.Z - ElbowLeftPos.Z) / (ShoulderLeftPos.X - ElbowLeftPos.X);

            if ((Ypositions[2] - Ypositions[0] > 0.05) || (Math.Abs(ShoulderSlope - ElbowSlope) > 1))
            {
                return false;
            }

            return true;
        }

        //RIGHT ARM            
        Boolean checkElbowRightInFront(SkeletonPoint ElbowRightPos, SkeletonPoint ShoulderRightPos)
        {
            //-right elbow in front of body
            if (ElbowRightPos.Z > ShoulderRightPos.Z)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        Boolean checkHandRightHeight(SkeletonPoint HandRightPos, SkeletonPoint ShoulderRightPos)
        {
            //-right hand about same Y as right shoulder
            if (Math.Abs(HandRightPos.Y - ShoulderRightPos.Y) > 0.05)
            {
                return false;
            }
            return true;
        }

        Boolean checkHandRightPlacement(SkeletonPoint HandRightPos, SkeletonPoint ElbowRightPos)
        {
            //-right hand in front of elbow
            if (Math.Abs(HandRightPos.Z - ElbowRightPos.Z) < 0.15)
            {
                return false;
            }

            //right hand is toward the body
            if (HandRightPos.X > ElbowRightPos.X)
            {
                return false;
            }
            return true;
        }

        Boolean checkElbowRightHeight(SkeletonPoint ElbowRightPos, SkeletonPoint ShoulderRightPos)
        {
            //-shoulder and elbow same Y
            if (Math.Abs(ShoulderRightPos.Y - ElbowRightPos.Y) > 0.05)
            {
                return false;
            }
            return true;
        }

        void GetFeetPositions(Skeleton first)
        {
            double xFL = first.Joints[JointType.FootLeft].Position.X;
            double zFL = first.Joints[JointType.FootLeft].Position.Z;
            double xFR = first.Joints[JointType.FootRight].Position.X;
            double zFR = first.Joints[JointType.FootRight].Position.Z;

            double newxFL = (xFL + 1) * 200;
            double newxFR = (xFR + 1) * 200;
            double newzFL = (zFL - 1) * 200;
            double newzFR = (zFR - 1) * 200;

            Canvas.SetLeft(leftfoot, newxFL - leftfoot.Width / 2);
            Canvas.SetTop(leftfoot, newzFL - leftfoot.Height / 2);

            Canvas.SetLeft(rightfoot, newxFR - rightfoot.Width / 2);
            Canvas.SetTop(rightfoot, newzFR - rightfoot.Height / 2);

        }

        public Boolean HandsAboveHead()
        {
            if (remembered.Count <= 59)
            {
                return false;
            }
            else if (remembered.TrueForAll(HandsAreAboveHead))
            {
                return true;
            }
            else return false;
        }

        public Boolean HandsAreAboveHead(Point3D[] pointarray)
        {
            if (pointarray[0].Y < pointarray[1].Y && pointarray[0].Y < pointarray[2].Y)
            {
                return true;
            }
            else return false;
        }


        Skeleton GetFirstSkeleton(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                {
                    return null;
                }


                skeletonFrameData.CopySkeletonDataTo(allSkeletons);

                // Get the first tracked skeleton
                Skeleton first = (from s in allSkeletons
                                  where s.TrackingState == SkeletonTrackingState.Tracked
                                  select s).FirstOrDefault();

                if (first != null)
                {
                    if (remembered.Count >= 60)
                    {
                        remembered.RemoveAt(0);
                        Point3D[] temp = new Point3D[3];
                        temp[0] = new Point3D(first.Joints[JointType.Head].Position.X, first.Joints[JointType.Head].Position.Y, first.Joints[JointType.Head].Position.Z);
                        temp[1] = new Point3D(first.Joints[JointType.HandLeft].Position.X, first.Joints[JointType.HandLeft].Position.Y, first.Joints[JointType.HandLeft].Position.Z);
                        temp[2] = new Point3D(first.Joints[JointType.HandRight].Position.X, first.Joints[JointType.HandRight].Position.Y, first.Joints[JointType.HandRight].Position.Z);
                        remembered.Add(temp);
                    }
                    else
                    {
                        Point3D[] temp = new Point3D[3];
                        temp[0] = new Point3D(first.Joints[JointType.Head].Position.X, first.Joints[JointType.Head].Position.Y, first.Joints[JointType.Head].Position.Z);
                        temp[1] = new Point3D(first.Joints[JointType.HandLeft].Position.X, first.Joints[JointType.HandLeft].Position.Y, first.Joints[JointType.HandLeft].Position.Z);
                        temp[2] = new Point3D(first.Joints[JointType.HandRight].Position.X, first.Joints[JointType.HandRight].Position.Y, first.Joints[JointType.HandRight].Position.Z);
                        remembered.Add(temp);
                    }
                }
                return first;

            }
        }

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    // Stop sensor 
                    sensor.Stop();

                    // Stop audio if not null
                    if (sensor.AudioSource != null)
                    {
                        sensor.AudioSource.Stop();
                    }
                }
            }
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closing = true;
            StopKinect(sensorChooser.Kinect);
        }

    }
}
