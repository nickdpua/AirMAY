   M S B A M L   `   `   `  ����A  =AirMAY, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AirMAYAirMAY  5   	       AirMAY.MainAviaWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:AirMAY     5   	   -   5          � Title$  MainAviaWindow��5   	   $	��900��6    $
��1300��6-   .��5	      � 
�������.+�5
   
   � $��10q�6   
������� � ColumnDefinitions 5      �� 5      $
��0.1* �6#   62   �� 5      $
��0.3* �6#   62   �� 5      $
��0.1* �6#   62   �� 5      $
��0.3* �6#   62   �� 5      $
��0.1* �6#   62   �� 5      $
��0.3* �6#   62   �� 5      $
��0.1* �6#   62   �� 5      $
��0.3* �6#   62   �� 5      $
��0.1* �6#   62   �� 5      $
��0.1* �6#   62   �� 5      $
��0.1* �6#   62    � RowDefinitions 5      �� 5      $
��0.3* �6    60   �� 5      $
��0.6* �6    60   �� 5      $��10 �6    6.   �� 5      $
��1.6* �6    60   �� 5      $
��0.8* �6    60   .+�5      �� $��1��6   $��4��6$   $��3q�61   $r��lhttps://icdn.lenta.ru/images/2020/08/03/21/20200803212243565/square_320_58d3b28c7b2117ab28ddd356fc09f557.jpg��6<   6�   �� 5       $��3��6   $��4��6$   $��3q�61   $r��lhttps://icdn.lenta.ru/images/2020/06/17/15/20200617150340795/square_320_acb67aa2871b6321e41c683adbcb19ba.jpg��6<   6�   �� 5!      $��5��6   $��4��6$   $��3q�61   $r��lhttps://icdn.lenta.ru/images/2020/04/15/11/20200415115655713/square_320_2ea2ead11f638c54a6a562cc9584dba7.jpg��6<   6�   �� 5"      $��7��6   $��4��6$   $��3q�61   $~��xhttps://images11.popmeh.ru/upload/img_cache/fbf/fbfa9965f9839aa8e21d6b3984b89e5f_ce_1600x1600x736x0_cropped_800x800.jpeg��6<   6�   �� 5#      $��8��6   $��2��6(   $��4��69   $Z��Thttps://zdtravel.az/content/assets/img/Partners/4491f6e388db4030a82c645e8e69981e.png��6I   6�   �� 5$      
�������$��0��6,   $��2��6<   $��11��6I   6`   �� 5%      -   5        $��1��5%      $��60��6&   $��30(�62   
�������
��� E��$��3��6n   $	��5,8q�6�   $	��Top=�6�   .��6�   Авиабилеты6�   �� 5&      -   5        $��6��5&      $��60��6%   $��30(�61   
�������
��� E��$��4��6m   $	��5,8q�6�   $	��Top=�6�   .��6�   Мои билеты6�   �� 5'      $��1��6   $��21(�6%   $
��Wrap=�63   $��1��6H   $��3��6X   ��Производим пассажирские и грузовые воздушные перевозки в крупные города по всему миру по буджетным ценам. На нашем сайте можно найти билеты во многие города и страны всего мира.6  �� 5*      -   5        $��mainListView��5*      $��1��6+   $��3��6;   $��9��6H   $��5q�6\   ��5+      �� 5,       �� 
TargetType" ��6   .�5-      ��  �� Property � �� �� Value$ Center=�6G   5.       �� View 50      �� 51      .P�52      �� $�� ��6)    �� Width$ 50��63    �� DisplayMemberBinding 6>   �� *Id+�� 53      $<��6Город из которого мы вылетаем��6)   $	 250��6P    6\   �� *FirstSity.SityName+�� 54      $6��0Город в который мы влетаем��6)   $	 230��6N    6[   �� *SecondSity.SityName+�� 55      $'��!Время отправления��6)   $	 140��6D    6P   �� *TimeOfDispatch+	 �� StringFormat$	 {0:dd.MM.yyyy HH:mm}���� 56      $4��.Примерное время прибытия��6)   $	 180��6K    6W   �� *EstimatedArrivalTime+$	 {0:dd.MM.yyyy HH:mm}���� 57      $��Стоимость��6)   $	 100��6<    6H   �� *Price+$	 0 USD��58      5:      �� 5;      -   5        $��	addButton��5;      $��8��6'   $��3��67   $��2��6D   $	��Top=�6X   $��30��6p   $	��100��6   $��10q�6�   ���	#FF12063A
�������$��16(�6�   ��6�   Z� .��6   Купить билет6  �� 5=      -   5        $��8��5=   .   $��3��6>   $��4��6R   $	��130��6_   $	��150��6o   
������ 
������ .��5>      �� $b��\https://www.damen.com/-/media/New-Corporate-Damen/Images/Services/Services_logo.png?la=ru-RU��6   6�   5?      � 5A      -   5        
 �Name$
 ChatGrid��5A      $y�Hidden=�6%   $��Right=�69   $��Bottom=�6U   $	��400��6p   $	��500��6   $��11��6�   $��6��6�    5B      �� 5C      6&   �� 5D      $
��0.1* �6$   64   .+�5G      �� -   5         ��Name$ chatListView��5G   @   $	��Top=�6   $��1��60   
����������	#FF12063A$��5q�6�   ���	#FF0E0E106�   �� 5I      $��1��6    �� Orientation$ 
Horizontal=�6*   .+�5J      �� -	   5        $��Bottom=�5J   ;   $��50��6V   $	��120��6e   ���	#FF12063A
�������$��16(�6�   $��Скрыть чат��6�   ��6t   Z� 6�   �� 5K      -
   5         ��Name$ massageTextBox��5K   9   $��Bottom=�6   $	��165��6Q   $��50��6]    �� TextWrapping$
 Wrap=�6j    �� 	MaxLength$
 1000��6~   ���	#FF12063A$��5q�6�   ���	#FF3A3A3D
�������6�   �� 5L      -   5        $��Right=�5L   6   $��Bottom=�6R   $��50��6m   $	��120��6|   ���	#FF12063A
�������$��16(�6�   $��Отправить��6�   ��6�   Z� 6�   5M      5N      5O      5P      5Q      