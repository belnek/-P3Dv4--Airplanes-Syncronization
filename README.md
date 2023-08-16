----------------------------------------------------------------------------------------------------------------------------------------------------------------------
������ ������ ��������� �� ������-��������� ����������� ������������� ���������� �������� ����� ����� � ����� ������������. ���� ���� ��������� ����������� ���������������� ��� ���������� ������� �� Aerosoft A321 Professional � P3dv4.
��������, ��� �� ������ ������ ���������� ����� �������.

��������� �������� ���, � ������� ����� ������ ������ ������ ������.
��������� ��������������:

������
�������
������
��������
��������� ����
���� �������� �� �������
���� �������� �� �����

��� ������������ ������:

1. ���������� ����������� � �������� ������� wcf_sync
2. �� ������� 5 �������� � �������. ��������������� App.config � �������� Client, MasterClient � Host ����� ������ ������������� ���� � ����� �� ����, �� ������� ����� ����������� ����.
3. ������������ �������.
4. ���������, ��� � ����� ���������� ���������� SimConnect, � �����, ��� �� ����� � ������ ����������� ���������� Net framework 4.5.2.
5. ��������� ���� �� �������.
6. ��������� MasterClient �� ����������, � �������� ������ �������� �������������. �� ����� ���� ���������.
7. ��������� ��������� �������.
8. ������� ��� ������������ � ������� Connect
9. ��������� ���������� � ����������� �������� ������.
10. ���� ��� � �������, ������ ������. 

����� ��������� ������, ������� Disconnect � �������� �������. ����� �������� ����.


���� � ��� ���� ������� ��� ��������� �� �������, ��������� �� ����� �� Github. � ���������� ��������.

���� � ��� ����� ��������� ������� ���������� ������, ��� ���������� ���������: 4279 3806 4798 8817. �������.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
This project implements synchronization of aircraft parameters between two or more computers on a client-server architecture. The goal was to eliminate the consequences of desynchronization during joint flights on Aerosoft A321 Professional in P3dv4.

Perhaps someone will find a better use for this project.



The programs contain a chat that only the master client can write to.


Programs synchronize:


LATITUDE

LONGITUDE

ALTITUDE

SPEED

MAGNETIC COURSE

PITCH ROTATION ANGLE

ROLL ANGLE OF ROTATION


How to use the project:


1. Clone the repository and open the wcf_sync solution

2. You will see 5 projects in the solution. Configure App.config in the Client, MasterClient and Host projects by replacing the existing IP and port with the IP on which the host will be launched.
3. Rebuild the solution.

4. Make sure that SimConnect is installed in your simulator, and also that Net framework 4.5.2 is installed on your and other computers.

5. Start the host on the server.

6. Run MasterClient on the computer from which you will start syncing. There may be several of them.
7. Launch the remaining clients.

8. Enter your username and click Connect
9. Check the connection with the simulator by pressing the button.

10. If everything is in order, you can fly.



After the flight ends, click Disconnect and close the clients.Then close the host.

If you have any questions or suggestions about the project, ask them directly on Github. I will try to answer.



If you suddenly have a desire to support the author, here are the bank details: 4279 3806 4798 8817. 
Thank you.


