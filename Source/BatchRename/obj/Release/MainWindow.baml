   M S B A M L   `   `   `  ���� F  BBatchRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BatchRenameBatchRename      BatchRename.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:BatchRename     -     � Title$  	My rename��$	��450��$	��800��.���  � RowDefinitions �� $
��0.6* ��� $
��9.0* � � ColumnDefinitions �� $
��3.5* ��� $
��6.5* �.+��� $��5 0 0 5q�$��0��$��0��$��2��.+���  �� Orientation$ 
Horizontal=�.+��� -   $��Refresh_Button��
�������.���� $ 
Horizontal=�.+��� $��Images/Refresh.png��$��10��$��10��$��Center=�$��3 0 0 0q�$��Center=��� $��Refresh��$��Center=�$��Center=�$��3 0 5 0q��� -   $��Help_Button��
�������.���� $ 
Horizontal=�.+��� $��Images/Help_book.png��$��10��$��10��$��Center=�$��3 0 0 0q�$��Center=��� $
��Help��$��Center=�$��Center=�$��3 0 5 0q��� -   $��LoadFromFile_Button��
�������.���� $ 
Horizontal=�.+��� $��Images/Load.png��$��10��$��10��$��Center=�$��3 3 3 3q�$��Center=��� $��Import���� -   $��Save_ListAction_Button��
�������.���� $ 
Horizontal=�.+��� $��Images/Save.png��$��10��$��10��$��Center=�$��3 0 0 0q�$��Center=��� $�� Save��]� $��0��$��1��.+��� .@��� $��String operation��.��]� .+��� $	��Top=�$ Vertical=�.+��� -   $��Replace_Button��$��5 5 5 5q����#E6E6E6.���� $ 
Horizontal=�.+��� $��Replace��$��Center=��� -   $��NewCase_Button��$��5 5 5 5q����#E6E6E6.���� $ 
Horizontal=�.+��� $��New Case��$��Center=��� -   $��FullNameNormalize_Button��$��5 5 5 5q����#E6E6E6.���� $ 
Horizontal=�.+��� $��FullName Normalize��$��Center=��� -	   $��Move_Button��$��5 5 5 5q����#E6E6E6.���� $ 
Horizontal=�.+��� $
��Move��$��Center=��� -
   $��UniqueName_Button��$��5 5 5 5q����#E6E6E6.���� $ 
Horizontal=�.+��� $��Unique Name��$��Center=��� $��List action��.��]� .+�]� ��. $	��Top=�.+��� -   $��actionsListBox������ .R���  � ContextMenu �� .@�m� -   $
��Edit��m� -   $��Delete��.+��� 
���  ��$
��Bold"����� *Name+�� ���� *Description+]� $��1��$��1�����#FAFAFF.+��� .@��� $��Rename files��.��]� .+��� $ 
Horizontal=�$��2 2 2 2q����#FAFAFA$	��Top=�.+��� -   $��AddFile_Button��
�������.���� $ 
Horizontal=�.+��� $��Images/Create.png��$��10��$��10��$��Center=�$��3 0 0 0q�$��Center=��� $	��ADD��$��Center=�$��Center=�$��3 0 5 0q��� $ 
Horizontal=�.+��� -   $��PreviewFiles_Button��
�������.���� $ 
Horizontal=�.+��� $��Images/Eye.png��$��10��$��10��$��Center=�$��3 0 0 0q�$��Center=��� $��PREVIEW��$��Center=�$��Center=�$��3 0 5 0q��� $ 
Horizontal=�.+��� -   $��StartBatchFile_Button��
�������.���� $ 
Horizontal=�.+��� $��Images/play_icon.png��$��10��$��10��$��Center=�$��3 0 0 0q�$��Center=��� $��
STARTBATCH��$��Center=�$��Center=�$��3 0 5 0q��� -   $��FileListView��$��Bottom=� �� View �� .P��� $��	File Name�� �� Width$	 120�� �� DisplayMemberBinding �� *
nameFile+�� $��New filename��$	 140�� �� *newFileName+�� $
��Path��$	 120�� �� *
pathFile+�� $��Error��$	 120�� �� *	errorFile+�� $��Rename folder��.��]� .+��� $ 
Horizontal=�$��2 2 2 2q�$	��Top=�.+��� -   $��
Add_folder��
�������.���� $ 
Horizontal=�.+��� $��Images/Create.png��$��10��$��10��$��Center=�$��3 0 0 0q�$��Center=��� $	��ADD��$��Center=�$��Center=�$��3 0 5 0q��� $ 
Horizontal=�.+��� -   $��PreviewFolders_Button��
�������.���� $ 
Horizontal=�.+��� $��Images/Eye.png��$��10��$��10��$��Center=�$��3 0 0 0q�$��Center=��� $��PREVIEW��$��Center=�$��Center=�$��3 0 5 0q��� -   $��StartBatchFolder_Button��
�������.���� $ 
Horizontal=�.+��� $��Images/play_icon.png��$��10��$��10��$��Center=�$��3 0 0 0q�$��Center=��� $��
STARTBATCH��$��Center=�$��Center=�$��3 0 5 0q��� -   $��FolderListView�� �� .P��� $��Folder Name��$	 120�� �� *
nameFolder+�� $��New Folder Name��$	 140�� �� *newFolderName+�� $
��Path��$	 120�� �� *
pathFolder+�� $��Error��$	 120�� �� *errorFolder+