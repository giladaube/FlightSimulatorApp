# fgapp

This project is of a flight simulator diagnosis, the app uses Flight-Gear as a projectorr. The project is written in a MVVM design.
View- the display is separated into smaller components of views; each individual view holds a View-Model and has its own logic. Command and data binding, if exist, are to the View Model. To have a more professional fill-like we added a setup window to initialize the app. Some of the user stories are opened with side menu (hamburger). 
View Model- the View-Model receives the model provided in the main. To that model the View-Model sends commands and receive notification from. 
Model- for the Model we have decided to have a façade that holds Models so the View-Models will know only it. Even though the View-Models are separated we have decided to hold one model for there are some methods that intertwines. More so, the Models need shared data and so each model will know only itself the façade is holding the shared data.
	Model Player- the model player opens the Flight Gear app, connecting a TCP client to the app and sends data. The user may change the data that it sends (changing pace, exact time etc.) and by chain of command from the View to the View-Model to the Model the model will change the data that is sends accordingly.
	Model Graphs- the model saves the correlated features in the csv, displays graphs on command from its VM to the user chosen features, and the model calculate the regression line to display.
	Model Anomalies- the model calculates the anomalies based on a “normal” flight, the anomalies in each corelated features may vary from algorithm to algorithm, therefor the user can upload a DLL to the app to calculate the anomalies based on the chosen library.
More about the DLL..


The project holds three main folders, in corresponding to the MVVM design pattern. Model folder, View-Model folder, and View folder.
 	Model Folder- holds the models and the model façades. And the classes the model uses.
	View-Model Folder- holds four View-Models that hold as members IModel that the Model facade is extending.
	View Folder- holds all the view components, each user story there is at least one view. Each view holds as a member a matched View-Model.

Required Installs: Flight Gear
NuGet Packages: MahApps.Metro, MahApp.Metro.IconsPacks, OxyPlot.Core
.NET framework 4.7.2
First installment: direction provided in setup window

You can find a Demo here: https://github.com/giladaube/FlightSimulatorApp/blob/main/DEMO.mp4
ALso, UML files:
https://github.com/giladaube/FlightSimulatorApp/blob/main/UML%20-%20only%20classes.jpg
https://github.com/giladaube/FlightSimulatorApp/blob/main/UML%20-details.png - with some more details.
