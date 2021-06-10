### Simple app to track the time your PC was turned on
The app needs to be running in the background in order to monitor the uptime of your computer.
Best way to achieve this is to create a shortcut of the .exe and copy it into the autostart directory (shell:startup).

The URL defaults to http://localhost:12345 but the port can be set in the application.json.

The collected information is shown in a calendar:

![](https://github.com/JacobDeuchert/uptime-tracker/blob/master/Screenshots/Calendar.PNG)



The app is build with ASP.Net Core and Razor Pages.