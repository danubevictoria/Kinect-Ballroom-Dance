   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5   o    KinectBallroomDance$   KinectBallroomDance.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   o   -   5          � Title$  Kinect Ballroom Dance��5   	   $	��768��6'   $	��910��64   .��5      �� -   5        $��wrapper��5   &   $	��720��6   $	��880��6   .+�5   
   =   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  -   5        $��sensorChooser��5   !   $	��880��66   $	��720��6B    �� Left$
 -916��6O   
 �� Top$ 0��6b   �� 5   
   -   5        $	��app��5   *   $	��720��6   $	��880��6   $y�Hidden=�65   .+�5      >   8Microsoft.Samples.Kinect.WpfViewers.KinectSkeletonViewer  -   5        $��kinectSkeletonViewer1��5   H   $ 21��6&   $	 400��67   $	��300��6e   $	��400��6r   $y�Hidden=�6�   5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect 6~   ��  �� ElementName$ sensorChooser�� �� Path$ Kinect�6�   A� 5	      -   5        $��leftfoot��5	   D   $ 64��6   $	 285��6'   $��60��68   
���   �$��30��6c   
���   �$y�Hidden=�6{   A� 5
      -   5        $��	rightfoot��5
   E   $	 128��6   $	 285��6(   $��60��69   
���   �$��30��6e   
���   �$y�Hidden=�6}   �� 5      -   5        $��uparrow��5   C   $	 183��6   $	 172��6&   $��75��67   $
��Fill=�6R   $��50��6a   $y�Hidden=�6l   $?��9/KinectBallroomDance;component/Images/straightarrowup.jpg��6�   �� 5      -	   5        $��curveupleftarrow��5   D   $	 183��6   $	 172��6&   $	��100��67   $
��Fill=�6\   $	��100��6k   $y�Hidden=�6w   $?��9/KinectBallroomDance;component/Images/curvedarrowleft.png��6�   �� 5      -
   5        $��	leftarrow��5   C   $	 183��6   $	 172��6&   $��50��67   $
��Fill=�6T   $��50��6c   $y�Hidden=�6n   $A��;/KinectBallroomDance;component/Images/straightarrowleft.jpg��6�   �� 5      -   5        $��
rightarrow��5   C   $	 183��6   $	 172��6&   $��50��67   $
��Fill=�6U   $��50��6d   $y�Hidden=�6o   $C��=/KinectBallroomDance;component/Images/straightarrowright1.jpg��6�   �� 5      -   5        $��cwturnarrow��5   C   $	 183��6   $	 172��6&   $��30��67   $
��Fill=�6V   $��30��6e   $y�Hidden=�6p   $;��5/KinectBallroomDance;component/Images/cwturnarrow.png��6�   �� 5      -   5        $��ccwturnarrow��5   C   $	 183��6   $	 172��6&   $��30��67   $
��Fill=�6W   $��30��6f   $y�Hidden=�6q   $<��6/KinectBallroomDance;component/Images/ccwturnarrow.png��6�   �� 5      -   5        $��crossrightarrow��5   C   $	 183��6   $	 172��6&   $��30��67   $
��Fill=�6Z   $	��100��6i   $y�Hidden=�6u   $?��9/KinectBallroomDance;component/Images/crossrightarrow.png��6�   �� 5      -   5        $��crossleftarrow��5   C   $	 183��6   $	 172��6&   $��30��67   $
��Fill=�6Y   $	��100��6h   $y�Hidden=�6t   $>��8/KinectBallroomDance;component/Images/crossleftarrow.png��6�   �� 5      -   5        $��posturepause��5      $	��880��6(   $y�Hidden=�64   $<��6/KinectBallroomDance;component/Images/posturepause.jpg��6H    K� ZIndex$ 99��6�   �� 5      -   5        $��
dancepause��5      $	��880��6'   $y�Hidden=�63   $:��4/KinectBallroomDance;component/Images/dancepause.jpg��6G   $ 99��6�   �� 5      -   5        $��border1��5   l   
�������$��5q�6*   $ 20��6>   $ 20��6O   $	��350��6_   $	��400��6{   $y�Hidden=�6�   �� 5      -   5        $��border2��5   m   
�������$��5q�6*   $	 470��6>   $ 20��6P   $	��100��6`   $	��400��6|   ._�5      �� -   5        $��label1��5   7   $��Posture��6   $��90��6+   $	��390��6E   $��20(�6Q    �� TextAlignment$ Center=�6_   $
��Wrap=�6v   5      �� 5      -   5        $��border3��5   n   
�������$��5q�6*   $	 470��6>   $	 145��6P   $	��100��6a   $	��400��6}   ._�5      �� -   5        $��label2��5   <   $��Dance Figure��6   $��90��60   $	��390��6J   $��20(�6V   $ Center=�6d   $
��Wrap=�6{   5      �� 5      -   5        $��border4��5   n   
�������$��5q�6*   $	 470��6>   $	 270��6P   $	��100��6a   $	��400��6}   ._�5      �� -   5        $��label3��5   5   $��Other��6   $��90��6)   $	��390��6C   $��20(�6O   $ Center=�6]   $
��Wrap=�6t   5      �� 5      -   5        $��border6��5   n   
�������$��1q�6*   $	 470��6>   $	 400��6P   $	��300��6a   $	��400��6}   ._�5       �� -   5        $��label99��5    C   $��INSTRUCTIONAL VIDEO��6   $��90��67   $	��390��6R   $��20(�6^   $ Center=�6l   $
��Wrap=�6�   5"      �� 5#      -   5        $��border5��5#   n   
�������$��1q�6*   $
 -230��6>   $ 25��6Q   $	��675��6a   $	��900��6}    J� Clip 5$      �� 5%      	 �� Rect$	 250, 0, 400, 675�6(   ._�5'      ;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -   5        $��CameraVideo��5'   '   $	��900��6:   $	��675��6F    6S   �� $ sensorChooser��$ Kinect�6�   5(      5*      �� 5+   
   -   5        $
��menu��5+   *   $	��720��6   $	��880��6   $y�Visible=�66   .+�5,      �� -   5        $��	menuImage��5,   ,   $��menu_title.jpg��6   $	��880��6=   �� 5-      -   5        $��
rectangle1��5-   ?   $��99��6   $	��167��6%   
 J� Opacity$	
 0.5Q�61   
����  �$	 175��6_   $	 262��6q   $��1��6�   
����  �6�   �� 5.      -    5        $��	canSeePic��5.   )   $��no_sign.jpg��6   $	��140��6:   $ 6��6F   $	 574��6V   50      51      52      