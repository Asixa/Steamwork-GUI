   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5       SteamworkGUI   SteamworkGUI.Login   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      -   5         	  Login  � Title#  �   5      $��341.375��5      $��401.96��5      (   "MahApps.Metro.Controls.MetroWindow    NonActiveWindowTitleBrush �	#FF2D2D30 � WindowStartupLocation$ CenterScreen=�5	       � 
ResizeMode$ NoResize=�5
        VSWindowStyleKey#���  5       � Topmost .  � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $J Dpack://application:,,,/MahApps.Metro;component/Styles/VS/Colors.xaml?�6%   �� 5      $J Dpack://application:,,,/MahApps.Metro;component/Styles/VS/Styles.xaml?�6%   %    5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5   
   	   
BlurRadius$	 15Q�6   
   	Direction$	
 -90Q�6+      RenderingBias$ Quality=�6;      ShadowDepth$ 2Q�6S   .��5      � .+�5   
   � -   5        $��Password��5      $y�Visible=�6   .+�5   
   �� -   5        $��account��5      $
��Left=�6!   $��49��6<   $��	43,50,0,0q�6H    �� TextWrapping$
 Wrap=�6[   $	��Top=�6p   $	��309��6�   $��Center=�6�   $��20(�6�   �� 5      -   5        #���   5      $
��Left=�67   $��52��6R   $��
43,210,0,0q�6^   $	��Top=�6r   $	��309��6�   6�   G� 5    
   -   5        $��password��5       $
��Left=�6&   $��49��6A   $��
43,137,0,0q�6M   $	��Top=�6a   $	��309��6y   ���	#FF3F3F46$��20(�6�   $��Center=�6�   
������� G� SelectionBrush 6�   Z� ��6�   Z� 6  �� 5#        Account#���  6   $
��Left=�68   $��25��6S   $��	43,25,0,0q�6_   $	��Top=�6r   $��80��6�   
�������6�   �� 5$        Password#���  6   $
��Left=�69   $��25��6T   $��
43,112,0,0q�6`   $	��Top=�6t   $��80��6�   
�������6�   5&      � 5(   
   -   5        $��
VerifyCode��5(      $y�Hidden=�6!   .+�5)      �� -   5        $��vcode��5)      $
��Left=�6#   $��84��6>   $��	43,50,0,0q�6J   $
 Wrap=�6]   $	��Top=�6r   $	��309��6�   $��Center=�6�   $��30(�6�   $��Center=�6�   6�   �� 5*      -   5          Confirm#���  5*      $
��Left=�69   $��52��6T   $��
43,210,0,0q�6`   $	��Top=�6t   $	��309��6�   6�   �� 5+        Verify Code#���  6   $
��Left=�6<   $��25��6W   $��	43,25,0,0q�6c   $	��Top=�6v   $	��134��6�   
�������6�   5,      � 5-   
   -	   5        $��progressing��5-      ���	#FF2D2D30$y�Hidden=�69   .+�5.      )   #MahApps.Metro.Controls.ProgressRing  $��1��6$   $��80��5/   (   $��80��50   (   
�������64   51      52      53      