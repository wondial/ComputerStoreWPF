   M S B A M L   `   `   `  ����C  ?DB_Store, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:DB_StoreDB_Store  5   	       DB_Store.LoginWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:DB_Store     5   	   -   5          � Title$  LoginWindow��5   	   $	��200��5	   	   $	��324��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	   .��5      � .+�5   
   � -   5         �Name$
 grid��5      $
��Left=�6   $	��200��68   $	��Top=�6E   $	��324��6]   ���#354052.+�5      �� -   5         ��Name$ buttonClose��5      $
��Left=�6*   $��35��6E   $��	289,0,0,0q�6Q   $	��Top=�6d   $��35��6|      TitleBarButtonClose#��[  6�   .��5      �� $
��Left=�6   $��18��63   $��18��6?   $��Icons\Close.png��6J   5      �� 5      -   5         ��Name$ textBoxLogin��5      $
��Left=�6,   $��40��6G   $��	20,55,0,0q�6S   $%��Имя пользователя��6f   $	��Top=�6~   $	��285��6�    �� 	MaxLength$ 15��6�     LoginTextBox#��[ 6�   6X  �� 5      $
��Left=�6   $��35��6/   $��	25,58,0,0q�6;   $	��Top=�6N   $��35��6f   $��Icons/User.png��6q   G� 5      -   5         G�Name$ passwordBox��5      	 G� Password$	 Пароль��6/   $
��Left=�6A   $��	20,95,0,0q�6\   $	��Top=�6o   $	��285��6�   $��40��6�   
 G� 	MaxLength$
 10��6�     PasswordBox#��[ 6�   6�   �� 5      $
��Left=�6   $��35��6/   $��	25,98,0,0q�6;   $	��Top=�6N   $��35��6f   $��Icons/Password.png��6q   �� 5      -   5        $ buttonLogin��5      $��
ВОЙТИ��6*   $
��Left=�6:   $��
20,145,0,0q�6U   $	��Top=�6i   $	��285��6�   $��35��6�     LoginButton#��[ 6�   6�   �� 5      $��Авторизация��6   $
��Left=�6*   $��	20,16,0,0q�6E   $	��Top=�6X   ���#ffffff$��18(�6�   5      5      5      