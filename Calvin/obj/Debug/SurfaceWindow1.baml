   M S B A M L   `   `   `  ����
  Calvin"    Drag_and_Drop.SurfaceWindow1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  5        � Title$  Calvin Calendar��5       � 	Resources 5         ImageOnlyTemplate  ImageAndCaptionTemplate J� IsManipulationEnabled  CursorStyle �� 
TargetType �� Property �� Value �� 
TargetType��%5  &	        &	 U     &	 H    7  �� 5	   
   .R�5
      �� $	��250��6.   ��6   �� *Source+6.   5      �� 5   
   .R�5      �� .+�5      �� $	��100��62   $��Uniform=�6A    . ��6   �� *Source+6S   �� 5      $	��250��65   $��WrapWithOverflow=�6D   $��Center=�6d   ��6   �� *	Caption+6d   5      5      �� 5   
   " ��6$   .�5      ��  � ��8   60   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��5      #��z��5      5      5      5      5       5!      .��5#      �  � RowDefinitions 5$   
   ��5'   
   �� 5(       �� ImageSource$ calvinf.png��6   	 j� Stretch$	 UniformToFill=�5)      .+�5,   
   <  6Microsoft.Surface.Presentation.Controls.SurfaceListBox  -   5        
  Name$
 ListBox��5-      $	��700��5.      $��Right=�5/      $��Bottom=�50      #��[ 52       J� 	AllowDrop . ��51      ��  �� Path$ LibraryItems���59      �� 5:      .R�5;      ��  �� Orientation$ Vertical=�6!   5<      5>      9  3Microsoft.Surface.Presentation.Controls.ScatterView  5@   
   -   5          Name$ scatter��5E      $
��1150��5A      $
��1150��5B      $
��Left=�5C      $��Bottom=�5D      
������  . #��[ 5H      ��5I      �� *ScatterItems+5N      �� 5P   
   -   5        $��PythonOutput��5P      
�������$y�Hidden=�66   $��Right=�6J   $��Bottom=�6f   $��Python Output��6�   6�   5R      5S      