   M S B A M L   `   `   `  ���� E  AA05_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:A05_Client
A05_Client      A05_Client.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:A05_Client       � Title$  
MainWindow��$	��600��$	��920�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�
������� � AllowsTransparency . .���� ���#272537 �� CornerRadius$ 20��._��  � ColumnDefinitions �� $��215.2 ���  � RowDefinitions �� $��75 ��� .+��� $��0�����#272537$��0,0,3.2,0.2q��� $��1�����#272537$��5.8,0,0,0.2q��� $��Number Guessing Game��$��Center=�$
��Left=�
�������$��22(�$��95,23,0,23.2q�$��29��$	��250��$��2���� $��
0,75,6.2,0q�$��2��.+��� $��0,0,0,0q�$��2��.+��� -    ��Name$ NameBox��$��45��$y�Visible=�$��Center=�$��Center=�$��35,25,28.8,0q�$	��145���� $��0,0,0,0q�$��2��.+��� -    ��Name$ 	NameLabel��
�������$��Center=�$y�Visible=�.��Name�� -   $ IPBox��$��45��$y�Visible=�$��Center=�$��Center=�$��35,25,23.8,0q�$	��150���� $��0,0,0,0q�$��2��.+��� -   $ IPLabel��
�������$��Center=�$y�Visible=�.��
IP Address�� -   $ PortBox��$��45��$y�Visible=�$��Center=�$��Center=�$��35,25,23.8,0q�$	��150���� -   $ 	PortLabel��
�������$��Center=�$y�Visible=�.��Port #�� -   	 ��Name$	 ConnectButton��$��54,30,54.8,0q�$	��100��$��Connect��
���   �$	��1.5q�$y�Visible=�$��34���� $��1��$��10.8,75,0,0q�$��2��
 J� RenderTransformOrigin$
 	0.497,0.2&�.+�� $��
0,0,-0.2,0q� �� $	��100 ��� $
��Auto ��� -    ��Name$ 	outputBox��$y�Visible=�" �� VerticalScrollBarVisibility$ Visible=�$ �� HorizontalScrollBarVisibility$ Visible=� �� 
IsReadOnly . $��139,0,129.6,50q�$	��Top=�$��Center=�$	��100��$	��425��. �/� �� -	   $ numLabel��
�������$��Center=�$y�Visible=�.��Enter a number�� $��1q�
����  �$ 10�� �� Padding$ 2q�$��Center=�$��Center=�._��� -
   $ guessBox��$��36��$y�Visible=�$��0,0,0,0q�$��0q�$��Center=�$��Center=�
����+��$��12(�$��65��.��.����  �� Path$
 Text� �� UpdateSourceTrigger$ 	LostFocus=� �� RelativeSource �� *Self+ �� ValidationRules �� -   $	 GuessButton��$��Guess��$��Center=�$��Center=�$y�Visible=�$��0,30,0,0q�$	��118��$��37���� -   $	 DisconnectButton��$��
Disconnect��$y�Visible=�$��Bottom=�$��Center=�$��297,100,296.6,0q�$	��100��$��28���� $��0,0,5.2,0.2q�