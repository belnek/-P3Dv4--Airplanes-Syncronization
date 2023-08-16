----------------------------------------------------------------------------------------------------------------------------------------------------------------------
Данный проект реализует на клиент-серверной архитектуре синхронизацию параметров самолета между двумя и более компьютерами. Цель была устранить последствия рассинхронизации при совместных полетах на Aerosoft A321 Professional в P3dv4.
Возможно, кто то найдет лучшее применение этому проекту.

Программы содержат чат, в который может писать только мастер клиент.
Программы синхронизируют:

ШИРОТУ
ДОЛГОТУ
ВЫСОТУ
СКОРОСТЬ
МАГНИТНЫЙ КУРС
УГОЛ ПОВОРОТА ПО ТАНГАЖУ
УГОЛ ПОВОРОТА ПО КРЕНУ

Как использовать проект:

1. Клонируйте репозиторий и откройте решение wcf_sync
2. Вы увидете 5 проектов в решении. Сконфигурируйте App.config в проектах Client, MasterClient и Host путем замены существующего айпи и порта на айпи, на котором будет запускаться хост.
3. Пересоберите решение.
4. Убедитесь, что в вашем симуляторе установлен SimConnect, а также, что на вашем и других компьютерах установлен Net framework 4.5.2.
5. Запустите хост на сервере.
6. Запустите MasterClient на компьютере, с которого будете начинать синхронизацию. Их может быть несколько.
7. Запустите остальные клиенты.
8. Введите имя пользователя и нажмите Connect
9. Проверьте соединение с симулятором нажатием кнопки.
10. Если все в порядке, можете лететь. 

После окончания полета, нажмите Disconnect и закройте клиенты. Затем закройте хост.


Если у вас есть вопросы или пожелания по проекту, задавайте их прямо на Github. Я постараюсь ответить.

Если у вас вдруг возникнет желание поддержать автора, вот банковские реквизиты: 4279 3806 4798 8817. Спасибо.
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


