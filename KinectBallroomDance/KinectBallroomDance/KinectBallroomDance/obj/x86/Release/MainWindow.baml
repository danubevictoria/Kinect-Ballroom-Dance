   M S B A M L   `   `   `  ���� ^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers   KinectBallroomDance$   KinectBallroomDance.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     -     � Title$  Kinect Ballroom Dance��$	��768��$	��910��.���� -   $��wrapper��$	��720��$	��880��.+�=   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  -   $��sensorChooser��$	��880��$	��720�� �� Left$
 -916��
 �� Top$ 0���� -   $	��app��$	��720��$	��880��$y�Hidden=�.+�>   8Microsoft.Samples.Kinect.WpfViewers.KinectSkeletonViewer  -   $��kinectSkeletonViewer1��$ 21��$	 400��$	��300��$	��400��$y�Hidden=�5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect ��  �� ElementName$ sensorChooser�� �� Path$ Kinect�A� -   $��leftfoot��$ 64��$	 285��$��60��
���   �$��30��
���   �$y�Hidden=�A� -   $��	rightfoot��$	 128��$	 285��$��60��
���   �$��30��
���   �$y�Hidden=��� -   $��uparrow��$	 183��$	 172��$��75��$
��Fill=�$��50��$y�Hidden=�$?��9/KinectBallroomDance;component/Images/straightarrowup.jpg���� -	   $��curveupleftarrow��$	 183��$	 172��$	��100��$
��Fill=�$	��100��$y�Hidden=�$?��9/KinectBallroomDance;component/Images/curvedarrowleft.png���� -
   $��	leftarrow��$	 183��$	 172��$��50��$
��Fill=�$��50��$y�Hidden=�$A��;/KinectBallroomDance;component/Images/straightarrowleft.jpg���� -   $��
rightarrow��$	 183��$	 172��$��50��$
��Fill=�$��50��$y�Hidden=�$C��=/KinectBallroomDance;component/Images/straightarrowright1.jpg���� -   $��cwturnarrow��$	 183��$	 172��$��30��$
��Fill=�$��30��$y�Hidden=�$;��5/KinectBallroomDance;component/Images/cwturnarrow.png���� -   $��ccwturnarrow��$	 183��$	 172��$��30��$
��Fill=�$��30��$y�Hidden=�$<��6/KinectBallroomDance;component/Images/ccwturnarrow.png���� -   $��crossrightarrow��$	 183��$	 172��$��30��$
��Fill=�$	��100��$y�Hidden=�$?��9/KinectBallroomDance;component/Images/crossrightarrow.png���� -   $��crossleftarrow��$	 183��$	 172��$��30��$
��Fill=�$	��100��$y�Hidden=�$>��8/KinectBallroomDance;component/Images/crossleftarrow.png���� -   $��posturepause��$	��880��$y�Hidden=�$<��6/KinectBallroomDance;component/Images/posturepause.jpg�� K� ZIndex$ 99���� -   $��
dancepause��$	��880��$y�Hidden=�$:��4/KinectBallroomDance;component/Images/dancepause.jpg��$ 99���� -   $��border1��
�������$��5q�$ 20��$ 20��$	��350��$	��400��$y�Hidden=��� -   $��border2��
�������$��5q�$	 470��$ 20��$	��100��$	��400��._��� -   $��label1��$��Posture��$��90��$	��390��$��20(� �� TextAlignment$ Center=�$
��Wrap=��� -   $��border3��
�������$��5q�$	 470��$	 145��$	��100��$	��400��._��� -   $��label2��$��Dance Figure��$��90��$	��390��$��20(�$ Center=�$
��Wrap=��� -   $��border4��
�������$��5q�$	 470��$	 270��$	��100��$	��400��._��� -   $��label3��$��Other��$��90��$	��390��$��20(�$ Center=�$
��Wrap=��� -   $��border6��
�������$��1q�$	 470��$	 400��$	��300��$	��400��._��� -   $��label99��$��INSTRUCTIONAL VIDEO��$��90��$	��390��$��20(�$ Center=�$
��Wrap=��� -   $��border5��
�������$��1q�$
 -230��$ 25��$	��675��$	��900�� J� Clip �� 	 �� Rect$	 250, 0, 400, 675�._�;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -   $��CameraVideo��$	��900��$	��675�� �� $ sensorChooser��$ Kinect��� -   $
��menu��$	��720��$	��880��$y�Visible=�.+��� -   $��	menuImage��$��menu_title.jpg��$	��880���� -   $��
rectangle1��$��99��$	��167��
 J� Opacity$	
 0.5Q�
����  �$	 175��$	 262��$��1��
����  ��� -    $��	canSeePic��$��no_sign.jpg��$	��140��$ 6��$	 574��